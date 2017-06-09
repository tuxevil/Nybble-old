using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProjectBase.WebControls.Validators
{
    public class DataFieldCustomValidator : CustomValidator
    {
        private DataField _ctrlToValidate = null;
        protected DataField FieldToValidate
        {
            get
            {
                if (_ctrlToValidate == null)
                    _ctrlToValidate = FindControl(this.ControlToValidate) as DataField;

                return _ctrlToValidate;
            }
        }

        protected override bool EvaluateIsValid()
        {
            bool evaluated = this.OnServerValidate(FieldToValidate.Value.ToString());
            return evaluated;
        }

        protected override bool ControlPropertiesValid()
        {
            // Make sure ControlToValidate is set
            if (this.ControlToValidate.Length == 0)
                throw new Exception(string.Format("The ControlToValidate property of '{0}' cannot be blank.", this.ID));

            // Ensure that the control being validated is a DataField
            if (this.FieldToValidate == null)
                throw new Exception(string.Format("The DataFieldValidator can only validate controls of type DataField."));

            return true;    // if we reach here, everything checks out
        }
    }
}
