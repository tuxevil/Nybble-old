<%@ Page Language="C#" ValidateRequest="false" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Moxiecode.Manager" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	string prefix, cmd;
	object result;
	ManagerEngine man;

	// Use install if it exists
	if (Directory.Exists(Server.MapPath("../install"))) {
		Response.Write("{\"result\":null,\"id\":null,\"error\":{\"errstr\":\"You need to run the installer or rename/remove the \"install\" directory.\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"FATAL\"}}");
		Response.End();
	}

	// Get input
	cmd = Request["cmd"];

	// Handle stream command
	try {
		if (cmd != null) {
			// Parse prefix
			Match match = Regex.Match(cmd, @"^([a-z]+)\.(.*)");
			prefix = match.Groups[1].Value;
			cmd = match.Groups[2].Value;

			// Setup file manager
			man = new ManagerEngine(prefix);

			if (man.IsAuthenticated) {
				// Initialize
				man.DispatchEvent(EventType.Init);

				// Dispatch events
				if (Request.RequestType.ToLower() == "get") {
					man.DispatchEvent(EventType.BeforeStream, cmd, Request.QueryString);
					man.DispatchEvent(EventType.Stream, cmd, Request.QueryString);
					man.DispatchEvent(EventType.AfterStream, cmd, Request.QueryString);
				} else if (Request.RequestType.ToLower() == "post") {
					NameValueCollection args = new NameValueCollection();

					args.Add(Request.Form);
					args.Add(Request.QueryString);

					man.DispatchEvent(EventType.BeforeUpload, cmd, args);
					result = man.ExecuteEvent(EventType.Upload, cmd, args);

					// Flash can't get our nice JSON output, so we need to return something... weird.
					if (Request["format"] == "flash") {
						// 400 Bad Request = Something just failed
						// 409 Conflict = File exists
						// 414 Request-URI Too Long = File to big or not enough space?
						// 415 Unsupported Media Type = Can't upload that type of file.
						// 412 Precondition Failed = Read / Write error.
						ResultSet rs = (ResultSet) result;

						if (rs.Data.Count > 0) {
							Hashtable row = rs[0];

							switch((string) row["status"]) {
								case "OK":
									// No header needed, 200 OK!
								break;

								case "DEMO_ERROR":
								case "ACCESS_ERROR":
								case "MCACCESS_ERROR":
								case "FILTER_ERROR":
									Response.StatusCode = 405;
								break;

								case "OVERWRITE_ERROR":
									Response.StatusCode = 409;
								break;

								case "RW_ERROR":
									Response.StatusCode = 412;
								break;

								case "SIZE_ERROR":
									Response.StatusCode = 414;
								break;

								default:
									Response.StatusCode = 501;
									break;
							}
						}
					} else {
						Response.Write("<html><body><script type=\"text/javascript\">parent.handleJSON(");

						// Call RPC
						JSON.SerializeRPC(
							"u1",
							null,
							result,
							Response.OutputStream
						);

						Response.Write(");</script></body></html>");
					}

					man.DispatchEvent(EventType.AfterUpload, cmd, args);
				}
			} else
				throw new ManagerException("Not authenticated.");
		}
	} catch (ManagerException ex) {
		if (Request.RequestType.ToLower() == "post" && Request["format"] != "flash") {
			Response.Write("<html><body><script type=\"text/javascript\">parent.handleJSON(");
			Response.Write("{\"result\":null,\"id\":null,\"error\":{\"errstr\":\"" + StringUtils.Escape(ex.Message) + "\",\"errfile\":\"\",\"errline\":null,\"errcontext\":\"\",\"level\":\"FATAL\"}}");
			Response.Write(");</script></body></html>");
		} else
			Response.Write(ex.Message);
	}
%>