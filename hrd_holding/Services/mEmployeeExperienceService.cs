using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeExperienceService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeExperienceService");
        private readonly mEmployeeExperienceRepo _repoExp;

        public mEmployeeExperienceService()
        {
            _repoExp = new mEmployeeExperienceRepo();
        }

        public List<mEmployeeExperienceModel> GetEmployeeExperienceList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeExperienceModel>();
            vList = _repoExp.getEmployeeExperienceList(pEmployeeCode);
            return vList;
        }

        public mEmployeeExperienceModel GetEmployeeExperienceInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeExperienceModel();
            vModel = _repoExp.getEmployeeExperienceInfo(pEmployeeCode, pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoExp.UpdateEmployeeExperience(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoExp.getEmployeeExperienceSeqNo(pModel.employee_code);

            Log.Debug(DateTime.Now + " =====>>>>>>   Seq No Edu : " + pModel.seq_no);

            var vModel = _repoExp.InsertEmployeeExperience(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeExperience(string pEmployeeCode, int pSeqNo)
        {
            var vModel = _repoExp.DeleteEmployeeExperience(pEmployeeCode, pSeqNo);
            return vModel;
        }

    }
}