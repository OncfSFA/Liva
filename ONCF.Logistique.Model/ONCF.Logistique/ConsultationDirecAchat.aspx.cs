﻿using System;
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
    public partial class ConsultationDirecAchat : System.Web.UI.Page
    {
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
        protected bool visibilite()
        {
            if (Session["Modele"].ToString() == "1") return true;
            else return false;
        }
        public void GetPDF(int _ID)
        {
          
            string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
            Response.Redirect( cheminReports + "Ordre de livraison des imprimes&rs:Command=Render&rs:format=PDF&rs:ClearSession=true&OrdreLivraison_Id=" + _ID) ;
            
             }
        protected void DDLOL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modul = Convert.ToInt32(Session["Modele"].ToString());
            int OL = 0;
            try
            {
              OL = Convert.ToInt32(DDLOL.SelectedValue);
                DataSet dsPrevision = BLLPrevision.GetPrevisionToConsultDrLiva(modul, OL);
                GDVArticle.DataSource = dsPrevision;
                GDVArticle.DataBind();
               // GetPDF(OL);
                if (dsPrevision.Tables[0].Rows.Count != 0) exporter.Visible = true;
                else exporter.Visible = false; 
            }
            catch (Exception)
            {
                exporter.Visible = false; 

                GDVArticle.DataSource = null;
                GDVArticle.DataBind(); 
               
            }
           
           
        }
        protected string getchamp(string chaine, int index)
        {
            if (Convert.ToInt32(Session["Modele"].ToString()) == 2 && index == 2)
            {
                index = 1;
            }
              string[] tab = chaine.Split('&');
            return tab[index];
        }

        protected void exporter_Click(object sender, ImageClickEventArgs e)
        {
            if (GDVArticle.Rows.Count > 0)
            {

                ////Export the GridView to Excel
                //GDVArticle.AllowPaging = false;
                //GDVArticle.AllowSorting = false;

                //GDVArticle.HeaderStyle.ForeColor = Color.White;
                //GDVArticle.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#C00000");
                //GDVArticle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
                //GDVArticle.RowStyle.HorizontalAlign = HorizontalAlign.Center;
                //GDVArticle.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFCC");
                //PrepareGridViewForExport(GDVArticle);
                //ExportGridView(GDVArticle);
                string value = DDLOL.SelectedValue;
                string cheminReports = System.Configuration.ConfigurationManager.AppSettings.Get("CheminReports").ToString();
                Response.Redirect(cheminReports + "Ordre de livraison des imprimes&rs:Command=Render&rs:ClearSession=true&OrdreLivraison_Id=" + value);

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

        protected void DDl_Annes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLOL.Items.Clear();
            DDLOL.Items.Add(" - Séléction - ");
        }

    }
