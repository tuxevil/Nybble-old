using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using ProjectBase.WebControls.Validators;

namespace ProjectBase.WebControls
{
    public class SubmitButton : Button
    {
        protected override void OnPreRender(EventArgs e)
        {
            Helpers.ShowValidatorsOnButton(this, this.ValidationGroup);
            base.OnPreRender(e);
        }
    }

    public class SubmitLinkButton : LinkButton
    {
        protected override void OnPreRender(EventArgs e)
        {
            Helpers.ShowValidatorsOnButton(this, this.ValidationGroup);
            base.OnPreRender(e);
        }
    }

    public class SubmitImageButton : ImageButton
    {
        protected override void OnPreRender(EventArgs e)
        {
            Helpers.ShowValidatorsOnButton(this, this.ValidationGroup);
            base.OnPreRender(e);
        }
    }
}
