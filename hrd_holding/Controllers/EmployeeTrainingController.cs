using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeTrainingController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeTrainingController");
        private mEmployeeTrainingService _empTrainService;
        //private ManageString _mString;

        public EmployeeTrainingController()
        {
            _empTrainService = new mEmployeeTrainingService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            LOG.Debug(DateTime.Now + "Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.entry_date = DateTime.Now;
            pModel.entry_user = "it";

            var vResp = _empTrainService.InsertEmployeeTraining(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vResp = _empTrainService.UpdateEmployeeTraining(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetEmployeeTrainingList(string pEmployeeCode)
        {
            var listTrn = _empTrainService.GetEmployeeTrainingList(pEmployeeCode);

            return Json(new { listTrn },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteEmployeeTraining(string pEmployeeCode, int pSeqNo)
        {
            //LOG.Debug(DateTime.Now + "MASUK DELETE Emp Code : " + pEmployeeCode + ", Seq No : " + pSeqNo);

            var vResp = _empTrainService.DeleteEmployeeTraining(pEmployeeCode, pSeqNo);


            return Json(new { vResp });
        }

    }
}
