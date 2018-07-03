using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace POM.Tests.Tools
{
    public class FileUtils
    {
        public static string GetFullPath(string fileName)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(path, fileName);
        }

        public static IEnumerable<string[]> GetTestData(string fileName)
        {
            fileName = GetFullPath(fileName);
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines.Select(l => l.Split(',')))
                yield return line;
        }
    }
}
