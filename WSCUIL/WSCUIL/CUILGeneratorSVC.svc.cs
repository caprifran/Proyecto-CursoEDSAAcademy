using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSCUIL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CUILGeneratorSVC" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CUILGeneratorSVC.svc o CUILGeneratorSVC.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CUILGeneratorSVC : ICUILGeneratorSVC
    {
        public void DoWork()
        {
        }
        public string GetCUIL(string nombre, string apellido, string genero/*, int numA, int numB*/)
        {
            try
            {
                //int a = numA / numB;

                string tipo;
                string DNI = "12345678";
                string serieNum = "5432765432";
                string identificador = "";
                string resultado;

                int indexSerieNum = 0;
                int auxIdentificador = 0;

                if (genero == "Masculino")
                {
                    tipo = "20";

                    for (int i = 0; i < tipo.Length; i++) auxIdentificador += Int32.Parse(tipo.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));
                    for (int i = 0; i < DNI.Length; i++) auxIdentificador += Int32.Parse(DNI.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));
                    indexSerieNum = 0;

                    auxIdentificador = auxIdentificador % 11;
                    auxIdentificador = 11 - auxIdentificador;


                    if (auxIdentificador >= 0 && auxIdentificador < 9)
                    {
                        identificador = auxIdentificador.ToString();
                    }
                    auxIdentificador = 0;
                }
                else
                {
                    tipo = "27";

                    for (int i = 0; i < tipo.Length; i++) auxIdentificador += Int32.Parse(tipo.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));
                    for (int i = 0; i < DNI.Length; i++) auxIdentificador += Int32.Parse(DNI.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));
                    indexSerieNum = 0;

                    auxIdentificador = auxIdentificador % 11;
                    auxIdentificador = 11 - auxIdentificador;

                    if (auxIdentificador >= 0 && auxIdentificador < 9)
                    {
                        identificador = auxIdentificador.ToString();
                    }
                    auxIdentificador = 0;
                }

                if (String.IsNullOrEmpty(identificador))
                {
                    tipo = "23";

                    for (int i = 0; i < tipo.Length; i++) auxIdentificador += Int32.Parse(tipo.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));
                    for (int i = 0; i < DNI.Length; i++) auxIdentificador += Int32.Parse(DNI.Substring(i, 1)) * Int32.Parse(serieNum.Substring(indexSerieNum++, 1));

                    auxIdentificador = auxIdentificador % 11;
                    auxIdentificador = 11 - auxIdentificador;

                    if (auxIdentificador >= 0 && auxIdentificador < 9)
                    {
                        identificador = auxIdentificador.ToString();
                    }
                }
                resultado = tipo + '-' + DNI + '-' + identificador;
                return resultado;
            }
            catch
            {
                ExceptionFaultContract faultContract = new ExceptionFaultContract();
                faultContract.StatusCode = "Error en solicitud";
                faultContract.Message = "No se ha podido calcular el CUIL";
                faultContract.Description = "Ha ocurrido un error en el servicio";

                throw new FaultException<ExceptionFaultContract>(faultContract);
            }

        }
    }
}
