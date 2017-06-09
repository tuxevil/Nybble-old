using System;
using System.Configuration;
using System.Xml;

namespace ProjectBase.Payment.CreditCard.Validators
{
	/// <summary>
	/// Creates a configuration section for the ASP.NET web.config and machine.config files.
	/// Allows users to add new card types to the validation control. 
	/// </summary>
	public class CreditCardValidatorSectionHandler : IConfigurationSectionHandler
	{
		public CreditCardValidatorSectionHandler()
		{
		}

		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlNodeList cardTypes;
			CardTypeCollection al = new CardTypeCollection();

			cardTypes = section.SelectNodes("cardTypes//cardType");

			foreach (XmlNode cardType in cardTypes)
			{
				String name = cardType.Attributes.GetNamedItem("name").Value;
				String regExp = cardType.Attributes.GetNamedItem("regExp").Value;

				//Add them to the card types collection
				CardType ct = new CardType(name, regExp);

				al.Add(ct);
			}

			return al;
		}

		#endregion
	}

	public class CardType
	{
		private String _name;
		private String _regExp;

		public CardType(String Name, String RegExp)
		{
			_name = Name;
			_regExp = RegExp;
		}

		public string Name
		{
			get { return _name; }
		}

		public string RegExp
		{
			get { return _regExp; }
		}
	}
}