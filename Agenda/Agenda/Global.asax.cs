using Agenda.BLL;
using Agenda.Entity;
using Agenda.Entity.Contacto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Agenda
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<Contacto> contactosEjemplo = new List<Contacto>();

            int idContacto = 0;
            int cantPaginas = 0;
            int nroPagina = 1;

            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Capristo Franco",
                    Genero = "Masculino",
                    Pais = "Argentina",
                    Localidad = "Rio Grande",
                    ContactoInterno = false,
                    Organizacion = "EDSA",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 03, 20),
                    Activo = true,
                    Direccion = "Pan 1100",
                    TelFijo = "2964 123456",
                    TelCel = "2964 123456",
                    Email = "example1@gmail.com",
                    Skype = "example1"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Rusca Sofia",
                    Genero = "Femenino",
                    Pais = "Argentina",
                    Localidad = "Tandil",
                    ContactoInterno = true,
                    Organizacion = "",
                    Area = "RRHH",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = false,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example2@gmail.com",
                    Skype = "example2"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Rusco Sofio",
                    Genero = "Femenino",
                    Pais = "Argentina",
                    Localidad = "Tandil",
                    ContactoInterno = true,
                    Organizacion = "",
                    Area = "RRHH",
                    FechaIngreso = new DateTime(2021, 02, 20),
                    Activo = false,
                    Direccion = "Pancito 1300",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example3@gmail.com",
                    Skype = "example3"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Azasel Dario",
                    Genero = "Masculino",
                    Pais = "Chile",
                    Localidad = "Santiago de Chile",
                    ContactoInterno = false,
                    Organizacion = "ETC",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = true,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example4@gmail.com",
                    Skype = "example4"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Ariel Dario",
                    Genero = "Masculino",
                    Pais = "Brasil",
                    Localidad = "Rio Grande do Soul",
                    ContactoInterno = false,
                    Organizacion = "ETC",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = true,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example5@gmail.com",
                    Skype = "example5"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Pedro Dario",
                    Genero = "Masculino",
                    Pais = "Argentina",
                    Localidad = "CABA",
                    ContactoInterno = false,
                    Organizacion = "ETC",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = true,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example6@gmail.com",
                    Skype = "example6"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Pedra Daria",
                    Genero = "Femenino",
                    Pais = "Argentina",
                    Localidad = "CABA",
                    ContactoInterno = false,
                    Organizacion = "ETC",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = true,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example7@gmail.com",
                    Skype = "example7"
                }
            );
            contactosEjemplo.Add(
                new Contacto
                {
                    Id = idContacto++,
                    ApellidoNombre = "Pedre Darie",
                    Genero = "Masculino",
                    Pais = "Argentina",
                    Localidad = "CABA",
                    ContactoInterno = false,
                    Organizacion = "ETC",
                    Area = "",
                    FechaIngreso = new DateTime(2021, 05, 20),
                    Activo = true,
                    Direccion = "Pancito 1200",
                    TelFijo = "123 123456",
                    TelCel = "123 123456",
                    Email = "example8@gmail.com",
                    Skype = "example8"
                }
            );

            contactosEjemplo = contactosEjemplo.OrderBy(contacto => contacto.ApellidoNombre).ToList();
            cantPaginas = (int)Decimal.ToInt32(Math.Ceiling((decimal)contactosEjemplo.Count / 5));

            Application["contactosEjemplo"] = contactosEjemplo;
            Application["contactosEjemploFiltrados"] = contactosEjemplo;
            Application["cantPaginas"] = cantPaginas;
            Application["nroPagina"] = nroPagina;

        }
    }
}