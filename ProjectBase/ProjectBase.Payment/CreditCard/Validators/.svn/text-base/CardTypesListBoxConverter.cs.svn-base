//////////////////////////////////////////////////////////////////////////
// Created [7/19/2003]
// Allows designer to display a listbox of all CardTypesListBox controls on the form.

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace ProjectBase.Payment.CreditCard.Validators
{
	public class CardTypesListBoxConverter : StringConverter
	{
		private object[] GetControls(IContainer container)
		{
			ComponentCollection componentCollection = container.Components;
			ArrayList ctrlList = new ArrayList();

			foreach (IComponent comp in componentCollection)
			{
				Control ctrl = (Control) comp;

				if (ctrl is CardTypesListBox)
				{
					if (ctrl.ID != null && ctrl.ID.Length != 0)
					{
						ValidationPropertyAttribute vpa =
							(ValidationPropertyAttribute) TypeDescriptor.GetAttributes(ctrl)[typeof (ValidationPropertyAttribute)];
						if (vpa != null && vpa.Name != null)
							ctrlList.Add(String.Copy(ctrl.ID));
					}
				}
			}

			ctrlList.Sort();

			return ctrlList.ToArray();
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null || context.Container == null)
			{
				return null;
			}
			object[] locals = GetControls(context.Container);
			if (locals != null)
			{
				return new StandardValuesCollection(locals);
			}
			else
			{
				return null;
			}
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}