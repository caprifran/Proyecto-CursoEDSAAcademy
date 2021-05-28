using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLoginEntity;
using System.Configuration;
using System.IO;
namespace AppLoginBLL
{
    public class Business
    {
        public List<Usuario> users { get; }
        public Business()
        {
            string path = ConfigurationManager.AppSettings["PATH"].ToString();
            try
            {
                if (File.Exists(path))
                {
                    List<Usuario> listaCredenciales = new List<Usuario>();
                    StreamReader sr = new StreamReader(path);
                    List<string> credenciales = sr.ReadToEnd().Split(' ').ToList();
                    sr.Close();

                    for(int i = 0; i < credenciales.Count(); i += 2)
                    {
                        if(credenciales[i] != "")
                        {
                            listaCredenciales.Add(
                                new Usuario{
                                    username = credenciales[i] ,
                                    password = credenciales[i + 1]
                                });
                        }
                    }

                    this.users = listaCredenciales;
                }
                else
                {
                    File.Create(path);
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            

            
        }
        public bool VerificarCredenciales(string username, string password)
        {
            Usuario user = new Usuario
            {
                username = username,
                password = password
            };
            foreach (Usuario usuario in users)
            {
                if (usuario.username == user.username && usuario.password == user.password) return true;
            }

            return false;
        }
    }
}
