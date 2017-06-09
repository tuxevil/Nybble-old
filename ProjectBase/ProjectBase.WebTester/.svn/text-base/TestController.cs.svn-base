using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Data;

namespace ProjectBase.WebTester
{
    public class TestController : AbstractNHibernateDao<Test, int>
    {
        public TestController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath)
        {
        }

        public IList TestHqlFullText(string text)
        {
            return this.NHibernateSession.CreateQuery("from Test t where freetext(t.TestProperty,:keywords)")
                .SetString("keywords", text)
                .List();
        }

    }
}
