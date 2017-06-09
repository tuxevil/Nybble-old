<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="addUser.aspx.cs" Inherits="Administration.WebSite.Admin.addUser" Title="Administración de Usuario"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cplHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" Runat="Server">
    <div class="page_header">
        <h1>
            <asp:Literal runat="server" ID="lblTitles">Usuarios</asp:Literal>
        </h1>
    </div>

    <div class="form">
        <fieldset>
            <asp:CreateUserWizard ID="CreateUserWizard1" NavigationStyle-HorizontalAlign="Center" runat="server" 
                OnCreatedUser="CreateUserWizard1_CreatedUser" OnContinueButtonClick="CreateUserWizard1_ContinueButtonClick" 
                AnswerLabelText="Pregunta de Seguridad:" AnswerRequiredErrorMessage="Pregunta de seguridad requerida." 
                CancelButtonText="Cancelar" CompleteSuccessText="La cuenta se ha creado satisfactoriamente." 
                ConfirmPasswordCompareErrorMessage="La contraseña y la confirmación deben coincidir." 
                ConfirmPasswordLabelText="Confirmar Contraseña:" ConfirmPasswordRequiredErrorMessage="Se requiere confirmar contraseña." 
                ContinueButtonText="Continuar" CreateUserButtonText="Crear Usuario" 
                DuplicateEmailErrorMessage="El e-mail que ha ingresado se encuentra en uso. Por favor ingrese un e-mail distinto." 
                DuplicateUserNameErrorMessage="Por favor ingrese un nombre de usario diferente." 
                EmailRegularExpression='^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$'
                EmailRegularExpressionErrorMessage="Ingrese el e-mail correctamente." 
                EmailRequiredErrorMessage="E-mail requerido." 
                FinishCompleteButtonText="Finalizar" 
                FinishPreviousButtonText="Anterior" 
                InvalidAnswerErrorMessage="Por favor ingrese una respuesta de seguridad diferente." 
                InvalidPasswordErrorMessage="La contraseña debe tener {0} caracteres como mínimo." 
                InvalidQuestionErrorMessage="Por favor ingrese una pregunta de seguridad diferente." 
                PasswordLabelText="Contraseña:" PasswordRegularExpressionErrorMessage="Por favor ingrese una contraseña diferente." 
                PasswordRequiredErrorMessage="Contraseña requerida." QuestionLabelText="Pregunta de Seguridad:" 
                QuestionRequiredErrorMessage="Pregunta de seguridad requerida." StartNextButtonText="Siguiente" 
                StepNextButtonText="Siguiente" StepPreviousButtonText="Anterior" 
                UnknownErrorMessage="Su cuenta no fue creada. Por favor intente nuevamente." UserNameLabelText="Nombre de Usuario:" 
                UserNameRequiredErrorMessage="Nombre de Usuario requerido." 
                EnableTheming="True" EnableViewState="True" LoginCreatedUser="False">
                <WizardSteps>
                    <asp:CreateUserWizardStep ID="createUser" Title="Cree una nueva cuenta" runat="server">
                        <ContentTemplate>
                            <table border="0">
                                <tr>
                                    <td align="center" colspan="2">
                                        Cree una nueva cuenta</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="Nombre de Usuario requerido." ToolTip="Nombre de Usuario requerido."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName">Nombre:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                            ErrorMessage="Nombre requerido." ToolTip="Nombre requerido."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName">Apellido:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="Apellido requerido." ToolTip="Apellido requerido."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Contraseña requerida." ToolTip="Contraseña requerida." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirmar Contraseña:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                            ErrorMessage="Se requiere confirmar contraseña." ToolTip="Se requiere confirmar contraseña."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                            ErrorMessage="E-mail requerido." ToolTip="E-mail requerido." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Pregunta de Seguridad:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                            ErrorMessage="Pregunta de seguridad requerida." ToolTip="Pregunta de seguridad requerida."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Pregunta de Seguridad:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                            ErrorMessage="Pregunta de seguridad requerida." ToolTip="Pregunta de seguridad requerida."
                                            ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                            ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="La contraseña y la confirmación deben coincidir."
                                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:RegularExpressionValidator ID="EmailRegExp" runat="server" ControlToValidate="Email"
                                            Display="Dynamic" ErrorMessage="Ingrese el e-mail correctamente." ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CreateUserWizard1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: red">
                                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:CreateUserWizardStep>
                    <asp:CompleteWizardStep ID="completeUser" Title="" runat="server">
                    </asp:CompleteWizardStep>
                </WizardSteps>
                <NavigationStyle HorizontalAlign="Center" />
            </asp:CreateUserWizard>
        </fieldset>
        
    <center>
       <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="addUser.aspx">Volver</asp:HyperLink>
    <center>
</div>

</asp:Content>
