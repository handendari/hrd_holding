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

        public void InsertEmployee(mEmployeeModel pModel)
        {
            var vNewModel = new mEmployeeModel();
            vNewModel.employee_code = pModel.employee_code;

            vNewModel.seq_no = _repoEmp.getEmployeeSeqNo(pModel.employee_code, pModel.company_code);

            vNewModel.nik = pModel.nik;
            vNewModel.nip = pModel.nip;
            vNewModel.employee_name = pModel.employee_name;
            vNewModel.employee_nick_name = pModel.employee_nick_name;
            vNewModel.company_code = pModel.company_code;
            vNewModel.company_name = pModel.company_name;
            vNewModel.branch_code = pModel.branch_code;
            vNewModel.branch_name = pModel.branch_name;
            vNewModel.department_code = pModel.department_code;
            vNewModel.department_name = pModel.department_name;
            vNewModel.division_code = pModel.division_code;
            vNewModel.title_code = pModel.title_code;
            vNewModel.title_name = pModel.title_name;
            vNewModel.subtitle_code = pModel.subtitle_code;
            vNewModel.subtitle_name = pModel.subtitle_name;
            vNewModel.level_code = pModel.level_code;
            vNewModel.level_name = pModel.level_name;
            vNewModel.status_code = pModel.status_code;
            vNewModel.status_name = pModel.status_name;
            vNewModel.flag_shiftable = pModel.flag_shiftable;
            vNewModel.flag_transport = pModel.flag_transport;
            vNewModel.place_birth = pModel.place_birth;
            vNewModel.date_birth = pModel.date_birth;
            vNewModel.sex = pModel.sex;
            vNewModel.religion = pModel.religion;
            vNewModel.marital_status = pModel.marital_status;
            vNewModel.no_of_children = pModel.no_of_children;
            vNewModel.emp_address = pModel.emp_address;
            vNewModel.npwp = pModel.npwp;
            vNewModel.kode_pajak = pModel.kode_pajak;
            vNewModel.npwp_method = pModel.npwp_method;
            vNewModel.npwp_registered_date = pModel.npwp_registered_date;
            vNewModel.npwp_address = pModel.npwp_address;
            vNewModel.no_jamsostek = pModel.no_jamsostek;
            vNewModel.jstk_registered_date = pModel.jstk_registered_date;
            vNewModel.bank_code = pModel.bank_code;
            vNewModel.bank_account = pModel.bank_account;
            vNewModel.bank_acc_name = pModel.bank_acc_name;
            vNewModel.start_working = pModel.start_working;
            vNewModel.appointment_date = pModel.appointment_date;
            vNewModel.phone_number = pModel.phone_number;
            vNewModel.hp_number = pModel.hp_number;
            vNewModel.email = pModel.email;
            vNewModel.country_code = pModel.country_code;
            vNewModel.country_name = pModel.country_name;
            vNewModel.identity_number = pModel.identity_number;
            vNewModel.last_education = pModel.last_education;
            vNewModel.last_employment = pModel.last_employment;
            vNewModel.description = pModel.description;
            vNewModel.flag_active = pModel.flag_active;
            vNewModel.end_working = pModel.end_working;
            vNewModel.reason = pModel.reason;
            vNewModel.picture = pModel.picture;
            vNewModel.salary_type = pModel.salary_type;
            vNewModel.tgl_mutasi = pModel.tgl_mutasi;
            vNewModel.flag_managerial = pModel.flag_managerial;
            vNewModel.spv_code = pModel.spv_code;
            //spv_name = aa.GetString("spv_name"),
            vNewModel.note1 = pModel.note1;
            vNewModel.note2 = pModel.note2;
            vNewModel.note3 = pModel.note3;
            vNewModel.entry_date = DateTime.Now;
            vNewModel.entry_user = ""; //aa.GetString("entry_user"),

            _repoEmp.InsertEmployee(vNewModel);

        }

        public void DeleteEmployee(mEmployeeModel pModel)
        {
            _repoEmp.DeleteEmployee(pModel);
        }

        public void UpdateEmployee(mEmployeeModel pModel)
        {
            var vNewModel = new mEmployeeModel();

            vNewModel.employee_code = pModel.employee_code;
            vNewModel.seq_no = pModel.seq_no;
            vNewModel.nik = pModel.nik;
            vNewModel.nip = pModel.nip;
            vNewModel.employee_name = pModel.employee_name;
            vNewModel.employee_nick_name = pModel.employee_nick_name;
            vNewModel.company_code = pModel.company_code;
            vNewModel.branch_code = pModel.branch_code;
            vNewModel.department_code = pModel.department_code;
            vNewModel.division_code = pModel.division_code;
            vNewModel.title_code = pModel.title_code;
            vNewModel.subtitle_code = pModel.subtitle_code;
            vNewModel.level_code = pModel.level_code;
            vNewModel.status_code = pModel.status_code;
            vNewModel.flag_shiftable = pModel.flag_shiftable;
            vNewModel.flag_transport = pModel.flag_transport;
            vNewModel.place_birth = pModel.place_birth;
            vNewModel.date_birth = pModel.date_birth;
            vNewModel.sex = pModel.sex;
            vNewModel.religion = pModel.religion;
            vNewModel.marital_status = pModel.marital_status;
            vNewModel.no_of_children = pModel.no_of_children;
            vNewModel.emp_address = pModel.emp_address;
            vNewModel.npwp = pModel.npwp;
            vNewModel.kode_pajak = pModel.kode_pajak;
            vNewModel.npwp_method = pModel.npwp_method;
            vNewModel.npwp_registered_date = pModel.npwp_registered_date;
            vNewModel.npwp_address = pModel.npwp_address;
            vNewModel.no_jamsostek = pModel.no_jamsostek;
            vNewModel.jstk_registered_date = pModel.jstk_registered_date;
            vNewModel.bank_code = pModel.bank_code;
            vNewModel.bank_account = pModel.bank_account;
            vNewModel.bank_acc_name = pModel.bank_acc_name;
            vNewModel.start_working = pModel.start_working;
            vNewModel.appointment_date = pModel.appointment_date;
            vNewModel.phone_number = pModel.phone_number;
            vNewModel.hp_number = pModel.hp_number;
            vNewModel.email = pModel.email;
            vNewModel.country_code = pModel.country_code;
            vNewModel.identity_number = pModel.identity_number;
            vNewModel.last_education = pModel.last_education;
            vNewModel.last_employment = pModel.last_employment;
            vNewModel.description = pModel.description;
            vNewModel.flag_active = pModel.flag_active;
            vNewModel.end_working = pModel.end_working;
            vNewModel.reason = pModel.reason;
            vNewModel.picture = pModel.picture;
            vNewModel.salary_type = pModel.salary_type;
            vNewModel.tgl_mutasi = pModel.tgl_mutasi;
            vNewModel.flag_managerial = pModel.flag_managerial;
            vNewModel.spv_code = pModel.spv_code;
            vNewModel.note1 = pModel.note1;
            vNewModel.note2 = pModel.note2;
            vNewModel.note3 = pModel.note3;
            vNewModel.edit_date = DateTime.Now;
            vNewModel.edit_user = "";


            _repoEmp.UpdateEmployee(vNewModel);
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