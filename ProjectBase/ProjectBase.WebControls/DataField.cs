using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using ProjectBase.WebControls.Validators;

namespace ProjectBase.WebControls
{
    public enum DataFieldType {
        Label = 0,
        HyperLink = 1,
        Text = 2,
        Number = 3,
        Date = 5,
        Email = 6,
        DateRange = 7,
        LongText = 8,
        HtmlEditor = 9,
        CheckBox = 10,
        DropdownList = 11,
        RadioButtonList = 12,
        CheckBoxList = 13,
    }

    public enum DataNumberType
    {
        Integer = 1,
        Decimal = 2,
        Currency = 3,
        Percentage = 4
    }

    //TODO: Should add a multiple selector (a combo with and add button, combo should not have the items with a list of selected items on the side, ajax based, selected items on value)
    //TODO: Could add a searcher as for distributors? Too complex should define address and function, still not working great.
    //TODO: Prepare grid as control to reuse.. remove UserControl.

    public class DataField : CompositeDataBoundControl
    {
        private const string VAL_CSSCLASS = "valerror";
        private const string VAL_GROUP = "form";
        private const string NULL_DISPLAY = "N/D";

        #region ChildControls Creation

        private TextBox txtFirst = new TextBox();
        private TextBox txtSecond = new TextBox();
        private ListControl lstFirst;
        private CheckBox chkFirst = new CheckBox();
        private Label lblFirst = new Label();
        private HyperLink hypFirst = new HyperLink();

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int itemCount = 0;

            if (dataSource != null && IsListControl())
            {
                switch (Type)
                {
                    case DataFieldType.DropdownList:
                        lstFirst = new DropDownList();
                        break;
                    case DataFieldType.RadioButtonList:
                        lstFirst = new RadioButtonList();
                        break;
                    case DataFieldType.CheckBoxList:
                        lstFirst = new CheckBoxList();
                        break;
                    default:
                        return 0;
                }

                Controls.Add(lstFirst);
                lstFirst.ID = "listFirst";

                lstFirst.SelectedIndexChanged += new EventHandler(ddlFirst_SelectedIndexChanged);

                lstFirst.AutoPostBack = this.AutoPostBack;

                switch (Type)
                {
                    case DataFieldType.DropdownList:
                        AddRequiredValidation(lstFirst);
                        break;
                    case DataFieldType.CheckBoxList:
                        AddRequiredListValidation(lstFirst);
                        break;
                }

                IEnumerator e = dataSource.GetEnumerator();

                if (dataBinding && this.Type == DataFieldType.DropdownList)
                {
                    ListItem item = new ListItem();
                    item.Value = string.Empty;
                    item.Text = "--Seleccione--";
                    lstFirst.Items.Add(item);
                    itemCount++;
                }

                while (e.MoveNext())
                {
                    ListItem item = new ListItem();

                    if (dataBinding)
                    {
                        if (!string.IsNullOrEmpty(DataValueField))
                            item.Value = DataBinder.Eval(e.Current, DataValueField).ToString();
                        else
                            item.Value = e.Current.ToString();

                        if (!string.IsNullOrEmpty(DataTextField))
                            item.Text = DataBinder.Eval(e.Current, DataTextField).ToString();
                        else
                            item.Text = e.Current.ToString();
                    }

                    lstFirst.Items.Add(item);
                    itemCount++;
                }
            }

            return itemCount;
        }

        protected override void CreateChildControls()
        {
            if (IsListControl())
            {
                base.CreateChildControls();
                return;
            }

            switch (Type)
            {
                case DataFieldType.DateRange:
                    this.Controls.Add(txtFirst);

                    txtFirst.ID = "txtFrom";
                    txtFirst.MaxLength = 10;
                    txtFirst.TabIndex = 0;
                    txtFirst.ValidationGroup = ValidationGroup;

                    AddDataTypeValidation(txtFirst, ValidationDataType.Date);

                    this.Controls.Add(new LiteralControl("&nbsp;al&nbsp;"));

                    this.Controls.Add(txtSecond);

                    txtSecond.ID = "txtTo";
                    txtSecond.MaxLength = 10;
                    txtSecond.TabIndex = 0;
                    txtSecond.ValidationGroup = ValidationGroup;

                    AddDataTypeValidation(txtSecond, ValidationDataType.Date);

                    CompareValidator cv = new CompareValidator();
                    this.Controls.Add(cv);
                    cv.ControlToValidate = txtSecond.ID;
                    cv.ControlToCompare = txtFirst.ID;
                    cv.Operator = ValidationCompareOperator.GreaterThan;
                    cv.Type = ValidationDataType.Date;
                    cv.Display = ValidatorDisplay.Dynamic;
                    cv.Text = Resources.Controls.Validator_Range;
                    cv.ValidationGroup = ValidationGroup;
                    cv.CssClass = ValidationCssClass;

                    CalendarExtender ce = new AjaxControlToolkit.CalendarExtender();
                    this.Controls.Add(ce);
                    ce.TargetControlID = txtFirst.ID;

                    ce = new AjaxControlToolkit.CalendarExtender();
                    this.Controls.Add(ce);
                    ce.TargetControlID = txtSecond.ID;

                    break;

                case DataFieldType.Label:
                    this.Controls.Add(lblFirst);
                    lblFirst.ID = "lblFirst";
                    break;

                case DataFieldType.HyperLink:
                    this.Controls.Add(hypFirst);
                    hypFirst.ID = "hypFirst";
                    break;

                case DataFieldType.CheckBox:
                    this.Controls.Add(chkFirst);
                    chkFirst.ID = "chkFirst";
                    chkFirst.AutoPostBack = AutoPostBack;
                    chkFirst.CheckedChanged += new EventHandler(chkFirst_CheckedChanged);

                    AddCheckBoxValidation(chkFirst);

                    break;
                default:
                    this.Controls.Add(txtFirst);

                    txtFirst.ID = "txtFrom";
                    txtFirst.MaxLength = MaxLength;
                    txtFirst.TabIndex = TabIndex;
                    txtFirst.ValidationGroup = ValidationGroup;
                    txtFirst.AutoPostBack = this.AutoPostBack;
                    txtFirst.TextChanged += new EventHandler(txtFirst_TextChanged);
                    AddRequiredValidation(txtFirst);

                    switch (Type)
                    {
                        case DataFieldType.Number:
                            if (NumberType == DataNumberType.Integer)
                            {
                                txtFirst.CssClass = "integer";
                                AddDataTypeValidation(txtFirst, ValidationDataType.Integer);
                            }
                            else
                            {
                                txtFirst.CssClass = "currency";
                                AddDataTypeValidation(txtFirst, ValidationDataType.Currency);
                            }

                            break;
                        case DataFieldType.Date:
                            txtFirst.CssClass = "date";
                            AddDataTypeValidation(txtFirst, ValidationDataType.Date);
                            break;
                        case DataFieldType.Email:
                            AddEmailValidation(txtFirst);
                            break;
                        case DataFieldType.LongText:
                            txtFirst.TextMode = TextBoxMode.MultiLine;
                            txtFirst.Rows = 10;
                            txtFirst.Columns = 40;
                            break;
                        case DataFieldType.HtmlEditor:
                            txtFirst.CssClass = "mceEditor";
                            txtFirst.TextMode = TextBoxMode.MultiLine;
                            txtFirst.Rows = 20;
                            txtFirst.Columns = 60;
                            break;
                    }

                    break;
            }
        }

        public void AddValidator(BaseValidator val)
        {
            this.EnsureChildControls();
            this.Controls.Add(val);
            val.ControlToValidate = this.txtFirst.ID;
        }
        #endregion

        #region Control Events

        private void ddlFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnValueChanged(sender, e);
        }

        private void txtFirst_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged(sender, e);
        }

        private void chkFirst_CheckedChanged(object sender, EventArgs e)
        {
            OnValueChanged(sender, e);
        }

        #endregion

        #region Constructors 
        public DataField()
        {
            this.Type = DataFieldType.Label;
        }

        public DataField(DataFieldType type)
        {
            this.Type = type;
        }

        #endregion

        #region Properties

        private bool IsListControl()
        {
            return this.Type == DataFieldType.DropdownList || this.Type == DataFieldType.RadioButtonList || this.Type == DataFieldType.CheckBoxList;
        }

        private bool CanBeRequired()
        {
            return (Type != DataFieldType.HyperLink &&
                    Type != DataFieldType.Label &&
                    Type != DataFieldType.RadioButtonList);
        }

        private bool CanBeEnabled
        {
            get { return (this.Enabled && !this.ReadOnly); }
        }

        public int MaxLength
        {
            get
            {
                return (ViewState["MaxLength"] != null) ? Convert.ToInt32(ViewState["MaxLength"]) : 0;
            }
            set
            {
                ViewState["MaxLength"] = value;
            }
        }

        public string DataValueField
        {
            get
            {
                return (ViewState["DataValueField"] != null) ? ViewState["DataValueField"].ToString() : null;
            }
            set
            {
                ViewState["DataValueField"] = value;
            }
        }

        public string DataTextField
        {
            get
            {
                return (ViewState["DataTextField"] != null) ? ViewState["DataTextField"].ToString() : null;
            }
            set
            {
                ViewState["DataTextField"] = value;
            }
        }

        public string Label
        {
            get
            {
                return (ViewState["_formLabel"] != null) ? ViewState["_formLabel"].ToString() : null;
            }
            set
            {
                ViewState["_formLabel"] = value;
            }
        }

        public string CurrencySymbol
        {
            get
            {
                return (ViewState["CurrencySymbol"] != null) ? ViewState["CurrencySymbol"].ToString() : null;
            }
            set
            {
                ViewState["CurrencySymbol"] = value;
            }
        }

        public DataFieldType Type
        {
            get
            {
                if (ViewState["_dataType"] == null)
                    throw new Exception("Type must be set as the first property");

                return (ViewState["_dataType"] != null) ? (DataFieldType)ViewState["_dataType"] : DataFieldType.Label;
            }
            set {
                ViewState["_dataType"] = value; 
            }
        }

        public DataNumberType NumberType
        {
            get
            {
                return (ViewState["DataNumberType"] != null) ? (DataNumberType)ViewState["DataNumberType"] : DataNumberType.Currency;
            }
            set { ViewState["DataNumberType"] = value; }
        }

        public bool AutoPostBack
        {
            get
            {
                return (ViewState["AutoPostBack"] != null) ? Convert.ToBoolean(ViewState["AutoPostBack"]) : false;
            }
            set
            {
                ViewState["AutoPostBack"] = value;
            }
        }

        public bool IsRequired
        {
            get
            {
                return (ViewState["_required"] != null) ? Convert.ToBoolean(ViewState["_required"]) : false;
            }
            set
            {
                if (CanBeRequired())
                    ViewState["_required"] = value;
                else
                    ViewState["_required"] = false;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return (ViewState["ReadOnly"] != null) ? Convert.ToBoolean(ViewState["ReadOnly"]) : false;
            }
            set
            {
                ViewState["ReadOnly"] = value;
            }
        }

        public string ValidationGroup
        {
            get
            {
                return (ViewState["ValidationGroup"] != null) ? ViewState["ValidationGroup"].ToString() : VAL_GROUP;
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }

        public string ValidationCssClass
        {
            get
            {
                return (ViewState["ValidationCssClass"] != null) ? ViewState["ValidationCssClass"].ToString() : VAL_CSSCLASS;
            }
            set
            {
                ViewState["ValidationCssClass"] = value;
            }
        }

        public string NullDisplay
        {
            get
            {
                return (ViewState["NullDisplay"] != null) ? ViewState["NullDisplay"].ToString() : NULL_DISPLAY;
            }
            set
            {
                ViewState["NullDisplay"] = value;
            }
        }

        #endregion

        #region Render
        private void RenderText(System.Web.UI.HtmlTextWriter writer)
        {
            writer.RenderBeginTag("span");
            writer.Write(this.Text);
            writer.RenderEndTag();
        }

        private void RenderRequiredMark(System.Web.UI.HtmlTextWriter writer)
        {
            if (!IsRequired || !(CanBeEnabled))
                return;

            writer.RenderBeginTag("em");
            writer.Write("*");
            writer.RenderEndTag();
        }

        public void RenderLabel(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Label))
            {
                writer.RenderBeginTag("label");
                writer.Write(this.Label);
                RenderRequiredMark(writer);
                writer.RenderEndTag();
            }
            else
                RenderRequiredMark(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Type == DataFieldType.HtmlEditor)
            {
                if (!CanBeEnabled || ReadOnly)
                    txtFirst.CssClass = "mceEditorReadOnly";
                else
                    txtFirst.CssClass = "mceEditor";
            }

            if (this.Type == DataFieldType.RadioButtonList)
            {
                lstFirst.CssClass = "radio";
                //if (string.IsNullOrEmpty(lstFirst.SelectedValue))
                //    lstFirst.Items[0].Selected = true;
            }

            if (this.Type == DataFieldType.CheckBoxList)
                lstFirst.CssClass = "radio";

            SetPropertiesOnChild();

            base.OnPreRender(e);
        }

        private void RegisterTestValidatorCode(WebControl c)
        {
            if (string.IsNullOrEmpty(c.ClientID))
                return;

            string javascriptCode = "$('#{0}, #{0} input').blur(function() {{ testValidators('{1}'); }}).change(function() {{ testValidators('{1}'); }});";
            Helpers.RegisterScriptOnClientSide(c, string.Format(javascriptCode, c.ClientID, this.ValidationGroup));
        }

        private void SetPropertiesOnChild()
        {
            switch (Type)
            {
                case DataFieldType.CheckBox:
                    chkFirst.ValidationGroup = this.ValidationGroup;
                    chkFirst.AutoPostBack = this.AutoPostBack;
                    RegisterTestValidatorCode(chkFirst);
                    break;

                case DataFieldType.DateRange:
                    txtFirst.ValidationGroup = this.ValidationGroup;
                    txtSecond.ValidationGroup = this.ValidationGroup;

                    RegisterTestValidatorCode(txtFirst);
                    RegisterTestValidatorCode(txtSecond);

                    break;

                default:

                    if (IsListControl())
                    {
                        lstFirst.ValidationGroup = this.ValidationGroup;
                        lstFirst.DataValueField = this.DataValueField;
                        lstFirst.DataTextField = this.DataTextField;
                        lstFirst.AutoPostBack = this.AutoPostBack;

                        RegisterTestValidatorCode(lstFirst);
                    }
                    else
                    {
                        txtFirst.ValidationGroup = this.ValidationGroup;
                        txtFirst.MaxLength = this.MaxLength;
                        txtFirst.AutoPostBack = this.AutoPostBack;

                        RegisterTestValidatorCode(txtFirst);
                    }
                    break;
            }

            foreach (Control c in Controls)
                if (c is BaseValidator)
                    (c as BaseValidator).ValidationGroup = this.ValidationGroup;
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.WriteBeginTag("div");

            string divClass = "line";

            if (IsRequired && CanBeEnabled)
                divClass += " fm-req";

            writer.WriteAttribute("class", divClass);
            writer.Write(HtmlTextWriter.TagRightChar);

            RenderLabel(writer);
            if ((CanBeEnabled) || this.Type == DataFieldType.HtmlEditor) 
            {
                foreach (Control c in Controls)
                    if (!(c is ExtenderControlBase))
                        c.RenderControl(writer);
            }
            else
                RenderText(writer);

            if (CanBeEnabled)
            {
                foreach (Control c in Controls)
                    if ((c is ExtenderControlBase))
                        c.RenderControl(writer);
            }

            writer.WriteEndTag("div");
        }

        #endregion

        #region Validators

        private void AddEmailValidation(Control control)
        {
            if (CanBeEnabled)
                AddRegexValidation(control, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", Resources.Controls.Validator_Email, Resources.Controls.Validator_Format_Email);
        }

        private void AddRegexValidation(Control control, string regularExpression, string text, string errorMessage)
        {
            RegularExpressionValidator rev = new RegularExpressionValidator();
            rev.ControlToValidate = control.ID;
            rev.ValidationExpression = regularExpression;
            rev.ErrorMessage = string.Format(errorMessage, this.Label);
            rev.Text = text;
            rev.Display = ValidatorDisplay.Dynamic;
            rev.ValidationGroup = ValidationGroup;
            rev.CssClass = ValidationCssClass;
            this.Controls.Add(rev);
        }

        private void AddRequiredValidation(Control control)
        {
            if (!IsRequired)
                return;

            RequiredFieldValidator rfv = new RequiredFieldValidator();
            this.Controls.Add(rfv);

            rfv.ControlToValidate = control.ID;
            rfv.Display = ValidatorDisplay.Dynamic;
            rfv.ErrorMessage = string.Format(Resources.Controls.Validator_Format_RequiredField, this.Label);
            rfv.Text = Resources.Controls.Validator_RequiredField;
            rfv.ValidationGroup = ValidationGroup;
            rfv.CssClass = ValidationCssClass;
        }

        private void AddRequiredListValidation(Control control)
        {
            if (!IsRequired)
                return;

            CheckBoxListValidator rfv = new CheckBoxListValidator();
            this.Controls.Add(rfv);

            rfv.ControlToValidate = control.ID;
            rfv.Display = ValidatorDisplay.Dynamic;
            rfv.ErrorMessage = string.Format(Resources.Controls.Validator_Format_RequiredField, this.Label);
            rfv.Text = Resources.Controls.Validator_RequiredField;
            rfv.ValidationGroup = ValidationGroup;
            rfv.CssClass = ValidationCssClass;
        }

        private void AddCheckBoxValidation(Control control)
        {
            if (!IsRequired)
                return;

            CheckBoxValidator rfv = new CheckBoxValidator();
            this.Controls.Add(rfv);

            rfv.ControlToValidate = control.ID;
            rfv.Display = ValidatorDisplay.Dynamic;
            rfv.ErrorMessage = string.Format(Resources.Controls.Validator_Format_RequiredField, this.Label);
            rfv.Text = Resources.Controls.Validator_RequiredField;
            rfv.ValidationGroup = ValidationGroup;
            rfv.CssClass = ValidationCssClass;
        }

        private void AddDataTypeValidation(Control control, ValidationDataType validationDataType) 
        {
            CompareValidator cv = new CompareValidator();
            this.Controls.Add(cv);

            cv.ControlToValidate = control.ID;
            cv.Operator = ValidationCompareOperator.DataTypeCheck;
            cv.Type = validationDataType;
            cv.Display = ValidatorDisplay.Dynamic;
            cv.ErrorMessage = string.Format(Resources.Controls.ResourceManager.GetString("Validator_Format_" + validationDataType.ToString()), this.Label);
            cv.Text = Resources.Controls.Validator_Format;
            cv.ValidationGroup = ValidationGroup;
            cv.CssClass = ValidationCssClass;
        }

        #endregion

        #region Value accessors

        public object ValueFrom
        {
            get
            {
                this.EnsureChildControls();

                if (Type == DataFieldType.DateRange)
                    return txtFirst.Text;

                return null;
            }
            set
            {
                this.EnsureChildControls();

                if (value != null) 
                {
                    if (value is DateTime)
                        txtFirst.Text = ((DateTime)value).ToShortDateString();
                    else
                        txtFirst.Text = value.ToString();
                }
                else
                    txtFirst.Text = string.Empty;
            }
        }

        public object ValueTo
        {
            get {
                this.EnsureChildControls();

                if (Type == DataFieldType.DateRange)
                    return txtSecond.Text;

                return null;
            }
            set
            {
                this.EnsureChildControls();

                if (value != null)
                {
                    if (value is DateTime)
                        txtSecond.Text = ((DateTime)value).ToShortDateString();
                    else
                        txtSecond.Text = value.ToString();
                }
                else
                    txtSecond.Text = string.Empty;
            }
        }

        public object Value
        {
            get
            {
                this.EnsureChildControls();
                return GetValue();
            }
            set
            {
                this.EnsureChildControls();
                if (value != null)
                    SetValue(value);
                else
                    ClearValue();
            }
        }

        public int IntValue
        {
            get { return Convert.ToInt32(this.Value); }
        }

        public decimal DecimalValue
        {
            get { return Convert.ToDecimal(this.Value); }
        }

        public Boolean BooleanValue
        {
            get { return Convert.ToBoolean(this.Value); }
        }

        public DateTime DateValue
        {
            get { return Convert.ToDateTime(this.Value); }
        }

        public string StringValue
        {
            get { return Value.ToString(); }
            set { Value = value; }
        }

        private object GetValue()
        {
            switch (Type)
            {
                case DataFieldType.Label:
                    return lblFirst.Text;
                    break;

                case DataFieldType.HyperLink:
                    return hypFirst.NavigateUrl;
                    break;

                case DataFieldType.DateRange:
                    throw new Exception("Use ValueFrom and ValueTo for range controls");
                    break;

                case DataFieldType.CheckBox:
                    return chkFirst.Checked;
                    break;

                default:
                    if (IsListControl())
                        return lstFirst.SelectedValue;
                    else
                        return txtFirst.Text;
                    break;
            }

            return null;
        }

        private void SetValue(object val)
        {
            if (val == null)
                return;

            switch (Type)
            {
                case DataFieldType.Label:
                    lblFirst.Text = val.ToString();
                    break;

                case DataFieldType.HyperLink:
                    hypFirst.NavigateUrl = val.ToString();
                    break;

                case DataFieldType.CheckBox:
                    chkFirst.Checked = Convert.ToBoolean(val);
                    break;

                case DataFieldType.DateRange:
                    throw new Exception("Use ValueFrom and ValueTo for range controls");

                default:

                    if (IsListControl())
                        lstFirst.SelectedValue = val.ToString();
                    else
                    {
                        if (val is DateTime)
                            txtFirst.Text = ((DateTime)val).ToShortDateString();
                        else
                            txtFirst.Text = val.ToString();
                    }

                    break;
            }
        }

        private void ClearValue()
        {
            txtFirst.Text = "";
        }

        public string SelectedValue
        {
            get {
                return StringValue;
            }
            set
            {
                StringValue = value;
            }
        }

        #endregion

        #region Text Accessors

        public string Text
        {
            get
            {
                this.EnsureChildControls();
                string text = NullDisplay;

                switch (Type)
                {
                    case DataFieldType.Label:

                        if (!string.IsNullOrEmpty(lblFirst.Text))
                            text = lblFirst.Text;
                        break;

                    case DataFieldType.DateRange:

                        if (!string.IsNullOrEmpty(txtFirst.Text) && !string.IsNullOrEmpty(txtSecond.Text))
                            text = string.Format("{0} a  {1}", this.txtFirst.Text, this.txtSecond.Text);
                        else if (!string.IsNullOrEmpty(txtFirst.Text))
                            text = string.Format("Del {0}", this.txtFirst.Text);
                        else if (!string.IsNullOrEmpty(txtSecond.Text))
                            text = string.Format("Al {0}", this.txtSecond.Text);

                        break;

                    case DataFieldType.Number:

                        if (!string.IsNullOrEmpty(txtFirst.Text)) 
                        {
                            if (DataNumberType.Percentage == NumberType)
                                text = string.Format("{0} %", this.DecimalValue.ToString("0.00"));
                            else if (!string.IsNullOrEmpty(CurrencySymbol))
                                text = string.Format("{0} {1}", CurrencySymbol, this.DecimalValue.ToString("0.00"));
                            else
                                text = this.DecimalValue.ToString("0.00");

                        }

                        break;

                    case DataFieldType.CheckBox:
                        text = Resources.Controls.ResourceManager.GetString("CheckBox_" + chkFirst.Checked.ToString());
                        break;

                    default:
                        if (IsListControl())
                        {
                            if (lstFirst.SelectedItem != null)
                            {
                                text = lstFirst.SelectedItem.Text;
                                if (Type == DataFieldType.DropdownList)
                                    if (lstFirst.SelectedIndex == 0)
                                        text = NullDisplay;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtFirst.Text))
                                text = txtFirst.Text;
                        }
                        break;
                }


                return text;

            }
            set {
                this.EnsureChildControls();

                switch (Type)
                {
                    case DataFieldType.DateRange:
                        throw new Exception("The text can not be set on a range field.");

                    case DataFieldType.CheckBox:
                        throw new Exception("The text can not be set on a checkbox field.");

                    case DataFieldType.Label:
                        lblFirst.Text = value;
                        break;

                    case DataFieldType.HyperLink:
                        hypFirst.Text = value;
                        break;

                    default:
                        if (!IsListControl())
                            txtFirst.Text = value;
                        else
                            throw new Exception("The text can not be set on a ListControl field.");

                        break;
                }
            }
        }

        #endregion

        #region ValueChanged Event
        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        #endregion
    }
}
