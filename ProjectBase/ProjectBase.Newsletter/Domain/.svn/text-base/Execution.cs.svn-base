using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Execution.
	/// </summary>
	public class Execution : DomainObject<int>
	{
		private bool _TestExecution;
		private Campaign _Campaign;
		private DateTime _RunDate;
		private DateTime _DateCreated;
		
		public Execution()
		{
		}

		public Execution( Int32 id )
		{
			base.id = id;
		}

		public virtual DateTime RunDate
		{
			get { return _RunDate; }
			set { _RunDate = value; }
		}

		public virtual Campaign Campaign
		{
			get { return _Campaign; }
			set { _Campaign = value; }
		}

		public virtual bool TestExecution
		{
			get { return _TestExecution; }
			set { _TestExecution = value; }
		}

		public virtual DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}