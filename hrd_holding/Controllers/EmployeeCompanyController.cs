using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeCompanyController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeCompanyController");
        private mEmployeeCompanyService _empCompService;
        //private ManageString _mString;

        public EmployeeCompanyController()
        {
            _empCompService = new mEmployeeCompanyService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertCompanyCompany(mEmployeeCompanyModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = _empCompService.InsertEmployeeCompany(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeCompany(mEmployeeCompanyModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = new ResponseModel();
            vResp = _empCompService.UpdateEmployeeCompany(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeCompanyList(string pEmployeeCode)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            LOG.Debug(DateTime.Now + " Employee Code : " + pEmployeeCode);

            var listCompany = _empCompService.GetEmployeeCompanyList(pEmployeeCode);

            return Json(new { listCompany },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeCompany(string pEmployeeCode, int pSeqNo)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empCompService.DeleteEmployeeCompany(pEmployeeCode, pSeqNo);


            return Json(new { vResp });
        }

    }
}
