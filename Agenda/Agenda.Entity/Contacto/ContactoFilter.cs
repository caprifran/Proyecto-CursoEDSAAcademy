using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Contacto
{
    public class ContactoFilter
    {
        public string ApellidoNombre { get; set; }
        public string Pais { get; set; }
        public string Localidad { get; set; }
        public string FechaIngresoD { get; set; }
        public string FechaIngresoH { get; set; }
        public int ContactoInterno { get; set; }
        public string Organizacion { get; set; }
        public string Area { get; set; }
        public int Activo { get; set; }

    }
}
