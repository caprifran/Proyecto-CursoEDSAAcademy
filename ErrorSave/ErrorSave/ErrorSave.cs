using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace ErrorSave
{
    public class ErrorSave
    {
        public static void volcarErrores(Exception err)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["PATH"].ToString();
                if (File.Exists(path)) 
                {
                    List<string> listaErrores = new List<string>();
                    StreamReader sr = new StreamReader(path);
                    string newError = DateTime.Now.ToString("dd / MM / yyyy H:mm:ss") + "-> " + err.Message + " *" ;
                    string textoArchivo = sr.ReadToEnd();
                    sr.Close();
                    if(textoArchivo.Length > 0)
                    {
                        listaErrores = textoArchivo.Split('*').ToList();
                        for (int i = 0; i < listaErrores.Count; i++) listaErrores[i] = listaErrores[i].Trim();
                    }
                    
                    listaErrores.Add(newError);

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        foreach (string error in listaErrores)
                        {
                            if (error != "")
                            {
                                sw.WriteLine(error);
                            }
                        }
                        sw.Close();
                    }

                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss") + " -> " + err.Message + " *");
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
