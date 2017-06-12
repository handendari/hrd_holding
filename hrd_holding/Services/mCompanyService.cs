using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mCompanyService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mCompanyService");
        private readonly mCompanyRepo _repoCompany;

        public mCompanyService()
        {
            _repoCompany = new mCompanyRepo();
        }

        public ResponseModel GetCompanyList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoCompany.getCompanyList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mCompanyModel GetCompanyInfo(string pCompanyCode)
        {
            var vModel = new mCompanyModel();
            vModel = _repoCompany.getCompanyInfo(pCompanyCode);
            return vModel;
        }

        /*
        public ResponseModel UpdateEmployeeEducation(mEmployeeEducationModel pModel)
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

        public ResponseModel DeleteEmployeeEducation(string pEmployeeCode, int pSeqNo)
        {
            var vModel = _repoEdu.DeleteEmployeeEducation(pEmployeeCode, pSeqNo);
            return vModel;
        }
        */
    }
}