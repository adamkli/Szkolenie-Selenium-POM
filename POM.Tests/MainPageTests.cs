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
    [Parallelizable]
    class MainPageTests : TestWith<MainPageObject>
    {
        [Test]
        public void ShouldHaveProperTitle()
        {
            Assert.That(Page.HeaderTitle, Is.EqualTo("Welcome to the-internet"));
        }
    }
}
