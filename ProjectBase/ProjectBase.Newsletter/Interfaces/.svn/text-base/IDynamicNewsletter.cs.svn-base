using System;
using System.Net.Mail;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Interface to create a dynamic newsletter
	/// </summary>
	/// <remarks></remarks>
	public interface IDynamicNewsletter
	{
		/// <summary>
		/// Creates a dynamic mail message using custom code.
		/// </summary>
		/// <param name="templatePath">Path to template files.</param>
		/// <param name="lastRunDate">Last date and time the campaign was run if available</param>
		/// <returns>Mail message to be send.</returns>
		MailMessage Get( string templatePath, DateTime? lastRunDate);
	}
}