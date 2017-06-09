using System;
using System.ComponentModel;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace ProjectBase.Payment.CreditCard.Validators
{
	[
		Designer(typeof (CardTypesListBoxDesigner))
	]
	public class CardTypesListBox : ListBox
	{
		private string _acceptedCardTypes;
		private CardTypeCollection _cardTypes;

		public CardTypesListBox()
		{
		}

		public string AcceptedCardTypes
		{
			get { return _acceptedCardTypes; }
			set { _acceptedCardTypes = value; }
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				// Remove anything in there already before re-filling it.
				Items.Clear();

				// If the user has specified which card types to accept, limit the contents
				// of the drop-down to just those.
				if (_acceptedCardTypes != null)
					LimitAcceptedCardTypes();

				foreach (CardType ct in _cardTypes)
				{
					ListItem li = new ListItem(ct.Name, ct.RegExp);
					Items.Add(li);
				}
			}

			base.OnPreRender(e);
		}

		protected void LimitAcceptedCardTypes()
		{
			CardTypeCollection acceptedTypes = new CardTypeCollection();

			foreach (CardType ct in _cardTypes)
			{
				if (Regex.IsMatch(_acceptedCardTypes, ".*" + ct.Name + ".*"))
					acceptedTypes.Add(ct);
			}

			_cardTypes = acceptedTypes;
		}

		protected override void OnInit(EventArgs e)
		{
			if (Site != null)
			{
				if (Site.DesignMode)
					// In design mode so just add a quick dummy item.
					Items.Add(new ListItem("CardTypes Here"));
			}
			else
			{
				CardTypeCollection ct = (CardTypeCollection) ConfigurationManager.GetSection("Etier.CreditCard");

				if (ct != null)
					_cardTypes = new CardTypeCollection(ct);
				else
					throw new NullReferenceException(
						"CardTypeListBox requires card types in the web.config in the Etier.CreditCard section.");
			}

			base.OnInit(e);
		}
	}
}