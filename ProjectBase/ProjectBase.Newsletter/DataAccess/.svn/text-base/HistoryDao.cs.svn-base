using System;
using System.Collections.Generic;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Criterion;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Provides DataAccess methods for the <see cref="Campaign">Campaign</see> objects.
	/// </summary>
	class HistoryDao : AbstractNHibernateDao<History , System.Int32>
    {
		public HistoryDao( string sessionFactoryConfigPath ) : base( sessionFactoryConfigPath ) { }

		public History GetLast(Campaign c)
		{
			ICriteria crit = this.GetCriteria();
			crit.CreateCriteria("Campaign").Add(Expression.Eq("ID", c.ID));
			crit.AddOrder( new Order( "SentDate" , false ) );
			crit.SetMaxResults(1);
			return crit.UniqueResult() as History;
		}
    }
}
