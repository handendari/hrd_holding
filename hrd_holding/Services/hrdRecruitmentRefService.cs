using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentRefService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentRefService");
        private readonly hrdRecruitmentRefRepo _repoRecRef;

        public hrdRecruitmentRefService()
        {
            _repoRecRef = new hrdRecruitmentRefRepo();
        }

        public List<hrdRecruitmentRefModel> GetRecruitmentRefList(int pRecruitmentId)
        {
            var vList = _repoRecRef.getRecruitmentRefList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentRefModel GetRecruitmentRefInfo(int pId)
        {
            var vModel = _repoRecRef.getRecruitmentRefInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentRef(hrdRecruitmentRefModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecRef.updateRecruitmentRef(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentRef(hrdRecruitmentRefModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var vModel = _repoRecRef.InsertRecruitmentRef(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentRef(int pId)
        {
            var vModel = _repoRecRef.DeleteRecruitmentRef(pId);
            return vModel;
        }

    }
}