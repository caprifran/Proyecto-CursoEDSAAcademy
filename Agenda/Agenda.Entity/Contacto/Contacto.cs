using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Contacto
{
    public class Contacto
    {
        public int Id { get; set; }
        public string ApellidoNombre { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Localidad { get; set; }
        public Boolean ContactoInterno { get; set; }
        public string Organizacion { get; set; }
        public string Area { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Boolean Activo { get; set; }
        public string Direccion { get; set; }
        public string TelFijo { get; set; }
        public string TelCel { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    }
}
