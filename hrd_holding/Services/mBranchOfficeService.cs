using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mBranchOfficeService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("BranchOfficeService");
        private readonly mBranchOfficeRepo _repoBranch;

        public mBranchOfficeService()
        {
            _repoBranch = new mBranchOfficeRepo();
        }

        public ResponseModel GetBranchOfficeList(int pCompanyCode, int? pPageNum = 0, int? pPageSize = 0, 
                                                 string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoBranch.getBranchOfficeList(pCompanyCode, vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mBranchOfficeModel GetBranchOfficeInfo(int pBranchCode)
        {
            var vModel = _repoBranch.getBranchOfficeInfo(pBranchCode);
            return vModel;
        }

        public ResponseModel UpdateBranchOffice(mBranchOfficeModel pModel)
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoBranch.UpdateBranchOffice(pModel);
            return vModel;
        }

        public ResponseModel InsertBranchOffice(mBranchOfficeModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.branch_code = _repoBranch.getBranchOfficeSeqNo();

            //Log.Debug(DateTime.Now + " =====>>>>>>   Seq No Edu : " + pModel.seq_no);

            var vModel = _repoBranch.InsertBranchOffice(pModel);
            return vModel;
        }

        public ResponseModel DeleteBranchOffice(int pBranchCode)
        {
            var vModel = _repoBranch.DeleteBranchOffice(pBranchCode);
            return vModel;
        }

    }
}