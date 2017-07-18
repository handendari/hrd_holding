using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentEduService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentEduService");
        private readonly hrdRecruitmentEduRepo _repoRecEdu;

        public hrdRecruitmentEduService()
        {
            _repoRecEdu = new hrdRecruitmentEduRepo();
        }

        public List<hrdRecruitmentEduModel> GetRecruitmentEduList(int pRecruitmentId)
        {
            var  vList = _repoRecEdu.getRecruitmentEduList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentEduModel GetRecruitmentEduInfo(int pId)
        {
            var vModel = _repoRecEdu.getRecruitmentEduInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecEdu.updateRecruitmentEdu(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoRecEdu.getRecruitmentEduSeqNo(pModel.recruitment_id);

            var vModel = _repoRecEdu.InsertRecruitmentEdu(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentEdu(int pId)
        {
            var vModel = _repoRecEdu.DeleteRecruitmentEdu(pId);
            return vModel;
        }

    }
}