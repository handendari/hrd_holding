using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentFamController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentFamController");
        private hrdRecruitmentFamService _recFamService;
        //private ManageString _mString;

        public hrdRecruitmentFamController()
        {
            _recFamService = new hrdRecruitmentFamService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentFam(hrdRecruitmentFamModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Fam Seq No : " + pModel.seq_no + ", Name : " + pModel.name);
            
            var vResp = _recFamService.InsertRecruitmentFam(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentFam(hrdRecruitmentFamModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Fam ID : " + pModel.fam_id + ", Name : " + pModel.name);

            var vResp = _recFamService.UpdateRecruitmentFam(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        public dynamic GetRecruitmentFamList(int pRequestId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listFamily = _recFamService.GetRecruitmentFamList(pRequestId);

            return Json(new { listFamily },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentFam(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recFamService.DeleteRecruitmentFam(pId);


            return Json(new { vResp });
        }

    }
}
