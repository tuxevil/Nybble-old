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
	class NewsletterDao : AbstractNHibernateDao<Newsletter , System.Int32>
	{
		public NewsletterDao( string sessionFactoryConfigPath ) : base( sessionFactoryConfigPath ) { }

		public IList<Newsletter> GetByCode( string applicationName )
		{
			ICriteria crit = GetCriteria( );
			crit.Add(Expression.Eq("ApplicationName", applicationName));
			return crit.List<Newsletter>( );
		}
	}
}
