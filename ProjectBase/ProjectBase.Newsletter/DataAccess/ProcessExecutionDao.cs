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
	class ProcessExecutionDao : AbstractNHibernateDao<ProcessExecution , System.Int32>
    {
		public ProcessExecutionDao( string sessionFactoryConfigPath ) : base( sessionFactoryConfigPath ) { }

		public ProcessExecution ExecutedToday( string applicationName, Campaign c )
		{
			ICriteria crit = GetCriteria( );
			crit.Add(Expression.Eq("ApplicationName", applicationName));
			crit.Add(Expression.Ge("RunDate", DateTime.Today));
            crit.Add(Expression.Eq("Campaign", c));
		    crit.SetMaxResults(1);
			return crit.UniqueResult<ProcessExecution>( );
		}

		public ProcessExecution ExecutedLast( string applicationName , Campaign c )
		{
			ICriteria crit = GetCriteria( );
			crit.Add( Expression.Eq( "ApplicationName" , applicationName ) );
			crit.Add( Expression.Eq( "Campaign" , c ) );
			crit.AddOrder(new Order("RunDate", false));

			IList<ProcessExecution> lst = crit.List<ProcessExecution>();
			if (lst.Count > 0)
				return lst[0];
			else
				return null;
		}
    }
}
