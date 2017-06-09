using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.WebControls;
using ProjectBase.WebControls.Validators;

namespace ProjectBase.WebTester
{
    public partial class testcontrols : System.Web.UI.Page
    {
        DataField fieldLabel;
        DataField fieldCheckBox;
        DataField fieldLink;
        DataField fieldDate;
        DataField fieldRange;
        DataField fieldInteger;
        DataField fieldCurrency;
        DataField fieldText;
        DataField fieldLongText;
        DataField fieldHtml;
        DataField fieldEmail;
        DataField fieldDropdown;
        DataField fieldDropdown2;
        DataField fieldRadiobuttonList;
        DataField fieldCheckBoxList;
        DataField fieldDropdown3;
        DataFieldCustomValidator valTest;
        protected override void OnInit(EventArgs e)
        {

            fieldDropdown = new DataField(DataFieldType.DropdownList);
            plhControls.Controls.Add(fieldDropdown);
            fieldDropdown.ID = "Drop1";
            fieldDropdown.ValueChanged += new EventHandler(fieldDropdown_ValueChanged);

            if (!IsPostBack)
            {
                fieldDropdown.Label = "Drop1";
                ListItemCollection lst = new ListItemCollection();
                lst.Add(new ListItem("texto", "valor"));
                lst.Add(new ListItem("texto", "valor"));
                lst.Add(new ListItem("texto", "valor"));
                fieldDropdown.DataSource = lst;
                fieldDropdown.AutoPostBack = true;
                fieldDropdown.DataTextField = "Text";
                fieldDropdown.DataValueField = "Value";
                fieldDropdown.DataBind();
                fieldDropdown.Value = "Leo3";
            }

            fieldDropdown2 = new DataField(DataFieldType.DropdownList);
            plhControls.Controls.Add(fieldDropdown2);
            fieldDropdown2.ID = "Drop2";

            if (!IsPostBack)
            {
                fieldDropdown2.Label = "Drop2";
                fieldDropdown2.IsRequired = true;
                ArrayList lst = new ArrayList();
                lst.Add("Vera1");
                lst.Add("Vera2");
                lst.Add("Vera3");
                fieldDropdown2.DataSource = lst;
                fieldDropdown2.DataBind();
            }

            fieldRadiobuttonList = new DataField(DataFieldType.RadioButtonList);
            plhControls.Controls.Add(fieldRadiobuttonList);
            fieldRadiobuttonList.ID = "Rad1";

            if (!IsPostBack)
            {
                fieldRadiobuttonList.Label = "Rad1";
                fieldRadiobuttonList.IsRequired = true;
                ArrayList lst = new ArrayList();
                lst.Add("Vera1");
                lst.Add("Vera2");
                lst.Add("Vera3");
                fieldRadiobuttonList.DataSource = lst;
                fieldRadiobuttonList.DataBind();
            }

            fieldCheckBoxList = new DataField(DataFieldType.CheckBoxList);
            plhControls.Controls.Add(fieldCheckBoxList);
            fieldCheckBoxList.ID = "Check1";

            if (!IsPostBack)
            {
                fieldCheckBoxList.Label = "Check1";
                fieldCheckBoxList.IsRequired = true;
                ArrayList lst = new ArrayList();
                lst.Add("Vera1");
                lst.Add("Vera2");
                lst.Add("Vera3");
                fieldCheckBoxList.DataSource = lst;
                fieldCheckBoxList.DataBind();
            }

            fieldLabel = new DataField(DataFieldType.Label);
            plhControls.Controls.Add(fieldLabel);
            fieldLabel.ID = "Label1";

            if (!IsPostBack)
            {
                fieldLabel.Label = "Label1";
                fieldLabel.Value = "This is the label value";
            }

            fieldLink = new DataField(DataFieldType.HyperLink);
            plhControls.Controls.Add(fieldLink);

            fieldLink.Label = "Link1";
            fieldLink.Text = "This is the link text";
            fieldLink.Value = "http://www.google.com";

            fieldCheckBox = new DataField(DataFieldType.CheckBox);
            plhControls.Controls.Add(fieldCheckBox);
            fieldCheckBox.Label = "CheckBox1";
            fieldCheckBox.Value = true;

            fieldDate = new DataField(DataFieldType.Date);
            plhControls.Controls.Add(fieldDate);
            fieldDate.Label = "Date1";
            fieldDate.Value = DateTime.Today;

            fieldRange = new DataField(DataFieldType.DateRange);
            plhControls.Controls.Add(fieldRange);
            fieldRange.Label = "DateRange1";
            fieldRange.ValueFrom = DateTime.Today;
            fieldRange.ValueTo = DateTime.Today.AddDays(123);

            fieldInteger = new DataField(DataFieldType.Number);
            plhControls.Controls.Add(fieldInteger);
            fieldInteger.Label = "Integer1";
            if (!IsPostBack)
                fieldInteger.Value = 20;

            CompareValidator cmp = new CompareValidator();
            cmp.Operator = ValidationCompareOperator.Equal;
            cmp.ValueToCompare = "20";
            cmp.ErrorMessage = "LEO";
            cmp.Text = "LEO";
            cmp.ValidationGroup = "form";
            fieldInteger.AddValidator(cmp);

            CustomValidator cval = new CustomValidator();
            cval.ID = "AAAAAAA";
            cval.ServerValidate += new ServerValidateEventHandler(cval_ServerValidate);
            cval.ErrorMessage = "LEO";
            cval.Text = "LEO";
            cval.ValidationGroup = "form";
            fieldInteger.AddValidator(cval);

            //CompareValidator cmp = new CompareValidator();
            //cmp.Operator = ValidationCompareOperator.Equal;
            //cmp.ValueToCompare = "20";
            //cmp.ErrorMessage = "LEO";
            //cmp.Text = "LEO";
            //fieldInteger.AddValidator(cmp);

            fieldCurrency = new DataField(DataFieldType.Number);
            plhControls.Controls.Add(fieldCurrency);

            fieldCurrency.NumberType = DataNumberType.Currency;
            fieldCurrency.CurrencySymbol = "$";
            fieldCurrency.Label = "Decimal1";
            fieldCurrency.IsRequired = true;
            fieldCurrency.ReadOnly = true;
            fieldCurrency.Value = 19.33;

            fieldEmail = new DataField(DataFieldType.Email);
            plhControls.Controls.Add(fieldEmail);
            fieldEmail.Label = "Text1";
            fieldEmail.Enabled = false;
            fieldEmail.Value = "leo@leo.com";

            fieldText = new DataField(DataFieldType.Text);
            plhControls.Controls.Add(fieldText);

            fieldText.ID = "Text1";
            if (!IsPostBack)
            {
                fieldText.Label = "Text1";
                fieldText.Value = "This is a common text field.";
            }

            fieldDropdown3 = new DataField(DataFieldType.DropdownList);
            plhControls.Controls.Add(fieldDropdown3);
            fieldDropdown3.ID = "Drop2M";
            fieldDropdown3.ValueChanged += new EventHandler(fieldDropdown_ValueChanged);

            if (!IsPostBack)
            {
                fieldDropdown3.Label = "Drop2M";
                fieldDropdown3.IsRequired = true;
                ListItemCollection lst = new ListItemCollection();
                lst.Add(new ListItem("texto", "valor"));
                lst.Add(new ListItem("texto", "valor"));
                lst.Add(new ListItem("texto", "valor"));
                fieldDropdown3.DataSource = lst;
                fieldDropdown3.AutoPostBack = true;
                fieldDropdown3.DataTextField = "Text";
                fieldDropdown3.DataValueField = "Value";
                fieldDropdown3.DataBind();
                //fieldDropdown.Value = "Leo3";
            }

            fieldLongText = new DataField(DataFieldType.LongText);
            plhControls.Controls.Add(fieldLongText);
            fieldLongText.Label = "Text1";
            fieldLongText.Value = "This is a common text area field.";

            fieldHtml = new DataField(DataFieldType.HtmlEditor);
            plhControls.Controls.Add(fieldHtml);
            fieldHtml.Label = "Text1";
            fieldHtml.Value = "This is a HTML <b>enabled</b> field common text area field. Requeries tinyMce installed.";

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void cval_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
        }

        void valTest_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            Response.Write(args.Value);
        }

        void fieldDropdown_ValueChanged(object sender, EventArgs e)
        {
            Response.Write(fieldDropdown.Value);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool enabled = !fieldLabel.Enabled;
                fieldLabel.Enabled = enabled;
                fieldRange.Enabled = enabled;
                fieldDate.Enabled = enabled;
                fieldInteger.Enabled = enabled;
                fieldCurrency.Enabled = enabled;
                fieldText.Enabled = enabled;
                fieldLongText.Enabled = enabled;
                fieldHtml.Enabled = enabled;
                fieldEmail.Enabled = enabled;
                fieldDropdown.Enabled = enabled;
                fieldDropdown2.Enabled = enabled;
                fieldCheckBox.Enabled = enabled;
                fieldRadiobuttonList.Enabled = enabled;
                fieldCheckBoxList.Enabled = enabled;
                fieldDropdown3.Enabled = enabled;
                Response.Write(fieldRange.ValueFrom);
                Response.Write(fieldRange.ValueTo);
                Response.Write(fieldLabel.Value);
                Response.Write(fieldDate.Value);
                Response.Write(fieldInteger.Value);
                Response.Write(fieldCurrency.Value);
                Response.Write(fieldText.Value);
                Response.Write(fieldLongText.Value);
                Response.Write(fieldHtml.Value);
                Response.Write(fieldEmail.Value);
                Response.Write(fieldDropdown.Value);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
        }
    }
}
