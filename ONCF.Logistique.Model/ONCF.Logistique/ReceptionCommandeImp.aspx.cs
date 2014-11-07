using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ModelClasse;
using System.Data;

public partial class ReceptionCommandeImp : System.Web.UI.Page

    {
    BLL_Prevision BLLPrevision = new BLL_Prevision();
    BLL_Livraison BLLLivraison = new BLL_Livraison();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 bool err = false;
                    try
                    {
                        string module = Session["Modele"].ToString();
                        BtnEnregistrer.Visible = false;
                    }
                    catch
                    {
                        err = true;
                        
                    }
                    if (err) Response.Redirect("login.aspx");
                   
            }
        }

        protected void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                int Reception ;// fixe car un seul magasin qui fait la reception
                int QteRecue = 0;

                Boolean err = Verification();
                if (err)
                {
                    title.InnerHtml = "Message";
                    msg.Text = "<b>La quantité reçue doit être inférieur ou égale à la quantité Commandée</b>";
                    ModalPopupExtender2.Show();
                    return;
                }
                else
                {
                    for (int i = 0; i < GDVArticle.Rows.Count; i++)
                    {

                        Reception = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LabelR")).Text);
                        QteRecue = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteRecu")).Text) + Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblRecu")).Text);
                        BLLLivraison.UpdateQteRecu(Reception, QteRecue, Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblIdMagasinReception")).Text));
                        BLLLivraison.UpdateEstLivre(Reception);

                    }
                    title.InnerHtml = "Message";
                    msg.Text = "<b>Insertion réussi</b>";
                    ModalPopupExtender2.Show();
                    BtnEnregistrer.Text = "Enregistrer";
                    remplirGrid();
                }
            }
       
           
        }
        protected bool Verification()
        {
            int QteRecue = 0, QtePrev = 0;
            Boolean err = false;
            for (int i = 0; i < GDVArticle.Rows.Count; i++)
            {
                QtePrev = Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblQte")).Text);                
                QteRecue = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteRecu")).Text) + Convert.ToInt32(((Label)GDVArticle.Rows[i].FindControl("LblRecu")).Text);
                if (QtePrev < QteRecue)
                {
                    err = true;
                    break;
                }
            }
            return err;
        
        }
        protected void DDLOL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLOL.SelectedIndex != 0)
            {
                BtnEnregistrer.Visible = true;

            }
            else
            {
                BtnEnregistrer.Visible = false;
            }
             remplirGrid();
           
        }
        protected void remplirGrid()
        {
            int modul = Convert.ToInt32(Session["Modele"].ToString());
            int OL = Convert.ToInt32(DDLOL.SelectedValue);
            DataSet dsPrevision = BLLPrevision.GetPrevisionToConsultDrLiva_1(modul, OL, Convert.ToInt32(Session["Mag"].ToString()));

            GDVArticle.DataSource = dsPrevision;
            GDVArticle.DataBind();
        
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
        protected void MAJESCALAModif(string codeetb)
        {
           
            //TODO : Imp a remplacé par Imp_comp a partir du etbimp_view
            int[] Table = new int[2];
            Table = Get_Oe_Imp(Convert.ToInt32(codeetb));
            string HdnCodEffet = "", IMP = Table[0].ToString();

            int sumQtDis = 0, j = 0;
            string OE = Table[1].ToString();
            //pour remplir IMP et OE

            //
            for (int i = 0; i < GDVArticle.Rows.Count; i++)
            {


                HdnCodEffet = ((Label)GDVArticle.Rows[i].FindControl("Labb")).Text;
                sumQtDis = Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteRecu")).Text);
                for (j = i + 1; j < GDVArticle.Rows.Count; j++)
                {
                    if (HdnCodEffet == ((Label)GDVArticle.Rows[i].FindControl("Labb")).Text)
                    {
                        sumQtDis += Convert.ToInt32(((TextBox)GDVArticle.Rows[i].FindControl("TXTQteRecu")).Text);
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
    }
