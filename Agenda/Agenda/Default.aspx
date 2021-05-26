<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Agenda._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenedorFiltros">
        
        <div class="form-group">
            <asp:Label runat="server" for="TxtApellidoNombre">Apellido y Nombre</asp:Label>
            <asp:TextBox ID="TxtApellidoNombre" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="DDPais">Pais</asp:Label>
            <asp:DropDownList ID="DDPais" runat="server">
                <asp:ListItem Text="TODOS"></asp:ListItem>
                <asp:ListItem Text="Argentina"></asp:ListItem>
                <asp:ListItem Text="Brasil"></asp:ListItem>
                <asp:ListItem Text="Chile"></asp:ListItem>
                <asp:ListItem Text="Uruguay"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="TxtLocalidad">Localidad</asp:Label>
            <asp:TextBox ID="TxtLocalidad" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="TxtFechaIngresoD">Fecha Ingreso Desde</asp:Label>
            <asp:TextBox ID="TxtFechaIngresoD" runat="server" placeholder="DD/MM/AAAA"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server">Fecha Ingreso Hasta</asp:Label>
            <asp:TextBox ID="TxtFechaIngresoH" runat="server" placeholder="DD/MM/AAAA"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="DDContactoInt">Contacto Interno</asp:Label>
            <asp:DropDownList ID="DDContactoInt" runat="server" >
                <asp:ListItem Text="TODOS"></asp:ListItem>
                <asp:ListItem Text="SI"></asp:ListItem>
                <asp:ListItem Text="NO"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="TxtOrganizacion">Organización</asp:Label>
            <asp:TextBox ID="TxtOrganizacion" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="DDArea">Area</asp:Label>
            <asp:DropDownList ID="DDArea" runat="server">
                <asp:ListItem Text="TODOS"></asp:ListItem>
                <asp:ListItem Text="Marketing"></asp:ListItem>
                <asp:ListItem Text="Finanzas"></asp:ListItem>
                <asp:ListItem Text="RRHH"></asp:ListItem>
                <asp:ListItem Text="Operaciones"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="DDActivo">Activo</asp:Label>
            <asp:DropDownList ID="DDActivo" runat="server">
                <asp:ListItem Text="TODOS"></asp:ListItem>
                <asp:ListItem Text="SI"></asp:ListItem>
                <asp:ListItem Text="NO"></asp:ListItem>
            </asp:DropDownList>
        </div>
      
    </div>
    <div id="contenedorBtns">
        <asp:Button ID="BtnLimpiarFiltros" runat="server" />
        <asp:Button ID="BtnNuevoContacto" runat="server" Text="Nuevo Contacto" />
        <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
    </div>
    <div id="contenedorAvisos" >
        <span id="SpanAviso" runat="server" class="inactivo" ></span>
    </div> 
    <div id="contenedorResultado" runat="server">
    </div>
    <div id="contenedorBtnsPaginas" runat="server">
    </div>
    
    
    
</asp:Content>
