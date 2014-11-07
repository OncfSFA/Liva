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
    public partial class DistributionImp : System.Web.UI.Page
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
                    
                }
                catch
                {
                    Response.Redirect("login.aspx");
                }
            }

        }

        protected void DDLDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            rafrichirDDL(DDLEtablissementMere);
            BtnEnregistrer.Visible = false;
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
        protected void DDL_Etablissement_Fille_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsliv=BLLLivraison.GetLivByEttab( Convert.ToInt32(DDL_Etablissement_Fille.SelectedValue), Convert.ToInt32(Session["Modele"].ToString()),0);
            if (dsliv.Tables[0].Rows.Count != 0)
            {
                GDVArticle.DataSource = null;
                GDVArticle.DataBind();
                GDModif.DataSource = dsliv;
                GDModif.DataBind();
                BtnModif.Visible = true;
                BtnEnregistrer.Visible = false;
                GDModif.Visible = true;
            }
            else
            {
                DataSet dsPrevision = BLLPrevision.GetPrevisionForValidImp(DDL_Etablissement_Fille.SelectedValue, Convert.ToInt32(Session["Modele"].ToString()), "Etab", 5);

                GDVArticle.DataSource = dsPrevision;
                GDVArticle.DataBind();
                GDVArticle.Visible = true;
                if (dsPrevision.Tables[0].Rows.Count != 0) BtnEnregistrer.Visible = true;
                else BtnEnregistrer.Visible = false;
                BtnModif.Visible = false;
                GDModif.DataSource = null;
                GDModif.DataBind();
            }
        }
        protected void DDLEtablissementMere_SelectedIndexChanged(object sender, EventArgs e)
        {

            rafrichirDDL(DDL_Etablissement_Fille);
            BtnEnregistrer.Visible = false;
            GDVArticle.DataSource = null ;
            GDVArticle.DataBind();
            
        }
        protected int  GetMagId(int ArticlePrevision)
        {
            DataSet ListOlMag = BLLLivraison.GetMagasinByArticlePrevision(ArticlePrevision);
            return Convert.ToInt32 (ListOlMag.Tables[0].Rows[0][0].ToString());
            
        }
        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            Boolean IsPdf = Convert.ToBoolean (Session["IsPdf"].ToString());
            string codeetb;
            if (RB_Code_Etb.Checked == false)
            {
                codeetb = DDLDirection.SelectedValue;

            }
            else
            {
                codeetb = TXT_CodeEtb.Text;
            }
            if (IsPdf == false)
            {
                Liv.Livraison_Etablissement = Convert.ToInt32(codeetb);
                Liv.Livraison_UtilisateurId = Convert.ToInt32(Session["IdUser"].ToString());

                Liv.Livraison_Module = Convert.ToInt32(Session["Modele"].ToString());
                for (int i = 0; i < GDVArticle.Rows.Count; i++)
                {
                    Liv.Livraison_ArticlePrevisionId = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblArticlePrevisionId")).Text);
                    Liv.Livraison_MagasinId = GetMagId(Liv.Livraison_ArticlePrevisionId);
                    Liv.Livraison_ArticleDesing = ((Label)GDVArticle.Rows[i].FindControl("LblArticleId")).Text + "&" + ((Label)GDVArticle.Rows[i].FindControl("LblArticleDesing")).Text;
                    Liv.Livraison_QteLivraison = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteLivre")).Text);
                    Liv.Livraison_QtePrev = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblQte")).Text);
                    if (Liv.Livraison_QtePrev == Liv.Livraison_QteLivraison) Liv.Livraison_StatutLivraisonId = 1;
                    else Liv.Livraison_StatutLivraisonId = 2;
                    BLLLivraison.InsertLivraison(Liv);
                   
                }

                //MAJESCALA();
                title.InnerHtml = "Message";
                msg.Text = "<b>Insertion réussite !!!</b>";
                ModalPopupExtender2.Show();
                GDVArticle.DataSource = null;
                GDVArticle.DataBind();
                GenerationFichDistribution();

            }
        }

        

        protected void BtnModif_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Boolean   Flag = true;
                for (int i = 0; i < GDModif.Rows.Count; i++)
                {


                    Liv.Livraison_Id = Convert.ToInt32(((Label)GDModif.Rows[i].FindControl("LblLivraisonId")).Text);
                    Liv.Livraison_QteLivraison = Convert.ToInt32(((TextBox)GDModif.Rows[i].FindControl("TXTQteLivre")).Text) + Convert.ToInt32(((Label)GDModif.Rows[i].FindControl("LblQte")).Text);
                    int Qte = Convert.ToInt32(((Label)GDModif.Rows[i].FindControl("lbl_qte_L_")).Text);
                    int QtePrevision = Convert.ToInt32(((Label)GDModif.Rows[i].FindControl("lbl_qte_P_")).Text);
                    if (Qte + Liv.Livraison_QteLivraison < QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 1);
                    else if (Qte + Liv.Livraison_QteLivraison == QtePrevision) BLLLivraison.UpdateLivraison(Liv.Livraison_Id, Liv.Livraison_QteLivraison, 2);
                    else Flag = false;
                }
                if (Flag == false)
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Vous avez depassez la Quantité demandée !!!</b>";
                    ModalPopupExtender2.Show();
                }
                else
                {
                //MAJESCALAModif();
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Mise a jour réussi !!!</b>";
                    ModalPopupExtender2.Show();
                GDModif.DataSource = null;
                GDModif.DataBind();
                GenerationFichDistribution();
                }
                
            }
        }
        protected string getchamp(string chaine, int index)
        {

            string[] tab = chaine.Split('&');
            return tab[index];
        }

        protected void GenerationFichDistribution()
        {

            //PdfConverter PDF = new PdfConverter();
            //PDF.LicenseKey = "+tHL2svazM302s/UytrJy9TLyNTDw8PD";
            //PDF.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            //PDF.PdfDocumentOptions.JpegCompressionEnabled = false;
            //PDF.PdfDocumentOptions.ShowFooter = PDF.PdfDocumentOptions.ShowHeader = false;
            //PDF.PdfDocumentOptions.RightMargin = PDF.PdfDocumentOptions.TopMargin = PDF.PdfDocumentOptions.LeftMargin = PDF.PdfDocumentOptions.BottomMargin = 0;
            //byte[] downloadBytes = null;


            //******************************* 
            string HTML = GetHtml();
            ConvertHTMLStringToPDF(HTML);
            //downloadBytes = PDF.GetPdfBytesFromHtmlString(HTML);

            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AddHeader("Content-Type", "binary/octet-stream");
            //response.AddHeader("Content-Disposition", "attachment; filename=Bordereau.pdf; size=" + downloadBytes.Length.ToString());
            //response.Flush();
            //response.BinaryWrite(downloadBytes);
            //response.Flush();
            //response.End();



        }
        public string GetHtml()
        {
            string codeetb;
            if (RB_Code_Etb.Checked == false)
            {
                codeetb = DDL_Etablissement_Fille.SelectedValue;

            }
            else
            {
                codeetb = TXT_CodeEtb.Text;
            }
            string HdnCodEffet = "", HdnEff = "",  HdnQte = "";
            DataSet dsliv = BLLLivraison.GetLivByEttab(Convert.ToInt32(codeetb), Convert.ToInt32(Session["Modele"].ToString()), 0);
            for (int i = 0; i < dsliv.Tables[0].Rows.Count; i++)
            {
                HdnCodEffet += getchamp(dsliv.Tables[0].Rows[i]["Livraison_ArticleDesing"].ToString(), 0) + "<br/>";
                HdnEff += getchamp(dsliv.Tables[0].Rows[i]["Livraison_ArticleDesing"].ToString(), 1) + "<br/>";
                HdnQte += dsliv.Tables[0].Rows[i]["Livraison_QteLivraison"].ToString() + "<br/>";
            }


            string HTML = "<html><body><div align='center'>" +
                 " <table border='0' cellpadding='0' cellspacing='0' width='960'>" +
                      "<tr><td><table width='100%'><tr><td align='left'><table><tr><td align='center'><b>" +
                         DDLDirection.SelectedItem.Text + "<br />" + DDLEtablissementMere.SelectedItem.Text + "<br />" +
                            codeetb + "<br /></b></td></tr></table></td>" +

                                  "<td align='right' valign='top'><b><br />LE.........................</b>" +
                                  " </td></tr></table></td></tr><br /><br /><br />" +
            "<tr><td align='center'><font size='3.5'><b>BORDEREAU DE DISTRIBUTION </b></font>" +
      "</td></tr><br /><br /><tr><td>" +
        "<table width='100%' style='border-style: solid; border-width: 1px'>" +
            "<tr align='center'>" +
               " <td style='border-style: solid; border-width: 1px'> <b>   Nomencloture         </b>   </td>" +
                "<td style='border-style: solid; border-width: 1px'> <b>   Désignation      </b>   </td>" +

                "<td style='border-style: solid; border-width: 1px'> <b>   Qte distribution   </b>   </td>" +
            "</tr>" +
            "<tr>" +
                "<td style='border-style: solid; border-width: 1px' align='left'>" + "<b>" + HdnCodEffet + "</b></td>" +
                "<td style='border-style: solid; border-width: 1px' align='left'>" + "<b>" + HdnEff + "</b></td>" +

                "<td style='border-style: solid; border-width: 1px' align='left'>" + "<b>" + HdnQte + "</td>" +
            "</tr></table></td></tr></table></div></body></html>";

            return HTML;
        }
        private void ConvertHTMLStringToPDF(string HtmlString)
        {

            Boolean IsPdf = true;
            Session.Add("IsPdf", IsPdf);
            PdfConverter pdfConverter = new PdfConverter();
            // set the license key
            pdfConverter.LicenseKey = "+tHL2svazM3O2s/UytrJy9TLyNTDw8PD";
            // set the converter options
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
            //string imagepath = Server.MapPath("Styles/Images");
            //pdfConverter.PdfDocumentInfo.AuthorName = "Group Scolaire Alexandre";
            // set if header and footer are shown in the PDF - optional - default is false 
            //pdfConverter.PdfDocumentOptions.ShowHeader = cbAddHeader.Checked;
            pdfConverter.PdfDocumentOptions.ShowFooter = true;
            // set to generate a pdf with selectable text or a pdf with embedded image - optional - default is true
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
            // set if the HTML content is resized if necessary to fit the PDF page width - optional - default is true
            //pdfConverter.PdfDocumentOptions.FitWidth = true;
            // 
            pdfConverter.PdfDocumentOptions.AutoSizePdfPage = true;
            pdfConverter.PdfDocumentOptions.BottomMargin = pdfConverter.PdfDocumentOptions.TopMargin
                = pdfConverter.PdfDocumentOptions.RightMargin = 10;
            pdfConverter.PdfDocumentOptions.LeftMargin = 10;
            // set the embedded fonts option - optional - default is false
            pdfConverter.PdfDocumentOptions.EmbedFonts = true;
            // set the live HTTP links option - optional - default is true
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = false;

            // set if the images in PDF are compressed with JPEG to reduce the PDF document size - optional - default is true
            pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;


            pdfConverter.PdfBookmarkOptions.TagNames = new string[] { "H1", "H2" };


            // set PDF security options - optional
            pdfConverter.PdfSecurityOptions.CanPrint = true;
            pdfConverter.PdfFooterOptions.ShowPageNumber = true;

            byte[] pdfBytes = null;

            pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(HtmlString);

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition",
                "attachment; filename=Examen.pdf; size=" + pdfBytes.Length.ToString());
            response.Flush();
            response.BinaryWrite(pdfBytes);
            response.Flush();
            response.End();
            
        }
        //protected void GenerationFichESCALA()
        //{
        //    PdfConverter PDF = new PdfConverter();
        //    PDF.LicenseKey = "+tHL2svazM302s/UytrJy9TLyNTDw8PD";
        //    PDF.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        //    PDF.PdfDocumentOptions.JpegCompressionEnabled = false;
        //    PDF.PdfDocumentOptions.ShowFooter = PDF.PdfDocumentOptions.ShowHeader = false;
        //    PDF.PdfDocumentOptions.RightMargin = PDF.PdfDocumentOptions.TopMargin = PDF.PdfDocumentOptions.LeftMargin = PDF.PdfDocumentOptions.BottomMargin = 0;
        //    byte[] downloadBytes = null;


        //    //*******************************
        //    string HTML = GetHtml();

        //    downloadBytes = PDF.GetPdfBytesFromHtmlString(HTML);

        //    HttpResponse response = HttpContext.Current.Response;
        //    response.Clear();
        //    response.AddHeader("Content-Type", "binary/octet-stream");
        //    response.AddHeader("Content-Disposition", "attachment; filename=FichierESCALA.pdf; size=" + downloadBytes.Length.ToString());
        //    response.Flush();
        //    response.BinaryWrite(downloadBytes);
        //    response.Flush();
        //    response.End();
        //}

        protected void RB_Lbl_Etb_CheckedChanged(object sender, EventArgs e)
        {
            TableZom.Visible = true;
            TrZom.Visible = true;
            RB_Code_Etb.Checked = false;
            TR_CodeEtb.Visible = false;
            GDModif.Visible = false;
            GDVArticle.Visible = false;
        }

        protected void RB_Code_Etb_CheckedChanged(object sender, EventArgs e)
        {
            TableZom.Visible = false;
            TrZom.Visible = false;
            RB_Lbl_Etb.Checked = false;
            TR_CodeEtb.Visible = true;
            GDModif.Visible = false;
            GDVArticle.Visible = false;
           
        }

        protected void BTN_Recherch_Click(object sender, EventArgs e)
        {
            try
            {
            DataSet dsliv=BLLLivraison.GetLivByEttab( Convert.ToInt32(TXT_CodeEtb.Text), Convert.ToInt32(Session["Modele"].ToString()),0);
            DataSet dsEtb = BLLPrevision.GetLblEtb(Convert.ToInt32 (TXT_CodeEtb .Text));
        
                if (dsliv.Tables[0].Rows.Count != 0)
            {
                GDVArticle.DataSource = null;
                GDVArticle.DataBind();
                GDModif.DataSource = dsliv;
                GDModif.DataBind();
                BtnModif.Visible = true;
                BtnEnregistrer.Visible = false;
                GDModif.Visible = true;
                LblEtb.Text = dsEtb.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                DataSet dsPrevision = BLLPrevision.GetPrevisionForValidImp(TXT_CodeEtb.Text, Convert.ToInt32(Session["Modele"].ToString()), "Etab", 5);

                LblEtb.Text = dsEtb.Tables[0].Rows[0][0].ToString();
                GDVArticle.DataSource = dsPrevision;
                GDVArticle.DataBind();
                GDVArticle.Visible = true;
                if (dsPrevision.Tables[0].Rows.Count != 0) BtnEnregistrer.Visible = true;
                else BtnEnregistrer.Visible = false;
                BtnModif.Visible = false;
                GDModif.DataSource = null;
                GDModif.DataBind();
            }
            }
            catch (Exception)
            {
                
                throw;
            }
        }





        ///////////////////////////////////////////////

        protected void MAJESCALA()
        {
            string codeetb;
            if (RB_Code_Etb.Checked == false )
            {
                codeetb = DDLDirection.SelectedValue;

            }
            else
            {
                codeetb = TXT_CodeEtb.Text ;
            }
            //TODO : Imp a remplacé par Imp_comp a partir du etbimp_view
            int[] Table = new int[2];
            Table = Get_Oe_Imp(Convert.ToInt32(codeetb));
            string HdnCodEffet = "", IMP = Table[0].ToString();

            int sumQtDis = 0, j = 0;
            string OE = Table[1].ToString();
            //pour remplir IMP et OE

            //
            DataSet dsliv = BLLLivraison.GetLivByEttab(Convert.ToInt32(codeetb), Convert.ToInt32(Session["Modele"].ToString()), 1);

            for (int i = 0; i < dsliv.Tables[0].Rows.Count; i++)
            {
                HdnCodEffet = getchamp(dsliv.Tables[0].Rows[i]["Livraison_ArticleDesing"].ToString(), 0);
                sumQtDis = Convert.ToInt32(dsliv.Tables[0].Rows[i]["Livraison_QteLivraison"].ToString());
                for (j = i + 1; j < dsliv.Tables[0].Rows.Count; j++)
                {
                    if (HdnCodEffet == getchamp(dsliv.Tables[0].Rows[j]["Livraison_ArticleDesing"].ToString(), 0))
                    {
                        sumQtDis += Convert.ToInt32(dsliv.Tables[0].Rows[j]["Livraison_QteLivraison"].ToString());
                        i = j;
                    }


                }

             

                string dateliv = dsliv.Tables[0].Rows[i]["Livraison_Date"].ToString();
                dateliv = dateliv.Substring(0, 2).ToString() + dateliv.Substring(3, 2).ToString() + dateliv.Substring(9, 1).ToString();
                string datenow = DateTime.Now.ToString();
                datenow = datenow.Substring(0, 2).ToString() + datenow.Substring(3, 2).ToString();

                // ajouter dans la table
                BLLLivraison.MAJ_ESCALA(datenow, " cod_dem ", dateliv, HdnCodEffet, " 0 ", codeetb, " NumDem ", IMP, sumQtDis.ToString() + "00 ", OE);

            }

            GenerationFichESCALA();
        }
        protected void GenerationFichESCALA()
        {
            //sup fichier d'hier
            string dteHier = (DateTime.Now.Day - 1).ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string chemainhier = System.Configuration.ConfigurationManager.AppSettings["lienEscala"] + "ESCALA" + dteHier + ".txt";

            if (File.Exists(chemainhier)) File.Delete(chemainhier);
            //
            string dte = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

            string chemain = System.Configuration.ConfigurationManager.AppSettings["lienEscala"] + "ESCALA" + dte + ".txt";

            string trf_cod, trf_filler1, trf_dte, trf_nmcl, trf_etab, trf_filler2, trf_dm, trf_filler3, trf_imp, trf_qte, trf_filler4, trf_oe, trf_filler5;

            FileStream fl = new FileStream(chemain, FileMode.Append);
            fl.Close();
            TextWriter tw = new StreamWriter(chemain);

            DataSet dsEscala = BLLLivraison.Get_ESCALA(DateTime.Now);

            for (int i = 0; i < dsEscala.Tables[0].Rows.Count; i++)
            {
                trf_cod = dsEscala.Tables[0].Rows[i]["trf_cod"].ToString();
                trf_filler1 = dsEscala.Tables[0].Rows[i]["trf_filler1"].ToString();
                trf_dte = dsEscala.Tables[0].Rows[i]["trf_dte"].ToString();
                trf_nmcl = dsEscala.Tables[0].Rows[i]["trf_nmcl"].ToString();
                trf_etab = dsEscala.Tables[0].Rows[i]["trf_etab"].ToString();
                trf_filler2 = dsEscala.Tables[0].Rows[i]["trf_filler2"].ToString();
                trf_dm = dsEscala.Tables[0].Rows[i]["trf_dm"].ToString();
                trf_filler3 = dsEscala.Tables[0].Rows[i]["trf_filler3"].ToString();
                trf_imp = dsEscala.Tables[0].Rows[i]["trf_imp"].ToString();
                trf_qte = dsEscala.Tables[0].Rows[i]["trf_qte"].ToString();
                trf_filler4 = dsEscala.Tables[0].Rows[i]["trf_filler4"].ToString();
                trf_oe = dsEscala.Tables[0].Rows[i]["trf_oe"].ToString();
                trf_filler5 = dsEscala.Tables[0].Rows[i]["trf_filler5"].ToString();

                tw.WriteLine(trf_cod.Trim() + trf_filler1.Trim() + trf_dte.Trim() + trf_nmcl.Trim() + trf_etab.Trim() + trf_filler2.Trim() + trf_dm.Trim() + trf_filler3.Trim() + trf_imp.Trim() + trf_qte.Trim() + trf_filler4.Trim() + trf_oe.Trim() + trf_filler5.Trim());
            }
            tw.Close();

            //title.InnerHtml = "Message";
            //msg.Text = "<b>Le fichier comptabilité est bien générer :   </br>    « d:\\ESCALA\\ESCALA.txt », veuillez le transféré à l’ESCALA .</b>";
            //ModalPopupExtender2.Show();
        }

        protected void MAJESCALAModif()
        {
            string codeetb;
            if (RB_Code_Etb.Checked == false)
            {
                codeetb = DDLDirection.SelectedValue;

            }
            else
            {
                codeetb = TXT_CodeEtb.Text;
            }
            //TODO : Imp a remplacé par Imp_comp a partir du etbimp_view
            int[] Table = new int[2];
            Table = Get_Oe_Imp(Convert.ToInt32(codeetb));
            string HdnCodEffet = "", IMP = Table[0].ToString();

            int sumQtDis = 0, j = 0;
            string OE = Table[1].ToString();
            //pour remplir IMP et OE

            //
            for (int i = 0; i < GDModif.Rows.Count; i++)
            {


                HdnCodEffet = ((Label)GDModif.Rows[i].FindControl("LblArticleId")).Text;
                sumQtDis = Convert.ToInt32(((TextBox)GDModif.Rows[i].FindControl("TXTQteLivre")).Text);
                for (j = i + 1; j < GDModif.Rows.Count; j++)
                {
                    if (HdnCodEffet == ((Label)GDModif.Rows[i].FindControl("LblArticleId")).Text)
                    {
                        sumQtDis += Convert.ToInt32(((TextBox)GDModif.Rows[i].FindControl("TXTQteLivre")).Text);
                        i = j;
                    }
                }


                string dateliv = DateTime.Now.ToString();
                dateliv = dateliv.Substring(0, 2).ToString() + dateliv.Substring(3, 2).ToString() + dateliv.Substring(9, 1).ToString();
                string datenow = DateTime.Now.ToString();
                datenow = datenow.Substring(0, 2).ToString() + datenow.Substring(3, 2).ToString();

                // ajouter dans la table
                if (sumQtDis != 0)
                    BLLLivraison.MAJ_ESCALA(datenow, " cod_dem ", dateliv, HdnCodEffet, " 0 ", codeetb, " NumDem ", IMP, sumQtDis.ToString() + "00 ", OE);

            }



            GenerationFichESCALA();

        }

        protected int[] Get_Oe_Imp(int etb_code)
        {
            DataSet DS_Imp = BLLLivraison.Get_TRF_Imp_Oe(etb_code);
            int[] Table = new int[2];
            if (DS_Imp.Tables[0].Rows.Count > 0)
            {
                Table[0] = Convert.ToInt32(DS_Imp.Tables[0].Rows[0][0].ToString());
                Table[1] = Convert.ToInt32(DS_Imp.Tables[0].Rows[0][1].ToString());
            }
            return Table;
        }



        ///////////////////////////////////////////////



    }
}
