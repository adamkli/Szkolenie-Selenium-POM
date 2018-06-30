using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    class TestWith<TPage> : TestBase where TPage : PageBase
    {
        TPage page;
        public TPage Page {
            get
            {
                if (page == null)
                {
                    page = (TPage)Activator.CreateInstance(typeof(TPage), Driver);
                    page.Open();
                }
                return page;
            }
        }
    }
}
