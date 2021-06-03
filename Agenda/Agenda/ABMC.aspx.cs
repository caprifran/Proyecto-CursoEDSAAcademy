using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Entity.Contacto;
using System.Text.RegularExpressions;
using Agenda.BLL;

namespace Agenda
{
    public partial class ABMC : System.Web.UI.Page
    {
        Business business;
        Contacto contacto;
        protected void Page_Load(object sender, EventArgs e)
        {
            business = new Business((List<Contacto>)Application["contactosEjemplo"]);

            if (Cache["Accion"] != null || Cache["contacto"] != null)
            {
                switch (Cache["Accion"])
                {
                    case "Zoom":
                        tituloAccion.InnerText = "Consultar Contacto";
                        TxtApellidoNombre.Enabled = false;
                        DDGenero.Enabled = false;
                        DDPais.Enabled = false;
                        TxtLocalidad.Enabled = false;
                        DDContactoInt.Enabled = false; ;
                        TxtOrganizacion.Enabled = false;
                        DDArea.Enabled = false;
                        DDActivo.Enabled = false;
                        TxtDireccion.Enabled = false;
                        TxtTelFijo.Enabled = false;
                        TxtTelCel.Enabled = false;
                        TxtEmail.Enabled = false;
                        TxtCuentaSkype.Enabled = false;
                        BtnGuardar.Visible = false;
                        this.contacto = (Contacto)Cache["contacto"];
                        break;

                    case "Edit":
                        tituloAccion.InnerText = "Editar Contacto";
                        this.contacto = (Contacto)Cache["contacto"];
                        break;

                    case "NuevoContacto":
                        tituloAccion.InnerText = "Agregar Contacto";
                        this.contacto = new Contacto();
                        break;
                }

                
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Cache["contacto"] != null)
            {
                TxtApellidoNombre.Text = this.contacto.ApellidoNombre;
                DDGenero.SelectedValue = this.contacto.Genero;
                DDPais.SelectedValue = this.contacto.Pais;
                TxtLocalidad.Text = this.contacto.Localidad;
                if (this.contacto.ContactoInterno.ToString() == "True") DDContactoInt.SelectedValue = "SI";
                else DDContactoInt.SelectedValue = "NO";
                if (this.contacto.Organizacion == "&nbsp;") TxtOrganizacion.Text = ""; // En caso de campo vacio
                else TxtOrganizacion.Text = this.contacto.Organizacion;
                if (this.contacto.Area == "&nbsp;") DDArea.SelectedValue = ""; // En caso de campo vacio
                else DDArea.SelectedValue = this.contacto.Area;
                if (this.contacto.Activo.ToString() == "True") DDActivo.SelectedValue = "SI";
                else DDActivo.SelectedValue = "NO";
                TxtDireccion.Text = this.contacto.Direccion;
                TxtTelFijo.Text = this.contacto.TelFijo;
                TxtTelCel.Text = this.contacto.TelCel;
                TxtEmail.Text = this.contacto.Email;
                TxtCuentaSkype.Text = this.contacto.Skype;
            }
        }
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgendaIndex.aspx");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            SpanAviso.InnerText = "";
            SpanAviso.Attributes.Add("class", "inactivo");

            if(!(RequiredFieldValidatorApellidoNombre.IsValid &&
               RequiredFieldValidatorGenero.IsValid &&
               RequiredFieldValidatorPais.IsValid &&
               RequiredFieldValidatorContInt.IsValid &&
               RequiredFieldValidatorActivo.IsValid &&
               RequiredFieldValidatorEmail.IsValid))
                    Cache["msjError"] += "- Datos invalidos, revise de haber completado los campos obligatorios, señalizados con el simbolo *" + "\r\n"; 

            if (IsValid)
            {
                this.contacto.ApellidoNombre = TxtApellidoNombre.Text;
                this.contacto.Genero = DDGenero.SelectedValue;
                this.contacto.Pais = DDPais.SelectedValue;
                this.contacto.Localidad = TxtLocalidad.Text;
                if (DDContactoInt.SelectedValue == "Si") this.contacto.ContactoInterno = true;
                else this.contacto.ContactoInterno = false;
                this.contacto.Organizacion = TxtOrganizacion.Text;
                this.contacto.Area = DDArea.SelectedValue;
                if (DDActivo.SelectedValue == "Si") this.contacto.Activo = true;
                else this.contacto.Activo = false;
                this.contacto.Direccion = TxtDireccion.Text;
                this.contacto.TelFijo = TxtTelFijo.Text;
                this.contacto.TelCel = TxtTelCel.Text;
                this.contacto.Email = TxtEmail.Text;
                this.contacto.Skype = TxtCuentaSkype.Text;
                
                switch (Cache["Accion"])
                {
                    case "Edit":
                        this.business.EditarContacto(this.contacto);
                        break;

                    case "NuevoContacto":
                        contacto.Id = ((List<Contacto>)Application["contactosEjemplo"]).Count;
                        contacto.FechaIngreso = DateTime.Now;
                        this.business.AgregarContacto(this.contacto);
                        break;
                }

                Response.Redirect("AgendaIndex.aspx");
            }
            else
            {
                ImprimirAviso((string)Cache["msjError"]);
                Cache["msjError"] = "";
            }
            
        }
        private void ImprimirAviso(String avisoText)
        {
            SpanAviso.Attributes.Add("class", "activo");
            SpanAviso.InnerText = avisoText;
        }
        protected void ValidacionEmail(object sender, ServerValidateEventArgs args)
        {
            Regex expReg = new Regex(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$");
            args.IsValid = expReg.IsMatch(TxtEmail.Text);
            if (args.IsValid)
            {
                SpanAviso.InnerText = "";
                SpanAviso.Attributes.Add("class", "inactivo");
            }
            else
            {
               Cache["msjError"] += "- Formato de Email invalido, debe ser por ejemplo example@example.com" + "\r\n";
            }
        }
        protected void ValidacionSkypeTel(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = ( TxtCuentaSkype.Text != "" || TxtTelFijo.Text != "" || TxtTelCel.Text != "");
            if (args.IsValid)
            {
                SpanAviso.InnerText = "";
                SpanAviso.Attributes.Add("class", "inactivo");
            }
            else
            {
                Cache["msjError"] += "- Por lo menos uno de los campos para poder contactarse debe estar rellenado. (Cuenta Skype, Tel. Fijo y Tel. Celular)" + "\r\n";
            }
        }
    }
}