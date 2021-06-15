using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Entity.Contacto;
using System.Text.RegularExpressions;
using Agenda.BLL;
using System.Configuration;
using System.ServiceModel;
using Agenda.WSCUILGenerator;

namespace Agenda
{
    public partial class ABMC : System.Web.UI.Page
    {
        string ServerUrl = ConfigurationManager.AppSettings["Server"].ToString();
        string DBName = ConfigurationManager.AppSettings["DBName"].ToString();
        private WSAreasChild WSAreasContactos = new WSAreasChild();
        Contacto contacto;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (Session["Accion"] != null || Session["contacto"] != null)
            {
                switch (Session["Accion"])
                {
                    case "Zoom":
                        // Defino el titulo de la pagina, desactivo todo los campos, escondo el boton de submit e inicializo contacto con los valores recibidos por de AgendaIndex
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
                        this.contacto = (Contacto)Session["contacto"];
                        break;

                    case "Edit":
                        // Defino el titulo e inicializo contacto con los valores recibidos por de AgendaIndex
                        tituloAccion.InnerText = "Editar Contacto";
                        this.contacto = (Contacto)Session["contacto"];
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
            if (DDPais.Items.Count == 1 && DDArea.Items.Count == 1)
            {

                using (Business business = new Business(this.ServerUrl, this.DBName))
                {
                    List<string> paises = business.getPaisesSQL();
                    List<string> areas = WSAreasContactos.getAreas().ToList();
                    foreach (string area in areas)
                    {
                        DDArea.Items.Add(new ListItem { Text = area });
                    }
                    foreach (string pais in paises)
                    {
                        DDPais.Items.Add(new ListItem { Text = pais });
                    }
                }
            }

            if (Session["contacto"] != null)
            {
                // Relleno lo campos si les corresponde 
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

                // Al no haber un evento onchange sino una carga de la pagina con los datos del contacto
                // Obtengo el CUIL al momento de llenar los campos
                GenerarCUIL(null, null); 
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
                    Session["msjError"] += "- Datos invalidos, revise de haber completado los campos obligatorios, señalizados con el simbolo *" + "\r\n"; 

            if (IsValid)
            {
                this.contacto.ApellidoNombre = TxtApellidoNombre.Text;
                this.contacto.Genero = DDGenero.SelectedValue;
                this.contacto.Pais = DDPais.SelectedValue;
                this.contacto.Localidad = TxtLocalidad.Text;
                if (DDContactoInt.SelectedValue == "SI") this.contacto.ContactoInterno = true;
                else this.contacto.ContactoInterno = false;
                this.contacto.Organizacion = TxtOrganizacion.Text;
                this.contacto.Area = DDArea.SelectedValue;
                if (DDActivo.SelectedValue == "SI") this.contacto.Activo = true;
                else this.contacto.Activo = false;
                this.contacto.Direccion = TxtDireccion.Text;
                this.contacto.TelFijo = TxtTelFijo.Text;
                this.contacto.TelCel = TxtTelCel.Text;
                this.contacto.Email = TxtEmail.Text;
                this.contacto.Skype = TxtCuentaSkype.Text;
                
                switch (Session["Accion"])
                {
                    case "Edit":
                        using (Business business = new Business(this.ServerUrl, this.DBName))
                        {
                            business.EditContactoSQL(this.contacto);
                        }
                        break;

                    case "NuevoContacto":
                        contacto.Id = 0;
                        contacto.FechaIngreso = DateTime.Now;
                        using (Business business = new Business(this.ServerUrl, this.DBName))
                        {
                            business.AgregarContactoSQL(this.contacto);
                        };
                        break;
                }

                Response.Redirect("AgendaIndex.aspx");
            }
            else
            {
                ImprimirAviso((string)Session["msjError"]);
                Session["msjError"] = "";
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
               Session["msjError"] += "- Formato de Email invalido, debe ser por ejemplo example@example.com" + "\r\n";
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
                Session["msjError"] += "- Por lo menos uno de los campos para poder contactarse debe estar rellenado. (Cuenta Skype, Tel. Fijo y Tel. Celular)" + "\r\n";
            }
        }
        protected void GenerarCUIL(object sender, EventArgs args)
        {
            if (!String.IsNullOrEmpty(TxtApellidoNombre.Text) && !String.IsNullOrEmpty(DDGenero.SelectedValue))
            {
                string apellido = TxtApellidoNombre.Text.Split(' ')[0];
                string nombre = TxtApellidoNombre.Text.Split(' ')[1];
                string genero = DDGenero.SelectedValue;

                // Redefino los atributos para que al actualizar la pantalla contenga estos nuevos valores
                this.contacto.ApellidoNombre = apellido + " " + nombre;
                this.contacto.Genero = genero;

                WSCUILGenerator.CUILGeneratorSVCClient CUIL = new WSCUILGenerator.CUILGeneratorSVCClient();
                try
                {
                    TxtCUIL.Text = CUIL.GetCUIL(nombre, apellido, genero/*, 1, 0*/);
                }
                catch(FaultException<ExceptionFaultContract> ex)
                {
                }
            }
        }
    }
}