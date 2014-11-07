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
    public partial class Distribution : System.Web.UI.Page
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
                    BtnModif.Visible = false;  
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }

        }
        protected void DDL_Article_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Article.SelectedIndex == 0)
            {
                BtnModif.Visible = false;  
            }
            else
            {
                BtnModif.Visible = true;  
            }
            verifierExistant(DDL_Article.SelectedValue, DDL_Article.SelectedItem.Text);

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
                        // Ajouter une boucle de magazin
                        int dsAR_Mag = GetMagId(Liv.Livraison_ArticlePrevisionId);

                        Liv.Livraison_MagasinId = dsAR_Mag;
                                Liv.Livraison_ArticleDesing = Id_Article + "&" + dsliv.Tables[0].Rows[i]["ArticlePrevision_ArticleDesing"].ToString();
                                Liv.Livraison_QteLivraison = 0;
                                Liv.Livraison_QtePrev = Convert.ToInt32(dsliv.Tables[0].Rows[i]["Qte"].ToString());
                                Liv.Livraison_StatutLivraisonId = 1;

                                BLLLivraison.InsertLivraison(Liv);
                         


                    }
                }
                DataSet dsArticle = BLLLivraison.SGPL_ListEtbByArticle_ToDistri_Imp(DDL_Article.SelectedItem.ToString());
                GDVArticle_Modif.DataSource = dsArticle;
                GDVArticle_Modif.DataBind();
                BtnModif.Visible = true;
                GDVArticle_Modif.Visible = true;

            }
            else
            {
                GDVArticle_Modif.DataSource = null;
                GDVArticle_Modif.DataBind();
                BtnModif.Visible = false;
                GDVArticle_Modif.Visible = true;
            }

        }

        protected int GetMagId(int ArticlePrevision)
        {
            DataSet ListOlMag = BLLLivraison.SGPL_GetOl_Imp_Mag(ArticlePrevision);
            return Convert.ToInt32(ListOlMag.Tables[0].Rows[0]["Magasin_Libelle"].ToString());
        }

        protected void BtnModif_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GDVArticle_Modif.Rows.Count; i++)
                {
                    Liv.Livraison_Id = Convert.ToInt32(((Label)GDVArticle_Modif.Rows[i].FindControl("lblLivId")).Text);
                    Liv.Livraison_QteLivraison = Convert.ToInt32(((TextBox)GDVArticle_Modif.Rows[i].FindControl("lbl_qte_D_")).Text) + Convert.ToInt32(((Label)GDVArticle_Modif.Rows[i].FindControl("lbl_qte_L_")).Text);
                    int Qte = Convert.ToInt32(((Label)GDVArticle_Modif.Rows[i].FindControl("lbl_qte_L_")).Text);
                    int QtePrevision = Convert.ToInt32(((Label)GDVArticle_Modif.Rows[i].FindControl("lbl_qte_P_")).Text);
                    if (Qte + Liv.Livraison_QteLivraison < QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 1);
                    else if (Qte + Liv.Livraison_QteLivraison == QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 2);

                }
                title.InnerHtml = "Message";
                msg.Text = "<b> Operation réussite </b>";
                ModalPopupExtender2.Show();
            }
            catch (Exception)
            {
                title.InnerHtml = "Message";
                msg.Text = "<b> Erreur, veillez re-saisie les information </b> ";
                ModalPopupExtender2.Show();

            }

        }
    }
}