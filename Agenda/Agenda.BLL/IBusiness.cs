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
        Contacto GetContactoByID(int Id);
        List<Contacto> GetContactosByFilter(ContactoFilter contactoFilter);
        List<Contacto> GetContactosByFilterSql(ContactoFilter contactoFilter);
        Contacto getContactoByIdSQL(int Id);
        int getCantPaginas(int cantContacos);
        void DeleteContacto(Contacto contacto);
        void CambiarEstadoContacto(Contacto contacto);
        void AgregarContacto(Contacto contacto);
        void EditarContacto(Contacto contacto);
        void AbrirConexion();
    }
}
