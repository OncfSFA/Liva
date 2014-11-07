using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ModelClasse;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Drawing;
namespace ONCF.Logistique
{
    public partial class ConsultationModifPrevision : System.Web.UI.Page
    {
        BLL_Article BLLArticle = new BLL_Article();
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                try
                {
                    string module = Session["Modele"].ToString();
                    if (Session["Role"].ToString() != "Admin Liva")
                    {
                        //DDL_Etablissement_Fille.Items.Clear();
                        //DDLPole.Enabled = false;
                        //DDLDirection.Enabled = false;
                        DDLEtablissementMere.Enabled = false;
                        DDL_Etablissement_Fille.Items.Clear();
                        DDL_Etablissement_Fille.DataSource = SDS_Etab_User;
                        DDL_Etablissement_Fille.DataBind();

                        DDLPole.Enabled = false;
                        DDLDirection.Enabled = false;

                        DDLDirection.Items.Clear();
                        ListItem item = new ListItem();
                        item.Text = "- Sélectionner -";
                        string ith = Session["idDirection"].ToString();
                        item.Value =  BLLPrevision.GetReelCode(Convert.ToInt32(ith)).ToString();
                        DDLDirection.Items.Add(item);
                        DDLEtablissementMere.DataBind();
                        DDL_Etablissement_Fille.SelectedIndex = 0;
                        TraitementDDL_Etb();
                      
                    }
                    else
                    {
                        //DDL_Etablissement_Fille.Items.Clear();
                        R_Article.Visible = true;
                        DDL_Etablissement_Fille.DataSource = SDS_ETABLISSEMENT_FILLE;
                        DDL_Etablissement_Fille.DataBind();
                    }
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }
        //}

        protected void DDLEtablissement_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dskit = BLLArticle.GetArticleByModulEtablissement(2, Convert.ToInt32(DDLEtablissement.SelectedValue),1);
            //if (dskit.Tables[0].Rows.Count != 0)
            //{
            //    GDVArticle.DataSource = BLLArticle.GetArticleByModulEtablissement(Convert.ToInt32(Session["Modele"]), Convert.ToInt32(DDLEtablissement.SelectedValue), 1);
            //GDVArticle.DataBind();
            //}
           
        }

        protected void DDLPole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DDLDirection.Items.Clear();
            //DDLEtablissementMere.Items.Clear();
            //DDL_Etablissement_Fille.Items.Clear();
         
            //DDLDirection.DataBind();
            //DDL_Etablissement_Fille.DataSource = SDS_ETABLISSEMENT_FILLE;
            //DDL_Etablissement_Fille.DataBind();
            rafrichirDDL(DDLDirection);
            rafrichirDDL(DDLEtablissementMere);
            rafrichirDDL(DDL_Etablissement_Fille);
        }

        protected void DDLDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DDLEtablissementMere.Items.Clear();
            //DDL_Etablissement_Fille.Items.Clear();
      
            //DDLEtablissementMere.DataBind();
            //DDL_Etablissement_Fille.DataSource = SDS_ETABLISSEMENT_FILLE;
            //DDL_Etablissement_Fille.DataBind();
            rafrichirDDL(DDLEtablissementMere);
        
            rafrichirDDL(DDL_Etablissement_Fille);
        }

        protected void DDLEtablissementMere_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            rafrichirDDL(DDL_Etablissement_Fille);
            DDL_Etablissement_Fille.DataSource = SDS_ETABLISSEMENT_FILLE;
            DDL_Etablissement_Fille.DataBind();
           
        }

        protected void DDL_Etablissement_Fille_SelectedIndexChanged(object sender, EventArgs e)
        {

            TraitementDDL_Etb();
        }
        public void TraitementDDL_Etb()
        {
            GDVArticle.DataSource = SDS_GetPrivision;
            GDVArticle.DataBind();
            if (Session["Role"].ToString() == "Admin Liva")
            {
                DDL_Etablissement_Fille.Items.Clear();
                DDL_Etablissement_Fille.DataSource = SDS_ETABLISSEMENT_FILLE;
                DDL_Etablissement_Fille.DataBind();
            }
            if (GDVArticle.Rows.Count != 0) exporter.Visible = true;
            else exporter.Visible = false;

        }
        protected void rafrichirDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "- Sélectionner -";
            item.Value = "0";
            ddl.Items.Add(item);
        }
        protected void exporter_Click(object sender, ImageClickEventArgs e)
        {
            if (GDVArticle.Rows.Count > 0)
            {

                //Export the GridView to Excel
                GDVArticle.AllowPaging = false;
                GDVArticle.AllowSorting = false;

                GDVArticle.HeaderStyle.ForeColor = Color.White;
                GDVArticle.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#C00000");
                GDVArticle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
                GDVArticle.RowStyle.HorizontalAlign = HorizontalAlign.Center;
                GDVArticle.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFCC");
                PrepareGridViewForExport(GDVArticle);
                ExportGridView(GDVArticle);
            }
            else
            {

            }
        }

        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].GetType() == typeof(HyperLink))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "Oui" : "Non";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }

        }
        private void ExportGridView(GridView gv)
        {
            string FileName = DateTime.Now + ".xls";
            string attachment = "attachment; filename=" + FileName;
            Response.ClearContent();
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();

            gv.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";

            frm.Controls.Add(gv);

            frm.RenderControl(htw);

            //GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }

        protected void DDL_Article_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdArticle = DDL_Article.SelectedValue;

        string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
        Response.Redirect(cheminReports + "Liste des previsions LIVA par Article&rs:Command=Render&rs:format=PDF&rs:ClearSession=true&Article=" + IdArticle);

    }
   

      
    } 
  
}