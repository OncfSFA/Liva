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
    public partial class GenererEscalaImp : System.Web.UI.Page
    {
        BLL_Livraison BLLLivraison = new BLL_Livraison();
        Boolean flag;
        double Qte_P, Qte_R;
        double QF;
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

      

        protected void GenerationFichESCALA(DateTime date)
        {
            //sup fichier d'hier
            string dteHier = (DateTime.Now.Day - 1).ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string chemainhier = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["lienEscala"]) + "ESCALA" + dteHier + ".txt";

            if (File.Exists(chemainhier)) File.Delete(chemainhier);
            //
            string dte = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

            string chemain = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["lienEscala"]) + "ESCALA" + dte + ".txt";

            string trf_cod, trf_filler1, trf_dte, trf_nmcl, trf_etab, trf_filler2, trf_dm, trf_filler3, trf_imp, trf_qte, trf_filler4, trf_oe, trf_filler5;

            FileStream fl = new FileStream(chemain, FileMode.Append);
            fl.Close();
            TextWriter tw = new StreamWriter(chemain);

            DataSet dsEscala = BLLLivraison.Get_ESCALA(date);

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

            title.InnerHtml = "Message";
            msg.Text = "<b>Le fichier comptabilité est bien générer :   </br>    « d:\\ESCALA\\ESCALA.txt », veuillez le transféré à l’ESCALA .</b>";
            ModalPopupExtender2.Show();
        }

        protected void BTN_Enregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean flag = false;
                if (GDVArticle.Rows.Count != 0)
                {
                    for (int i = 0; i < GDVArticle.Rows.Count; i++)
                    {
                     if(((CheckBox)GDVArticle.Rows[i].FindControl("Check_Article")).Checked == true)
                        {
                            string id = ((Label)GDVArticle.Rows[i].FindControl("ArticleId")).Text;
                            int OL = Convert.ToInt32(DDL_Article.SelectedItem.ToString());
                            MAJESCALAModif(id,OL );
                            flag = true;
                        }
                    }
                       
                }
                if (flag == true)
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Operation Réussite</b>";
                    ModalPopupExtender2.Show();
                }
                else
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Aucun Article n'est choisie</b>";
                    ModalPopupExtender2.Show();
                }
                  
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void DDL_Article_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            int Id = Convert.ToInt32(DDL_Article.SelectedItem.ToString()) ;
            DataSet ds_GetEscala = BLLLivraison.GetOl_Imp(Id);
        //  DataSet ds_GetEscala_ = BLLLivraison.SGPL_GetListP_Escala("970031Z01 &FEUILLE DE CHARGEMENT");
            GDVArticle.DataSource = ds_GetEscala;
            GDVArticle.DataBind();
            }
            catch (Exception)
            {
                
                
            }
           

        //    Qte_P = Qte_R = 0;
        //    for (int i = 0; i < GDVArticle.Rows.Count; i++)
        //    {
        //        Qte_R = Qte_R + Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblRecu")).Text);
        //        Qte_P = Qte_P + Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblQte")).Text);
        //    }
        //    Session.Add("Qte_R", Qte_R);
        //    Session.Add("Qte_P", Qte_P);
        //    if (Qte_R == Qte_P)
        //    {
        //        QF = 0;
        //        Session.Add("QF", QF);
        //    }
        //    else
        //    {
        //        QF = Qte_R / Qte_P;
        //        Session.Add("QF", QF);
                
        //    }
        }

        protected void MAJESCALAModif(string id_article, int OL)
        {
            string codeetb;

            //TODO : Imp a remplacé par Imp_comp a partir du etbimp_view
            string[] Table = new string[2];

            string HdnCodEffet = "", IMP = "";
            int Somme = 0;
            double sumQtDis = 0;
            string OE;
            //pour remplir IMP et OE
            DataSet ds_Imp_prev = BLLLivraison.GetArticle_P_By_IdArticle(id_article, OL);
            //
            for (int i = 0; i < ds_Imp_prev.Tables[0].Rows.Count; i++)
            {
                codeetb = ds_Imp_prev.Tables[0].Rows[i]["Prevision_EtablissementId"].ToString();
                Table = Get_Oe_Imp(Convert.ToInt32(codeetb));
                OE = Table[1].ToString();
                IMP = Table[0].ToString();
                HdnCodEffet = ds_Imp_prev.Tables[0].Rows[i]["ArticlePrevision_ArticleId"].ToString();
            //    QF = Convert.ToDouble(Session["QF"].ToString());
                //if (QF == 0)
                //{
                   sumQtDis = Convert.ToInt32(ds_Imp_prev.Tables[0].Rows[i]["ArticlePrevision_QtePrevision"].ToString());
                //}
                //else
                //{

                //    sumQtDis = QF * Convert.ToInt32(ds_Imp_prev.Tables[0].Rows[i]["ArticlePrevision_QtePrevision"].ToString());
                //    // Somme = Somme + Convert.ToInt32(sumQtDis);
                //    //TODO : A reflichire
                //}

                string dateliv = DateTime.Now.ToString();
                dateliv = dateliv.Substring(0, 2).ToString() + dateliv.Substring(3, 2).ToString() + dateliv.Substring(9, 1).ToString();
                string datenow = DateTime.Now.ToString();
                datenow = datenow.Substring(0, 2).ToString() + datenow.Substring(3, 2).ToString();

                // ajouter dans la table
                if (sumQtDis != 0)
                    BLLLivraison.MAJ_ESCALA(datenow, " cod_dem ", dateliv, HdnCodEffet, " 0 ", codeetb, " NumDem ", IMP, Convert.ToInt32(sumQtDis).ToString(), OE);

            }
            GenerationFichESCALA();

        }

        protected string[] Get_Oe_Imp(int etb_code)
        {
            DataSet DS_Imp = BLLLivraison.Get_TRF_Imp_Oe(etb_code);
            string[] Table = new string[2];
            if (DS_Imp.Tables[0].Rows.Count > 0)
            {
                Table[0] = DS_Imp.Tables[0].Rows[0][0].ToString();
                Table[1] = DS_Imp.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                Table[0] = "0";
                Table[1] = "0";

            }
            return Table;
        }
        protected void GenerationFichESCALA()
        {
            //sup fichier d'hier
            string dteHier = (DateTime.Now.Day - 1).ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string chemainhier = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["lienEscala"]) + "ESCALA" + dteHier + ".txt";

            if (File.Exists(chemainhier)) File.Delete(chemainhier);
            //
            string dte = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

            string chemain = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["lienEscala"]) + "ESCALA" + dte + ".txt";

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

                tw.WriteLine(trf_cod.Trim() + " " + trf_dte.Trim() + trf_nmcl.Trim() + trf_etab.Trim() + "  " + trf_dm.Trim() + "   " + trf_imp.Trim() + trf_qte.Trim() + "    " + trf_oe.Trim() + "     ");
            }
            tw.Close();

            //title.InnerHtml = "Message";
            //msg.Text = "<b>Le fichier comptabilité est bien générer :   </br>    « d:\\ESCALA\\ESCALA.txt », veuillez le transféré à l’ESCALA .</b>";
            //ModalPopupExtender2.Show();
        }
        protected string getchamp(string chaine, int index)
        {

            string[] tab = chaine.Split('&');
            return tab[index];
        }

      
       

    }
}
