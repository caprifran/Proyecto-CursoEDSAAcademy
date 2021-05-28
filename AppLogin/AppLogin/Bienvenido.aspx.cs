using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppLogin
{
    public partial class Bienvenido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelFechaIngreso.Text += (string)Session["date"];
            LabelUsuario.Text += (string)Session["username"];
        }
    }
}