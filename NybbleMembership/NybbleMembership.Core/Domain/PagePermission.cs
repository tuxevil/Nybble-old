using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace NybbleMembership.Core.Domain
{
    public class PagePermission : Permission
    {
        private string pageName;
        private string folderName;
        
        public virtual string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        public virtual string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }

        public override Type[] GetClassTypes()
        {
            Type[] allowtypes = new Type[1];
            allowtypes[0] = typeof(TemplateControl);
            return allowtypes;
        }

        public override bool CheckInstance(object o, Enum action)
        {
            string info = (o as TemplateControl).AppRelativeVirtualPath;

            string folder = info.Substring(1, info.LastIndexOf("/"));
            string name = info.Substring(info.LastIndexOf("/") + 1);

            if(folder.ToLower() == this.folderName.ToLower() && name.ToLower() == this.pageName.ToLower())
                return true;
            
            return false;
        }
    }
}
