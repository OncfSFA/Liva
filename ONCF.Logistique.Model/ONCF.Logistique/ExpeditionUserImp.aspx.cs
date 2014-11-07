using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web.Security;
using BLL;
using ModelClasse;

public partial class ExpeditionUserImp : System.Web.UI.Page
    {
        BLL_Kit BLLKit = new BLL_Kit();
        SGPL_PREVISION prev = new SGPL_PREVISION();
        SGPL_ARTICLE_PREVISION ArticPrevis = new SGPL_ARTICLE_PREVISION();
        BLL_Prevision BLLprev = new BLL_Prevision();

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

        
        
       
        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            int qtePrev = 0;
            ArticPrevis.ArticlePrevision_UtilisateurId = Convert.ToInt32(Session["IdUser"].ToString());
            for (int i = 0; i < GDVArticle.Rows.Count; i++)
            {

                ArticPrevis.ArticlePrevision_Id = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblPrevisionid")).Text);
                ArticPrevis.ArticlePrevision_Taille =0;
                ArticPrevis.ArticlePrevision_QteRecue = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteRecu")).Text);
                qtePrev = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblQte")).Text);
                if (qtePrev == ArticPrevis.ArticlePrevision_QteRecue )
                ArticPrevis.ArticlePrevision_EstLivree = 1;
                else ArticPrevis.ArticlePrevision_EstLivree = 0;

                BLLprev.UpdateArticlePrevisionHab(ArticPrevis,1);// 1 = pour modifier la quantité recu ; 0 = modif taille
            }
            title.InnerHtml = "Message";
            msg.Text = "<b>Modification  réussi</b>";
            ModalPopupExtender2.Show();
        }
        protected void DDL_Etablissement_Fille_SelectedIndexChanged(object sender, EventArgs e)
        {

            GDVArticle.DataSource = SDS_GetPrivision;
            GDVArticle.DataBind();
        } 
       
      

      

  
    }

