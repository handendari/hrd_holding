using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mCountryService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mCountryService");
        private readonly mCountryRepo _repoCountry;

        public mCountryService()
        {
            _repoCountry = new mCountryRepo();
        }

        public ResponseModel GetCountryList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoCountry.getCountryList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mCountryModel GetCountryInfo(string pCountryCode)
        {
            var vModel = new mCountryModel();
            vModel = _repoCountry.getCountryInfo(pCountryCode);
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