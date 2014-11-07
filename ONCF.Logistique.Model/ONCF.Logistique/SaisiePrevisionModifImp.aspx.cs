using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ModelClasse;
using System.Data;

namespace ONCF.Logistique
{
      
    public partial class SaisiePrevisionModifImp : System.Web.UI.Page
     
    {
       
    BLL_Article BLLArticle = new BLL_Article();
    BLL_Prevision prev = new BLL_Prevision();
    SGPL_ARTICLE_PREVISION MyPrevision = new SGPL_ARTICLE_PREVISION();
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

                        DataSet dskit;
                        if (Request.QueryString["IdModule"] != null)
                        {
                            dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Request.QueryString["IdModule"].ToString()), Request.QueryString["IdPrevision"].ToString(), 5);

                        }
                        else
                        {
                            dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Session["Modele"]), Request.QueryString["IdPrevision"].ToString(), 5);

                        }
                        if (dskit.Tables[0].Rows.Count != 0)
                        {
                            if (GDVArticle.Rows.Count == 0)
                            {

                                GDVArticle.DataSource = dskit;
                                GDVArticle.DataBind();
                            }

                        }
                    }
        }
    }
        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow item in GDVArticle.Rows)
            {
                MyPrevision.ArticlePrevision_Id = Convert.ToInt32(GDVArticle.DataKeys[item.RowIndex].Value.ToString());
                MyPrevision.ArticlePrevision_QtePrevision = Convert.ToInt32(((TextBox)item.FindControl("Txtprevision")).Text);
             
               // if (((Label)item.FindControl("LblOL")).Text =="")
                    prev.UpdateArticlePrevision(MyPrevision);
              //  else 


                BtnAnnuler.Text = "Retour";
                title.InnerHtml = "Message";
                msg.Text = "<b>Modification réussi</b>";
                ModalPopupExtender2.Show();
                BtnEnregistrer.Text = "Enregistrer";
            
                DataSet dskit ;
                
            if (Request.QueryString["IdModule"] != null)
            {
                dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Request.QueryString["IdModule"].ToString()), Request.QueryString["IdPrevision"].ToString(), 5);
            
            }
            else
            {
                dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Session["Modele"]),Request.QueryString["IdPrevision"].ToString(), 5);
            
            }
                       if (dskit.Tables[0].Rows.Count != 0)
                {
                    GDVArticle.DataSource = dskit;
                        GDVArticle.DataBind();
                }
            }
            prev.UpdateStatutPrevision(Convert.ToInt32(Request.QueryString["IdPrevision"].ToString()), 1, Convert.ToInt32(Session["IdUser"].ToString()));
            if (Convert.ToInt32(Request.QueryString["Id"].ToString()) != 1)
                prev.UpdateStatutPrevision(Convert.ToInt32(Request.QueryString["IdPrevision"].ToString()), 5, Convert.ToInt32(Session["IdUser"].ToString()));
                    
        }
        protected string getchamp(string chaine1, string chaine2)
        {
            if (Convert.ToInt32(chaine2) != 0)
            {
                return chaine2 ;
            }
            else
            {
                return chaine1;
            }
            
            
        }
        protected void BtnAnnuler_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["Id"].ToString()) == 1)
            {
                Response.Redirect("SaisiePrevisionImprim.aspx");
            }
            else
            {
                Response.Redirect("ValidationPol.aspx");
            }
        }

        protected void gdvAlerteTolTechn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                //bloc de recuperation des info pour faire la modification
               
                //bloc de suppression
                if (e.CommandName == "supprimer")
                {
                    //////on recupére DEMO_ID
                    int ID = int.Parse(GDVArticle.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                    BLLArticle.DeleteArticle(ID);
                    prev.UpdateStatutPrevision(Convert.ToInt32(Request.QueryString["IdPrevision"].ToString()), 1, Convert.ToInt32(Session["IdUser"].ToString()));
                  
                    if (Convert.ToInt32(Request.QueryString["Id"].ToString()) != 1)
                       prev.UpdateStatutPrevision(Convert.ToInt32(Request.QueryString["IdPrevision"].ToString()), 5, Convert.ToInt32(Session["IdUser"].ToString()));
                    
                    DataSet dskit;
                    if (Request.QueryString["IdModule"] != null)
                    {
                        dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Request.QueryString["IdModule"].ToString()), Request.QueryString["IdPrevision"].ToString(), 5);

                    }
                    else
                    {
                        dskit = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Session["Modele"]), Request.QueryString["IdPrevision"].ToString(), 5);

                    }
                   
                       

                            GDVArticle.DataSource = dskit;
                            GDVArticle.DataBind();
                   
                    title.InnerHtml = "Message ";
                    msg.Text = "<b>Suppression réussi</b>";
                    ModalPopupExtender2.Show();
                    BtnEnregistrer.Text = "Enregistrer";

                }
                
                                     //       <asp:TemplateField  HeaderText="Actions" >
                                     //       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor ="#fbb17f"  BorderWidth="2"/>
                                     //    <ItemTemplate>
                                     //        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/supprime.gif" />
                                     //        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                                     //            CommandName="supprimer" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                     //             Text="supprimer" OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cet utilisateur ?');"></asp:LinkButton>
                                     //    </ItemTemplate>
                                        
                                     //    <ItemStyle CssClass="grvIcon" />
                                     //</asp:TemplateField>
            }
            catch (Exception ex)
            {
                title.InnerHtml = "ERREUR ";
                msg.Text = "<b>" + ex.Message + "</b>";
                ModalPopupExtender2.Show();
            }
        }
    }
}