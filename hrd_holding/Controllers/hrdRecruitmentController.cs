using hrd_holding.GeneralFunction;
using hrd_holding.Models;
using hrd_holding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrd_holding.Controllers
{
    public class hrdRecruitmentController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("hrdRecruitmentController");
        private hrdRecruitmentService _recService;
        private ManageString _mString;

        public hrdRecruitmentController()
        {
            _recService = new hrdRecruitmentService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecruitmentDetail()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetRecruitmentInfoAll(int pRecId)
        {
            var vModel = new RecruitmentModel_All();

            //var vEmployeeCode = Request["employeecode"].ToString();
            //var vSeqNo = int.Parse(Request["seqno"]);

            vModel = _recService.GetRecuitmentInfo(pRecId);

            return Json(new
            {
                vModel.recModel,
                vModel.listEdu,
                vModel.listExp,
                vModel.listFams,
                vModel.listMem,
                vModel.listRef,
                vModel.listSkill
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public dynamic GetRecruitmentList(int pCompanyCode,int pBranchCode)
        {
            //LOG.Debug(DateTime.Now + " ---- PageNum : " + pagenum + " PageSize : " + pagesize);

            String where = "";
            int filterscount = 0;
            if (null != Request["filterscount"])
            {
                try
                {
                    filterscount = int.Parse(Request["filterscount"]);
                }
                catch (FormatException nfe) { }
                {
                }
            }
            if (filterscount > 0)
            {
                var filtervalue = new List<string>();
                var filtercondition = new List<string>(); ;
                var filterdatafield = new List<string>(); ;
                var filteroperator = new List<string>(); ;

                for (int i = 0; i < filterscount; i++)
                {
                    filtervalue.Add(Request["filtervalue" + i]);
                    filtercondition.Add(Request["filtercondition" + i]);
                    filterdatafield.Add(Request["filterdatafield" + i]);
                    filteroperator.Add(Request["filteroperator" + i]);
                }
                where = _mString.ConstructWhere(false, filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
            }

            var orderby = _mString.ConstructOrderBy(Request["sortdatafield"], Request["sortorder"]);
            var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
            var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

            //LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            var vObjRes = _recService.GetRecruitmentList(pCompanyCode,pBranchCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<hrdRecruitmentModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.id,
                        vItem.req_id,
                        vItem.date_started,
                        vItem.nik,
                        vItem.name,
                        vItem.name_employer,
                        vItem.no_employees,
                        vItem.department_name,
                        vItem.title_name,
                        vItem.status_name,
                        vItem.entry_date
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetRecruitmentLookUp(int pCompanyCode,int pBranchCode)
        {
            //LOG.Debug(DateTime.Now + " ---- PageNum : " + pagenum + " PageSize : " + pagesize);

            String where = "";
            int filterscount = 0;
            if (null != Request["filterscount"])
            {
                try
                {
                    filterscount = int.Parse(Request["filterscount"]);
                }
                catch (FormatException nfe) { }
                {
                }
            }
            if (filterscount > 0)
            {
                var filtervalue = new List<string>();
                var filtercondition = new List<string>(); ;
                var filterdatafield = new List<string>(); ;
                var filteroperator = new List<string>(); ;

                for (int i = 0; i < filterscount; i++)
                {
                    filtervalue.Add(Request["filtervalue" + i]);
                    filtercondition.Add(Request["filtercondition" + i]);
                    filterdatafield.Add(Request["filterdatafield" + i]);
                    filteroperator.Add(Request["filteroperator" + i]);
                }
                where = _mString.ConstructWhere(false, filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
            }

            var orderby = _mString.ConstructOrderBy(Request["sortdatafield"], Request["sortorder"]);
            var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
            var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

            //LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            var vObjRes = _recService.GetRecruitmentList(pCompanyCode,pBranchCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<hrdRecruitmentModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.id,
                        vItem.req_id,
                        vItem.date_started,
                        vItem.nik,
                        vItem.name,
                        vItem.name_employer,
                        vItem.no_employees,
                        vItem.department_name,
                        vItem.title_name,
                        vItem.status_name,
                        vItem.entry_date
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertRecruitment(hrdRecruitmentModel pModel)
        {
            LOG.Debug(DateTime.Now + "Req id : " + pModel.req_id + ", employee_name : " + pModel.name);

            var vResp = _recService.InsertRecruitment(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRecruitment(hrdRecruitmentModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller req Id : " + pModel.req_id + ", employee_name : " + pModel.name);

            var vResp = new ResponseModel();
            vResp = _recService.UpdateRecruitment(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteReruitment(int pRecId)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Recruitment Id : " + pRecId);

            var vResp = _recService.DeleteRecruitment(pRecId);


            return Json(new { vResp });
        }

    }
}
