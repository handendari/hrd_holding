using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeSkillController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeSkillController");
        private mEmployeeSkillService _empSkillService;
        //private ManageString _mString;

        public EmployeeSkillController()
        {
            _empSkillService = new mEmployeeSkillService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertEmployeeSkill(mEmployeeSkillModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.entry_date = DateTime.Now;
            pModel.entry_user = "it";

            var vResp = _empSkillService.InsertEmployeeSkill(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeSkill(mEmployeeSkillModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vResp = _empSkillService.UpdateEmployeeSkill(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeSkillList(string pEmployeeCode)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listSkill = _empSkillService.GetEmployeeSkillList(pEmployeeCode);

            return Json(new { listSkill },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeSkill(string pEmployeeCode, int pSeqNo)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empSkillService.DeleteEmployeeSkill(pEmployeeCode, pSeqNo);


            return Json(new { vResp });
        }

    }
}
