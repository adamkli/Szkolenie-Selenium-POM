using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    [TestFixture]
    class FileUploadPageTests : TestWith<FileUploadPageObject>
    {
        [Test]
        public void FileUploadShouldSucceed()
        {
            string path = Path.GetFullPath(@"TestData/UsersPasswords.csv");
            Page.UploadFile(path);
            Assert.IsTrue(Page.FileUploaded.Contains("UsersPasswords.csv"));
        }
    }
}
