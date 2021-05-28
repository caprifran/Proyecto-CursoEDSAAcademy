using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using AppLoginBLL;
using AppLoginEntity;

namespace AppLogin
{
    public partial class _Default : Page
    {
        private Business business = new Business();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AutenticarLogin(object sender, AuthenticateEventArgs e)
        {
            string username = ((Login)sender).UserName;
            string password = ((Login)sender).Password;
            if (this.business.VerificarCredenciales(username, password))
            {
                Session["username"] = username;
                Session["date"] = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
                Response.Redirect("Bienvenido.aspx");
            }


        }
    }
}