using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelClasse;
using BLL;
using System.Data;

using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Winnovative.WnvHtmlConvert;

namespace ONCF.Logistique
{
    public partial class DistributionHab : System.Web.UI.Page
    {
        BLL_Article BLLArticle = new BLL_Article();
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        SGPL_LIVRAISON Liv = new SGPL_LIVRAISON();
        BLL_Livraison BLLLivraison = new BLL_Livraison();
        BLL_Article BLLUSR = new BLL_Article();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
                    DDL_Article.Items.Add("- Selectionner -");
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }

        }

        protected void DDL_Article_SelectedIndexChanged(object sender, EventArgs e)
        {
            verifierExistant(DDL_Article.SelectedValue, DDL_Article.SelectedItem.Text);
            DataSet dsArticle = BLLLivraison.SGPL_ListEtbByArticle_ToDistri_Imp(DDL_Article.SelectedItem.ToString());
            GDVArticle.DataSource = dsArticle;
            GDVArticle.DataBind();
        }

        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {

        }
        protected void verifierExistant(string libelle, string Id_Article)
        {
            DataSet dsliv = BLLLivraison.ListEtbByArticle_ToDistri(Id_Article);

            if (dsliv.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsliv.Tables[0].Rows.Count; i++)
                {
                    DataSet dsVerif = BLLLivraison.Verifier_Exist(Id_Article + "&" + libelle, Convert.ToInt32(dsliv.Tables[0].Rows[i]["CODE_ORGANISATION"].ToString()));
                    if (dsVerif.Tables[0].Rows.Count == 0)
                    {
                        Liv.Livraison_Etablissement = Convert.ToInt32(Convert.ToInt32(dsliv.Tables[0].Rows[i]["CODE_ORGANISATION"].ToString()));
                        Liv.Livraison_UtilisateurId = Convert.ToInt32(Session["IdUser"].ToString());
                        Liv.Livraison_Module = Convert.ToInt32(Session["Modele"].ToString());
                        Liv.Livraison_ArticlePrevisionId = Convert.ToInt32(dsliv.Tables[0].Rows[i]["ArticlePrevision_Id"].ToString());
                        Liv.Livraison_MagasinId = GetMagId(Liv.Livraison_ArticlePrevisionId);
                        Liv.Livraison_ArticleDesing = Id_Article + "&" + dsliv.Tables[0].Rows[i]["ArticlePrevision_ArticleDesing"].ToString();
                        Liv.Livraison_QteLivraison = 0;
                        Liv.Livraison_QtePrev = Convert.ToInt32(dsliv.Tables[0].Rows[i]["Qte"].ToString());
                        Liv.Livraison_StatutLivraisonId = 1;

                        BLLLivraison.InsertLivraison(Liv);

                    }
                }

                //GDVArticle.Visible = true;
            }

        }

        protected int GetMagId(int ArticlePrevision)
        {
            DataSet ListOlMag = BLLLivraison.GetMagasinByArticlePrevision(ArticlePrevision);
            return Convert.ToInt32(ListOlMag.Tables[0].Rows[0][0].ToString());

        }

        protected void BtnModif_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GDVArticle.Rows.Count; i++)
            {
                Liv.Livraison_Id = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("lblLivId")).Text);
                Liv.Livraison_QteLivraison = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("lbl_qte_D_")).Text) + Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("lbl_qte_L_")).Text);
                int Qte = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("lbl_qte_L_")).Text);
                int QtePrevision = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("lbl_qte_P_")).Text);
                if (Qte + Liv.Livraison_QteLivraison < QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 1);
                else if (Qte + Liv.Livraison_QteLivraison == QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 2);

            }

        }


    }





}

