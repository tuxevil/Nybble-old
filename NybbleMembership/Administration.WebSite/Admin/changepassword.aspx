<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="changepassword.aspx.cs" Inherits="Administration.WebSite.Admin.changepassword" Title="Cambio de Contraseña"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cplHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" Runat="Server">
    <div class="page_header">
        <h1>
            <asp:Literal runat="server" ID="lblTitles">Cambiar Contraseña</asp:Literal>
        </h1>
    </div>

    <div class="form">
        <fieldset>
            <asp:LoginView ID="LoginView1" Runat="server" 
              Visible="true">
              <LoggedInTemplate>
                <asp:LoginName ID="LoginName1" Runat="server" FormatString="Esta logueado como {0}." />
                <br />
              </LoggedInTemplate>
              <AnonymousTemplate>
                No ha iniciado sesión
              </AnonymousTemplate>
            </asp:LoginView><br />
                <asp:ChangePassword ID="ChangePassword1" DisplayUserName="true" runat="server" CancelButtonText="Cancelar" ChangePasswordButtonText="Cambiar Contraseña" OnContinueButtonClick="ChangePassword1_ContinueButtonClick" ChangePasswordFailureText="Contraseña incorrecta o nueva contraseña no válida. La nueva contraseña debe tener {0} caracteres como mínimo." ChangePasswordTitleText="Cambie su contraseña" ConfirmNewPasswordLabelText="Confirme nueva contraseña:" ConfirmPasswordCompareErrorMessage="La confirmación de la contraseña debe coincidir con la nueva contraseña ingresada." ConfirmPasswordRequiredErrorMessage="Confirmación de nueva password requerida." ContinueButtonText="Continuar" NewPasswordLabelText="Nueva contraseña:" NewPasswordRegularExpressionErrorMessage="Ingrese una contraseña diferente." NewPasswordRequiredErrorMessage="Nueva contraseña requerida." PasswordLabelText="Contraseña:" PasswordRequiredErrorMessage="Contraseña requerida." SuccessText="Su contraseña ha cambiado con exito!" SuccessTitleText="" UserNameLabelText="Nombre de usuario:" UserNameRequiredErrorMessage="Nombre de usuario requerido.">
            </asp:ChangePassword>
        </fieldset>
        
        <center>
           <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Users.aspx">Volver</asp:HyperLink>
        </center>
    </div>
</asp:Content>
