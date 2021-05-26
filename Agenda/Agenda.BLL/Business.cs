using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity.Contacto;
namespace Agenda.BLL
{
    public class Business: IBusiness
    {
        private List<Contacto> contactos;

        public Business(List<Contacto> contactos)
        {
            this.contactos = contactos;
        }

        public Contacto GetContactoByID(Contacto contacto)
        {
            return this.contactos.Single(p => p.Id.Equals(contacto.Id));
        }

        public List<Contacto> GetContactosByFilter(ContactoFilter contactoFilter)
        {
            List<Contacto> contactosFiltrados = this.contactos;
            if (!string.IsNullOrEmpty(contactoFilter.ApellidoNombre))        
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ApellidoNombre.ToUpper().Contains(contactoFilter.ApellidoNombre.ToUpper())).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (contactoFilter.Pais != "TODOS")
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Pais.Contains(contactoFilter.Pais)).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (!string.IsNullOrEmpty(contactoFilter.Localidad))
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Localidad.Contains(contactoFilter.Localidad)).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            
            if (!(string.IsNullOrEmpty(contactoFilter.FechaIngresoD) && string.IsNullOrEmpty(contactoFilter.FechaIngresoH)))
            {
                DateTime ingresoD = Convert.ToDateTime(contactoFilter.FechaIngresoD);
                DateTime ingresoH = Convert.ToDateTime(contactoFilter.FechaIngresoH);

                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.FechaIngreso >= ingresoD && contacto.FechaIngreso <= ingresoH).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }

            if(contactoFilter.ContactoInterno != "TODOS")
            {
                if (contactoFilter.ContactoInterno == "SI") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ContactoInterno == true).OrderBy(contacto => contacto.ApellidoNombre).ToList();
                if (contactoFilter.ContactoInterno == "NO") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.ContactoInterno == false).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }

            if (!string.IsNullOrEmpty(contactoFilter.Organizacion))
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Organizacion.Contains(contactoFilter.Organizacion)).OrderBy(contacto => contacto.ApellidoNombre).ToList();

            if (contactoFilter.Area != "TODOS")
                contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Area.Contains(contactoFilter.Area)).OrderBy(contacto => contacto.ApellidoNombre).ToList();

            if (contactoFilter.Activo != "TODOS")
            {
                if (contactoFilter.Activo == "SI") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Activo == true).OrderBy(contacto => contacto.ApellidoNombre).ToList();
                if (contactoFilter.Activo == "NO") contactosFiltrados = contactosFiltrados.FindAll(contacto => contacto.Activo == false).OrderBy(contacto => contacto.ApellidoNombre).ToList();
            }

            return contactosFiltrados;
        }

        public int getCantPaginas(int cantContactos)
        {
            return (int)Decimal.ToInt32(Math.Ceiling((decimal)this.contactos.Count / cantContactos));
        }
    }
}
