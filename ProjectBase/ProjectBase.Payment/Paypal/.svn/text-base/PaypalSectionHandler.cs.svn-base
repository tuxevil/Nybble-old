using System;
using System.Configuration;
using System.Xml;

namespace ProjectBase.Payment.Paypal
{
	/// <summary>
	/// Paypal Section Handler.
	/// The handler should be configured in the .config file eg.:
	/// 
	/// <configSections>
	///		<section name="paypal" type="ProjectBase.Payment.Paypal.PaypalSectionHandler, ProjectBase.Payment"/>
	/// </configSections>
	/// </summary>
	/// <remarks>Use always the section name paypal.</remarks>
	public class PaypalSectionHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext,
		                     XmlNode section)
		{
			PaypalConfig cfg = new PaypalConfig();

			// Get Test Data
			bool test = Convert.ToBoolean(section.SelectSingleNode("test").Attributes["enabled"].Value);

			XmlNode root;

			if (test)
				root = section.SelectSingleNode("test");
			else
				root = section.SelectSingleNode("default");

			cfg.Business = root.SelectSingleNode("business").InnerText;
			cfg.UrlProcessor = root.SelectSingleNode("urlProcessor").InnerText;
			cfg.UrlNotify = root.SelectSingleNode("urlNotify").InnerText;
			cfg.UrlReturn = root.SelectSingleNode("urlReturn").InnerText;
			cfg.UrlCancelled = root.SelectSingleNode("urlCancel").InnerText;
			cfg.CurrencyCode = root.SelectSingleNode("currencyCode").InnerText;
			cfg.Shipping = root.SelectSingleNode("shipping").InnerText;
			cfg.ReturnText = root.SelectSingleNode("returnText").InnerText;

			return cfg;
		}

		#endregion
	}
}