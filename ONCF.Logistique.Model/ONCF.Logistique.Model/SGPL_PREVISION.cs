using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelClasse
{
   public class SGPL_PREVISION
    {
        private int _Prevision_Id	;
        private int _Prevision_EtablissementId	;
        private int _Prevision_UtilisateurId;
        private int _Prevision_ModuleId;
        private string _Prevision_Agent;
        private int _Prevision_Flag;
        public SGPL_PREVISION()   { }

        public string Prevision_Agent
        {
            get { return _Prevision_Agent; }
            set { this._Prevision_Agent = value; }
        }
        public int Prevision_Id
        {
            get { return _Prevision_Id; }
            set { this._Prevision_Id = value; }
        }
        public int Prevision_Flag
        {
            get { return _Prevision_Flag; }
            set { this._Prevision_Flag = value; }
        }
        public int Prevision_EtablissementId
        {
            get { return _Prevision_EtablissementId; }
            set { this._Prevision_EtablissementId = value; }
        }
        public int Prevision_UtilisateurId
        {
            get { return _Prevision_UtilisateurId; }
            set { this._Prevision_UtilisateurId = value; }
        }
        public int Prevision_ModuleId
        {
            get { return _Prevision_ModuleId; }
            set { this._Prevision_ModuleId = value; }
        }
    }
}
