using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NybbleMembership.Core.Domain
{
    public class WebControlPermission : Permission
    {
        private string relativePath;
        private string controlIdentifier;

        public virtual string RelativePath
        {
            get { return relativePath; }
            set { relativePath = value; }
        }

        public virtual string ControlIdentifier
        {
            get { return controlIdentifier; }
            set { controlIdentifier = value; }
        }

        public override Type[] GetClassTypes()
        {
            Type[] allowtypes = new Type[2];
            allowtypes[0] = typeof(HtmlControl);
            allowtypes[1] = typeof(WebControl);
            return allowtypes;
        }

        public override bool CheckInstance(object o, Enum action)
        {
            Control temp = (Control)o;
            string objectid = temp.ID;
            
            string info;
            if (temp.Page != null)
                info = temp.Page.AppRelativeTemplateSourceDirectory;
            else
                info = temp.AppRelativeTemplateSourceDirectory;

            string folder = info.Substring(1, info.LastIndexOf("/"));

            if (objectid.ToLower() == this.controlIdentifier.ToLower() && (string.IsNullOrEmpty(this.relativePath)  || folder.ToLower() == this.relativePath.ToLower()))
                return true;

            return false;
        }
    }
}
