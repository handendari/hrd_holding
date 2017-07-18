using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentFamService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentFamService");
        private readonly hrdRecruitmentFamRepo _repoRecFam;

        public hrdRecruitmentFamService()
        {
            _repoRecFam = new hrdRecruitmentFamRepo();
        }

        public List<hrdRecruitmentFamModel> GetRecruitmentFamList(int pRecruitmentId)
        {
            var vList = _repoRecFam.getRecruitmentFamilyList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentFamModel GetRecruitmentFamInfo(int pId)
        {
            var vModel = _repoRecFam.getRecruitmentFamilyInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentFam(hrdRecruitmentFamModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecFam.updateRecruitmentFamily(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentFam(hrdRecruitmentFamModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoRecFam.getRecruitmentFamilySeqNo(pModel.recruitment_id);

            var vModel = _repoRecFam.InsertRecruitmentFamily(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentFam(int pId)
        {
            var vModel = _repoRecFam.DeleteRecruitmentFamily(pId);
            return vModel;
        }

    }
}