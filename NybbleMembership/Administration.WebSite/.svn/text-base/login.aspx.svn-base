<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Administration.WebSite.login" Title="Ingreso"%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <meta http-equiv="content-type" content="text/html;charset=UTF-8" />
  <title>[Price Manager Permiso de Usuarios] Ingreso</title>
    <link rel="stylesheet" href="/css/main.css" type="text/css" />
    <style>html {background: #003366} </style>
</head>
<body class="login">
    <form id="frmBody" runat="server">
    <div id="LogoBox" class="white"><img src="/img/grundfos_logo.gif" /></div>
        
        <div class="login">
            <div class="flash_notice" id="Flash" runat="server"><asp:Literal ID="lblInfo" runat="server" Text="Por favor, complete sus datos para ingresar."></asp:Literal></div>
            
            <div id="login_dialog" class="login_dialog">
                <div id="user_name_login">
                    <asp:Login ID="Login1" runat="server" OnLoginError="Login1_LoginError1" OnLoggedIn="Login1_LoggedIn" OnLoggingIn="Login1_LoggingIn" FailureText=" ">
                        <LayoutTemplate>
                            <h2>Usuario</h2>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Nombre de usuario requerido." ToolTip="Nombre de usuario requerido."
                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    
                            <h2>Contraseña</h2>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Contraseña requerida." ToolTip="Contraseña requerida." ValidationGroup="Login1">*</asp:RequiredFieldValidator><br />
                             <label style="font-size:12px;"><asp:CheckBox ID="RememberMe" runat="server" CssClass="auto"/>Recordarme en este equipo</label><br />
                             <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                             <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Ingresar" ValidationGroup="Login1" CssClass="button" />
                        </LayoutTemplate>
                    </asp:Login>
                </div>
           </div>
         </div> 
    </form>
</body>
</html>    