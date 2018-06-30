using NUnit;
using NUnit.Framework.Api;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace TestRunner
{
    class Program
    {
        public static void Main(String[] args)
        {

            Assembly assembly = null;
            var path = "POM.Tests.dll";
            assembly = Assembly.Load(Path.GetFileNameWithoutExtension(path));

            IDictionary<String, Object> options = new Dictionary<String, Object>();
            //options.Add(FrameworkPackageSettings.DefaultTestNamePattern, testName);

            DefaultTestAssemblyBuilder builder = new NUnit.Framework.Api.DefaultTestAssemblyBuilder();
            
            ITest test = builder.Build(assembly, options);
            var runner = new NUnit.Framework.Api.NUnitTestAssemblyRunner(builder);
            runner.Load(assembly, options);
            runner.Run(null, TestFilter.Empty);
            var x = runner.Result.ToXml(true);
            var y = x.ToString();
        }


    }
}
