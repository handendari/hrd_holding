using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mSubTitleService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mSubTitleService");
        private readonly mSubtitleRepo _repoSub;

        public mSubTitleService()
        {
            _repoSub = new mSubtitleRepo();
        }

        public ResponseModel GetSubTitleList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoSub.getSubtitleList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mSubtitleModel GetSubTitleInfo(int pStatusCode)
        {
            var vModel = _repoSub.getSubtitleInfo(pStatusCode);
            return vModel;
        }


        public ResponseModel UpdateSubTitle(mSubtitleModel pModel)
        {
            var vModel = _repoSub.UpdateSubtitle(pModel);
            return vModel;
        }

        public ResponseModel InsertSubTitle(mSubtitleModel pModel)
        {
            pModel.subtitle_code = _repoSub.getSubtitleSeqNo();

            var vModel = _repoSub.InsertSubtitle(pModel);
            return vModel;
        }

        public ResponseModel DeleteSubTitle(int pCode)
        {
            var vModel = _repoSub.DeleteSubtitle(pCode);
            return vModel;
        }
        
    }
}