using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelClasse;
using BLL;
using System.Data;

public partial class SaisiePrevision : System.Web.UI.Page
    {
    BLL_Kit BLLKit = new BLL_Kit();
    SGPL_PREVISION prev=new SGPL_PREVISION ();
    SGPL_ARTICLE_PREVISION ArticPrevis=new SGPL_ARTICLE_PREVISION ();
    BLL_Prevision BLLprev=new BLL_Prevision();

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
                    else
                    {
                        visibleBtnEnregistre();
                        DDLKit.DataSource = SqlDataSourceKit;
                        DDLKit.DataBind();
                    }
            }
        }
      
        protected void remplirgridannee()
        {
            int count = GDVArticle.Rows.Count;
            string agent = HdnAgen.Value;// DDLAgent.SelectedValue;
            for (int i = 0; i < count; i++)
            {
                ((SqlDataSource)GDVArticle.Rows[i].FindControl("SqlDataSource1")).SelectParameters["ArticlePrevision_ArticleId"].DefaultValue = ((Label)GDVArticle.Rows[i].FindControl("TblIdArticle")).Text;
                ((SqlDataSource)GDVArticle.Rows[i].FindControl("SqlDataSource1")).SelectParameters["Prevision_Agent"].DefaultValue = agent ;
             
          }

        }
        protected void DDLAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
           HdnAgen.Value = DDLAgent.SelectedValue;
            DataSet dskit = BLLKit.GetKitByPersonne(DDLAgent.SelectedValue);
            if (dskit.Tables[0].Rows.Count != 0)
            {
                DDLKit.SelectedValue = dskit.Tables[0].Rows[0]["COD_FCT"].ToString();
                HdnEncFonc.Value = dskit.Tables[0].Rows[0]["COD_FCT"].ToString();
                remplireGrid();
            }
            else DDLKit.SelectedValue = "0";
            visibleLienModif();
        }

        protected void DDLKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            remplireGrid();
            
        }
        protected void remplireGrid()
        {
             DataSet dseffet = BLLKit.GeteffetByFonction(DDLKit.SelectedValue);
             if (dseffet.Tables[0].Rows.Count != 0)
             {
                 GDVArticle.DataSource = dseffet;
                 GDVArticle.DataBind();
                 remplirgridannee();
                 LblAucunEffet.Visible = false;
                
             }
             else
             {
                 LblAucunEffet.Visible = true;
                 GDVArticle.DataSource = null;
                 GDVArticle.DataBind();
             }
        }
        protected void BtnModification_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaisiePrevisionModif.aspx?source=saisi&idAgent=" + HdnAgen.Value);
        }
        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {

            string exist = "Non";
            DataSet dsexist = BLLprev.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()),DDLAgent.SelectedValue ,0);
            if (dsexist.Tables[0].Rows.Count > 0)
            {
                exist = "Oui";
            }
           
            if (exist == "Non")
            {
                prev.Prevision_EtablissementId = Convert.ToInt32(DDLEtablissement.SelectedValue);
                prev.Prevision_ModuleId = Convert.ToInt32(Session["Modele"].ToString());
                prev.Prevision_UtilisateurId = Convert.ToInt32(Session["IdUser"].ToString());
                prev.Prevision_Agent = DDLAgent.SelectedValue;
                if (HdnEncFonc.Value != DDLKit.SelectedValue) prev.Prevision_Flag = Convert.ToInt32(DDLKit.SelectedValue);
                int IDprevv = BLLprev.AjoutePrevision(prev);

                if (IDprevv != 0)
                {
                    ArticPrevis.ArticlePrevision_PrevisionId = IDprevv;
                    ArticPrevis.ArticlePrevision_UtilisateurId = Convert.ToInt32(Session["IdUser"].ToString());
                    for (int i = 0; i < GDVArticle.Rows.Count; i++)
                    {

                        ArticPrevis.ArticlePrevision_ArticleId = ((Label)GDVArticle.Rows[i].FindControl("TblIdArticle")).Text;
                        ArticPrevis.ArticlePrevision_Taille = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("Txtprevision")).Text);
                        ArticPrevis.ArticlePrevision_QtePrevision = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblQte")).Text);
                        ArticPrevis.ArticlePrevision_ArticleDesing = ((Label)GDVArticle.Rows[i].FindControl("LblLibEffet")).Text;
                        BLLprev.AjoutArticlePrevision(ArticPrevis);
                    }

                    HdnAgen.Value = DDLAgent.SelectedValue;

                    title.InnerHtml = "Message";
                    msg.Text = "<b>Insertion réussite !!!</b>";
                    ModalPopupExtender2.Show();
                    visibleLienModif();
                }
                else
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Insertion non effectuée !!!</b>";
                    ModalPopupExtender2.Show();
                }

                remplireGrid();
                DDLEtablissement.SelectedValue = "0";
                DDLAgent.SelectedValue = "0";
            }
            else
            {
                title.InnerHtml = "Message";
                msg.Text = "<b>Il existe déjà une prévision pour cette année  !!!</b>";
                ModalPopupExtender2.Show();
            }
        }
        protected void visibleBtnEnregistre()
        {

            int activeAjout = BLLprev.GetActivedesactive(Convert.ToInt32(Session["Modele"].ToString()), "Ajout");
            if (activeAjout == 0) BtnEnregistrer.Visible = false;

                       
        }
        protected void visibleLienModif()
        {
            string exist = "Non";
            string statut = "";
            DataSet dsexist = BLLprev.GetPrevisionByModule(DateTime.Now.Year, Convert.ToInt32(Session["Modele"].ToString()), DDLAgent.SelectedValue,0);
            if (dsexist.Tables[0].Rows.Count > 0)
            {
                exist = "Oui";
            }

            if (exist == "Non")
            {
              //  HyperLinkModif.Visible = false;
                BtnValider.Visible = false ;
                BtnModification.Visible = false;
            }
            else
            {
                DataSet dsstatut = BLLprev.GetStatutPrevision(Convert.ToInt32(dsexist.Tables[0].Rows[0]["Prevision_Id"].ToString()));
                statut = dsstatut.Tables[0].Rows[0]["HistoStatutPrevision_StatutPrevisionId"].ToString();
                 if (dsexist.Tables[0].Rows[0]["Prevision_Flag"].ToString() != "0")
                    {
                        GDVArticle.DataSource = BLLKit.GeteffetByFonction(dsexist.Tables[0].Rows[0]["Prevision_Flag"].ToString());
                        GDVArticle.DataBind();
                        remplirgridannee();
                    }
                if (statut =="1" || statut =="6" )
                {
                    
                    LblAucunEffet.Text = "";
                    int activeModif = BLLprev.GetActivedesactive(Convert.ToInt32(Session["Modele"].ToString()), "Modif");
                    BtnValider.Visible = true;
                    if (activeModif == 0) BtnModification.Visible = false; else BtnModification.Visible = true;
                }
                else 
                
                {
                    BtnValider.Visible = false;
                    BtnModification.Visible = false;
                   
                }

            }
        }

        protected void BtnValider_Click(object sender, EventArgs e)  
        {
            DataSet ds = BLLprev.GetArticlePrevisionHabForMod(HdnAgen.Value);
            int idprev = Convert.ToInt32(ds.Tables[0].Rows[0]["ArticlePrevision_PrevisionId"].ToString());
            BLLprev.UpdateStatutPrevision(idprev,2,Convert.ToInt32(Session["IdUser"].ToString()));
            BtnValider.Visible = false;
            BtnModification.Visible = false;
           // HyperLinkModif.Visible = false;
            title.InnerHtml = "Message";
            msg.Text = "<b>Validation réussi</b>";
            ModalPopupExtender2.Show();
        }

        protected void DDLEtablissement_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                DDLAgent.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "- Sélectionner -";
                item.Value = "0";
                DDLAgent.Items.Add(item);            
           
            DDLAgent.DataSource = BLLprev.INTER_GetUserByEtab(DDLEtablissement.SelectedValue);
            DDLAgent.DataBind();
            GDVArticle.DataSource = null;
            GDVArticle.DataBind();
            BtnModification.Visible = false;
            BtnValider.Visible = false;
        }

        protected void RBchangeFonction_CheckedChanged(object sender, EventArgs e)
        {
            DDLKit.Visible = true;
        }
    }
