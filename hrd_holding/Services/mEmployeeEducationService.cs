using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeEducationService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeEducationService");
        private readonly mEmployeeEducationRepo _repoEdu;

        public mEmployeeEducationService()
        {
            _repoEdu = new mEmployeeEducationRepo();
        }

        public List<mEmployeeEducationModel> GetEmployeeEducationList(string pEmployeeCode)
        { 
            var vList = new List<mEmployeeEducationModel>();
            vList = _repoEdu.getEmployeeEducationList(pEmployeeCode);
            return vList;
        }

        public mEmployeeEducationModel GetEmployeeEducationInfo(string pEmployeeCode,int pSeqNo)
        {
            var vModel = new mEmployeeEducationModel();
            vModel = _repoEdu.getEmployeeEducationInfo(pEmployeeCode,pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeEducation(mEmployeeEducationModel pModel )
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoEdu.UpdateEmployeeEducation(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeEducation(mEmployeeEducationModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var vModel = _repoEdu.InsertEmployeeEducation(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeEducation(string pEmployeeCode,int pSeqNo)
        {
            var vModel = _repoEdu.DeleteEmployeeEducation(pEmployeeCode,pSeqNo);
            return vModel;
        }

    }
}