using System;
using System.Collections.Generic;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Criterion;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Provides DataAccess methods for the <see cref="Campaign">ExecutionDao</see> objects.
	/// </summary>
	class ExecutionDao : AbstractNHibernateDao<Execution , System.Int32>
	{
		public ExecutionDao( string sessionFactoryConfigPath ) : base( sessionFactoryConfigPath ) { }

		public IList<Execution> GetPendings(string applicationName)
		{
			ICriteria crit = GetCriteria( );
			crit.Add(Expression.Le("RunDate", DateTime.Now));
			crit.CreateCriteria("Campaign").Add(Expression.Eq("ApplicationName", applicationName));
			return crit.List<Execution>();
		}
	}
}
