<%@ Page Language="C#" ContentType="text/javascript" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	bool compress = true;
	string baseURL = Request.Path.Substring(0, Request.Path.LastIndexOf('/'));

	// Use install if it exists
	if (Directory.Exists(Server.MapPath("../install"))) {
		Response.Write("alert('You need to run the installer or rename/remove the \"install\" directory.');");
		Response.End();
	}

	if (compress) {
		JSCompressor compressor = new JSCompressor();

		compressor.GzipCompress = false;

		// Compress these
		compressor.AddFile("mox.js");
		compressor.AddFile("gz_loader.js");
		compressor.AddContent("mox.baseURL = '" + baseURL + "';");
		compressor.AddContent("mox.defaultDoc = 'default.aspx';");
		compressor.Compress(Request, Response);
	} else {
		Response.WriteFile("mox.js");
		Response.Write("\nmox.baseURL = '" + baseURL + "';\n");
		Response.Write("\nmox.defaultDoc = 'default.aspx';\n");
	}
%>