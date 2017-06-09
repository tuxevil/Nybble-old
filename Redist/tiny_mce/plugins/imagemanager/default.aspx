<%@ Page Language="C#" ContentType="text/plain" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Moxiecode.Manager" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	string type = Request["type"];
	ManagerEngine man;

	// Run install if it exists
	if (Directory.Exists(Server.MapPath("install")))
		Response.Redirect("install/default.aspx", true);

	if (type == null)
		Response.Redirect("examples.html", true);

	// Setup file manager
	man = new ManagerEngine(type, Path.GetDirectoryName(Request.PhysicalPath));

	if (man.IsAuthenticated)
		Response.Redirect("pages/" + man.Config["general.theme"] + "/index.html", true);
	else
		Response.Redirect(man.Config["authenticator.login_page"] + "?return_url=" + Server.UrlEncode("" + Request.Url), true);
%>