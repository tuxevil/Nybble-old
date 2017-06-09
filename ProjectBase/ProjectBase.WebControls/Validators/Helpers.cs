using System;
using System.Xml;
using System.Configuration;
using System.Runtime;
using System.Web;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProjectBase.WebControls.Validators
{
    class Helpers
    {
        // Replicates the functionality of the internal Page.EnableLegacyRendering property
        public static bool EnableLegacyRendering()
        {
            // 2007-10-02: The following commented out code will NOT work in Medium Trust environments
            //Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //XhtmlConformanceSection xhtmlSection = (XhtmlConformanceSection) cfg.GetSection("system.web/xhtmlConformance");

            //return xhtmlSection.Mode == XhtmlConformanceMode.Legacy;


            // 2007-10-02: The following work around, provided by Michael Tobisch, works in
            //              Medium Trust by directly reading the Web.config file as XML.
            bool result;
            
            try
            {
                string webConfigFile = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config");
                XmlTextReader webConfigReader = new XmlTextReader(new StreamReader(webConfigFile));
                result = ((webConfigReader.ReadToFollowing("xhtmlConformance")) && (webConfigReader.GetAttribute("mode") == "Legacy"));
                webConfigReader.Close();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static void ShowValidatorsOnButton(WebControl c, string validationGroup)
        {
            if (string.IsNullOrEmpty(c.ClientID))
                return;

            // Do not add Jquery to avoid the need of Jquery in the control
            string javascriptCode = "document.getElementById('{0}').onclick = function() {{showValidators('{1}');}};";
            RegisterScriptOnClientSide(c, string.Format(javascriptCode, c.ClientID, validationGroup));
        }

        public static void RegisterScriptOnClientSide(WebControl c, string script)
        {
            ScriptManager sm = ScriptManager.GetCurrent(c.Page);
            if (sm == null)
                c.Page.ClientScript.RegisterStartupScript(c.GetType(), c.ClientID, script, true);
            else
                ScriptManager.RegisterStartupScript(c, c.GetType(), c.ClientID, script, true);
        }
    }
}

