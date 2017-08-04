using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentExpController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentExpController");
        private hrdRecruitmentExpService _recExpService;
        //private ManageString _mString;

        public hrdRecruitmentExpController()
        {
            _recExpService = new hrdRecruitmentExpService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentExp(hrdRecruitmentExpModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Exp Seq No : " + pModel.seq_no + ", Name : " + pModel.position_held);

            var vResp = _recExpService.InsertRecruitmentExp(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentExp(hrdRecruitmentExpModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Exp Seq No : " + pModel.seq_no + ", name : " + pModel.position_held);

            var vResp = _recExpService.UpdateRecruitmentExp(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetRecruitmentExpList(int pRequestId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listExp = _recExpService.GetRecruitmentExpList(pRequestId);

            return Json(new { listExp },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentExp(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recExpService.DeleteRecruitmentExp(pId);


            return Json(new { vResp });
        }

    }
}
