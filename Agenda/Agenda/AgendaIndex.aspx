﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgendaIndex.aspx.cs" Inherits="Agenda.AgendaIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            <asp:TextBox ID="TxtFechaIngresoD" runat="server" placeholder="DD/MM/AAAA" OnTextChanged="FechaIngresoD_Text_Changed"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server">Fecha Ingreso Hasta</asp:Label>
            <asp:TextBox ID="TxtFechaIngresoH" runat="server" placeholder="DD/MM/AAAA" OnTextChanged="FechaIngresoH_Text_Changed"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" for="DDContactoInt">Contacto Interno</asp:Label>
            <asp:DropDownList ID="DDContactoInt" runat="server">
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
    <asp:CustomValidator ID="FechaIngresoValidator" runat="server"
        ControlToValidate="TxtFechaIngresoD" OnServerValidate="ValidacionFecha"></asp:CustomValidator>
    <div id="contenedorBtns">
        <asp:Button ID="BtnLimpiarFiltros" runat="server" OnClientClick="return LimpiarCampos()"/>
        <asp:Button ID="BtnNuevoContacto" runat="server" Text="Nuevo Contacto" />
        <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click"/>
    </div>
    <div id="contenedorAvisos" >
        <span id="SpanAviso" runat="server" class="inactivo" ></span>
    </div> 
    <div id="contenedorResultado" runat="server">
        <asp:GridView ID="GridViewConsulta" runat="server" 
            AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center" 
            Width="100%" GridLines="Horizontal" OnRowCommand="GridViewConsulta_RowCommand">
            <Columns>
                <asp:BoundField DataField="ApellidoNombre" HeaderText="Apellido y Nombre" ReadOnly="True"/>
                <asp:BoundField DataField="Genero" HeaderText="Genero" ReadOnly="True"/>
                <asp:BoundField DataField="Pais" HeaderText="País" ReadOnly="True"/>
                <asp:BoundField DataField="Localidad" HeaderText="Localidad" ReadOnly="True"/>
                <asp:BoundField DataField="ContactoInterno" HeaderText="Contacto Interno" ReadOnly="True"/>
                <asp:BoundField DataField="Organizacion" HeaderText="Organización" ReadOnly="True"/>
                <asp:BoundField DataField="Area" HeaderText="Área" ReadOnly="True"/>
                <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" ReadOnly="True"/>
                <asp:BoundField DataField="Activo" HeaderText="Activo" ReadOnly="True"/>
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True"/>
                <asp:BoundField DataField="TelFijo" HeaderText="Tel Fijo" ReadOnly="True"/>
                <asp:BoundField DataField="TelCel" HeaderText="Tel Cel" ReadOnly="True"/>
                <asp:BoundField DataField="Email" HeaderText="E-mail" ReadOnly="True"/>
                <asp:BoundField DataField="Skype" HeaderText="Skype" ReadOnly="True"/>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:ImageButton ID="ZoomContacto" CommandName="Zoom" runat="server" ImageUrl="./Images/zoom.png"/>
                        <asp:ImageButton ID="EditContacto" CommandName="Edit" runat="server" ImageUrl="./Images/edit.png"/>
                        <asp:ImageButton ID="EliminarContacto" CommandName="Eliminar" runat="server" ImageUrl="./Images/delete.png"/>
                        <asp:ImageButton ID="ActivarDesactivarContacto" CommandName="ActivarDesactivar" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="contenedorBtnsPaginas" runat="server">
    </div>
    
    
    
</asp:Content>
