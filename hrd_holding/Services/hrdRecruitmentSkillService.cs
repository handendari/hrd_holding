using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentSkillService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentSkillService");
        private readonly hrdRecruitmentSkillRepo _repoRecSkill;

        public hrdRecruitmentSkillService()
        {
            _repoRecSkill = new hrdRecruitmentSkillRepo();
        }

        public List<hrdRecruitmentSkillModel> GetRecruitmentSkillList(int pRecruitmentId)
        {
            var vList = _repoRecSkill.getRecruitmentSkillList(pRecruitmentId);
            return vList;
        }

        public hrdRecruitmentSkillModel GetRecruitmentSkillInfo(int pId)
        {
            var vModel = _repoRecSkill.getRecruitmentSkillInfo(pId);
            return vModel;
        }

        public ResponseModel UpdateRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {
            //pModel.edit_user = "it";
            //pModel.edit_date = DateTime.Now;

            var vModel = _repoRecSkill.updateRecruitmentSkill(pModel);
            return vModel;
        }

        public ResponseModel InsertRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var vModel = _repoRecSkill.InsertRecruitmentSkill(pModel);
            return vModel;
        }

        public ResponseModel DeleteRecruitmentSkill(int pId)
        {
            var vModel = _repoRecSkill.DeleteRecruitmentSkill(pId);
            return vModel;
        }

    }
}