<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testrepeditor.aspx.cs" Inherits="ProjectBase.WebTester.testrepeditor" %>

<%@ Register Src="editor.ascx" TagName="editor" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
    <link rel="stylesheet" type="text/css" href="main.css" media="screen"  />
   <script language="javascript" type="text/javascript" src="jquery-1.3.1.min.js"></script>
   <script language="javascript" type="text/javascript" src="tiny_mce/tiny_mce.js"></script>
   <script language="javascript" type="text/javascript">
   
   $(document).ready(function() {
        $('.form .integer').keydown(function(e){
            if (e.keyCode == 190)
                return false;
            if (!(e.keyCode == 27 || e.keyCode == 109 || e.keyCode == 189 || e.keyCode == 110 || e.keyCode == 188 || e.keyCode == 46 || e.keyCode == 190 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 13 || (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 37 && e.keyCode <= 40)))
                return false;
        });
          
        $('.form .currency').keydown(function(e){
            if (e.keyCode == 190)
                return false;
            if (!(e.keyCode == 27 || e.keyCode == 109 || e.keyCode == 189 || e.keyCode == 110 || e.keyCode == 188 || e.keyCode == 46 || e.keyCode == 190 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 13 || (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 37 && e.keyCode <= 40)))
                return false;
        });
        
        $('.form .currency').keyup(function(e){
            if (e.keyCode == 110)
                $(this).val($(this).val().replace(".",","));
        });
          
   });
        
    function CheckCodes(src, args)
    {
        alert('LEO');
    }
    
    function showValidators(validationGroup) {
        Page_ValidationSummaries[0].validationGroup = validationGroup;
        if (!Page_ClientValidate(validationGroup))
            $("#validators").show('slow');
        else
            $("#validators").hide('slow');
    }
   
    function testValidators(validationGroup) {
        if ($("#validators").is(":visible") && Page_ClientValidate(validationGroup))
            $("#validators").hide('slow');
    }

    tinyMCE.init({
        mode : "textareas",
     	editor_selector : "mceEditor",
        theme : "advanced",
        //editor_deselector : "excludeEditor",
		plugins : "imagemanager,paste",
		theme_advanced_buttons1_add_before : "insertimage,pastetext,pasteword,selectall",
		imagemanager_insert_template : '<img src="{$path}" width="{$custom.width}" height="{$custom.height}" border="0" /></a>',
		relative_urls : false,
		paste_create_paragraphs : false,
		paste_create_linebreaks : false,
		paste_use_dialog : true,
		paste_auto_cleanup_on_paste : true,
		paste_convert_middot_lists : false,
		paste_unindented_list_class : "unindentedList",
		paste_convert_headers_to_strong : true,
		paste_insert_word_content_callback : "convertWord"
    });
    
    tinyMCE.init({
        mode : "textareas",
     	editor_selector : "mceEditorReadOnly",
     	readonly : true,
        theme : "advanced",
        //editor_deselector : "excludeEditor",
		plugins : "imagemanager,paste",
		theme_advanced_buttons1_add_before : "insertimage,pastetext,pasteword,selectall",
		imagemanager_insert_template : '<img src="{$path}" width="{$custom.width}" height="{$custom.height}" border="0" /></a>',
		relative_urls : false,
		paste_create_paragraphs : false,
		paste_create_linebreaks : false,
		paste_use_dialog : true,
		paste_auto_cleanup_on_paste : true,
		paste_convert_middot_lists : false,
		paste_unindented_list_class : "unindentedList",
		paste_convert_headers_to_strong : true,
		paste_insert_word_content_callback : "convertWord"
    });
    
    
	function convertWord(type, content) {
		return content;
	}    
    </script>    
<body>
    <form id="form1" runat="server">
    <div>
               <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
        </asp:ScriptManager>
 
    <div id="validators" style="background:#eee;display:none;top:10px;right:10px;border:3px solid red;position:fixed; width:300px; height:200px;">
        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="false" ShowSummary="true" ValidationGroup="form" />
    </div>
    
        <asp:Button ID="btnTest" runat="Server" Text="Test" />
            <asp:Repeater runat="server" ID="rptDistributorContacts" OnItemDataBound="rptDistributorContacts_ItemDataBound">
            <ItemTemplate>
                    <uc1:editor id="DistributorContactEditor1" runat="server"></uc1:editor>
            </ItemTemplate>
        </asp:Repeater>
</div>
    </form>
</body>
</html>
