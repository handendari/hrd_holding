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
    public class StatusController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("StatusController");
        private mStatusService _statusService;
        private ManageString _mString;

        public StatusController()
        {
            _statusService = new mStatusService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetStatusList()
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

            //LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            //LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _statusService.GetStatusList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mEmployeeStatusModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.status_code,
                        vItem.int_status,
                        vItem.status_name,
                        vItem.flag_period,
                        vItem.kode_pajak,
                        vItem.nama_pajak,
                        vItem.description
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public dynamic GetStatusLookUp()
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

            //LOG.Debug(DateTime.Now + " WHERE : " + where);
            //LOG.Debug(DateTime.Now + " ORDER BY : " + orderby);

            // Get Data
            //LOG.Debug(DateTime.Now + " Page Num : " + pagenum + " Page Size : " + pagesize);

            var vObjRes = _statusService.GetStatusList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mEmployeeStatusModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.status_code,
                        vItem.int_status,
                        vItem.status_name,
                        vItem.description
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertStatus(mEmployeeStatusModel pModel)
        {
            //LOG.Debug(DateTime.Now + "Status Code : " + pModel.status_code + ", Status name : " + pModel.status_name);

            var vResp = _statusService.InsertStatus(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateStatus(mEmployeeStatusModel pModel)
        {
            //LOG.Debug(DateTime.Now + " Masuk Controller Status Code : " + pModel.status_code + ", Status name : " + pModel.status_name);

            var vResp = new ResponseModel();
            vResp = _statusService.UpdateStatus(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

    }
}
