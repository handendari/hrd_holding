using hrd_holding.GeneralFunction;
using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeService");
        private readonly mEmployeeRepo _repoEmp;
        private readonly mEmployeeFamilyService _srvFam;
        private readonly mEmployeeSkillRepo _repoSkill;
        private readonly mEmployeeEducationRepo _repoEdu;
        private readonly mEmployeeExperienceRepo _repoExp;
        private readonly mEmployeeContractRepo _repoCon;
        private readonly mEmployeeTrainingRepo _repoTrain;
        private readonly mEmployeeCompanyRepo _repoCompany;

        public mEmployeeService()
        {
            _repoEmp = new mEmployeeRepo();
            _srvFam = new mEmployeeFamilyService();
            _repoSkill = new mEmployeeSkillRepo();
            _repoEdu = new mEmployeeEducationRepo();
            _repoExp = new mEmployeeExperienceRepo();
            _repoCon = new mEmployeeContractRepo();
            _repoTrain = new mEmployeeTrainingRepo();
            _repoCompany = new mEmployeeCompanyRepo();
        }

        private string GenerateMasterCode(int code)
        {
            string mCode = "";
            try
            {
                //mCode = GeneralFunction.Replicate(code.ToString(), 6);
            }
            catch (Exception ex)
            {
                Log.Error("GenerateMasterEmployeeCode Failed,", ex);
            }

            return mCode;
        }

        //public ResponseModel GetEmployeeList_lama(int pCompany, string pSearch, int? pPage=0, int? pRows=0, string pSortField = "", string pSortDir = "", string pFilter = "")
        //{
        //    // Construct where statement
        //    string strWhere = "";
        //    if (pSearch.Equals("true"))
        //    {
        //        strWhere = ManageString.ConstructWhere(pFilter);
        //    }

        //    Log.Debug(DateTime.Now + " pPage : " + pPage + " pRows : " + pRows);

        //    var vRows = pRows == 0 ? 10 : pRows;
        //    var vStart = (pPage) * vRows;

        //    Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

        //    var vModel = new ResponseModel(); ;
        //    try
        //    {
        //        vModel = _repoEmp.getEmployeeList(pCompany,vStart,vRows,pSortField, pSortDir, strWhere);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Error("GetEmployeeList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
        //    }

        //    return vModel;
        //}

        public ResponseModel GetEmployeeList(int pCompany,int pBranchCode, int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            Log.Debug(DateTime.Now + " pPage : " + pPageNum + " pRows : " + pPageSize);

            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoEmp.getEmployeeList(pCompany,pBranchCode, vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetEmployeeList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public EmployeeModelAll GetEmployeeInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = _repoEmp.getEmployeeInfo(pEmployeeCode, pSeqNo);

            Log.Debug(DateTime.Now + " SERVICE EMP CODE : " + vModel.employee_code);

            var vtblFamily = _srvFam.GetEmployeeFamList(pEmployeeCode);
            var vtblContract = _repoCon.getEmployeeContractList(pEmployeeCode);
            var vtblEducation = _repoEdu.getEmployeeEducationList(pEmployeeCode);
            var vtblExperience = _repoExp.getEmployeeExperienceList(pEmployeeCode);
            var vtblSkill = _repoSkill.getEmployeeSkillList(pEmployeeCode);
            var vtblTrain = _repoTrain.getEmployeeTrainingList(pEmployeeCode);
            var vtblCompany = _repoCompany.getEmployeeCompanyList(pEmployeeCode);

            var vEmpModel = new EmployeeModelAll
            {
                empModel = vModel,
                listContract = vtblContract,
                listEducation = vtblEducation,
                listExperience = vtblExperience,
                listFamily = vtblFamily,
                listSkill = vtblSkill,
                listTrain = vtblTrain,
                listCompany = vtblCompany
            };

            return vEmpModel;
        }

        public ResponseModel InsertEmployee(mEmployeeModel pModel)
        {
            pModel.seq_no = _repoEmp.getEmployeeSeqNo(pModel.employee_code, pModel.company_code);
            pModel.employee_code = pModel.nik + pModel.seq_no.ToString();
            pModel.entry_date = DateTime.Now;
            pModel.entry_user = ""; //aa.GetString("entry_user"),

            var vResp = _repoEmp.InsertEmployee(pModel);

            return vResp;
        }

        public ResponseModel DeleteEmployee(mEmployeeModel pModel)
        {
            var vResp = _repoEmp.DeleteEmployee(pModel);

            return vResp;
        }

        public ResponseModel UpdateEmployee(mEmployeeModel pModel)
        {
            pModel.edit_date = DateTime.Now;
            pModel.edit_user = "it";


            var vResp = _repoEmp.UpdateEmployee(pModel);

            return vResp;
        }

        //public bool Validation(CountryModel model, out string message)
        //{
        //    bool isValid = true;
        //    message = "";

        //    if (String.IsNullOrEmpty(model.CountryName) || model.CountryName.Trim() == "")
        //    {
        //        message = "Invalid Country Name";
        //        isValid = false;
        //    }
        //    else if (String.IsNullOrEmpty(model.IntCountry) || model.IntCountry.Trim() == "")
        //    {
        //        message = "Invalid Int Country";
        //        isValid = false;
        //    }

        //    ContinentalModel selectedContinent = _continentRepo.GetContinentalInfo(model.ContinentalId);
        //    if (selectedContinent == null)
        //    {
        //        message = "Invalid Continental Code";
        //        isValid = false;
        //    }

        //    return isValid;
        //}

        //public bool ValidationInsert(CountryModel model, out string message)
        //{
        //    return this.Validation(model, out message);
        //}

        //public bool ValidationUpdate(CountryModel model, out string message)
        //{
        //    bool isValid = this.Validation(model, out message);

        //    if (model.Id == 0)
        //    {
        //        message = "Invalid Country Code";
        //        isValid = false;
        //    }
        //    else
        //    {
        //        CountryModel selectedCountry = this.GetCountryInfo(model.Id);
        //        if (selectedCountry == null)
        //        {
        //            message = "Invalid Country Code";
        //            isValid = false;
        //        }
        //    }

        //    return isValid;
        //}

    }
}