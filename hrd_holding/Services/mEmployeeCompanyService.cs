using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeCompanyService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeCompanyService");
        private readonly mEmployeeCompanyRepo _repoComp;

        public mEmployeeCompanyService()
        {
            _repoComp = new mEmployeeCompanyRepo();
        }

        public List<mEmployeeCompanyModel> GetEmployeeCompanyList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeCompanyModel>();
            vList = _repoComp.getEmployeeCompanyList(pEmployeeCode);
            return vList;
        }

        public mEmployeeCompanyModel GetEmployeeCompanyInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeCompanyModel();
            vModel = _repoComp.getEmployeeCompanyInfo(pEmployeeCode, pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoComp.UpdateEmployeeCompany(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;

            var seqno = _repoComp.getEmployeeCompanySeqNo(pModel.employee_code);

            pModel.seq_no = seqno + 1;
            Log.Debug(DateTime.Now + " =====>>>>>>   Seq No Edu : " + pModel.seq_no);

            var vModel = _repoComp.InsertEmployeeCompany(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeCompany(string pEmployeeCode, int pSeqNo)
        {
            var vResp = new ResponseModel();
            var seqno = _repoComp.getEmployeeCompanySeqNo(pEmployeeCode);
            if (pSeqNo < seqno || pSeqNo == 1)
            {
                vResp.isValid = false;
                if (pSeqNo == 1)
                {
                    vResp.message = "Transaksi Yang Terakhir Tidak Boleh Di Hapus....";
                }
                else
                {
                    vResp.message = "Hanya Boleh Menghapus Transaksi Yang Terakhir Saja....";
                }
            }
            else
            {
                vResp = _repoComp.DeleteEmployeeCompany(pEmployeeCode, pSeqNo);
            }
            return vResp;
        }

    }
}