using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mStatusService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("mStatusService");
        private readonly mEmployeeStatusRepo _repoStatus;

        public mStatusService()
        {
            _repoStatus = new mEmployeeStatusRepo();
        }

        public ResponseModel GetStatusList(int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoStatus.getEmployeeStatusList(vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mEmployeeStatusModel GetStatusInfo(int pStatusCode)
        {
            var vModel = _repoStatus.getEmployeeStatusInfo(pStatusCode);
            return vModel;
        }


        public ResponseModel UpdateStatus(mEmployeeStatusModel pModel)
        {
            var vModel = _repoStatus.UpdateEmployeeStatus(pModel);
            return vModel;
        }

        public ResponseModel InsertStatus(mEmployeeStatusModel pModel)
        {
            pModel.status_code = _repoStatus.getStatusSeqNo();

            var vModel = _repoStatus.InsertEmployeeStatus(pModel);
            return vModel;
        }

        public ResponseModel DeleteStatus(int pStatusCode)
        {
            var vModel = _repoStatus.DeleteEmployeeStatus(pStatusCode);
            return vModel;
        }
        
    }
}