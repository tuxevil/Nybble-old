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
	class FrequencyDao : AbstractNHibernateDao<Frequency , System.Int32>
    {
		public FrequencyDao( string sessionFactoryConfigPath ) : base( sessionFactoryConfigPath ) { }

		public IList<Frequency> GetByApplication( string applicationName )
		{
			ICriteria crit = GetCriteria( );
			crit.Add( Expression.Eq( "ApplicationName" , applicationName ) )
				.Add( Expression.Le( "StartDate" , DateTime.Today ) )
				.Add( Expression.Ge( "EndDate" , DateTime.Today ) );
			return crit.List<Frequency>( );
		}
    }
}
