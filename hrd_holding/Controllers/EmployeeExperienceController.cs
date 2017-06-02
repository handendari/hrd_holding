using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeExperienceController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeExperienceController");
        private mEmployeeExperienceService _empExpService;
        //private ManageString _mString;

        public EmployeeExperienceController()
        {
            _empExpService = new mEmployeeExperienceService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.entry_date = DateTime.Now;
            pModel.entry_user = "it";

            var vResp = _empExpService.InsertEmployeeExperience(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeExperience(mEmployeeExperienceModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vResp = _empExpService.UpdateEmployeeExperience(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeExperienceList(string pEmployeeCode)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listExp = _empExpService.GetEmployeeExperienceList(pEmployeeCode);

            return Json(new { listExp },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeExperience(string pEmployeeCode, int pSeqNo)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empExpService.DeleteEmployeeExperience(pEmployeeCode, pSeqNo);


            return Json(new { vResp });
        }

    }
}
