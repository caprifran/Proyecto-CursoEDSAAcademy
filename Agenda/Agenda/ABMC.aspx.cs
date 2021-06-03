using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Entity.Contacto;
using System.Text.RegularExpressions;

namespace Agenda
{
    public partial class ABMC : System.Web.UI.Page
    {
        Contacto contacto;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                Contacto contacto = new Contacto();
                if ((string)Cache["Accion"] != "NuevoContacto")
                {
                    contacto = ((List<Contacto>)Application["contactosEjemplo"]).Find(c => c.Id == this.contacto.Id);
                    ((List<Contacto>)Application["contactosEjemplo"]).Remove(contacto);
                }
                else
                {
                    contacto.Id = ((List<Contacto>)Application["contactosEjemplo"]).Count;
                    contacto.FechaIngreso = DateTime.Now;
                }

                contacto.ApellidoNombre = TxtApellidoNombre.Text;
                contacto.Genero = DDGenero.SelectedValue;
                contacto.Pais = DDPais.SelectedValue;
                contacto.Localidad = TxtLocalidad.Text;
                if (DDContactoInt.SelectedValue == "Si") contacto.ContactoInterno = true;
                else contacto.ContactoInterno = false;
                contacto.Organizacion = TxtOrganizacion.Text;
                contacto.Area = DDArea.SelectedValue;
                if (DDActivo.SelectedValue == "Si") contacto.Activo = true;
                else contacto.Activo = false;
                contacto.Direccion = TxtDireccion.Text;
                contacto.TelFijo = TxtTelFijo.Text;
                contacto.TelCel = TxtTelCel.Text;
                contacto.Email = TxtEmail.Text;
                contacto.Skype = TxtCuentaSkype.Text;

                ((List<Contacto>)Application["contactosEjemplo"]).Add(contacto);

                List<Contacto> asdasd = ((List<Contacto>)Application["contactosEjemplo"]);

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