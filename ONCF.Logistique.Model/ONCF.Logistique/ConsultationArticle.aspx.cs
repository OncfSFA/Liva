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

using System.Configuration;

namespace ONCF.Logistique
{
    public partial class ConsultationArticleHabiement : System.Web.UI.Page
    {
        BLL_Article BLLArticle = new BLL_Article();
        BLL_Prevision BLLPrevision = new BLL_Prevision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string module = Session["Modele"].ToString();


                    RP_Article.ProcessingMode = ProcessingMode.Remote;
                 // RV_EtatReceptions.ServerReport.ReportServerCredentials = new ReportCredentials("D36963", "190562@@eaz", "ONCF.MA");
                    RP_Article.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
                    RP_Article.ServerReport.ReportPath = String.Format("{0}/{1}", ConfigurationManager.AppSettings["ReportFolder"], "Liste des previsions LIVA par Article");

                    //ReportParameter[] param = new ReportParameter[1];

                    //RV_EtatReceptions.ServerReport.SetParameters(param);


                    // Actualiser sourcerue
                    RP_Article.Visible = true;
                    RP_Article.ServerReport.Refresh();


                }


                catch
                {
                    Response.Redirect("login.aspx");
                }
            }

        }

    }
}

     
     
                                                                                                                                                       


