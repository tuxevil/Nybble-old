using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NHibernate;
using NHibernate.Criterion;
using ProjectBase.Data;

namespace NybbleMembership.TestWebSite
{
    public class NumerosController : AbstractNHibernateDao<Numeros, int>
    {
        public NumerosController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath)
        {
        }

        public IList<int> GetNumbers ()
        {
            if(!PermissionManager.Check(MethodBase.GetCurrentMethod()))
                return new List<int>();

            ICriteria crit = GetCriteria();
            crit.SetProjection(Projections.ProjectionList()
                                  .Add(Projections.Property("Num"))
               );
            return crit.List<int>();
        }
    }
}
