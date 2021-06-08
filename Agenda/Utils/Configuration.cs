using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Utils
{
    public class Configuration
    {
        
        public static string GetConnectionString(string Server, string DBName)
        {
            //string Server = ConfigurationManager.AppSettings["Server"].ToString();  //"localhost\\SQLEXPRESS";
            //string DBName = ConfigurationManager.AppSettings["DBName"].ToString();  //"Agenda";

            return string.Concat(
                "Data Source=",
                Server,
                ";",
                "Initial Catalog=",
                DBName,
                ";Integrated Security=true;"
                );
        }
    }
}
