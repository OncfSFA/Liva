using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace ONCF.Logistique
{
    public partial class Basculation_Imp : System.Web.UI.Page
    {
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                bool err = false;
                try
                {
                    string module = Session["Modele"].ToString();
                }
                catch
                {
                    err = true;

                }
                if (err) Response.Redirect("login.aspx");
            }
        }

        protected void Enregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                BLLPrevision.Basculement((DateTime.Now.Year -1), Convert.ToInt32(Session["Modele"].ToString()));
                title.InnerHtml = "Message";
                msg.Text = "<b>Opération réussite </b>";
                ModalPopupExtender2.Show();
                return;
            }
            catch (Exception)
            {

                title.InnerHtml = "Message";
                msg.Text = "<b> une erreur s'est produite veuillez contacter l'administrateur </b>";
                ModalPopupExtender2.Show();
                return;
            }
               }
    }
}