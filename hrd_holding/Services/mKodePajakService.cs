using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mKodePajakService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mKodePajakService");
        private readonly mKodePajakRepo _repoPjk;

        public mKodePajakService()
        {
            _repoPjk = new mKodePajakRepo();
        }

        public ResponseModel GetKodePajakList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoPjk.getKodePajakList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mKodePajakModel GetKodePajakInfo(string pCode)
        {
            var vModel = new mKodePajakModel();
            vModel = _repoPjk.getKodePajakInfo(pCode);
            return vModel;
        }


        public ResponseModel UpdateKodePajak(mKodePajakModel pModel)
        {
            var vModel = _repoPjk.UpdateKodePajak(pModel);
            return vModel;
        }

        public ResponseModel InsertKodePajak(mKodePajakModel pModel)
        {
            var vModel = _repoPjk.InsertKodePajak(pModel);
            return vModel;
        }

        public ResponseModel DeleteKodePajak(int pCode)
        {
            var vModel = _repoPjk.DeleteKodePajak(pCode);
            return vModel;
        }
        
    }
}