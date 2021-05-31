using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Entity.Contacto;
using Agenda.BLL;
using System.Globalization;
namespace Agenda
{
    public partial class _Default : Page
    {
        private IBusiness business;
        private void Imprimir(List<Contacto> contactos)
        {
            List<Button> btnsPaginas = new List<Button>();
            int indexContacto = 0;

            Table tablaResultados = new Table();

            TableHeaderRow tablaResHeaderRow = new TableHeaderRow();

            TableHeaderCell HCApellidoNombre = new TableHeaderCell();
            TableHeaderCell HCGenero = new TableHeaderCell();
            TableHeaderCell HCPais = new TableHeaderCell();
            TableHeaderCell HCLocalidad = new TableHeaderCell();
            TableHeaderCell HCContactoInterno = new TableHeaderCell();
            TableHeaderCell HCOrganizacion = new TableHeaderCell();
            TableHeaderCell HCArea = new TableHeaderCell();
            TableHeaderCell HCFechaIngreso = new TableHeaderCell();
            TableHeaderCell HCActivo = new TableHeaderCell();
            TableHeaderCell HCDireccion = new TableHeaderCell();
            TableHeaderCell HCTelFijo = new TableHeaderCell();
            TableHeaderCell HCTelCel = new TableHeaderCell();
            TableHeaderCell HCEmail = new TableHeaderCell();
            TableHeaderCell HCCuentaSkype = new TableHeaderCell();
            TableHeaderCell HCAcciones = new TableHeaderCell();

            tablaResultados.ID = "tablaResultados";

            tablaResHeaderRow.ID = "tablaResHeaderRow";
            tablaResHeaderRow.TableSection = TableRowSection.TableHeader;

            HCApellidoNombre.Text = "APELLIDO Y NOMBRE";
            HCGenero.Text = "GÉNERO";
            HCPais.Text = "PAÍS";
            HCLocalidad.Text = "LOCALIDAD";
            HCContactoInterno.Text = "CONTACTO INTERNO";
            HCOrganizacion.Text = "ORGANIZACIÓN";
            HCArea.Text = "ÁREA";
            HCFechaIngreso.Text = "FECHA INGRESO";
            HCActivo.Text = "ACTIVO";
            HCDireccion.Text = "DIRECCIÓN";
            HCTelFijo.Text = "TEL FIJO";
            HCTelCel.Text = "TEL CELULAR";
            HCEmail.Text = "E-MAIL";
            HCCuentaSkype.Text = "CUENTA SKYPE";
            HCAcciones.Text = "ACCIONES";

            tablaResHeaderRow.Cells.Add(HCApellidoNombre);
            tablaResHeaderRow.Cells.Add(HCGenero);
            tablaResHeaderRow.Cells.Add(HCPais);
            tablaResHeaderRow.Cells.Add(HCLocalidad);
            tablaResHeaderRow.Cells.Add(HCContactoInterno);
            tablaResHeaderRow.Cells.Add(HCOrganizacion);
            tablaResHeaderRow.Cells.Add(HCArea);
            tablaResHeaderRow.Cells.Add(HCFechaIngreso);
            tablaResHeaderRow.Cells.Add(HCActivo);
            tablaResHeaderRow.Cells.Add(HCDireccion);
            tablaResHeaderRow.Cells.Add(HCTelFijo);
            tablaResHeaderRow.Cells.Add(HCTelCel);
            tablaResHeaderRow.Cells.Add(HCEmail);
            tablaResHeaderRow.Cells.Add(HCCuentaSkype);
            tablaResHeaderRow.Cells.Add(HCAcciones);

            tablaResultados.Rows.Add(tablaResHeaderRow);

            foreach (Contacto contacto in contactos)
            {
                indexContacto++;

                TableRow rDatos = new TableRow();
                
                TableCell cApellidoNombre = new TableCell();
                TableCell cGenero = new TableCell();
                TableCell cPais = new TableCell();
                TableCell cLocalidad = new TableCell();
                TableCell cContactoInterno = new TableCell();
                TableCell cOrganizacion = new TableCell();
                TableCell cArea = new TableCell();
                TableCell cFecha = new TableCell();
                TableCell cActivo = new TableCell();
                TableCell cDireccion = new TableCell();
                TableCell cTelFijo = new TableCell();
                TableCell cTelCel = new TableCell();
                TableCell cEmail = new TableCell();
                TableCell cSkype = new TableCell();
                TableCell cAcciones = new TableCell();
                
                Button zoom = new Button();
                Button edit = new Button();
                Button eliminar = new Button();
                Button estado = new Button();

                zoom.Attributes.Add("id", "zoom");
                edit.Attributes.Add("id", "edit");
                eliminar.Attributes.Add("id", "eliminar");
                estado.Attributes.Add("id", "estado");

                if (contacto.Activo)
                    estado.CssClass = "activo";
                else estado.CssClass = "inactivo";

                cApellidoNombre.Text = contacto.ApellidoNombre;
                cGenero.Text = contacto.Genero;
                cPais.Text = contacto.Pais;
                cLocalidad.Text = contacto.Localidad;
                if (contacto.ContactoInterno) 
                    cContactoInterno.Text = "SI"; 
                else 
                    cContactoInterno.Text = "NO";
                cOrganizacion.Text = contacto.Organizacion;
                cArea.Text = contacto.Area;
                cFecha.Text = contacto.FechaIngreso.ToString("dd/MM/yyyy");
                if (contacto.Activo)
                    cActivo.Text = "SI";
                else cActivo.Text = "NO";
                cDireccion.Text = contacto.Direccion;
                cTelFijo.Text = contacto.TelFijo;
                cTelCel.Text = contacto.TelCel;
                cEmail.Text = contacto.Email;
                cSkype.Text = contacto.Skype;

                cAcciones.Controls.Add(zoom);
                cAcciones.Controls.Add(edit);
                cAcciones.Controls.Add(eliminar);
                cAcciones.Controls.Add(estado);

                rDatos.Cells.Add(cApellidoNombre);
                rDatos.Cells.Add(cGenero);
                rDatos.Cells.Add(cPais);
                rDatos.Cells.Add(cLocalidad);
                rDatos.Cells.Add(cContactoInterno);
                rDatos.Cells.Add(cOrganizacion);
                rDatos.Cells.Add(cArea);
                rDatos.Cells.Add(cFecha);
                rDatos.Cells.Add(cActivo);
                rDatos.Cells.Add(cDireccion);
                rDatos.Cells.Add(cTelFijo);
                rDatos.Cells.Add(cTelCel);
                rDatos.Cells.Add(cEmail);
                rDatos.Cells.Add(cSkype);
                rDatos.Cells.Add(cAcciones);

                tablaResultados.Rows.Add(rDatos);
            }

            Button btnAnterior = new Button();
            btnAnterior.Text = "Anterior";
            btnAnterior.CssClass = "btnAnterior";
            btnAnterior.Click += new EventHandler(this.BtnAnterior_Click);
            contenedorBtnsPaginas.Controls.Add(btnAnterior);

            contenedorResultado.Controls.Add(tablaResultados);

            for (var i = 1; i <= (int)Application["cantPaginas"]; i++)
            {
                btnsPaginas.Add(new Button());
                btnsPaginas.Last().Text = i.ToString();
                if (i == (int)Application["nroPagina"]) 
                    btnsPaginas.Last().CssClass = "nropagina activo";
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
        private void ImprimirAviso(String avisoText)
        {
            SpanAviso.Attributes.Add("class", "activo");
            SpanAviso.InnerText = "No se han encontrado Contactos, intentelo de nuevo.";
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            TxtFechaIngresoD.Text = DateTime.Now.Date.AddDays(-30).ToString("dd/MM/yyyy");
            TxtFechaIngresoH.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SpanAviso.InnerText = "";
            SpanAviso.Attributes.Add("class","inactivo");

            this.business = new Business((List<Contacto>)Application["contactosEjemplo"]);                        
            int nroPagina = ((int)Application["nroPagina"]) - 1;
            int contactoInicialIndex;
            int lastContacts;

            contactoInicialIndex = ((int)Application["nroPagina"] - 1) * 5;

            if (((List<Contacto>)Application["contactosEjemplo"]).Count <= 4)
            {
                lastContacts = ((List<Contacto>)Application["contactosEjemplo"]).Count;
                Imprimir(((List<Contacto>)Application["contactosEjemplo"]).GetRange(contactoInicialIndex, lastContacts));
            }
            else if (((List<Contacto>)Application["contactosEjemplo"]).Count >= 5)
            {
                try
                {
                    Imprimir(((List<Contacto>)Application["contactosEjemplo"]).GetRange(contactoInicialIndex, 5));
                }
                catch (Exception err)
                {
                    lastContacts = ((List<Contacto>)Application["contactosEjemplo"]).Count - 5;
                    Imprimir(((List<Contacto>)Application["contactosEjemplo"]).GetRange(contactoInicialIndex, lastContacts));
                }
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.business = new Business((List<Contacto>)Application["contactosEjemploAux"]);

            ContactoFilter filtros = new ContactoFilter();
            filtros.ApellidoNombre = TxtApellidoNombre.Text;
            filtros.Pais = DDPais.SelectedValue;
            filtros.Localidad = TxtLocalidad.Text;
            filtros.FechaIngresoD = TxtFechaIngresoD.Text;
            filtros.FechaIngresoH = TxtFechaIngresoH.Text;
            filtros.ContactoInterno = DDContactoInt.SelectedValue;
            filtros.Organizacion = TxtOrganizacion.Text;
            filtros.Area = DDArea.SelectedValue;
            filtros.Activo = DDActivo.SelectedValue;

            contenedorResultado.Controls.Clear();
            contenedorBtnsPaginas.Controls.Clear();
            List<Contacto> contactosFiltrados = this.business.GetContactosByFilter(filtros).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            if (contactosFiltrados.Count >= 1)
            {
                this.business = new Business(contactosFiltrados);
                int cantPaginas = this.business.getCantPaginas(5);
                int lastContacts;
                int contactoInicialIndex;

                Application["nroPagina"] = 1;
                Application["cantPaginas"] = cantPaginas;
                Application["contactosEjemplo"] = contactosFiltrados;

                contactoInicialIndex = ((int)Application["nroPagina"] - 1) * 5;

                if (contactosFiltrados.Count <= 4)
                {
                    lastContacts = contactosFiltrados.Count;
                    Imprimir(contactosFiltrados.GetRange(contactoInicialIndex, lastContacts));
                } else if(contactosFiltrados.Count >= 5)
                {
                    try
                    {
                        Imprimir(contactosFiltrados.GetRange(contactoInicialIndex, 5));
                    }
                    catch (Exception err)
                    {
                        lastContacts = ((List<Contacto>)Application["contactosEjemplo"]).Count - 5;
                        Imprimir(contactosFiltrados.GetRange(contactoInicialIndex, lastContacts));
                    }
                }
            }  else
            {
                ImprimirAviso("No se han encontrado Contactos, intentelo de nuevo.");
            }
        }

        protected void BtnAnterior_Click(object sender, EventArgs e)
        {
            if((int)Application["nroPagina"] > 1)
                Application["nroPagina"] = (int)Application["nroPagina"] - 1;

            Response.Redirect(Request.RawUrl);
        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if ((int)Application["nroPagina"] < (int)Application["cantPaginas"])
                Application["nroPagina"] = (int)Application["nroPagina"] + 1;
            Response.Redirect(Request.RawUrl);
        }
        protected void BtnNroPagina_Click(object sender, EventArgs e)
        {
            Application["nroPagina"] = Int32.Parse(((Button)sender).Text);
            Response.Redirect(Request.RawUrl);
        }
    }
}