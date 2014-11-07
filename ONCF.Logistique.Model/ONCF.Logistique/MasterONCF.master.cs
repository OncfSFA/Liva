using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelClasse;

public partial class MasterONCF : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (Session["user"] != null)
            {
                Lbluser.Text = Session["User"].ToString();
                profil.InnerText = Session["idEtablissement"].ToString() + " " + Session["Etablissement"].ToString();
                string role = Session["Role"].ToString();
                visibilitéMenue();
                if (role == "Achat")
                {
                  
                    Achat.Visible = true;
                    Utilisateur.Visible = true;                   
                    Magasin.Visible = true;
                  //  Pole.Visible = true;
                    Backoffice.Visible = true;
                    Olachat.Visible = true;
                    Consachat.Visible = true;
                   
                }
                else if (role == "Admin Liva")
                {
                    Liva.Visible = true;
                    Achat.Visible = false;
                    Utilisateur.Visible = true;
                    Magasin.Visible = true;
                    Backoffice.Visible = true;
                    DivPol.Visible = false;
                }
                else if (role == "Utilisateur")
                {                   
                    Utilisateur.Visible = true;
                    lienConImp.Visible = false;
                   
                }
                
                else if (role == "Magasin")
                {
                    Magasin.Visible = true;
                }
                else if (role == "Pole")
                {
                    DivPol.Visible = true;
                }
               
                lbDeconeexion.Text = "Déconnexion";
            }
            else
            {
                lbDeconeexion.Text = "Connexion";
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write("ERREUR : " + ex.Message);
        }
    }
    protected void visibilitéMenue()
    {
        if (Session["Modele"].ToString() == "2")
        {
            lienhabi.Visible = false;
            lienimp.Visible = true;
           // lienConHab.Visible = false;
            lienConImp.Visible = true;
            
           // lienvalPolImp.Visible = false;
           
            consRecHab.Visible = false;
            consRecImp.Visible = true;
            Titreimp.Visible = true;
            lienResImp.Visible = false;
            lienResHab.Visible = false;
            lienMarche.Visible = true;
            LiGreception.Visible = false;
            LiGreceptionImp.Visible = true;            
            DistriHab.Visible = false;
            DistriImp.Visible = true;
            generationEscala.Visible = true;
          
            CrLotHab.Visible =false ;
            CrLotImp.Visible =true ;
         
           
        }
        else
        {
            lienhabi.Visible = true;
            lienimp.Visible = false;
        //    lienConHab.Visible = true;
            lienConImp.Visible = false;
           
            //lienvalPolImp.Visible = false;
            
            consRecHab.Visible = true;
            consRecImp.Visible = false;
            TitreHab.Visible = true;
            lienResImp.Visible = false;
            lienResHab.Visible = true;
            lienMarche.Visible = false;
            LiGreception.Visible = true;
            LiGreceptionImp.Visible =false ;
            DistriHab.Visible =true ;
            DistriImp.Visible = false;
            generationEscala.Visible =false ;
           
            CrLotHab.Visible = true;
            CrLotImp.Visible = false;
        }
    
    }
    protected void lbDeconeexion_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbDeconeexion.Text == "Déconnexion")
            {
                Session["User"] = null;
                Session["Etablissement"] = null;
                Session["Modele"] = null;
                Session["Role"] = null;
                profil.InnerText = "Profil";
                lbDeconeexion.Text = "Connexion";
                Response.Redirect("~/login.aspx");
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write("ERREUR : " + ex.Message);
        }

    }

    protected string returnLienModul()
    {
        string lien = "SaisiePrevision.aspx";
        if (Session["Modele"].ToString() == "2") lien="SaisiePrevisionImprim.aspx";
        return lien;
    
    }
}
