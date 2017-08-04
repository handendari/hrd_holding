using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentSkillController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentSkillController");
        private hrdRecruitmentSkillService _recSkillService;
        //private ManageString _mString;

        public hrdRecruitmentSkillController()
        {
            _recSkillService = new hrdRecruitmentSkillService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic InsertRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {
            LOG.Debug(DateTime.Now + "Insert Recruitment Skill Seq No : " + pModel.seq_no + ", Name : " + pModel.skill);

            var vResp = _recSkillService.InsertRecruitmentSkill(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitmentSkill(hrdRecruitmentSkillModel pModel)
        {
            LOG.Debug(DateTime.Now + "Update Recruitment Skill Seq No : " + pModel.seq_no + ", Name : " + pModel.skill);

            var vResp = _recSkillService.UpdateRecruitmentSkill(pModel);


            return Json(new{vResp}, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        public dynamic GetRecruitmentSkillList(int pRequestId)
        {

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            //LOG.Debug(DateTime.Now + " Emp FAMS Code : " + pEmployeeCode);

            var listSkill = _recSkillService.GetRecruitmentSkillList(pRequestId);

            return Json(new { listSkill },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRecruitmentSkill(int pId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id : " + pId);

            var vResp = _recSkillService.DeleteRecruitmentSkill(pId);


            return Json(new { vResp });
        }

    }
}
