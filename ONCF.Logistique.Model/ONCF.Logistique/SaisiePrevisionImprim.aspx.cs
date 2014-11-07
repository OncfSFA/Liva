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
    public partial class SaisiePrevisionImprim : System.Web.UI.Page
    {

        #region Variable
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        SGPL_PREVISION Prevision = new SGPL_PREVISION();
        SGPL_ARTICLE_PREVISION ArticleP = new SGPL_ARTICLE_PREVISION();
        int activeAjout, activeModif;
        List<Article_DTO> ListArticle
        {
            get
            {
                return (List<Article_DTO>)ViewState["ListArticle"];
            }
            set
            {
                ViewState["ListArticle"] = value;
            }
        }
        #endregion

        #region Evenements

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
                    ListArticle = new List<Article_DTO>();
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
                List<Article_DTO> olist = new List<Article_DTO>();
                GDVArticle.DataSource = olist;
                GDVArticle.DataBind();
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

        protected void DDL_Article_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            Label LblLibEffet = (Label)row.FindControl("LblLibEffet");
            LblLibEffet.Text = ddl.SelectedValue.ToString();

        }

        protected void GDVArticle_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                DropDownList ddl_Article = (DropDownList)e.Row.FindControl("DDL_Article");
                ddl_Article.SelectedIndexChanged += new EventHandler(DDL_Article_SelectedIndexChanged);

                ImageButton img_AddNew = (ImageButton)e.Row.FindControl("imgBtnAddNew");
                img_AddNew.Click += new ImageClickEventHandler(imgBtnAddNew_Click);
            }
        }

        protected void imgBtnAddNew_Click(object sender, EventArgs e)
        {
            ImageButton imgAddNew = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgAddNew.NamingContainer;

            DropDownList ddlArticle = (DropDownList)row.FindControl("DDL_Article");
            Label LblLibEffet = (Label)row.FindControl("LblLibEffet");
            TextBox TxtQte = (TextBox)row.FindControl("Txtprevision");


            string article_id = ddlArticle.SelectedItem.ToString();
            if (article_id.ToString() != "- Sélectionner -")
            {
                
           int etablissement_id ;
           if (Session["Role"].ToString() == "Admin Liva")
           {
               etablissement_id = Convert.ToInt32(Txt_IdEtb.Text);
           }
           else
           {
               etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);
           }

            if (!CheckIfTheArticleExsits(ListArticle, article_id, etablissement_id))
            {
                Article_DTO article = new Article_DTO();

                article.Article_Id = article_id;
                article.libelle_effet = LblLibEffet.Text;
                article.Qte_Article = Convert.ToInt32(TxtQte.Text);
                article.Etablissement_id = etablissement_id;
                article.InsertedIntoDatabase = false;
                DataSet DsVerif_Article = BLLPrevision.Verifier_Article_OL(article_id);
                if (DsVerif_Article.Tables[0].Rows.Count==0)
                {
                  ListArticle.Add(article);
                }
                else
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Cet Article est en cours de fabrication pour cette année, nous allons traiter votre demande le plutot possible</b>";
                    ListArticle.Add(article);
                    ModalPopupExtender2.Show();
                }
                
            }
            else
            {
                title.InnerHtml = "Message";
                msg.Text = "<b>Cet Article existe déjà dans la liste</b>";
                ModalPopupExtender2.Show();
            }

            ddlArticle.SelectedIndex = 0;
            LblLibEffet.Text = "";
            TxtQte.Text = "";

            BindData(etablissement_id);
            }
            else
            {
                title.InnerHtml = "Message";
                msg.Text = "<b>Vous devez choisir un Article</b>";
                ModalPopupExtender2.Show();
            }
        }

        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {
      
            int etablissement_id;
            if (Session["Role"].ToString() == "Admin Liva")
            {
                etablissement_id = Convert.ToInt32(Txt_IdEtb.Text );
            }
            else
            {
               etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);
            }
            
            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", etablissement_id);
            int prevision_id;

            if (dsexist.Tables[0].Rows.Count > 0)
            {
                prevision_id = Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"]);
            }

            else
            {
                Prevision.Prevision_EtablissementId = etablissement_id;
                Prevision.Prevision_ModuleId = Convert.ToInt32(Session["Modele"]);
                Prevision.Prevision_UtilisateurId = Convert.ToInt32(Session["IdUser"]);
                Prevision.Prevision_Agent = "0";
                Prevision.Prevision_Flag = 0;

                prevision_id = BLLPrevision.AjoutePrevision(Prevision);
            }

            InsertArticles(prevision_id, etablissement_id);
            title.InnerHtml = "Message";
            msg.Text = "<b>Insertion réussie</b>";
            ModalPopupExtender2.Show();
            BtnEnregistrer.Text = "Enregistrer";
            //BtnValider.Visible = true;
          //  visibleBtnEnregistre();
            //if (activeAjout == 0)
            //{
            //    BtnEnregistrer.Visible = false;
            //    GDVArticle.Visible = false;
            //}
            //if (activeModif == 0)
            //{
            //    BtnModification.Visible = false;
            //}
            BindIndexChanged(etablissement_id);
            //int etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);

           // ValidateArticles(etablissement_id);
            RefrechData(etablissement_id);
            BindIndexChanged(etablissement_id);
        }

        protected void BtnValider_Click(object sender, EventArgs e)
        {
            int etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);
           
            ValidateArticles(etablissement_id);
            RefrechData(etablissement_id);
            BindIndexChanged(etablissement_id);

            title.InnerHtml = "Message";
            msg.Text = "<b>Validation réussie !!!</b>";
            ModalPopupExtender2.Show();
        }

    protected void BtnModification_Click(object sender, EventArgs e)
        {
            int etablissement_id;
            if (Session["Role"].ToString() == "Admin Liva")
            {
                etablissement_id = Convert.ToInt32(Txt_IdEtb.Text);
            }
            else
            {
                etablissement_id = Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue);
            }
            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", Convert.ToInt32(etablissement_id));
          
            int prevision_id = Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"]);
            Response.Redirect("SaisiePrevisionModifImp.aspx?IdPrevision=" + prevision_id + "&Id=1");
        }
        #endregion

        #region Methodes
        private bool CheckIfTheArticleExsits(List<Article_DTO> list, string articleID, int etablissementID)
        {
            var item = list.SingleOrDefault(x => x.Article_Id == articleID && x.Etablissement_id == etablissementID);

            if (item.Article_Id != null)
                return true;
            else
                return false;
        }

        private void BindIndexChanged(int etablissement_id)
        {


            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", etablissement_id);
            if (dsexist.Tables[0].Rows.Count != 0)
            {
                int prevision_id = Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"]);
                dsexist = BLLPrevision.SGPL_CheckStatutPrevision(prevision_id);
                if (Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 1 || Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 2 || Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 3)
                {
                    if ( Convert.ToInt32(dsexist.Tables[0].Rows[0][0]) == 2)
                    {
                        LBLRectifier.Visible  = true; 
                    }
                    else
                    {
                        LBLRectifier.Visible = false;
                    }
                    //BtnValider.Visible = true;
               
                   
                        BtnEnregistrer.Visible = true;
                        GDVArticle.Visible = true;
                 
                  
                        BtnModification.Visible = true;
                   

                }
                else
                {
                   //// visibleBtnEnregistre();
                   // if (activeAjout == 0)
                   // {
                   //     BtnEnregistrer.Visible = false;
                   //     BtnValider.Visible = false;
                   //     GDVArticle.Visible = false;
                   // }
                   // else
                   // {
                    BtnEnregistrer.Visible = true;
                    //BtnValider.Visible = true;
                    GDVArticle.Visible = true;

                   // }
                   // if (activeModif == 0)
                   // {
                   //     BtnModification.Visible = false;
                   // }
                   // else
                   // {
                    BtnModification.Visible = true;
                   // }
                    //GDVArticle.Visible = false;
                    //BtnValider.Visible = false;
                    //BtnEnregistrer.Visible = false;
                    //BtnModification.Visible = false;
                }

            }
            else
            {
       //  visibleBtnEnregistre();
                //if (activeAjout == 0)
                //{
                //    BtnEnregistrer.Visible = false;
                //    GDVArticle.Visible = false;
                //}
                //else
                //{
                GDVArticle.Visible = true;
                BtnEnregistrer.Visible = true;
                //}
              
                //BtnValider.Visible = false;
                
                BtnModification.Visible = false;
            }




            //visibleBtnEnregistre();
            GDVArticleConsultatoin.Visible = true;


            ListArticle.RemoveAll(x => x.InsertedIntoDatabase == true);

            RefrechData(etablissement_id);
            BindData(etablissement_id);  
        }


        private void BindData(int etablissementID)
        {
            List<Article_DTO> oList = (ListArticle.AsQueryable<Article_DTO>().
                                       Where(x => x.Etablissement_id == etablissementID)).ToList<Article_DTO>();

            GDVArticleConsultatoin.DataSource = oList;
            GDVArticleConsultatoin.DataBind();
        }

        private void InsertArticles(int previsionID, int etablissementID)
        {
            foreach (Article_DTO article in ListArticle)
            {
                if (article.InsertedIntoDatabase == false && article.Etablissement_id == etablissementID)
                {
                    ArticleP.ArticlePrevision_ArticleId = article.Article_Id;
                    ArticleP.ArticlePrevision_QtePrevision = article.Qte_Article;
                    ArticleP.ArticlePrevision_PrevisionId = previsionID;
                    ArticleP.ArticlePrevision_UtilisateurId = Convert.ToInt32(Session["IdUser"]);
                    ArticleP.ArticlePrevision_ArticleDesing = article.libelle_effet;
                    BLLPrevision.AjoutArticlePrevision(ArticleP);
                }
            }

            ListArticle.RemoveAll(x => x.Etablissement_id == etablissementID && x.InsertedIntoDatabase == false);

        }

        private void GetArticles(DataTable articlesDT)
        {
            foreach (DataRow row in articlesDT.Rows)
            {
                Article_DTO article = new Article_DTO();

                article.Article_Id = row["ArticlePrevision_ArticleId"].ToString();
                article.libelle_effet = row["ArticlePrevision_ArticleDesing"].ToString();
                article.Etablissement_id = Convert.ToInt32(row["Prevision_EtablissementId"].ToString());
                article.Qte_Article = Convert.ToInt32(row["ArticlePrevision_QtePrevision"].ToString());
                article.Qte_Modif = Convert.ToInt32(row["ArticlePrevision_Taille"].ToString()); 
                article.InsertedIntoDatabase = true;
                ListArticle.Add(article);
            }
        }

        private void RefrechData(int etablissementID)
        {
            DataSet article_set = BLLPrevision.SGPL_GetPrevisionArticleToConsulte(etablissementID, DateTime.Now.Year);
            DataTable article_DT = article_set.Tables[0];

            GetArticles(article_DT);
        }

        private void ValidateArticles(int etablissementID)
        {
            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", etablissementID);
            int prevision_id = Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"]);
            BLLPrevision.UpdateStatutPrevision(prevision_id, 5, Convert.ToInt32(Session["IdUser"].ToString()));
        }

        protected void visibleBtnEnregistre()
        {

            activeAjout = BLLPrevision.GetActivedesactive(Convert.ToInt32(Session["Modele"].ToString()), "Ajout");
            if (activeAjout == 0)
            {
                BtnEnregistrer.Visible = false;
                BtnValider.Visible = false;
                GDVArticle.Visible = false;

            }
            else
            {
                BtnValider.Visible = true;
                BtnEnregistrer.Visible = true;
                GDVArticle.Visible = true;
            }

            int activeModif_1 = BLLPrevision.GetActivedesactive(Convert.ToInt32(Session["Modele"].ToString()), "Modif");


            string exist = "Non";
            DataSet dsexist = BLLPrevision.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), "0", Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue));
            if (dsexist.Tables[0].Rows.Count > 0)
            {
                exist = "Oui";
            }

            if (activeModif_1 == 0)
            {
                activeModif = 0;
                //BtnModification.Visible = false;
            }
            else if (exist == "Non")
            {
                //BtnModification.Visible = false;
                activeModif = 0;
            }
            else
            {
                //BtnModification.Visible = true;
                activeModif = 1;
            }
        }


        [Serializable]
        struct Article_DTO
        {
            public string Article_Id { get; set; }
            public string libelle_effet { get; set; }
            public int Qte_Article { get; set; }
            public int Etablissement_id { get; set; }
            public int Qte_Modif { get; set; }
            public bool InsertedIntoDatabase { get; set; }
        
        }

        #endregion

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
                title.InnerHtml = "Message";
                msg.Text = "<b>Code inéxistant</b>";
                ModalPopupExtender2.Show();
             
            }
            
        }




        protected string getColor(string chaine1, string chaine2)
        {
            if (Convert.ToInt32(chaine2) != 0)
            {
                return "Red";
            }
            else
            {
                return "White";
            }
        }





    }
}