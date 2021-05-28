<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bienvenido.aspx.cs" Inherits="AppLogin.Bienvenido" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contentBienvenida">
        <asp:Label ID="LabelUsuario" runat="server" Text="Bienvenido " Font-Bold="true"></asp:Label>
        <br/>
        <asp:Label ID="LabelFechaIngreso" runat="server" Text="Fecha de ingreso: "></asp:Label>
    </div>
    
</asp:Content>
