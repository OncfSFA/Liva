using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelClasse;
using BLL;
using System.Data;
using System.Collections;

using System.Configuration;
public partial class ActiveDesactive : System.Web.UI.Page
    {
    BLL_Prevision BLLPrevision = new BLL_Prevision();
      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
                    RD_Article.Checked = false;
                    RD_Etab.Checked = true;
                    checked_RD();
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

       

        protected void RD_Etab_CheckedChanged(object sender, EventArgs e)
        {
            RD_Article.Checked = false;
            checked_RD();
        }

        protected void RD_Article_CheckedChanged(object sender, EventArgs e)
        {
            RD_Etab.Checked = false;
            checked_RD();
        }

        public void checked_RD()
        {
            if (RD_Etab.Checked== true)
            {
                RD_Oncf.Enabled = false;
                RD_Art.Enabled = false;
                DDL_Article.Enabled = false;
                //DDL_TT.Enabled = false;

                RD_Pole.Enabled = true;
                RD_Pole.Checked = true;
                RD_Dir.Enabled = true;
                RD_Dir.Checked = false;
                RD_Etb.Enabled = true;
                RD_Etb.Checked = false;

                DDLDirection.Enabled = false;
                DDLPole.Enabled = true;

                Txt_EtbCode.Enabled = false;
            }
            else
            {
                RD_Oncf.Enabled = true;
                RD_Oncf.Checked = false;
                RD_Art.Enabled = true;
                RD_Art.Checked = true;
                DDL_Article.Enabled = true;
                //DDL_TT.Enabled = false;

                RD_Pole.Enabled = false;
                RD_Dir.Enabled = false;
                RD_Etb.Enabled = false;
                DDLDirection.Enabled = false;
                DDLPole.Enabled = false;
                Txt_EtbCode.Enabled = false;
            }
        }

        protected void RD_Pole_CheckedChanged(object sender, EventArgs e)
        {
            //RD_Pole.Checked = true;
            RD_Dir.Checked = false;
            RD_Etb.Checked = false;
            DDLDirection.Enabled = false;
            DDLPole.Enabled = true;
            Txt_EtbCode.Enabled = false;
        }

        protected void RD_Dir_CheckedChanged(object sender, EventArgs e)
        {
            RD_Pole.Checked = false;
            //RD_Dir.Checked = true;
            RD_Etb.Checked = false;
            DDLDirection.Enabled = true;
            DDLPole.Enabled = false;
            Txt_EtbCode.Enabled = false;
        }

        protected void RD_Etb_CheckedChanged(object sender, EventArgs e)
        {
            RD_Pole.Checked = false;
            RD_Dir.Checked = false;
            //RD_Etb.Checked = false;
            DDLDirection.Enabled = false;
            DDLPole.Enabled = false;
            Txt_EtbCode.Enabled = true;
        }

        protected void RD_Art_CheckedChanged(object sender, EventArgs e)
        {
            RD_Oncf.Checked = false;
            //RD_Art.Checked = false;
            DDL_Article.Enabled = true;
            //DDL_TT.Enabled = false;
        }

        protected void RD_Oncf_CheckedChanged(object sender, EventArgs e)
        {
            //RD_Oncf.Checked = false;
            RD_Art.Checked = false;
            DDL_Article.Enabled = false;
            //DDL_TT.Enabled = true;
        }

    
   
        public Boolean VerifExistEtb(int codeEtb)
        {
            DataSet Ds_Verif_Etb = BLLPrevision.GetLblEtb(codeEtb);
            if (Ds_Verif_Etb.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
              
            }

        }
        protected void Btn_LanceRv_Click(object sender, EventArgs e)
        {
            string date = Txt_Annee.Text;
          
                
       
            if (RD_Etab.Checked == true)
            {
                if (RD_Pole.Checked == true)
                {
                    string Value = DDLPole.SelectedValue;
                    //refrech_RV("Liste des previsions LIVA par Etablissement_Boucle_Pole", "EtbCode", Value);
                    string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                    Response.Redirect(cheminReports + "Liste des previsions LIVA par Etablissement_Boucle_Pole&rs:Command=Render&rs:ClearSession=true&EtbCode=" + Value + "&Date=" + date);

                }
                else if (RD_Etb.Checked == true)
                {
                    string Value = Txt_EtbCode.Text;
                    if (VerifExistEtb(Convert.ToInt32(Value))==true)
                    {
                        // refrech_RV("Liste des previsions LIVA par Etablissement", "EtbCode", Value);
                         string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                         Response.Redirect(cheminReports + "Liste des previsions LIVA par Etablissement&rs:Command=Render&rs:ClearSession=true&EtbCode=" + Value + "&Date=" + date);
                    }
                    else
                    {
                        title.InnerHtml = "Message";
                        msg.Text = "<b>Code inéxistant</b>";
                        ModalPopupExtender2.Show();
                    }
                  
                }
                else if (RD_Dir.Checked == true)
                {
                    string Value = DDLDirection.SelectedValue;
                  //  refrech_RV("Liste des previsions LIVA par Etablissement_Boucle_Dir", "EtbCode", Value);
                    string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                    Response.Redirect(cheminReports + "Liste des previsions LIVA par Etablissement_Boucle_Dir&rs:Command=Render&rs:ClearSession=true&EtbCode=" + Value + "&Date=" + date);
            
                }

            }
            else if (RD_Article.Checked == true)
            {
                if (RD_Art.Checked == true)
                {
                    string Value = DDL_Article.SelectedValue;
                    //  refrech_RV("Liste des previsions LIVA par Article", "Article", Value);
                    string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                    Response.Redirect(cheminReports + "Liste des previsions LIVA par Article&rs:Command=Render&rs:ClearSession=true&Article=" + Value + "&Date=" + date);

                }
                else if (RD_Oncf.Checked == true)
                {
                    // refrech_RV_ONCF("Liste des previsions LIVA par Etablissement_Boucle_ONCF");
                    string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                    Response.Redirect(cheminReports + "Liste des previsions LIVA par Etablissement_Boucle_ONCF&rs:Command=Render&rs:ClearSession=true&Date=" + date);

                }

        

            }
        }

       

      
     
       
        
    }
