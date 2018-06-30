using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    [TestFixture]
    [Category("SmokeTest")]
    class MainPageTests : TestWith<MainPageObject> //TestBase
    {

        [Test]
        public void ShouldHaveProperTitle()
        {
            //var page = new MainPageObject(Driver);
            //page.Open();
            Assert.That(Page.HeaderTitle, Is.EqualTo("Welcome to the-internet1"));
        }
    }
}
