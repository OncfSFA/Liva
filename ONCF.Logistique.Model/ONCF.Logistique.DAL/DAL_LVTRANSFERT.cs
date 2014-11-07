using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using ModelClasse;


namespace DAL
{
    public class DAL_LVTRANSFERT
    {
        private DataSet Ds = new DataSet();
        private DatabaseHelper db = new DatabaseHelper();
        public void AjouteLVTRANSFERT(LVTRANSFERT ESCALA)
        {

            db.AddParameter("@trf_cod", ESCALA._trf_cod);
            db.AddParameter("@trf_filler1", ESCALA._trf_filler1);
            db.AddParameter("@trf_dte", ESCALA._trf_dte);
            db.AddParameter("@trf_nmcl", ESCALA._trf_nmcl);
            db.AddParameter("@trf_etab", ESCALA._trf_etab);
            db.AddParameter("@trf_filler2", ESCALA._trf_filler2);
            db.AddParameter("@trf_dm", ESCALA._trf_dm);
            db.AddParameter("@trf_filler3", ESCALA._trf_filler3);
            db.AddParameter("@trf_imp", ESCALA._trf_imp);
            db.AddParameter("@trf_qte", ESCALA._trf_qte);
            db.AddParameter("@trf_filler4", ESCALA._trf_filler4);
            db.AddParameter("@trf_oe", ESCALA._trf_oe);
            db.AddParameter("@trf_filler5", ESCALA._trf_filler5);
            db.ExecuteNonQuery("SGPL_InsertEscala", CommandType.StoredProcedure);
        }
        public void DeleteLVTRANSFERT()
        {
            db.ExecuteNonQuery("SGPL_DeleteEscala", CommandType.StoredProcedure);
        }
    }
}
