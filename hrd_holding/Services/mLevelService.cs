using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mLevelService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mLevelService");
        private readonly mLevelRepo _repoLevel;

        public mLevelService()
        {
            _repoLevel = new mLevelRepo();
        }

        public ResponseModel GetLevelList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoLevel.getLevelList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mLevelModel GetLevelInfo(int pLevelCode)
        {
            var vModel = _repoLevel.getLevelInfo(pLevelCode);
            return vModel;
        }


        public ResponseModel UpdateLevel(mLevelModel pModel)
        {
            pModel.user_edit = "it";
            pModel.date_edit = DateTime.Now;

            var vModel = _repoLevel.UpdateLevel(pModel);
            return vModel;
        }

        public ResponseModel InsertLevel(mLevelModel pModel)
        {
            pModel.user_entry = "it";
            pModel.date_entry = DateTime.Now;

            var vModel = _repoLevel.InsertLevel(pModel);
            return vModel;
        }

        public ResponseModel DeleteLevel(int pLevelCode)
        {
            var vModel = _repoLevel.DeleteLevel(pLevelCode);
            return vModel;
        }
        
    }
}