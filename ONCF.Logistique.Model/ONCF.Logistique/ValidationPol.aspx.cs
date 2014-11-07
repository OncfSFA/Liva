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
    public partial class ValidationPol : System.Web.UI.Page
    {
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        BLL_User user = new BLL_User();
        Alert Email = new Alert();
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
       

        protected void DDLDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            rafrichirDDL(DDLEtablissementMere);
            
                DataSet dsPrevision = BLLPrevision.GetPrevisionForValidImp(DDLDirection.SelectedValue, Convert.ToInt32(Session["Modele"].ToString()), "Dir", 1);
                if (dsPrevision.Tables[0].Rows.Count != 0)
                {
                    GDValid.DataSource = dsPrevision;
                    GDValid.DataBind();
                    BTN_Valider.Visible = true;
                  
                    GDValid.Visible = true;
                    
                    
                }
                else
                {
                    GDValid.DataSource = null;
                    GDValid.DataBind();
                    BTN_Valider.Visible = false ;
                    BtnModification.Visible = false;
                }
                GDVArticle.DataSource = null;
                GDVArticle.DataBind();
        }
        protected void rafrichirDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "- Sélectionner -";
            item.Value = "0";
            ddl.Items.Add(item);
        }
      
        protected void DDLEtablissementMere_SelectedIndexChanged(object sender, EventArgs e)
        {
            rafrichirDDL(DDL_Etablissement_Fille);
          
                DataSet dsPrevision = BLLPrevision.GetPrevisionForValidImp(DDLEtablissementMere.SelectedValue, Convert.ToInt32(Session["Modele"].ToString()), "Mere", 1);
                     if (dsPrevision.Tables[0].Rows.Count != 0)
                         {
                               GDValid.DataSource = dsPrevision;
                               GDValid.DataBind();
                               BTN_Valider.Visible = true;
                               GDValid.Visible = true;
                              
                          }
                            else
                          {
                              GDValid.DataSource = null;
                              GDValid.DataBind(); 
                              BtnModification.Visible = false;
                            
                          }

                     GDVArticle.DataSource = null;
                     GDVArticle.DataBind();
        }

       
        protected void DDL_Etablissement_Fille_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                  GDVArticle.DataSource = SDS_GetPrivision;
                 GDVArticle.DataBind();
                BTN_Valider.Visible = false;
                if (GDVArticle.Rows.Count != 0)
                {
                  
                    BtnModification.Visible = true;
                }
                else
                {
                   
                    BtnModification.Visible = false;
                   
                }
                GDValid.DataSource = null;
                GDValid.DataBind(); 
            }
            catch (Exception)
            {
                BTN_Valider.Visible = false;
               
                BtnModification.Visible = false;
                
            }
         
        }

       
        protected void BtnModification_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaisiePrevisionModifImp.aspx?IdPrevision=" + Convert.ToInt32(((Label)GDVArticle.Rows[0].FindControl("LBL_IdPrevision")).Text) + "&Id=2");
        }

        protected void BTN_Valider_Click(object sender, EventArgs e)
        {
            SGPL_HISTO_STATUT_PREVISION HistoPrevi = new SGPL_HISTO_STATUT_PREVISION();
            int etab = Convert.ToInt32(DDLDirection.SelectedValue);
            string type = "Dir";
            if (DDLEtablissementMere.SelectedValue != "0")
            {
                etab = Convert.ToInt32(DDLEtablissementMere.SelectedValue);
                type = "Mere";
            }

            DataSet dsPrevision = BLLPrevision.GetIdPrevisionToValidByPoleHab(etab, Convert.ToInt32(Session["Modele"].ToString()), type);
            if (dsPrevision.Tables[0].Rows.Count != 0)
            {
                try
                {
                    for (int i = 0; i < dsPrevision.Tables[0].Rows.Count; i++)
                    {
                        BLLPrevision.UpdateStatutPrevision(Convert.ToInt32(dsPrevision.Tables[0].Rows[i][0].ToString()), 5, Convert.ToInt32(Session["IdUser"].ToString()));
                    }
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Opération réussi </b>";
                    ModalPopupExtender2.Show();
                    GDValid.DataBind();

                }
                catch (Exception)
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Erreur </b>";
                    ModalPopupExtender2.Show();

                }

            }
        }

       
             
    }
}