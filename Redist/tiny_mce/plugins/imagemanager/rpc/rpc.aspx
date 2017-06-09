<%@ Page Language="C#" ContentType="text/plain" ValidateRequest="false" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Moxiecode.Manager" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	Hashtable args = new Hashtable();
	string id, method, prefix = "im";
	ManagerEngine man = null;

	// Use install if it exists
	if (Directory.Exists(Server.MapPath("../install"))) {
		Response.Write("{\"result\":null,\"id\":null,\"error\":{\"errstr\":\"You need to run the installer or rename/remove the \"install\" directory.\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"FATAL\"}}");
		Response.End();
	}

	// Parse JSON
	try {
		object o = JSON.ParseJSON( new System.IO.StreamReader( Request.InputStream ) );

		Hashtable ht = (Hashtable)o;
		id = ht["id"].ToString();
		method = ht["method"].ToString( );
		ArrayList arr = (ArrayList)ht["params"];
		if( arr.Count > 0 )
			args = (Hashtable)arr[0];

//		JSONRpcCall call = JSON.ParseRPC(new System.IO.StreamReader(Request.InputStream));

		// Get JSON items
//		id = call.Id;
//		method = call.Method;
//		args = call.Args;

		// Parse prefix
		if (method != null) {
			Match match = Regex.Match(method, @"^([a-z]+)\.(.*)");
			prefix = match.Groups[1].Value;
			method = match.Groups[2].Value;
		}

		if (id != null) {
			// Setup file manager
			man = new ManagerEngine(prefix);

			if (man.IsAuthenticated) {
				// Initialize
				man.DispatchEvent(EventType.Init);

				// Call RPC
				Moxiecode.Manager.Utils.JSON.SerializeRPC(
					id,
					null,
					man.ExecuteEvent(EventType.RPC, method, args),
					Response.OutputStream
				);
			} else
				Response.Write("{\"result\":{\"login_url\":\"" + man.Config["authenticator.login_page"] + "\"},\"id\":null,\"error\":{\"errstr\":\"Access denied by authenicator.\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"AUTH\"}}");
		}
	} catch (ManagerException ex) {
		Response.Write("{\"result\":null,\"id\":null,\"error\":{\"errstr\":\"" + StringUtils.Escape(ex.Message) + "\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"" + ex.LevelName + "\"}}");

		// Log error
		man = new ManagerEngine(prefix);
		man.Logger.Error(ex.ToString());
	} catch (Exception ex) {
		Response.Write("{\"result\":null,\"id\":null,\"error\":{\"errstr\":\"" + StringUtils.Escape(ex.Message) + "\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"FATAL\"}}");

		// Log error
		man = new ManagerEngine(prefix);
		man.Logger.Error(ex.ToString());
	}
%>