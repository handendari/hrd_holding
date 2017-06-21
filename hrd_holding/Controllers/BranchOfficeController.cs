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
    public class BranchOfficeController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("BranchOfficeController");
        private mBranchOfficeService _branchService;
        private ManageString _mString;

        public BranchOfficeController()
        {
            _branchService = new mBranchOfficeService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public dynamic GetBranchOfficeLookUp(int pCompanyCode)
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
            //LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _branchService.GetBranchOfficeList(pCompanyCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mBranchOfficeModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.branch_code,
                        vItem.int_branch,
                        vItem.branch_name,
                        vItem.address
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic GetBranchOfficeList(int pCompanyCode)
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
            //LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _branchService.GetBranchOfficeList(pCompanyCode, pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mBranchOfficeModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.branch_code,
                        vItem.int_branch,
                        vItem.branch_name,
                        vItem.company_code,
                        vItem.int_company,
                        vItem.company_name,
                        vItem.country_code,
                        vItem.int_country,
                        vItem.country_name,
                        vItem.address,
                        vItem.postal_code,
                        vItem.city_name,
                        vItem.state,
                        vItem.phone_number,
                        vItem.fax_number,
                        vItem.web_address,
                        vItem.email_address,
                        vItem.npwp,
                        vItem.pimpinan,
                        vItem.pimpinan_npwp,
                        vItem.npp,
                        vItem.entry_date,
                        vItem.entry_user,
                        vItem.edit_date,
                        vItem.edit_user
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertBranchOffice(mBranchOfficeModel pModel)
        {
            //LOG.Debug(DateTime.Now + "BranchOffice Code : " + pModel.branch_code + ", Branch_name : " + pModel.branch_name);

            var vResp = _branchService.InsertBranchOffice(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateBranchOffice(mBranchOfficeModel pModel)
        {
            //LOG.Debug(DateTime.Now + " Masuk Controller BranchOffice Code : " + pModel.branch_code + ", BranchOffice_name : " + pModel.branch_name);

            var vResp = new ResponseModel();
            vResp = _branchService.UpdateBranchOffice(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }
    }
}
