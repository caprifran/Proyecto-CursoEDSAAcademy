using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Entity.Contacto;
using Agenda.BLL;
namespace Agenda
{
    public partial class AgendaIndex : System.Web.UI.Page
    {
        private Business business;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) Response.Write("Es postBack");
            else
            {
                TxtFechaIngresoD.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                TxtFechaIngresoH.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void FechaIngresoD_Text_Changed(object sender, EventArgs e)
        {
            FechaIngresoValidator.Validate();
        }
        protected void FechaIngresoH_Text_Changed(object sender, EventArgs e)
        {
            FechaIngresoValidator.Validate();
        }

        protected void ValidacionFecha(object sender, ServerValidateEventArgs args)
        {
            try
            {
                DateTime fechaIngresoD = DateTime.Parse(TxtFechaIngresoD.Text);
                DateTime fechaIngresoH = DateTime.Parse(TxtFechaIngresoH.Text);

                args.IsValid = fechaIngresoD < fechaIngresoH;
                if (args.IsValid)
                {
                    SpanAviso.InnerText = "";
                    SpanAviso.Attributes.Add("class", "inactivo");
                }
                else
                {
                    SpanAviso.InnerText = "Fecha de ingreso invalida. Fecha ingreso desde debe ser menor a fecha ingreso hasta.";
                    SpanAviso.Attributes.Add("class", "activo");
                }
                
            }
            catch (Exception ex)
            {
                SpanAviso.InnerText = "Formato de fecha de ingreso invalido. Debe tener el siguiente formato Ej: 23/07/1996";
                SpanAviso.Attributes.Add("class","activo");
                args.IsValid = false;

            }
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("Es valido");
                List<Contacto> contactos;
                List<Contacto> gridData = new List<Contacto>();
                ContactoFilter filtros = new ContactoFilter();
                this.business = new Business((List<Contacto>)Application["contactosEjemplo"]);

                filtros.ApellidoNombre = TxtApellidoNombre.Text;
                filtros.Pais = DDPais.SelectedValue;
                filtros.Localidad = TxtLocalidad.Text;
                filtros.FechaIngresoD = TxtFechaIngresoD.Text;
                filtros.FechaIngresoH = TxtFechaIngresoH.Text;
                filtros.ContactoInterno = DDContactoInt.SelectedValue;
                filtros.Organizacion = TxtOrganizacion.Text;
                filtros.Area = DDArea.SelectedValue;
                filtros.Activo = DDActivo.SelectedValue;

                contactos = business.GetContactosByFilter(filtros);

                //contenedorResultado.Controls.Clear();
                //contenedorBtnsPaginas.Controls.Clear();

                if (gridData.Count() < 5)
                {
                    gridData.AddRange(contactos.GetRange(0, contactos.Count() - 1));
                } 
                else
                {
                    gridData.AddRange(contactos.GetRange(0, 5));
                }
                
                GridViewConsulta.DataSource = gridData;
                GridViewConsulta.DataBind();
            }
            else
            {
                Response.Write("Es invalido");
            }
        }
        protected void GridViewConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}