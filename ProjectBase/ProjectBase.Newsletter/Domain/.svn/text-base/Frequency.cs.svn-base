using System;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Campaigns.
	/// </summary>
	public class Frequency : DomainObject<int>
	{
		private String _ApplicationName;
		private MailFrequency _Frequency;

		public Frequency()
		{
		}

		public Frequency(Int32 id)
		{
			base.id = id;
		}

		public virtual String ApplicationName
		{
			get { return _ApplicationName; }
			set { _ApplicationName = value; }
		}

		public virtual MailFrequency FrequencyCode
		{
			get { return _Frequency; }
			set { _Frequency = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}