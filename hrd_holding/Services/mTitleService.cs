using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mTitleService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mTitleService");
        private readonly mTitleRepo _repoTitle;

        public mTitleService()
        {
            _repoTitle = new mTitleRepo();
        }

        public ResponseModel GetTitleList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoTitle.getTitleList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mTitleModel GetTitleInfo(string pCountryCode)
        {
            var vModel = new mTitleModel();
            vModel = _repoTitle.getTitleInfo(pCountryCode);
            return vModel;
        }


        public ResponseModel UpdateTitle(mTitleModel pModel)
        {
            var vModel = _repoTitle.UpdateTitle(pModel);
            return vModel;
        }

        public ResponseModel InsertTitle(mTitleModel pModel)
        {
            var vModel = _repoTitle.InsertTitle(pModel);
            return vModel;
        }

        public ResponseModel DeleteTitle(int pTitleCode)
        {
            var vModel = _repoTitle.DeleteTitle(pTitleCode);
            return vModel;
        }
        
    }
}