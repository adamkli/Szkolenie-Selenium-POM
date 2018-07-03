using NUnit.Framework;
using POM.Tests.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    [TestFixture]
    class LoginPageTests : TestWith<LoginPageObject>
    {
        [Test, TestCaseSource("GetTestData")]
        public void ValidUserShouldLogin(string userName, string password, string message)
        {
            Page.Login(userName, password);
            Assert.That(Page.FlashMessage.Contains(message));
        }

        private static IEnumerable<string[]> GetTestData()
        {
            return FileUtils.GetTestData( @"TestData\UsersPasswords.csv");
        }
    }
}
