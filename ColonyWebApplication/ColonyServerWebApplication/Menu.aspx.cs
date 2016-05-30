using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColonyServerWebApplication
{
    public partial class Menu : System.Web.UI.Page
    {
        MenuSupportClass supporter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                supporter = new MenuSupportClass();
            }
        }

        protected void ButtonWant_Click(object sender, EventArgs e)
        {
            ButtonWant.Enabled = false;
            Response.Redirect("WantTop.aspx");
            ButtonWant.Enabled = true;
        }

        protected void ButtonGet_Click(object sender, EventArgs e)
        {
            ButtonWant.Enabled = false;
            Response.Redirect("GetTop.aspx");
            ButtonWant.Enabled = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            ButtonWant.Enabled = false;
            Response.Redirect("SearchTop.aspx");
            ButtonWant.Enabled = true;
        }

        protected void ButtonGive_Click(object sender, EventArgs e)
        {
            ButtonWant.Enabled = false;
            Response.Redirect("GiveTop.aspx");
            ButtonWant.Enabled = true;
        }

        protected void ButtonConfig_Click(object sender, EventArgs e)
        {
            ButtonWant.Enabled = false;
            Response.Redirect("ConfigTop.aspx");
            ButtonWant.Enabled = true;
        }

    }
}