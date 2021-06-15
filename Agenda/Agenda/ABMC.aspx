<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMC.aspx.cs" Inherits="Agenda.ABMC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ABMCContent">
        <h3 id="tituloAccion" runat="server"></h3>

        <div class="form-group">
            <asp:Label runat="server" for="TxtApellidoNombre">Apellido y Nombre</asp:Label>
            
            <asp:TextBox ID="TxtApellidoNombre" runat="server" AutoPostBack="True" ontextchanged="GenerarCUIL"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorApellidoNombre"
                    ControlToValidate="TxtApellidoNombre" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:Label runat="server" for="DDGenero">Género</asp:Label>
            <asp:DropDownList ID="DDGenero" runat="server" AutoPostBack="True" ontextchanged="GenerarCUIL">
                <asp:ListItem Text=""></asp:ListItem>
                <asp:ListItem Text="Masculino"></asp:ListItem>
                <asp:ListItem Text="Femenino"></asp:ListItem>
                <asp:ListItem Text="Otro"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorGenero"
                    ControlToValidate="DDGenero" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:Label runat="server" for="DDPais">País</asp:Label>
            <asp:DropDownList ID="DDPais" runat="server">
                <asp:ListItem Text=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorPais"
                    ControlToValidate="DDPais" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:Label runat="server" for="TxtLocalidad">Localidad</asp:Label>
            <asp:TextBox ID="TxtLocalidad" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="DDContactoInt">Contacto Interno</asp:Label>
            <asp:DropDownList ID="DDContactoInt" runat="server">
                <asp:ListItem Text=""></asp:ListItem>
                <asp:ListItem Text="SI"></asp:ListItem>
                <asp:ListItem Text="NO"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorContInt"
                    ControlToValidate="DDContactoInt" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:Label runat="server" for="TxtOrganizacion">Organización</asp:Label>
            <asp:TextBox ID="TxtOrganizacion" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="DDArea">Area</asp:Label>
            <asp:DropDownList ID="DDArea" runat="server">
                <asp:ListItem Text=""></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="DDActivo">Activo</asp:Label>
            <asp:DropDownList ID="DDActivo" runat="server">
                <asp:ListItem Text=""></asp:ListItem>
                <asp:ListItem Text="SI"></asp:ListItem>
                <asp:ListItem Text="NO"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorActivo"
                    ControlToValidate="DDActivo" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>

        <div class="form-group">
            <asp:Label runat="server" for="TxtDireccion">Dirección</asp:Label>
            <asp:TextBox ID="TxtDireccion" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="TxtTelFijo">Tel. Fijo - Interno</asp:Label>
            <asp:TextBox ID="TxtTelFijo" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="TxtTelCel">Tel. Celular</asp:Label>
            <asp:TextBox ID="TxtTelCel" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="TxtEmail">E-mail</asp:Label>
            <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator id="RequiredFieldValidatorEmail"
                    ControlToValidate="TxtEmail" ErrorMessage="*"
                    runat="server" ForeColor="Red" ValidationGroup="ValidacionGuardar">
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ID="EmailValidator" runat="server"
        ControlToValidate="TxtEmail" OnServerValidate="ValidacionEmail" ValidationGroup="ValidacionGuardar"></asp:CustomValidator>

        <div class="form-group">
            <asp:Label runat="server" for="TxtCuentaSkype">Cuenta Skype</asp:Label>
            <asp:TextBox ID="TxtCuentaSkype" runat="server"></asp:TextBox>
        </div>
        <asp:CustomValidator ID="SkypeValidator" runat="server"
        ControlToValidate="TxtCuentaSkype" OnServerValidate="ValidacionSkypeTel" ValidateEmptyText="true" ValidationGroup="ValidacionGuardar"></asp:CustomValidator>

        <div class="form-group">
            <asp:Label runat="server" for="TxtCUIL">CUIL</asp:Label>
            <asp:TextBox ID="TxtCUIL" runat="server" Enabled="false"></asp:TextBox>
        </div>

        <div id="contenedorBtnsABMC">
            <asp:Button ID="BtnSalir" runat="server" Text="Volver" OnClick="BtnSalir_Click"/>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" ValidationGroup="ValidacionGuardar" OnClientClick="return window.confirm('¿Seguro que desea realizar esta accion?');"/>
        </div>

        <div id="contenedorAvisos" >
            <span id="SpanAviso" runat="server" class="inactivo" ></span>
        </div>
    </div>
    
</asp:Content>
