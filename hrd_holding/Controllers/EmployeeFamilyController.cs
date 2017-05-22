using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class EmployeeFamilyController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeFamilyController");
        private mEmployeeFamilyService _empFamService;
        //private ManageString _mString;

        public EmployeeFamilyController()
        {
            _empFamService = new mEmployeeFamilyService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic UpdateEmployeeFamily(mEmployeeFamiliesModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Emp Code : " + pModel.employee_code + ", employee_name : " + pModel.employee_name);

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);
            var vHasil = new ResponseModel();
            vHasil = _empFamService.UpdateEmployeeFamily(pModel);


            return Json(new
            {
                vHasil
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
