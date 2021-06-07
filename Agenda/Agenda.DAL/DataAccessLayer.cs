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

        public SqlDataReader GetContactosByFilter(SqlConnection connection, ContactoFilter filter)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ConsultaContactosFiltrados"
            };

            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@ApellidoNombre", Value = DBNull.Value  /*filter.ApellidoNombre*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Pais", Value = DBNull.Value /*filter.Pais*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Localidad", Value = DBNull.Value /*filter.Localidad*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@FechaIngD", Value = DBNull.Value /*filter.FechaIngresoH*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@FechaIngH", Value = DBNull.Value /*filter.FechaIngresoH*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@ContactoInterno", Value = DBNull.Value /*filter.ContactoInterno*/, SqlDbType = SqlDbType.Bit },
                new SqlParameter() { ParameterName = "@Organizacion", Value = DBNull.Value /*filter.Organizacion*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Area", Value = DBNull.Value /*filter.Area*/, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Activo", Value = DBNull.Value /*filter.Activo*/, SqlDbType = SqlDbType.Bit }                
            });

            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }

        public SqlDataReader GetContactoById(SqlConnection connection, int Id)
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

            SqlDataReader result = cmd.ExecuteReader();

            return result;
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
