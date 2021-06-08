using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity.Contacto;
using Agenda.DAL;
using System.Data;
using System.Data.SqlClient;

namespace Agenda.BLL
{
    public class Business: IBusiness, IDisposable
    {
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
        public List<Contacto> GetContactosByFilterSQL(ContactoFilter contactoFilter)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();
                    
                    DataSet contactosSql = dal.GetContactosByFilter(connection, contactoFilter);
                    List<Contacto> result = new List<Contacto>();             
                    foreach (DataRow row in contactosSql.Tables[0].Rows)
                    {
                        result.Add(new Contacto
                        {
                            Id = (int)row.ItemArray[0],
                            ApellidoNombre = row.ItemArray[1].ToString(),
                            Genero = row.ItemArray[2].ToString(),
                            Pais = row.ItemArray[3].ToString(),
                            Localidad = row.ItemArray[4].ToString(),
                            ContactoInterno = (Boolean)row.ItemArray[5],
                            Organizacion = row.ItemArray[6].ToString(),
                            Area = row.ItemArray[7].ToString(),
                            Activo = (Boolean)row.ItemArray[8],
                            Direccion = row.ItemArray[9].ToString(),
                            TelFijo = row.ItemArray[10].ToString(),
                            TelCel = row.ItemArray[11].ToString(),
                            Email = row.ItemArray[12].ToString(),
                            Skype = row.ItemArray[13].ToString(),
                            FechaIngreso = DateTime.Parse(row.ItemArray[14].ToString())
                        });
                    }
                    

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

                    DataSet contactosSql = dal.GetContactoById(connection, Id);
                    Contacto result = new Contacto
                    {
                        Id = (int)contactosSql.Tables[0].Rows[0].ItemArray[0],
                        ApellidoNombre = contactosSql.Tables[0].Rows[0].ItemArray[1].ToString(),
                        Genero = contactosSql.Tables[0].Rows[0].ItemArray[2].ToString(),
                        Pais = contactosSql.Tables[0].Rows[0].ItemArray[3].ToString(),
                        Localidad = contactosSql.Tables[0].Rows[0].ItemArray[4].ToString(),
                        ContactoInterno = (Boolean)contactosSql.Tables[0].Rows[0].ItemArray[5],
                        Organizacion = contactosSql.Tables[0].Rows[0].ItemArray[6].ToString(),
                        Area = contactosSql.Tables[0].Rows[0].ItemArray[7].ToString(),
                        Activo = (Boolean)contactosSql.Tables[0].Rows[0].ItemArray[8],
                        Direccion = contactosSql.Tables[0].Rows[0].ItemArray[9].ToString(),
                        TelFijo = contactosSql.Tables[0].Rows[0].ItemArray[10].ToString(),
                        TelCel = contactosSql.Tables[0].Rows[0].ItemArray[11].ToString(),
                        Email = contactosSql.Tables[0].Rows[0].ItemArray[12].ToString(),
                        Skype = contactosSql.Tables[0].Rows[0].ItemArray[13].ToString(),
                        FechaIngreso = DateTime.Parse(contactosSql.Tables[0].Rows[0].ItemArray[14].ToString())
                    };

                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void CambiarEstadoContactoByIdSQL(int Id)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();

                    dal.CambiarEstadoContactoById(connection, Id);
                }
            }
            catch (Exception e)
            {
            }
        }
        public void DeleteContactoByIdSQL(int Id)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();

                    dal.DeleteContactoById(connection, Id);
                }
            }
            catch (Exception e)
            {
            }
        }
        public void EditContactoSQL(Contacto contacto)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();

                    dal.EditContacto(connection, contacto);
                }
            }
            catch (Exception e)
            {
            }
        }
        public void AgregarContactoSQL(Contacto contacto)
        {
            try
            {
                using (DataAccessLayer dal = new DataAccessLayer())
                {
                    var connection = dal.AbrirConexion();

                    dal.AgregarContacto(connection, contacto);
                }
            }
            catch (Exception e)
            {
            }
        }
        public void Dispose()
        {
        }
    }
}
