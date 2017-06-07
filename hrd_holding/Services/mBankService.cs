using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mBankService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mBankService");
        private readonly mBankRepo _repoBank;

        public mBankService()
        {
            _repoBank = new mBankRepo();
        }

        public ResponseModel GetBankList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoBank.getBankList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mBankModel GetBankInfo(string pBankCode)
        {
            var vModel = _repoBank.getBankInfo(pBankCode);
            return vModel;
        }


        public ResponseModel UpdateBank(mBankModel pModel)
        {
            var vModel = _repoBank.UpdateBank(pModel);
            return vModel;
        }

        public ResponseModel InsertBank(mBankModel pModel)
        {
            var vModel = _repoBank.InsertBank(pModel);
            return vModel;
        }

        public ResponseModel DeleteBank(string pBankCode)
        {
            var vModel = _repoBank.DeleteBank(pBankCode);
            return vModel;
        }
        
    }
}