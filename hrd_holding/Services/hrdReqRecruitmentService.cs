using hrd_holding.GeneralFunction;
using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdReqRecruitmentService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdReqRecruitmentService");
        private readonly hrdReqReqruitmentRepo _repoReq;

        public hrdReqRecruitmentService()
        {
            _repoReq = new hrdReqReqruitmentRepo();
        }


        public ResponseModel GetRequestList(int pCompany,int pBranchCode,int pFlagStatus, 
            int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            Log.Debug(DateTime.Now + " pPage : " + pPageNum + " pRows : " + pPageSize);

            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoReq.getRequestList(pCompany,pBranchCode,pFlagStatus, vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetEmployeeList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public hrdReqReqruitmentModel GetRequestInfo(int pIdNo)
        {
            var vModel = _repoReq.getRequestInfo(pIdNo);

            //Log.Debug(DateTime.Now + " SERVICE EMP CODE : " + vModel.employee_code);

            return vModel;
        }

        public ResponseModel InsertRequest(hrdReqReqruitmentModel pModel)
        {
            pModel.id = _repoReq.getRequestSeqNo();
            
            pModel.entry_date = DateTime.Now;
            pModel.entry_user = ""; //aa.GetString("entry_user"),

            var vResp = _repoReq.InsertRequest(pModel);

            return vResp;
        }

        public ResponseModel DeleteRequest(int pIdNo)
        {
            var vResp = _repoReq.DeleteRequset(pIdNo);

            return vResp;
        }

        public ResponseModel UpdateRequest(hrdReqReqruitmentModel pModel)
        {
            pModel.edit_date = DateTime.Now;
            pModel.edit_user = "it";


            var vResp = _repoReq.UpdateRequest(pModel);

            return vResp;
        }

        //public bool Validation(CountryModel model, out string message)
        //{
        //    bool isValid = true;
        //    message = "";

        //    if (String.IsNullOrEmpty(model.CountryName) || model.CountryName.Trim() == "")
        //    {
        //        message = "Invalid Country Name";
        //        isValid = false;
        //    }
        //    else if (String.IsNullOrEmpty(model.IntCountry) || model.IntCountry.Trim() == "")
        //    {
        //        message = "Invalid Int Country";
        //        isValid = false;
        //    }

        //    ContinentalModel selectedContinent = _continentRepo.GetContinentalInfo(model.ContinentalId);
        //    if (selectedContinent == null)
        //    {
        //        message = "Invalid Continental Code";
        //        isValid = false;
        //    }

        //    return isValid;
        //}

        //public bool ValidationInsert(CountryModel model, out string message)
        //{
        //    return this.Validation(model, out message);
        //}

        //public bool ValidationUpdate(CountryModel model, out string message)
        //{
        //    bool isValid = this.Validation(model, out message);

        //    if (model.Id == 0)
        //    {
        //        message = "Invalid Country Code";
        //        isValid = false;
        //    }
        //    else
        //    {
        //        CountryModel selectedCountry = this.GetCountryInfo(model.Id);
        //        if (selectedCountry == null)
        //        {
        //            message = "Invalid Country Code";
        //            isValid = false;
        //        }
        //    }

        //    return isValid;
        //}

    }
}