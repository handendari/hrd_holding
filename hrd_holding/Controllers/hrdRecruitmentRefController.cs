using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentRefController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentRefController");
        private hrdRecruitmentRefService _recRefService;
        //private ManageString _mString;

        public hrdRecruitmentRefController()
        {
            _recRefService = new hrdRecruitmentRefService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentRef(hrdRecruitmentRefModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Ref Seq No : " + pModel.seq_no + ", Name : " + pModel.name);

            var vResp = _recRefService.InsertRecruitmentRef(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentRef(hrdRecruitmentRefModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Ref Seq No : " + pModel.seq_no + ", Name : " + pModel.name);

            var vResp = _recRefService.UpdateRecruitmentRef(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetRecruitmentRefList(int pRecId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listRec = _recRefService.GetRecruitmentRefList(pRecId);

            return Json(new { listRec },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentRef(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recRefService.DeleteRecruitmentRef(pId);


            return Json(new { vResp });
        }

    }
}
