using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeSkillService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeSkillService");
        private readonly mEmployeeSkillRepo _repoSkill;

        public mEmployeeSkillService()
        {
            _repoSkill = new mEmployeeSkillRepo();
        }

        public List<mEmployeeSkillModel> GetEmployeeSkillList(string pEmployeeCode)
        { 
            var vList = new List<mEmployeeSkillModel>();
            vList = _repoSkill.getEmployeeSkillList(pEmployeeCode);
            return vList;
        }

        public mEmployeeSkillModel GetEmployeeSkillInfo(string pEmployeeCode,int pSeqNo)
        {
            var vModel = new mEmployeeSkillModel();
            vModel = _repoSkill.getEmployeeSkillInfo(pEmployeeCode,pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeSkill(mEmployeeSkillModel pModel )
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoSkill.UpdateEmployeeSkill(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeSkill(mEmployeeSkillModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoSkill.getEmployeeSkillSeqNo(pModel.employee_code);

            //Log.Debug(DateTime.Now + " =====>>>>>>   Seq No Edu : " + pModel.seq_no);

            var vModel = _repoSkill.InsertEmployeeSkill(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeSkill(string pEmployeeCode,int pSeqNo)
        {
            var vModel = _repoSkill.DeleteEmployeeSkill(pEmployeeCode,pSeqNo);
            return vModel;
        }

    }
}