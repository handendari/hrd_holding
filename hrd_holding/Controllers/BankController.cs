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
    public class BankController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("BankController");
        private mBankService _bankService;
        private ManageString _mString;

        public BankController()
        {
            _bankService = new mBankService();
            _mString = new ManageString();
        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public dynamic GetBankList()
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

            var vObjRes = _bankService.GetBankList(pagenum, pagesize, where, orderby);
            var vList = vObjRes.objResult as IEnumerable<mBankModel>;

            var totalRecords = vObjRes.total_record;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pagesize);

            return Json(new
            {
                TotalRows = totalRecords,
                Rows = (
                    from vItem in vList
                    select new
                    {
                        vItem.bank_code,
                        vItem.bank_name,
                        vItem.description
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic InsertBank(mBankModel pModel)
        {
            LOG.Debug(DateTime.Now + "Bank Code : " + pModel.bank_code + ", Bank name : " + pModel.bank_name);

            var vResp = _bankService.InsertBank(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic UpdateBank(mBankModel pModel)
        {
            LOG.Debug(DateTime.Now + " Masuk Controller Bank Code : " + pModel.bank_code + ", Bank name : " + pModel.bank_name);

            var vResp = new ResponseModel();
            vResp = _bankService.UpdateBank(pModel);


            return Json(new { vResp }, JsonRequestBehavior.AllowGet);
        }

    }
}
