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
    public class ReqRecruitmentController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("ReqRecruitmentController");
        private hrdReqRecruitmentService _reqService;
        private mCompanyService _mCompService;
        private mBranchOfficeService _mBranchService;
        private ManageString _mString;

        public ReqRecruitmentController()
        {
            _reqService = new hrdReqRecruitmentService();
            _mCompService = new mCompanyService();
            _mBranchService = new mBranchOfficeService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestDetail()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetRequestInfo(string pBranchCode, string pIdCode)
        {
            var vModel = new hrdReqReqruitmentModel();
            var vIdCode = pIdCode == "" ? 0 : int.Parse(pIdCode);

            if (pIdCode == "")
            {
                //var vCompany = _mCompService.GetCompanyInfo(pCompanyCode);
                var vBranch = _mBranchService.GetBranchOfficeInfo(int.Parse(pBranchCode));

                vModel.company_code = vBranch.company_code;
                vModel.int_company = vBranch.int_company;
                vModel.company_name = vBranch.company_name;

                vModel.branch_code = vBranch.branch_code;
                vModel.int_branch = vBranch.int_branch;
                vModel.branch_name = vBranch.branch_name;

                vModel.sex = 0;
                vModel.marital_status = 0;
                vModel.source_employee = 0;
                vModel.work_plan = DateTime.Now;
                vModel.date_req = DateTime.Now;
            }
            else
            {
                vModel = _reqService.GetRequestInfo(vIdCode);
            }

            return Json(new
            {
                vModel.id,
                vModel.company_code,
                vModel.int_company,
                vModel.company_name,
                vModel.branch_code,
                vModel.int_branch,
                vModel.branch_name,
                vModel.date_req,
                vModel.no_req,
                vModel.position_need,
                vModel.reason,
                vModel.sex,
                vModel.age_min,
                vModel.education,
                vModel.job_experience,
                vModel.english_skill,
                vModel.certificate,
                vModel.marital_status,
                vModel.job_title,
                vModel.job_purpose,
                vModel.responsibility,
                vModel.count_staff,
                vModel.authority,
                vModel.job_relationship,
                vModel.job_self,
                vModel.source_employee,
                vModel.work_plan,
                vModel.note,
                vModel.count_needed,
                vModel.request_by,
                vModel.flag_status,
                vModel.flag_approval,
                vModel.user_approval,
                vModel.entry_date,
                vModel.entry_user
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public dynamic GetRequestList(int pCompanyCode, int pBranchCode, int pFlagStatus)
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

            // Get Data
            var vObjRes = _reqService.GetRequestList(pCompanyCode, pBranchCode, pFlagStatus, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<hrdReqReqruitmentModel>;

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
                        vItem.date_req,
                        vItem.no_req,
                        vItem.position_need,
                        vItem.reason,
                        vItem.company_name,
                        vItem.source_employee,
                        vItem.work_plan,
                        vItem.note,
                        vItem.flag_approval
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        //public dynamic GetEmployeeLookUp(int pCompanyCode, int pBranchCode)
        //{
        //    //LOG.Debug(DateTime.Now + " ---- PageNum : " + pagenum + " PageSize : " + pagesize);

        //    String where = "";
        //    int filterscount = 0;
        //    if (null != Request["filterscount"])
        //    {
        //        try
        //        {
        //            filterscount = int.Parse(Request["filterscount"]);
        //        }
        //        catch (FormatException nfe) { }
        //        {
        //        }
        //    }
        //    if (filterscount > 0)
        //    {
        //        var filtervalue = new List<string>();
        //        var filtercondition = new List<string>(); ;
        //        var filterdatafield = new List<string>(); ;
        //        var filteroperator = new List<string>(); ;

        //        for (int i = 0; i < filterscount; i++)
        //        {
        //            filtervalue.Add(Request["filtervalue" + i]);
        //            filtercondition.Add(Request["filtercondition" + i]);
        //            filterdatafield.Add(Request["filterdatafield" + i]);
        //            filteroperator.Add(Request["filteroperator" + i]);
        //        }
        //        where = _mString.ConstructWhere(false, filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
        //    }

        //    var orderby = _mString.ConstructOrderBy(Request["sortdatafield"], Request["sortorder"]);
        //    var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
        //    var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

        //    //LOG.Debug(DateTime.Now + " WHERE : " + where);
        //    //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

        //    // Get Data
        //    var vObjRes = _reqService.GetEmployeeList(pCompanyCode, pBranchCode, pagenum, pagesize, where, orderby);
        //    var vList = vObjRes.objResult as IEnumerable<mEmployeeModel>;

        //    var totalRecords = vObjRes.total_record;
        //    var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

        //    return Json(new
        //    {
        //        TotalRows = totalRecords,
        //        Rows = (
        //            from vItem in vList
        //            select new
        //            {
        //                vItem.employee_code,
        //                vItem.nik,
        //                vItem.employee_name,
        //                vItem.department_name
        //            }).ToArray()
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public dynamic InsertRequest(hrdReqReqruitmentModel pModel)
        {
            LOG.Debug(DateTime.Now + "No Req : " + pModel.no_req);

            var vResp = _reqService.InsertRequest(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateRequest(hrdReqReqruitmentModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk ControllerNo Req : " + pModel.no_req);

            var vResp = new ResponseModel();
            vResp = _reqService.UpdateRequest(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic DeleteRequest(int pIdCode)
        {
            LOG.Debug(DateTime.Now + "MASUK DELETE Id Req : " + pIdCode);

            var vResp = _reqService.DeleteRequest(pIdCode);


            return Json(new { vResp });
        }

    }
}
