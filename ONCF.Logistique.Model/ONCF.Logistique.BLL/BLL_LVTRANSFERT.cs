using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using ModelClasse;

namespace BLL
{
   public class BLL_LVTRANSFERT
    {
       DAL_LVTRANSFERT dal_LVTRANSFERT = new DAL_LVTRANSFERT();
       public void AjouteLVTRANSFERT(LVTRANSFERT LV)
       {
           dal_LVTRANSFERT.AjouteLVTRANSFERT(LV);
       }
       public void DeleteLVTRANSFERT()
       {
           dal_LVTRANSFERT.DeleteLVTRANSFERT();
       }
    }
}
