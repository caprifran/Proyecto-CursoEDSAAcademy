using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Agenda.Entity.Contacto;
using Utils;
namespace Agenda.DAL
{
    public class DataAccessLayer : IDisposable
    {
        public SqlConnection connection;

        public DataAccessLayer()
        {
            connection = new SqlConnection
            {
                ConnectionString = Configuration.GetConnectionString()
            };
        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                connection.Open();

                return connection;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataSet GetContactosByFilter(SqlConnection connection, ContactoFilter filter)
        {
            Object ApellidoNombre,
                   Pais,
                   Localidad,
                   FechaIngresoD,
                   FechaIngresoH,
                   ContactoInterno,
                   Organizacion,
                   Area,
                   Activo;

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ConsultaContactosFiltrados"
            };
            if (string.IsNullOrEmpty(filter.ApellidoNombre)) ApellidoNombre = DBNull.Value; else ApellidoNombre = filter.ApellidoNombre;
            if (filter.Pais == "TODOS") Pais = DBNull.Value; else Pais = filter.Pais;
            if (string.IsNullOrEmpty(filter.Localidad)) Localidad = DBNull.Value; else Localidad = filter.Localidad;
            if (string.IsNullOrEmpty(filter.FechaIngresoD)) FechaIngresoD = DBNull.Value; else FechaIngresoD = filter.FechaIngresoD;
            if (string.IsNullOrEmpty(filter.FechaIngresoH)) FechaIngresoH = DBNull.Value; else FechaIngresoH = filter.FechaIngresoH;
            if (filter.ContactoInterno == "TODOS") ContactoInterno = DBNull.Value; else ContactoInterno = filter.ContactoInterno;
            if (string.IsNullOrEmpty(filter.Organizacion)) Organizacion = DBNull.Value; else Organizacion = filter.Organizacion;
            if (filter.Area == "TODOS") Area = DBNull.Value; else Area = filter.Area;
            if (filter.Activo == "TODOS") Activo = DBNull.Value; else Activo = filter.Activo;

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@ApellidoNombre", Value = ApellidoNombre, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Pais", Value = Pais, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Localidad", Value =Localidad, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@FechaIngD", Value = FechaIngresoD, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@FechaIngH", Value = FechaIngresoH, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@ContactoInterno", Value = ContactoInterno, SqlDbType = SqlDbType.Bit },
                new SqlParameter() { ParameterName = "@Organizacion", Value = Organizacion, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Area", Value = Area, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Activo", Value = Activo, SqlDbType = SqlDbType.Bit }
            });

            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };

            DataSet contactosFiltradosSQL = new DataSet();
            
            adapter.Fill(contactosFiltradosSQL);

            return contactosFiltradosSQL;
        }

        public DataSet GetContactoById(SqlConnection connection, int Id)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ConsultaContactoPorId"
            };
            
            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Id", Value = Id, SqlDbType = SqlDbType.Int },                
            });

            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };

            DataSet contactoById = new DataSet();

            adapter.Fill(contactoById);

            return contactoById;
        }
        public void CambiarEstadoContactoById(SqlConnection connection, int Id)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CambiarEstadoContactoPorId"
            };

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Id", Value = Id, SqlDbType = SqlDbType.Int },
            });

            cmd.ExecuteNonQuery();
        }
        public void DeleteContactoById(SqlConnection connection, int Id)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "EliminarContactoPorId"
            };

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Id", Value = Id, SqlDbType = SqlDbType.Int },
            });

            cmd.ExecuteNonQuery();
        }
        public void EditContacto(SqlConnection connection, Contacto contacto)
        {
            string[] ApellidoNombres = contacto.ApellidoNombre.Split(' ');
            string apellido = ApellidoNombres[0];
            string nombres = "";
            for (int i = 1; i < ApellidoNombres.Length; i++) nombres += ApellidoNombres[i];

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "EditarContacto"
            };

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Id", Value = contacto.Id, SqlDbType = SqlDbType.Int},
                new SqlParameter() { ParameterName = "@Nombre", Value = nombres, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Apellido", Value = apellido, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Genero", Value = contacto.Genero, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Pais", Value = contacto.Pais, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Localidad", Value = contacto.Localidad, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@ContactoInterno", Value = contacto.ContactoInterno, SqlDbType = SqlDbType.Bit},
                new SqlParameter() { ParameterName = "@Organizacion", Value = contacto.Organizacion, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Area", Value = contacto.Area, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Activo", Value = contacto.Activo, SqlDbType = SqlDbType.Bit},
                new SqlParameter() { ParameterName = "@Direccion", Value = contacto.Direccion, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@TelFijo", Value = contacto.TelFijo, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@TelCel", Value = contacto.TelCel, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Email", Value = contacto.Email, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Skype", Value = contacto.Skype, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@FechaIngreso", Value = contacto.FechaIngreso.ToString("dd/MM/yyyy"), SqlDbType = SqlDbType.VarChar}
            });

            cmd.ExecuteNonQuery();
        }
        public void AgregarContacto(SqlConnection connection, Contacto contacto)
        {
            string[] ApellidoNombres = contacto.ApellidoNombre.Split(' ');
            string apellido = ApellidoNombres[0];
            string nombres = "";
            for (int i = 1; i < ApellidoNombres.Length; i++) nombres += ApellidoNombres[i];

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AgregarContacto"
            };

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@Nombre", Value = nombres, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Apellido", Value = apellido, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Genero", Value = contacto.Genero, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Pais", Value = contacto.Pais, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Localidad", Value = contacto.Localidad, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@ContactoInterno", Value = contacto.ContactoInterno, SqlDbType = SqlDbType.Bit},
                new SqlParameter() { ParameterName = "@Organizacion", Value = contacto.Organizacion, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Area", Value = contacto.Area, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Activo", Value = contacto.Activo, SqlDbType = SqlDbType.Bit},
                new SqlParameter() { ParameterName = "@Direccion", Value = contacto.Direccion, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@TelFijo", Value = contacto.TelFijo, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@TelCel", Value = contacto.TelCel, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Email", Value = contacto.Email, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Skype", Value = contacto.Skype, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@FechaIngreso", Value = contacto.FechaIngreso.ToString("dd/MM/yyyy"), SqlDbType = SqlDbType.VarChar}
            });

            cmd.ExecuteNonQuery();
        }
        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
