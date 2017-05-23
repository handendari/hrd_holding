using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeFamilyService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeFamilyService");
        private readonly mEmployeeFamilyRepo _repoFamEmp;

        public mEmployeeFamilyService()
        {
            _repoFamEmp = new mEmployeeFamilyRepo();
        }

        public List<mEmployeeFamiliesModel> GetEmployeeFamList(string pEmployeeCode)
        { 
            var vList = new List<mEmployeeFamiliesModel>();
            vList = _repoFamEmp.getEmployeeFamilyList(pEmployeeCode);
            return vList;
        }

        public mEmployeeFamiliesModel GetEmployeeFamInfo(string pEmployeeCode,int pSeqNo)
        {
            var vModel = new mEmployeeFamiliesModel();
            vModel = _repoFamEmp.getEmployeeFamilyInfo(pEmployeeCode,pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeFamily(mEmployeeFamiliesModel pModel )
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoFamEmp.UpdateEmployeeFamily(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeFamily(mEmployeeFamiliesModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var vModel = _repoFamEmp.InsertEmployeeFamily(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeFamily(string pEmployeeCode,int pSeqNo)
        {
            var vModel = _repoFamEmp.DeleteEmployeeFamily(pEmployeeCode,pSeqNo);
            return vModel;
        }

    }
}