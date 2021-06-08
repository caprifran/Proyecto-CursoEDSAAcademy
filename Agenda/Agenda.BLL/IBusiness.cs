using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity.Contacto;
using Agenda.Entity;
namespace Agenda.BLL
{
    public interface IBusiness
    {
        List<Contacto> GetContactosByFilterSQL(ContactoFilter contactoFilter);
        Contacto getContactoByIdSQL(int Id);
        void DeleteContactoByIdSQL(int Id);
        void CambiarEstadoContactoByIdSQL(int Id);
        void AgregarContactoSQL(Contacto contacto);
        void EditContactoSQL(Contacto contacto);
        void AbrirConexion();
    }
}
