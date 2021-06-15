using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSCUIL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICUILGeneratorSVC" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICUILGeneratorSVC
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        [FaultContract(typeof(ExceptionFaultContract))]
        string GetCUIL(string nombre, string apellido, string genero/*, int numA, int numB*/);
         
    }
}
