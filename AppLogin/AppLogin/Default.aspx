<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppLogin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Login ID="ContentLogin" runat="server" 
        LoginButtonText="Ingresar" PasswordLabelText="Contraseña:" 
        RememberMeText="Recuerdame" TitleText="Login" 
        UserNameLabelText="Usuario:" OnAuthenticate="AutenticarLogin" 
        FailureText="Usuario y/o Contraseña invalido" FailureTextStyle-BackColor="#ff9a9a"
        FailureTextStyle-Font-Bold="true" FailureTextStyle-ForeColor="#3a3a3a"
        ForeColor="#3a3a3a">
    </asp:Login>
</asp:Content>
