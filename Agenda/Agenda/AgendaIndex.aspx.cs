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
            if (IsPostBack) {
                List<Button> btnsPaginas = new List<Button>();

                Button btnAnterior = new Button();
                btnAnterior.Text = "Anterior";
                btnAnterior.CssClass = "btnAnterior";
                btnAnterior.Click += new EventHandler(this.BtnAnterior_Click);
                contenedorBtnsPaginas.Controls.Add(btnAnterior);

                for (var i = 1; i <= (int)Application["cantPaginas"]; i++)
                {
                    btnsPaginas.Add(new Button());
                    btnsPaginas.Last().Text = i.ToString();
                    if (i == (int)Application["nroPagina"])
                        btnsPaginas.Last().CssClass = "nroPagina activo";
                    else
                        btnsPaginas.Last().CssClass = "nroPagina";
                    btnsPaginas.Last().Click += new EventHandler(this.BtnNroPagina_Click);

                    contenedorBtnsPaginas.Controls.Add(btnsPaginas.Last());
                }

                Button btnSiguiente = new Button();
                btnSiguiente.Text = "Siguiente";
                btnSiguiente.CssClass = "btnSiguiente";
                btnSiguiente.Click += new EventHandler(this.BtnSiguiente_Click);
                contenedorBtnsPaginas.Controls.Add(btnSiguiente);

            }
            else
            {
                TxtFechaIngresoD.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                TxtFechaIngresoH.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewConsulta.DataSource = null;
                GridViewConsulta.DataBind();

                if ((List<Contacto>)Session["contactosEjemploFiltrados"] != null)
                {
                    List<Contacto> contactos = (List<Contacto>)Session["contactosEjemploFiltrados"];
                    List<Contacto> gridData = new List<Contacto>();
                    int indexPContacto = ((int)Application["nroPagina"] - 1) * 5;
                    int indexLContacto = contactos.Count() - indexPContacto;
                    if (contactos.Count() != 0)
                    {
                        if (contactos.GetRange(indexPContacto, indexLContacto).Count() < 5)
                        {
                            gridData.AddRange(contactos.GetRange(indexPContacto, indexLContacto));
                        }
                        else
                        {
                            gridData.AddRange(contactos.GetRange(indexPContacto, 5));
                        }

                        GridViewConsulta.DataSource = gridData;
                        GridViewConsulta.DataBind();

                        foreach (GridViewRow row in GridViewConsulta.Rows)
                        {
                            ImageButton activarDesactivarContacto = ((ImageButton)row.FindControl("ActivarDesactivarContacto"));//Cells[18].Controls[0]);

                            if (row.Cells[9].Text == "True")
                                activarDesactivarContacto.ImageUrl = "Images/anular.png";
                            else
                                activarDesactivarContacto.ImageUrl = "images/play_pause.png";
                        }
                    }
                    else
                    {
                        ImprimirAviso("No se han encontrado Contactos, intentelo de nuevo.");
                    }
                }                
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
                List<Contacto> contactos;
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
                Session["contactosEjemploFiltrados"] = contactos;
                Application["cantPaginas"] = (int)Decimal.ToInt32(Math.Ceiling((decimal)contactos.Count / 5));
                Application["nroPagina"] = 1;
            }
            else
            {
                // Es invalido
            }
        }

        protected void BtnAnterior_Click(object sender, EventArgs e)
        {
            if ((int)Application["nroPagina"] > 1)
                Application["nroPagina"] = (int)Application["nroPagina"] - 1;
        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if ((int)Application["nroPagina"] < (int)Application["cantPaginas"])
                Application["nroPagina"] = (int)Application["nroPagina"] + 1;
        }
        protected void BtnNroPagina_Click(object sender, EventArgs e)
        {
            Application["nroPagina"] = Int32.Parse(((Button)sender).Text);
        }
        private void ImprimirAviso(String avisoText)
        {
            SpanAviso.Attributes.Add("class", "activo");
            SpanAviso.InnerText = avisoText;
        }
        protected void DeleteContacto_Click(object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;
            Contacto contactoDelete = ((List<Contacto>)Application["contactosEjemplo"]).Find(contacto => contacto.Id == Int32.Parse(row.Cells[0].Text));
            ((List<Contacto>)Application["contactosEjemplo"]).Remove(contactoDelete);

            Response.Redirect("AgendaIndex.aspx");
        }
        protected void ActivarDesactivarContacto_Click(object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;
            bool contactoEstado = ((List<Contacto>)Application["contactosEjemplo"]).Find(contacto => contacto.Id == Int32.Parse(row.Cells[0].Text)).Activo;
            ((List<Contacto>)Application["contactosEjemplo"]).Find(contacto => contacto.Id == Int32.Parse(row.Cells[0].Text)).Activo = !contactoEstado;

            Response.Redirect("AgendaIndex.aspx");
        }
        protected void ZoomContacto_Click(object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;

            Contacto contactoZoom = new Contacto
            {
                Id = Int32.Parse(row.Cells[0].Text),
                ApellidoNombre = row.Cells[1].Text,
                Genero = row.Cells[2].Text,
                Pais = row.Cells[3].Text,
                Localidad = row.Cells[4].Text,
                ContactoInterno = Boolean.Parse(row.Cells[5].Text),
                Organizacion = row.Cells[6].Text,
                Area = row.Cells[7].Text,
                FechaIngreso = DateTime.Parse(row.Cells[8].Text),
                Activo = Boolean.Parse(row.Cells[9].Text),
                Direccion = row.Cells[10].Text,
                TelFijo = row.Cells[11].Text,
                TelCel = row.Cells[12].Text,
                Email = row.Cells[13].Text,
                Skype = row.Cells[14].Text
            };

            Cache["contacto"] = contactoZoom;
            Cache["Accion"] = "Zoom";
            Response.Redirect("ABMC.aspx");
        }
        protected void EditContacto_Click(object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;

            Contacto contactoEdit = new Contacto
            {
                Id = Int32.Parse(row.Cells[0].Text),
                ApellidoNombre = row.Cells[1].Text,
                Genero = row.Cells[2].Text,
                Pais = row.Cells[3].Text,
                Localidad = row.Cells[4].Text,
                ContactoInterno = Boolean.Parse(row.Cells[5].Text),
                Organizacion = row.Cells[6].Text,
                Area = row.Cells[7].Text,
                FechaIngreso = DateTime.Parse(row.Cells[8].Text),
                Activo = Boolean.Parse(row.Cells[9].Text),
                Direccion = row.Cells[10].Text,
                TelFijo = row.Cells[11].Text,
                TelCel = row.Cells[12].Text,
                Email = row.Cells[13].Text,
                Skype = row.Cells[14].Text
            };

            Cache["contacto"] = contactoEdit;
            Cache["Accion"] = "Edit";
            Response.Redirect("ABMC.aspx");
        }

        protected void BtnNuevoContacto_Click(object sender, EventArgs e)
        {
            Cache["Accion"] = "NuevoContacto";
            Response.Redirect("ABMC.aspx");
        }
    }
}