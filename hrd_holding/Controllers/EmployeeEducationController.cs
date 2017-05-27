using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeEducationController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeFamilyController");
        private mEmployeeEducationService _empFamService;
        //private ManageString _mString;

        public EmployeeEducationController()
        {
            _empFamService = new mEmployeeEducationService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertEmployeeFamily(mEmployeeFamiliesModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = _empFamService.InsertEmployeeFamily(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeFamily(mEmployeeFamiliesModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = new ResponseModel();
            vResp = _empFamService.UpdateEmployeeFamily(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeFamilyList(string pEmployeeCode)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listFamily = _empFamService.GetEmployeeFamList(pEmployeeCode);

            return Json(new { listFamily },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeFamily(string pEmployeeCode,int pSeqNo)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empFamService.DeleteEmployeeFamily(pEmployeeCode,pSeqNo);


            return Json(new { vResp });
        }

    }
}
