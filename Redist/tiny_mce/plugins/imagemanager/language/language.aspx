<%@ Page Language="C#" ContentType="text/javascript" %>
<%@ Import Namespace="Moxiecode.Manager" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	string type = Request["type"];
	string format = Request["format"];
	string prefix = Request["prefix"];

	try {
		if (type != null) {
			ManagerEngine man = new ManagerEngine(type);

			// Let the authenticators override config options
			if (man.IsAuthenticated)
				man.DispatchEvent(EventType.Init);

			LanguagePack langPack = man.LangPack;
			Moxiecode.Manager.Utils.GroupCollection groups = langPack.Languages[man.Config.Get("general.language", "en")].Groups;

			// TinyMCE specific format
			if (format != null && format == "tinymce") {
				Response.Write("tinyMCE.addToLang('',{\n");

				foreach (string key in groups["tinymce"].Items.AllKeys)
					Response.Write(prefix +  key + ":" + "'" + JSONWriter.EncodeString(groups["tinymce"].Items[key]) + "',\n");

				Response.Write("end:'null'\n});\n\n");
			} else if (format != null && format == "tinymce_3_x") {
				Response.Write("tinyMCE.addI18n('en',{\n");

				foreach (string key in groups["tinymce"].Items.AllKeys)
					Response.Write(prefix +  key + ":" + "'" + JSONWriter.EncodeString(groups["tinymce"].Items[key]) + "',\n");

				Response.Write("end:'null'\n});\n\n");
			} else {
				Response.Write("mox.require(['mox.lang.LangPack'], function() {\n");

				foreach (string groupName in groups.Keys) {
					Moxiecode.Manager.Utils.Group group = (Moxiecode.Manager.Utils.Group) groups[groupName];

					Response.Write("mox.lang.LangPack.add('en', '" + group.Target + "', {\n");

					foreach (string key in group.Items.AllKeys) {
						Response.Write(key + ":" + "'" + JSONWriter.EncodeString(group.Items[key]) + "',\n");
						// Todo: Add , check
					}

					Response.Write("end:'null'\n});\n\n");
				}

				Response.Write("\n});\n\n");
			}
		}
	} catch (Exception ex) {
		Response.Write("alert('" + StringUtils.Escape(ex.Message) + "');");
	}
%>

function translatePage() {
	if (mox && mox.lang && mox.lang.LangPack)
		mox.lang.LangPack.translatePage();
}
