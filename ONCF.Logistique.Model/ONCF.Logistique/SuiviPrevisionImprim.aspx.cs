using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using ModelClasse;



namespace ONCF.Logistique
{
    public partial class SuiviPrevisionImprim : System.Web.UI.Page
    {

        BLL_Prevision BLLPrevision = new BLL_Prevision();
        SGPL_PREVISION Prevision = new SGPL_PREVISION();
        SGPL_ARTICLE_PREVISION ArticleP = new SGPL_ARTICLE_PREVISION();
      
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
                 
                if (Session["Role"].ToString() == "Admin Liva")
                {
                    Tr_Liva.Visible = true;
                    Tr_Utilisateur.Visible = false;
                }
                else
                {
                    Tr_Utilisateur.Visible = true;
                    Tr_Liva.Visible = false;
                }
               
                }
                catch
                {
                     Response.Redirect("login.aspx");
                }
            
               
            }
        }

        protected void DDL_Etablissement_Fille_SelectedIndexChanged(object sender, EventArgs e)
        {
            int etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);
            BindIndexChanged(etablissement_id);
        }

        private void BindIndexChanged(int etablissement_id)
        {

            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", etablissement_id);
            if (dsexist.Tables[0].Rows.Count != 0)
            {
                int prevision_id = Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"]);
                dsexist = BLLPrevision.SGPL_CheckStatutPrevision(prevision_id);
                if (Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 1)
                {
                    LblStatut.Text = "nouvelle prévision";
                    GDVArticleConsultation.DataSource = null;
                    GDVArticleConsultation.DataBind();
                }
                else if (Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 5)
                {
                    LblStatut.Text = "prévision validé par pole";
                    DataSet dsp = BLLPrevision.SGPL_GetPrevisionArticleToConsulte(etablissement_id, Convert.ToInt32(DateTime.Now.Year));
                    if (dsp.Tables[0].Rows.Count != 0)
                    {
                        GDVArticleConsultation.DataSource = dsp;
                        GDVArticleConsultation.DataBind();
                    }
                    else
                    {
                        GDVArticleConsultation.DataSource = null;
                        GDVArticleConsultation.DataBind();
                    }
                }
                else
                {
                    LblStatut.Text = "";
                    GDVArticleConsultation.DataSource = null;
                    GDVArticleConsultation.DataBind();
                }

            }
            else
            {
                LblStatut.Text = "";
                GDVArticleConsultation.DataSource = null;
                GDVArticleConsultation.DataBind();
            }
        }

        protected string ReturnStatut(string ol)
        {
            if (ol != "") return "Encours....";
            else return "";
        }
     
        protected void Btn_Recherche_Click(object sender, EventArgs e)
        {
            
            int etablissement_id = Convert.ToInt32(Txt_IdEtb.Text);
            DataSet Ds_Verif_Etb = BLLPrevision.GetLblEtb(etablissement_id);
            if (Ds_Verif_Etb.Tables[0].Rows.Count > 0)
            {
                BindIndexChanged(etablissement_id);
            }
            else
            {
                   LblStatut.Text = "";
                    GDVArticleConsultation.DataSource = null;
                    GDVArticleConsultation.DataBind();
                
                title.InnerHtml = "Message";
                msg.Text = "<b>Code inéxistant</b>";
                ModalPopupExtender2.Show();
             
            }
            
        }

    }
}