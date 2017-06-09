using System;

namespace ProjectBase.Newsletter
{
	public interface INewsletterProcessor
	{
		string Name { get; }
		bool Execute( out string errors );
		bool ExecuteTest( out string errors );
	}
}
