using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Newsletter;
using ProjectBase.Utils;
using Iesi.Collections.Generic;

namespace ProjectBase.WebTester
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load( object sender , EventArgs e )
		{
            Response.Write(User.Identity.IsAuthenticated);

		    TestController tc = new TestController("~/WebNHibernate.config");
		    //IList lstA = tc.TestHqlFullText("2");
            //Response.Write(lstA.Count);

            //Random r = new Random();
            //for (int i = 0; i < 20; i++)
            //{
            //    Test t = new Test();
            //    t.TestProperty = r.Next().ToString();
            //    tc.Save(t);
            //}

		    List<Test> lstTest = tc.GetAll() as List<Test>;

            Test t = new Test();
            t.TestProperty = lstTest[0].TestProperty;

            Test t1 = new Test();
            t1.TestProperty = lstTest[0].TestProperty;


            HashedSet<Test> lstOld = new HashedSet<Test>();
            lstOld.Add(lstTest[0]);
            lstOld.Add(t);
            lstOld.Add(t1);

            Response.Write(lstOld.Count);
            //Response.Write(contains);
            //foreach (Test t in lstOld)
            //    t.TestProperty = "2";

            //IList<Test> lst = tc.GetAll();
            //Response.Write(lst[0].Version + lst[0].TimeStamp.ModifiedOn.ToString());

            //// Creates a conflict
            //lstOld[0].TestProperty = "3";
            //tc.Save(lstOld[0]);

            //Response.Write(lst[0].Version);

            //foreach(Test t in tc.GetAll())
            //{
            //    //Response.Write(t.TimeStamp);
            //    t.TestProperty = r.Next().ToString();
            //}
		}

		protected void Button1_Click( object sender , EventArgs e )
		{
		}

		protected void Button2_Click( object sender , EventArgs e )
		{
		}
	}
}
