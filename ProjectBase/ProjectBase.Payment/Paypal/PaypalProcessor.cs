using System;
using System.Collections;
using System.Configuration;
using System.Web;

namespace ProjectBase.Payment.Paypal
{
	/// <summary>
	/// Provides connection to paypal for doing Payments.
	/// 
	/// In the .config file the handler must be configured
	/// and the following configuration should exist:
	/// 
	/// <paypal>
	///		<section name="paypal" type="ProjectBase.Payment.Paypal.PaypalSectionHandler, ProjectBase.Payment"/>
	/// </paypal>
	/// 
	///	<paypal>
	/// 	<test enabled="false">
	///		 	<url></url>	
	/// 		<business></business>
	/// 	</test>
	/// 	
	///		<default>
	/// 		<url></url>
	/// 		<business></business>
	///		</default>
	/// </paypal>
	/// 
	/// </summary>
	public class PaypalProcessor
	{
		private string invoice;
		private IList items = new ArrayList();

		/// <summary>
		/// Collection of items to be proccesed in PayPal.
		/// </summary>
		/// <remarks>Each item should be of type <see cref="PayPalItem"/></remarks>
		public IList Items
		{
			get { return items; }
		}

		public string Invoice
		{
			get { return invoice; }
			set { invoice = value; }
		}

		/// <summary>
		/// Add a new item to the processor.
		/// </summary>
		/// <param name="item"></param>
		public void AddItem(PaypalItem item)
		{
			items.Add(item);
		}

		/// <summary>
		/// Clear all items in the processor.
		/// </summary>
		public void ClearItems()
		{
			items.Clear();
		}

		/// <summary>
		/// CheckOut al the items included in the <see cref="Items"/> property.
		/// </summary>
		public void CheckOutOnPaypal()
		{
			int itemNumber = 1;

			// Obtengo configuracion
			PaypalConfig cfg = (PaypalConfig) ConfigurationManager.GetSection("paypal");

			string strPaypal = cfg.UrlProcessor;
			strPaypal += "?cmd=_cart&upload=1";
			strPaypal += "&business=" + HttpContext.Current.Server.UrlEncode(cfg.Business);
			strPaypal += "&notify_url=" + HttpContext.Current.Server.UrlEncode(cfg.UrlNotify);
			strPaypal += "&cancel_url=" + HttpContext.Current.Server.UrlEncode(cfg.UrlCancelled);
			strPaypal += "&return=" + HttpContext.Current.Server.UrlEncode(cfg.UrlReturn);
			strPaypal += "&invoice=" + invoice;

			// To configure
			// currency_code
			// no_shipping (0: optional, 1: No, 2: Required)
			// cbt (text for button to return page)

			// Customer Fields
			// address1, address2, city, country (code), first_name, last_name, state, zip

			foreach (PaypalItem pi in items)
			{
				string cartProduct;
				int cartQuantity;
				decimal cartCost;

				cartProduct = HttpContext.Current.Server.UrlEncode(pi.Product);
				cartQuantity = pi.Quantity;
				cartCost = pi.UnitPrice;

				strPaypal += "&item_name_" + itemNumber + "=" + cartProduct;
				strPaypal += "&item_number_" + itemNumber + "=" + cartQuantity;
				strPaypal += "&amount_" + itemNumber + "=" + Decimal.Round(cartQuantity*cartCost, 2);

				itemNumber++;
			}

			strPaypal += "&add_" + "=" + 1;

			HttpContext.Current.Response.Redirect(strPaypal);
		}
	}
}