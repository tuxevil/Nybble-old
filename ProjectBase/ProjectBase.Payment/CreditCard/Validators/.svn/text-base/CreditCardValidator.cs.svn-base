using System;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectBase.Payment.CreditCard.Validators
{
	[
		Designer(typeof (CreditCardValidatorDesigner))
	]
	public class CreditCardValidator : BaseValidator
	{
		private string _acceptedTypes;
		private CardTypeCollection _cardTypes;
		private string _cardTypesListBox;
		private TextBox _creditCardNumberTextBox;

		public CreditCardValidator()
		{
		}

		[
			Description(
				"Comma separated list of types set within the web.config file. Optional. If left empty, will accept all types specified in the web.config file."
				)
		]
		public string AcceptedCardTypes
		{
			get { return _acceptedTypes; }
			set { _acceptedTypes = value; }
		}

		[
			Description("CardTypesListBox control that shows the card types to be accepted. Optional."),
			TypeConverter(typeof (CardTypesListBoxConverter))
		]
		public string CardTypesListBox
		{
			get { return _cardTypesListBox; }
			set { _cardTypesListBox = value; }
		}

		protected override bool ControlPropertiesValid()
		{
			bool valid = false;

			if (ControlToValidate != null)
			{
				Control controlToValidate = FindControl(ControlToValidate);

				if (controlToValidate is TextBox)
				{
					_creditCardNumberTextBox = (TextBox) controlToValidate;
					valid = true;
				}
			}

			return valid;
		}

		/// <summary>
		/// Checks that the entered card type is supported. Supported types should be added
		/// to the web.config using the <code>CreditCardValidatorSectionHandler</code> section
		/// handler.
		/// </summary>
		/// <returns></returns>
		private bool ValidCardType(String cardNumber)
		{
			if (_cardTypes != null)
			{
				foreach (CardType ct in _cardTypes)
				{
					// Check that the user wants to include this one in their search
					if (_acceptedTypes != null)
						if (!Regex.IsMatch(_acceptedTypes, ".*" + ct.Name + ".*"))
							break;

					if (Regex.IsMatch(cardNumber, ct.RegExp))
						return true;
				}

				// No matches were found so it's not a valid card type.
				return false;
			}
			else
				// No card types where specified so return true -- performing just
				// a Luhn check.
				return true;
		}

		/// <summary>
		/// Checks that the number is valid according to the Luhn algorithm.
		/// </summary>
		/// <returns></returns>
		protected bool LuhnValid(String cardNumber)
		{
			return ValidateCardNumber(cardNumber);
		}

		/// <summary>
		/// Performs the Luhn check.
		/// </summary>
		/// <param name="cardNumber">Credit Card Number</param>
		/// <returns></returns>
		/// <author>Frode N. Rosand</author>
		private static bool ValidateCardNumber(string cardNumber)
		{
			int length = cardNumber.Length;

			if (length < 13)
				return false;
			int sum = 0;
			int offset = length%2;
			byte[] digits = new ASCIIEncoding().GetBytes(cardNumber);

			for (int i = 0; i < length; i++)
			{
				digits[i] -= 48;
				if (((i + offset)%2) == 0)
					digits[i] *= 2;

				sum += (digits[i] > 9) ? digits[i] - 9 : digits[i];
			}
			return ((sum%10) == 0);
		}

		/// <summary>
		/// If the validator has an associated <code>CardTypesListBox</code> control that
		/// shows the types supported.
		/// </summary>
		/// <returns>Reference to the control.</returns>
		private CardTypesListBox FindCardTypesListBox()
		{
			if (_cardTypesListBox != null)
			{
				Control ctrl = Page.FindControl(_cardTypesListBox);
				if (ctrl is CardTypesListBox)
				{
					return (CardTypesListBox) ctrl;
				}
				else
					return null;
			}
			else
				return null;
		}

		protected override bool EvaluateIsValid()
		{
			// Determine if they wanted a card types list box. If so, then limit the valid
			// type to what was chosen.
			CardTypesListBox tl = FindCardTypesListBox();

			// Remove any non-numerical characters from the string.
			string cardNumber = Regex.Replace(_creditCardNumberTextBox.Text, @"[^0-9]", "");

			if (tl != null)
			{
				string regexp = tl.SelectedItem.Value;

				// valid card type regular expression is in the list box, check it against that.
				// If it's a valid card type, check it using the Luhn algorithm
				if (Regex.IsMatch(cardNumber, regexp))
					return LuhnValid(cardNumber);
				else
					// Failed both checks!
					return false;
			}
			else
			{
				// Determine if the card number is of the valid types supported
				// (those in the XML config file), and check it using Luhn's
				// If the card types don't exist then just perform a Luhn check.
				if (_cardTypes != null)
				{
					if (ValidCardType(cardNumber))
						return LuhnValid(cardNumber);
					else
						// Failed both checks
						return false;
				}
				else
					return LuhnValid(cardNumber);
			}
		}

		/// <summary>
		/// Loads the supported card types from the applications configuration file. Allowing
		/// users to add new regular expressions. This populates the <code>_cardTypes</code> member.
		/// </summary>
		private void LoadCardTypes()
		{
			CardTypeCollection ct = (CardTypeCollection) ConfigurationManager.GetSection("Etier.CreditCard");

			if (ct != null)
				_cardTypes = new CardTypeCollection(ct);
		}

		protected override void OnPreRender(EventArgs e)
		{
			// If the validator should provide client-side support, check that the browser
			// supports JavaScript first.
			if (EnableClientScript)
			{
				if (HttpContext.Current.Request.Browser.EcmaScriptVersion.Major >= 1)
					RegisterClientScript();
			}

			base.OnPreRender(e);
		}

		/// <summary>
		/// Emits JavaScript validation code to the client.
		/// </summary>
		protected void RegisterClientScript()
		{
			Attributes["evaluationfunction"] = "ccnumber_verify";
			StringBuilder sb_Script = new StringBuilder();

			sb_Script.Append("<script language=\"javascript\">");
			sb_Script.Append("\r");
			sb_Script.Append("\r");
			sb_Script.Append("function ccnumber_verify() {");
			sb_Script.Append("\r");
			sb_Script.Append("var strNum = document.all[document.all[\"");
			sb_Script.Append(ID);
			sb_Script.Append("\"].controltovalidate].value;");
			sb_Script.Append("\r");

			sb_Script.Append(@"strNum = strNum.replace(/-|\s/g,");
			sb_Script.Append("\"\");");

			// If the user has added different card types to the XML config file, then
			// include a check as part of the validation. Otherwise, just do a Luhn check.
			if (_cardTypes != null)
				sb_Script.Append("if(isNumberValid(strNum) && isCardTypeCorrect(strNum)) ");
			else
				sb_Script.Append("if(isNumberValid(strNum))");

			sb_Script.Append("\r");

			sb_Script.Append("return true;");
			sb_Script.Append("\r");
			sb_Script.Append("else ");
			sb_Script.Append("return false;");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("\r");
			sb_Script.Append("\r");
			sb_Script.Append("function isNumberValid(strNum) {");
			sb_Script.Append("\r");
			sb_Script.Append("var nCheck = 0;");
			sb_Script.Append("\r");
			sb_Script.Append("var nDigit = 0;");
			sb_Script.Append("\r");
			sb_Script.Append("var bEven  = false;");
			sb_Script.Append("\r");
			sb_Script.Append("\r");
			sb_Script.Append("for (n = strNum.length - 1; n >= 0; n--) {");
			sb_Script.Append("\r");
			sb_Script.Append("var cDigit = strNum.charAt (n);");
			sb_Script.Append("\r");
			sb_Script.Append("if (isDigit (cDigit)) {");
			sb_Script.Append("\r");
			sb_Script.Append("var nDigit = parseInt(cDigit, 10);");
			sb_Script.Append("\r");
			sb_Script.Append("if (bEven) {");
			sb_Script.Append("\r");
			sb_Script.Append("if ((nDigit *= 2) > 9) nDigit -= 9;");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("\r");
			sb_Script.Append("nCheck += nDigit;");
			sb_Script.Append("\r");
			sb_Script.Append("bEven = ! bEven;");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("\r");
			sb_Script.Append("else if (cDigit != ' ' && cDigit != '.' && cDigit != '-') ");
			sb_Script.Append("return false;");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("return (nCheck % 10) == 0;");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("\r");
			sb_Script.Append("\r");
			sb_Script.Append("function isDigit (c) {");
			sb_Script.Append("\r");
			sb_Script.Append("var strAllowed = \"1234567890\";");
			sb_Script.Append("\r");
			sb_Script.Append("return (strAllowed.indexOf(c) != -1);");
			sb_Script.Append("\r");
			sb_Script.Append("}");
			sb_Script.Append("\r");
			sb_Script.Append("\r");

			if (_cardTypes != null)
			{
				// Card Type Function
				StringBuilder sCardTypeFunction = new StringBuilder();
				sCardTypeFunction.Append("function isCardTypeCorrect(strNum) {");
				sCardTypeFunction.Append("\r");

				// If the listbox is being shown, accept only the selected one.
				if (_cardTypesListBox != null)
				{
					sCardTypeFunction.Append("    return strNum.match(");
					sCardTypeFunction.Append("document.all[\"" + _cardTypesListBox + "\"].value");
					sCardTypeFunction.Append(")");
					sCardTypeFunction.Append("\r");
					sCardTypeFunction.Append("}");

					sb_Script.Append(sCardTypeFunction.ToString());
				}
				else
				{
					// Listbox isn't being shown so should just accept everything in the web.config

					// If the user has set the card types to accept then override the defaults
					if (_acceptedTypes != null)
						LimitAcceptedCardTypes();

					int i = 0;

					sCardTypeFunction.Append("    if ( ");
					foreach (CardType ct in _cardTypes)
					{
						sCardTypeFunction.AppendFormat("strNum.match(\"{0}\")", ct.RegExp);
						i++;

						if (i < _cardTypes.Count)
							sCardTypeFunction.Append(" || ");
					}
					sCardTypeFunction.Append(" ) \r");
					sCardTypeFunction.Append("        return true;");
					sCardTypeFunction.Append("\r");
					sCardTypeFunction.Append("    else");
					sCardTypeFunction.Append("\r");
					sCardTypeFunction.Append("        return false;");
					sCardTypeFunction.Append("\r");
					sCardTypeFunction.Append("}");

					// Add to the main client-side script
					sb_Script.Append(sCardTypeFunction.ToString());
				}
			}

			sb_Script.Append("</script>");
			Page.RegisterClientScriptBlock("CreditCardValidation", sb_Script.ToString());
		}

		private void LimitAcceptedCardTypes()
		{
			CardTypeCollection acceptedTypes = new CardTypeCollection();

			foreach (CardType ct in _cardTypes)
			{
				if (Regex.IsMatch(_acceptedTypes, ".*" + ct.Name + ".*"))
					acceptedTypes.Add(ct);
			}

			_cardTypes = acceptedTypes;
		}

		protected override void OnInit(EventArgs e)
		{
			if (Site == null)
			{
				// Load the supported types from the card types collection.
				LoadCardTypes();
			}

			base.OnInit(e);
		}
	}
}