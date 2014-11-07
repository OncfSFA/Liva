using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using ModelClasse;
namespace BLL
{
    public class BLL_Prevision
    {
        DAL_Prevision dal_previs = new DAL_Prevision();

       
        public int AjoutePrevision(SGPL_PREVISION previs)
        {
            return dal_previs.AjoutePrevision(previs);
        }
        public void AjoutArticlePrevision(SGPL_ARTICLE_PREVISION Articlepreviv)
       {
            dal_previs.AjoutArticlePrevision(Articlepreviv);
       }
        
        public void Activedesactive(DateTime dateD, DateTime dateF, int module, string action)
        {
            dal_previs.Activedesactive(dateD,dateF, module, action);
        }
        public int GetActivedesactive(int module, string action)
        {
            int activ=0;
            DateTime dateactuel = DateTime.Now;
            DataSet ds=dal_previs.GetActivedesactive(module, action);
            DateTime dateD = Convert.ToDateTime(ds.Tables[0].Rows[0]["ActiveDesactive_DateD"].ToString());
            DateTime dateF = Convert.ToDateTime(ds.Tables[0].Rows[0]["ActiveDesactive_DateF"].ToString());
            if (dateactuel <= dateF && dateactuel >= dateD) activ = 1;
            return activ;
        }
        public void UpdateArticlePrevisionHab(SGPL_ARTICLE_PREVISION Articlepreviv,int exp)
        {
            dal_previs.UpdateArticlePrevisionHab(Articlepreviv,exp);
        }
        public void UpdatePrevisionFlag(int idPrev,int flag)
        {
            dal_previs.UpdatePrevisionFlag(idPrev,flag);
        }
        public DataSet GetArticlePrevisionHabForMod(string idAgent)
        {
            return dal_previs.GetArticlePrevisionHabForMod(idAgent);
        }
        public DataSet GetPrevisionByModuleId(int annee, int module, int agent)
        {
            return dal_previs.GetPrevisionByModuleId(annee, module, agent);
        }
        public DataSet GetPrevisionByModule(int annee, int module, string agent, int EtablissementId)
        {
            return dal_previs.GetPrevisionByModule(annee, module, agent, EtablissementId);
        }
        public void UpdateArticlePrevision(SGPL_ARTICLE_PREVISION Articlepreviv)
        {
            dal_previs.UpdateArticlePrevision(Articlepreviv); 
              
        }
        public void UpdateStatutPrevision(int prevId, int statut, int userId)
        {
            dal_previs.UpdateStatutPrevision(prevId,statut,userId);
        }
        public DataSet GetStatutPrevision(int PrevisionID)
        {
           return  dal_previs.GetStatutPrevision(PrevisionID);
        }

        public DataSet GetIdPrevisionByEtablissement(int IdEtablissement)
        {

            return dal_previs.GetIdPrevisionByEtablissement(IdEtablissement);

        }
        public DataSet GetPrevisionToValidByModule(int Etablissement_PoleId, int Module)
        {
            return dal_previs.GetPrevisionToValidByModule(Etablissement_PoleId, Module);
        }
        public DataSet GetPrevisionToValidByEtablissement(int Etablissement_PoleId, int Module, int EtablissementId)
        {

            return dal_previs.GetPrevisionToValidByEtablissement(Etablissement_PoleId, Module, EtablissementId);

        }
        public DataSet GetPrevisionToValid(int GetPrevisionToValid)
        {
            return dal_previs.GetPrevisionToValid(GetPrevisionToValid);

        }
        public DataSet GetIdPrevisionToValidByPole(int Etablissement_PoleId)
        {
            return dal_previs.GetIdPrevisionToValidByPole(Etablissement_PoleId);
        }
        public DataSet GetIdPrevisionToValidByPoleHab(int Etablissement, int module, string type)
        {
            return dal_previs.GetIdPrevisionToValidByPoleHab(Etablissement,module ,type );
        }
        public DataSet GetPrevisionToConsultDrLiva(int module,int OL)
        {
            return dal_previs.GetPrevisionToConsultDrLiva(module,OL);

        }
        public DataSet GetPrevisionToConsultDrLiva_1(int module, int OL, int ID_Mag)
        {
            return dal_previs.GetPrevisionToConsultDrLiva_1(module, OL,ID_Mag);

        }
        public DataSet GetLotForOL(string Etablissement_PoleId, int Module)
        {
            return dal_previs.GetLotForOL(Etablissement_PoleId, Module);

        }
        public DataSet GetPrevisionForValidHabil(string Etablissement_PoleId, int Module,string type,int statut)
        {
            return dal_previs.GetPrevisionForValidHabil(Etablissement_PoleId, Module,type,statut );

        }
        public DataSet GetExpeditionAchat(string Etablissement_PoleId, int Module, string type)
        {
            return dal_previs.GetExpeditionAchat(Etablissement_PoleId, Module, type);

        }
        public DataSet GetPrevisionForValidImp(string Etablissement_PoleId, int Module, string type, int statut)
        {
            return dal_previs.GetPrevisionForValidImp(Etablissement_PoleId, Module, type, statut);

        }
        public void UpdateOrdreLivraison_ArticlePrevision(int OrdreLivraisonId, int IdArticle,int taille, DateTime date)
        {
            dal_previs.UpdateOrdreLivraison_ArticlePrevision(OrdreLivraisonId, IdArticle,taille, date);

        }
        public void UpdateOrdreLivraison_ArticlePrevisionImp(int OrdreLivraisonId, string IdArticle, DateTime date)
        {
            dal_previs.UpdateOrdreLivraison_ArticlePrevisionImp(OrdreLivraisonId, IdArticle, date);

        }

        public int InsertOrderLivraison(int OrdreLivraison_Numero, int OrdreLivraison_ModuleId, int OrdreLivraison_UtilisateurId, DateTime OrdreLivraison_DateLivraison)
        {

            return dal_previs.InsertOrderLivraison(OrdreLivraison_Numero, OrdreLivraison_ModuleId, OrdreLivraison_UtilisateurId, OrdreLivraison_DateLivraison);

        }
        public DataSet INTER_GetUserByEtab(string codeEtab)
        {
            return dal_previs.INTER_GetUserByEtab(codeEtab);
        }
        public DataSet INTER_GetPoleByEtab(string codeEtab)
        {
            return dal_previs.INTER_GetPoleByEtab(codeEtab);
        }
        public DataSet INTER_GetDirectionByEtab(string codeEtab)
        {
            return dal_previs.INTER_GetDirectionByEtab(codeEtab);
        }
        public DataSet INTER_GetEtabByEtab(string codeEtab)
        {
            return dal_previs.INTER_GetEtabByEtab(codeEtab);
        }
        public void DeleteArticlePrevision(int idArticleprev)
        {
            dal_previs.DeleteArticlePrevision(idArticleprev);
        }
        public int GetReelCode(int code)
        {
            DataSet ds = dal_previs.GetReelCode(code);
            if (ds.Tables[0].Rows.Count != 0)
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            else return 0;
        }
        public DataSet SGPL_GetPrevisionArticleToConsulte(int codeEtab, int annee)
        {

            return dal_previs.SGPL_GetPrevisionArticleToConsulte(codeEtab, annee);
        }

        public DataSet SGPL_CheckStatutPrevision(int Id_Prevision)
        {

            return dal_previs.SGPL_CheckStatutPrevision(Id_Prevision);
        }
        public void Basculement(int Annee, int module)
        {
             dal_previs.Basculement( Annee,  module);
                    }
        public DataSet GetLblEtb(int Etablissement_Id)
        {
            return dal_previs.GetLblEtb(Etablissement_Id);
        }

        public DataSet Verifier_Article_OL(string ArticleId)
        {
            return dal_previs.Verifier_Article_OL(ArticleId);
        }
        public DataSet GetArticle_Comptabilise(DateTime  date)
        {
            return dal_previs.SGPL_GetArticle_Comptabilise(date);
        }

        
    }
}
