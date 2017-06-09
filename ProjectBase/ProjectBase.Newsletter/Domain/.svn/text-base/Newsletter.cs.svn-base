using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Newsletters.
	/// </summary>
	public class Newsletter : DomainObject<int>
	{
		private String _Body;
		private IList<Campaign> _Campaigns = new List<Campaign>();
		private NewsletterType _NewsletterType;
		private String _Subject;

		public Newsletter()
		{
		}

		public Newsletter(Int32 id)
		{
			base.id = id;
		}

		public virtual NewsletterType NewsletterType
		{
			get { return _NewsletterType; }
			set { _NewsletterType = value; }
		}

		public virtual String Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}

		public virtual String Body
		{
			get { return _Body; }
			set { _Body = value; }
		}

		public virtual IList<Campaign> Campaigns
		{
			get { return _Campaigns; }
			set { _Campaigns = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}