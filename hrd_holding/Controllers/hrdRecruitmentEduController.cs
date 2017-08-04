using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentEduController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentEduController");
        private hrdRecruitmentEduService _recEduService;
        //private ManageString _mString;

        public hrdRecruitmentEduController()
        {
            _recEduService = new hrdRecruitmentEduService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Edu Seq No : " + pModel.seq_no + ", School : " + pModel.school);

            var vResp = _recEduService.InsertRecruitmentEdu(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentEdu(hrdRecruitmentEduModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Edu Seq No : " + pModel.seq_no + ", School : " + pModel.school);

            var vResp = _recEduService.UpdateRecruitmentEdu(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        public dynamic GetRecruitmentEduList(int pRequestId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            LOG.Debug(DateTime.Now + " Request ID : " + pRequestId);

            var listEdu = _recEduService.GetRecruitmentEduList(pRequestId);

            return Json(new { listEdu },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentEdu(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recEduService.DeleteRecruitmentEdu(pId);


            return Json(new { vResp });
        }

    }
}
