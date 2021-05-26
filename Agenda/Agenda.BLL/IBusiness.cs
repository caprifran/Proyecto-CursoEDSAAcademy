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
        Contacto GetContactoByID(Contacto contacto);
        List<Contacto> GetContactosByFilter(ContactoFilter contactoFilter);
        int getCantPaginas(int cantContacos);
    }
}
