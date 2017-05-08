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
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("CountryController");
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

        
        public dynamic GetEmployeeList(int pCompanyCode, string _search, long nd, int rows, int page, string sidx, string sord = "ASC", string filters = "")
        {
            // Get Data
            var vRes = _empService.GetEmployeeList(pCompanyCode,_search, page, rows, sidx, sord, filters);
            var vList = vRes.objResult as IEnumerable<mEmployeeModel>;

            var pageIndex = page;
            var pageSize = rows;
            var totalRecords = vRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            //vList = vList.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from vItem in vList
                    select new
                    {
                        id = vItem.employee_code,
                        cell = new object[] {
                            vItem.employee_code,
                            vItem.employee_name,
                            vItem.emp_address,
                            vItem.company_name,
                            vItem.department_name,
                            vItem.division_name,
                            vItem.level_name,
                            vItem.entry_date
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        //public dynamic GetEmployeeList_Lama(int pCompanyCode,int? filterscount=0,int? groupscount=0,int? pagenum=0,
        //    int pagesize=10,int? recordstartindex=0, int? recordendindex=16,_=14940)
        //{
        [HttpPost]
       public dynamic GetEmployeeList_Lama(int pCompanyCode)
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
               where = " WHERE (";

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
               where = _mString.ConstructWhere(filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
           }

           var orderby = _mString.ConstructOrderBy(Request["sortdatafield"],Request["sortorder"]);
           var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
           var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

           LOG.Debug(DateTime.Now + " WHERE : " + where);
           LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            var vObjRes = _empService.GetEmployeeList(pCompanyCode,"false", pagenum, pagesize, "employee_code", "asc", where);
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
    }
}
