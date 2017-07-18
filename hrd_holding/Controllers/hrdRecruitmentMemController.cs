using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentMemController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentMemController");
        private hrdRecruitmentMemService _recMemService;
        //private ManageString _mString;

        public hrdRecruitmentMemController()
        {
            _recMemService = new hrdRecruitmentMemService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentMem(hrdRecruitmentMemberModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Member Seq No : " + pModel.seq_no + ", Name : " + pModel.name);

            var vResp = _recMemService.InsertRecruitmentMem(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentMem(hrdRecruitmentMemberModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Member Seq No : " + pModel.seq_no + ", Name : " + pModel.name);

            var vResp = _recMemService.UpdateRecruitmentMem(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public dynamic GetRecruitmentMemList(int pRecId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listRec = _recMemService.GetRecruitmentMemList(pRecId);

            return Json(new { listRec },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentMem(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recMemService.DeleteRecruitmentMem(pId);


            return Json(new { vResp });
        }

    }
}
