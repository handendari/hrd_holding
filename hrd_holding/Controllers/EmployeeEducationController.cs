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
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeEducationController");
        private mEmployeeEducationService _empEduService;
        //private ManageString _mString;

        public EmployeeEducationController()
        {
            _empEduService = new mEmployeeEducationService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertEmployeeEducation(mEmployeeEducationModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = _empEduService.InsertEmployeeEducation(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeEducation(mEmployeeEducationModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            var vResp = new ResponseModel();
            vResp = _empEduService.UpdateEmployeeEducation(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeEducationList(string pEmployeeCode)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listEdu = _empEduService.GetEmployeeEducationList(pEmployeeCode);

            return Json(new { listEdu },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeEducation(string pEmployeeCode, int pSeqNo)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empEduService.DeleteEmployeeEducation(pEmployeeCode, pSeqNo);


            return Json(new { vResp });
        }

    }
}
