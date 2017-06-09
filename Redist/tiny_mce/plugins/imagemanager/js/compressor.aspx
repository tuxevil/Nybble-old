<%@ Page Language="C#" ContentType="text/javascript" %>
<%@ Import Namespace="Moxiecode.Manager.Utils" %>
<%
	string[] classes = Request["classes"].Split(new char[]{','});

	JSCompressor compressor = new JSCompressor();

	foreach (string file in classes)
		compressor.AddFile(file.ToLower().Replace('.', '/') + ".js");

	compressor.Compress(Request, Response);
%>
