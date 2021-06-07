using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity.Contacto;
using Agenda.DAL;
using System.Data.SqlClient;

namespace Agenda.BLL
{
    public class Business: IBusiness, IDisposable
    {
        public List<Contacto> contactos { get; set; }

        /*public Business(List<Contacto> contactos)
        {
            this.contactos = contactos;
        }*/

        public Contacto GetContactoByID(int Id)
        {
            return this.contactos.Single(p => p.Id.Equals(Id));
        }

        public List<Contacto> GetContactosByFilter(ContactoFilter contactoFilter)
        {
            List<Contacto> contactosFiltrados = this.contactos;
            if (!string.IsNullOrEmpty(contactoFilter.ApellidoNombre))        
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ApellidoNombre.ToUpper().Contains(contactoFilter.ApellidoNombre.ToUpper())).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (contactoFilter.Pais != "TODOS")
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Pais.Contains(contactoFilter.Pais)).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (!string.IsNullOrEmpty(contactoFilter.Localidad))
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Localidad.Contains(contactoFilter.Localidad)).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (!(string.IsNullOrEmpty(contactoFilter.FechaIngresoD) && string.IsNullOrEmpty(contactoFilter.FechaIngresoH)))
            {
                DateTime ingresoD = Convert.ToDateTime(contactoFilter.FechaIngresoD);
                DateTime ingresoH = Convert.ToDateTime(contactoFilter.FechaIngresoH);

                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.FechaIngreso >= ingresoD && contacto.FechaIngreso <= ingresoH).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }

            /*if(contactoFilter.ContactoInterno != "TODOS")
            {
                if (contactoFilter.ContactoInterno == "SI") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ContactoInterno == true).OrderBy(contacto => contacto.ApellidoNombre).ToList();
                if (contactoFilter.ContactoInterno == "NO") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ContactoInterno == false).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }*/

            if (!string.IsNullOrEmpty(contactoFilter.Organizacion))
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Organizacion.Contains(contactoFilter.Organizacion)).OrderBy(contacto => contacto.ApellidoNombre).ToList();

            if (contactoFilter.Area != "TODOS")
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Area.Contains(contactoFilter.Area)).OrderBy(contacto => contacto.ApellidoNombre).ToList();

/*            if (contactoFilter.Activo != "TODOS")
            {
                if (contactoFilter.Activo == "SI") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Activo == true).OrderBy(contacto => contacto.ApellidoNombre).ToList();
                if (contactoFilter.Activo == "NO") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Activo == false).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }
*/
            return contactosFiltrados;
        }

        public int getCantPaginas(int cantContactos)
        {
            return (int)Decimal.ToInt32(Math.Ceiling((decimal)this.contactos.Count / cantContactos));
        }
        public void DeleteContacto(Contacto contacto)
        {
            this.contactos.Remove(contacto);
        }
        public void CambiarEstadoContacto(Contacto contacto)
        {
            bool estadoContacto = contacto.Activo;
            this.contactos.Find(c => c.Id == contacto.Id).Activo = !estadoContacto;
        }
        public void AgregarContacto(Contacto contacto)
        {
            this.contactos.Add(contacto);
        }
        public void EditarContacto(Contacto contacto)
        {
            Contacto contactoBLL = this.contactos.Find(c => c.Id == contacto.Id);

            contactoBLL.ApellidoNombre = contacto.ApellidoNombre;
            contactoBLL.Genero = contacto.Genero;
            contactoBLL.Pais = contacto.Pais;
            contactoBLL.Localidad = contacto.Localidad;
            contactoBLL.ContactoInterno = contacto.ContactoInterno;
            contactoBLL.Organizacion = contacto.Organizacion;
            contactoBLL.Area = contacto.Area;
            contactoBLL.Activo = contacto.Activo;
            contactoBLL.Direccion = contacto.Direccion;
            contactoBLL.TelFijo = contacto.TelFijo;
            contactoBLL.TelCel = contacto.TelCel;
            contactoBLL.Email = contacto.Email;
            contactoBLL.Skype = contacto.Skype;

        }

        public void AbrirConexion()
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();
                }
            }
            catch (Exception e)
            {

            }
        }
        public List<Contacto> GetContactosByFilterSql(ContactoFilter contactoFilter)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();
                    SqlDataReader contactosSql = dal.GetContactosByFilter(connection, contactoFilter);
                    List<Contacto> result = new List<Contacto>();

                    if (contactosSql.HasRows)
                    {
                        while (contactosSql.Read()) { 
                            result.Add(new Contacto
                            {
                                Id = contactosSql.GetInt32(0),
                                ApellidoNombre = contactosSql.GetString(1),
                                Genero = contactosSql.GetString(2),
                                Pais = contactosSql.GetString(3),
                                Localidad = contactosSql.GetString(4),
                                ContactoInterno = contactosSql.GetString(5) == "1" ? true : false,
                                Organizacion = contactosSql.GetString(6),
                                Area = contactosSql.GetString(7),
                                FechaIngreso = contactosSql.GetDateTime(8),
                                Activo = contactosSql.GetString(9) == "1" ? true : false,
                                Direccion = contactosSql.GetString(10),
                                TelFijo = contactosSql.GetString(11),
                                TelCel = contactosSql.GetString(12),
                                Email = contactosSql.GetString(13),
                                Skype = contactosSql.GetString(14)
                            });
                        };
                    };

                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Contacto getContactoByIdSQL(int Id)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();
                    var reader = dal.GetContactoById(connection, Id);
                    Contacto result = new Contacto();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = new Contacto
                            {
                                Id = reader.GetInt32(0),
                                ApellidoNombre = reader.GetString(1),
                                Genero = reader.GetString(2),
                                Pais = reader.GetString(3),
                                Localidad = reader.GetString(4),
                                ContactoInterno = reader.GetString(5) == "1" ? true : false,
                                Organizacion = reader.GetString(6),
                                Area = reader.GetString(7),
                                FechaIngreso = reader.GetDateTime(8),
                                Activo = reader.GetString(9) == "1" ? true : false,
                                Direccion = reader.GetString(10),
                                TelFijo = reader.GetString(11),
                                TelCel = reader.GetString(12),
                                Email = reader.GetString(13),
                                Skype = reader.GetString(14)
                            };
                        };
                    };
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void Dispose()
        {
        }
    }
}
