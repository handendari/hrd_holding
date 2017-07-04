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
    public class KodePajakController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("KodePajakController");
        private mKodePajakService _pjkService;
        private ManageString _mString;

        public KodePajakController()
        {
            _pjkService = new mKodePajakService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetKodePajakList()
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

            var vObjRes = _pjkService.GetKodePajakList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mKodePajakModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.kode_pajak,
                        vItem.flag_status,
                        vItem.description
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertKodePajak(mKodePajakModel pModel)
        {
            //LOG.Debug(DateTime.Now + "Kode Pajak : " + pModel.kode_pajak + ", Name : " + pModel.description);

            var vResp = _pjkService.InsertKodePajak(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateKodePajak(mKodePajakModel pModel)
        {
            //LOG.Debug(DateTime.Now + " Masuk Controller Code : " + pModel.kode_pajak + ", Name : " + pModel.description);

            var vResp = new ResponseModel();
            vResp = _pjkService.UpdateKodePajak(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

    }
}
