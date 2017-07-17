using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentMemService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentMemService");
        private readonly hrdRecruitmentMemberRepo _repoRecMem;

        public hrdRecruitmentMemService()
        {
            _repoRecMem = new hrdRecruitmentMemberRepo();
        }

        public List<hrdRecruitmentMemberModel> GetRecruitmentMemList(int pRecruitmentId)
        {
            var vList = _repoRecMem.getRecruitmentMemberList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentMemberModel GetRecruitmentMemInfo(int pId)
        {
            var vModel = _repoRecMem.getRecruitmentMemberInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentMem(hrdRecruitmentMemberModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecMem.updateRecruitmentMember(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentMem(hrdRecruitmentMemberModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var vModel = _repoRecMem.InsertRecruitmentMember(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentMem(int pId)
        {
            var vModel = _repoRecMem.DeleteRecruitmentMember(pId);
            return vModel;
        }

    }
}