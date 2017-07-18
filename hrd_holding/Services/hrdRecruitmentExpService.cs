using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentExpService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentExpService");
        private readonly hrdRecruitmentExpRepo _repoRecExp;

        public hrdRecruitmentExpService()
        {
            _repoRecExp = new hrdRecruitmentExpRepo();
        }

        public List<hrdRecruitmentExpModel> GetRecruitmentExpList(int pRecruitmentId)
        {
            var vList = _repoRecExp.getRecruitmentExpList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentExpModel GetRecruitmentExpInfo(int pId)
        {
            var vModel = _repoRecExp.getRecruitmentExpInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentExp(hrdRecruitmentExpModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecExp.updateRecruitmentExp(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentExp(hrdRecruitmentExpModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoRecExp.getRecruitmentExpSeqNo(pModel.recruitment_id);

            var vModel = _repoRecExp.InsertRecruitmentExp(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentExp(int pId)
        {
            var vModel = _repoRecExp.DeleteRecruitmentExp(pId);
            return vModel;
        }

    }
}