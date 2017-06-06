using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mDepartmentService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("DepartmentService");
        private readonly mDepartmentRepo _repoDept;

        public mDepartmentService()
        {
            _repoDept = new mDepartmentRepo();
        }

        public ResponseModel GetDepartmentList(int pCompanyCode, int pBranchCode, 
                                                        int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                //vModel = _repoCountry.getCountryList(vStart, vRows, pWhere, pOrderBy);
                vModel = _repoDept.getDepartmentList(pCompanyCode, pBranchCode,vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetCountryList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public mDepartmentModel GetDepartmentInfo(int pDepartmentCode)
        {
            var vModel = _repoDept.getDepartmentInfo(pDepartmentCode);
            return vModel;
        }

        public ResponseModel UpdateDepartment(mDepartmentModel pModel)
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoDept.UpdateDepartment(pModel);
            return vModel;
        }

        public ResponseModel InsertDepartment(mDepartmentModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.department_code = _repoDept.getDepartmentSeqNo();

            //Log.Debug(DateTime.Now + " =====>>>>>>   Seq No Edu : " + pModel.seq_no);

            var vModel = _repoDept.InsertDepartment(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeSkill(int pDepartmentCode)
        {
            var vModel = _repoDept.DeleteDepartment(pDepartmentCode);
            return vModel;
        }

    }
}