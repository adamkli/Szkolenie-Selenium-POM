using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace POM.Tests.Tools
{
    internal class ProcKiller
    {
        public static void KillOrphantDriversWithChildren(string driverName)
        {
            var items = GetWin32Processes(driverName);

            var chromedrivers = items
                .Where(el => el.ProcessId != null)
                .Where(p => items.FirstOrDefault(el2 =>
                    el2.ProcessId.Equals(p.ParentProcessId)) == null ||
                            (p.CreationDate.HasValue && DateTime.Now - p.CreationDate.Value > TimeSpan.FromMinutes(10)))
                .ToList();

            foreach (var p in chromedrivers)
            {
                KillProcessAndChildren(Convert.ToInt32(p.ProcessId.Value));
            }

        }

        public static List<Win32Process> GetWin32Processes(string processName = null)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            ManagementObjectCollection collection = searcher.Get();

            var items = new List<Win32Process>();
            foreach (var obj in collection)
            {

                var item = new Win32Process
                {
                    Name = (string)obj["Name"],
                    ProcessId = (uint?)obj["ProcessId"],
                    ParentProcessId = (uint?)obj["ParentProcessId"],
                    CreationDate =
                        obj["CreationDate"] == null
                            ? (DateTime?)null
                            : ManagementDateTimeConverter.ToDateTime(obj["CreationDate"].ToString())
                };
                if (!string.IsNullOrEmpty(processName) &&
                    !item.Name.StartsWith(processName, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                items.Add(item);
            }
            return items;
        }

        public static List<uint> GetProcessAndChildrenIDs(int pid)
        {
            var ids = new List<uint> { (uint)pid };

            var processes = GetWin32Processes().Where(el => el.ProcessId != null && el.ParentProcessId != null && el.ParentProcessId > 0).ToList();
            var changed = false;
            do
            {
                changed = false;
                foreach (var proc in processes.Where(proc => ids.Contains(proc.ParentProcessId.GetValueOrDefault()) && !ids.Contains(proc.ProcessId.GetValueOrDefault())))
                {
                    ids.Add(proc.ProcessId.GetValueOrDefault());
                    changed = true;
                }
            } while (changed);
            return ids;
        }

        public static int KillProcesses(List<uint> processIds)
        {
            int killed = 0;
            foreach (var pid in processIds)
            {
                try
                {
                    Process proc = Process.GetProcessById((int)pid);
                    proc.Kill();
                    killed++;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Cannot kill process with pid= " + pid + " because it already exited");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot kill process with pid= " + pid + " because of exception: " + e.Message);
                }
            }
            return killed;
        }
        public static int KillProcessAndChildren(int pid)
        {
            int killed = 0;
            if (pid == 0)
                return killed;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (var mo in moc)
            {
                var procId = Convert.ToInt32(mo["ProcessID"]);
                if (procId == pid)
                    continue;
                killed += KillProcessAndChildren(procId);
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
                killed++;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Cannot kill process with pid= " + pid + " because it already exited");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot kill process with pid= " + pid + " because of exception: " + e.Message);
            }
            return killed;
        }

        public class Win32Process : IComparable
        {
            public string Name;
            public uint? ParentProcessId;
            public uint? ProcessId;
            public DateTime? CreationDate;

            public int CompareTo(object obj)
            {
                var win32Process = obj as Win32Process;
                if (win32Process == null || !ProcessId.HasValue || !win32Process.ProcessId.HasValue)
                    return -1;
                return ProcessId.Value.CompareTo(win32Process.ProcessId.Value);
            }
        }
    }

}
