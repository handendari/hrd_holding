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
    public class CompanyController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("CompanyController");
        private mCompanyService _compService;
        private ManageString _mString;

        public CompanyController()
        {
            _compService = new mCompanyService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetCompanyLookUp()
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
                where = _mString.ConstructWhere(true, filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
            }

            var orderby = _mString.ConstructOrderBy(Request["sortdatafield"], Request["sortorder"]);
            var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
            var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

            LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _compService.GetCompanyList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mCompanyModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.company_code,
                        vItem.int_company,
                        vItem.company_name,
                        vItem.city_name
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public dynamic GetCompanyList()
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
                where = _mString.ConstructWhere(true, filterscount, filtervalue, filtercondition, filterdatafield, filteroperator);
            }

            var orderby = _mString.ConstructOrderBy(Request["sortdatafield"], Request["sortorder"]);
            var pagenum = _mString.ConstructPageNum(Request["pagenum"]);
            var pagesize = _mString.ConstructPageNum(Request["pagesize"]);

            LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _compService.GetCompanyList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mCompanyModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.company_code,
                        vItem.int_company,
                        vItem.company_name,
                        vItem.address,
                        vItem.postal_code,
                        vItem.city_name,
                        vItem.state,
                        vItem.phone_number,
                        vItem.fax_number,
                        vItem.web_address,
                        vItem.email_address,
                        //picture = aa.GetString("picture"),
                        vItem.country_code,
                        vItem.int_country,
                        vItem.country_name,
                        vItem.pimpinan,
                        vItem.entry_date,
                        vItem.entry_user,
                        vItem.edit_date,
                        vItem.edit_user
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertCompany(mCompanyModel pModel)
        {
            LOG.Debug(DateTime.Now + "COMPANY Code : " + pModel.company_code + ", company_name : " + pModel.company_name);

            var vResp = _compService.InsertCompany(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateCompany(mCompanyModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Comp Code : " + pModel.company_code + ", company_name : " + pModel.company_name);

            var vResp = new ResponseModel();
            vResp = _compService.UpdateCompany(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

    }
}
