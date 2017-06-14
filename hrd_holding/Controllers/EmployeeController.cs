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
    public class EmployeeController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("EmployeeController");
        private mEmployeeService _empService;
        private ManageString _mString;

        public EmployeeController()
        {
            _empService = new mEmployeeService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeDetail()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetEmployeeInfoAll()
        {
            var vModel = new EmployeeModelAll();

            var vEmployeeCode = Request["employeecode"].ToString();
            var vSeqNo = int.Parse(Request["seqno"]);

            vModel = _empService.GetEmployeeInfo(vEmployeeCode, vSeqNo);

            return Json(new
            {
                vModel.empModel,
                vModel.listFamily,
                vModel.listEducation,
                vModel.listSkill,
                vModel.listExperience,
                vModel.listTrain,
                vModel.listContract,
                vModel.listCompany
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public dynamic GetEmployeeList(int pCompanyCode,int pBranchCode)
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
            var vObjRes = _empService.GetEmployeeList(pCompanyCode,pBranchCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mEmployeeModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.employee_code,
                        vItem.employee_name,
                        vItem.emp_address,
                        vItem.company_name,
                        vItem.department_name,
                        vItem.division_name,
                        vItem.level_name,
                        vItem.entry_date
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetEmployeeLookUp(int pCompanyCode,int pBranchCode)
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
            var vObjRes = _empService.GetEmployeeList(pCompanyCode,pBranchCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mEmployeeModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.employee_code,
                        vItem.nik,
                        vItem.employee_name,
                        vItem.department_name
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
