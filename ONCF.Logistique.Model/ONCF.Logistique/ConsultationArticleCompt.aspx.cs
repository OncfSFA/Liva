using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelClasse;
using BLL;
using System.Data;

namespace ONCF.Logistique
{
    public partial class ValidationPolHab : System.Web.UI.Page
    {
        BLL_Prevision BLLprev = new BLL_Prevision();
        BLL_Article BLLArticle = new BLL_Article();
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
              
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet dsT = BLLprev.GetArticle_Comptabilise(Convert .ToDateTime(DropDownList1 .SelectedValue.ToString()));
                GDVArticle.DataSource = dsT;
                GDVArticle.DataBind();
            }
            catch (Exception)
            {
                GDVArticle.DataSource = null;
                GDVArticle.DataBind();
          
            }
        }

   
    }
}
