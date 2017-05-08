//using hrd_holding.Models;
//using hrd_holding.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace hrd_holding.Services
//{
//    public class mCountryService
//    {
//        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("CountryService");
//        private readonly mCountryRepo _repoCountry;

//        public mCountryService()
//        {
//            _repoCountry = new mCountryRepo();
//        }

//        private string GenerateMasterCode(int code)
//        {
//            string mCode = "";
//            try
//            {
//                mCode = GeneralFunction.Replicate(code.ToString(), 6);
//            }
//            catch (Exception ex)
//            {
//                LOG.Error("GenerateMasterCode Failed,", ex);
//            }

//            return mCode;
//        }

//        public List<CountryModel> GetCountries(string sortby, string sortdir, string vWhere)
//        {
//            List<CountryModel> model = new List<CountryModel>();
//            try
//            {
//                model = _countryRepo.GetCountries(sortby, sortdir, vWhere).ToList();
//            }
//            catch (Exception ex)
//            {
//                LOG.Error("GetCountry Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
//            }

//            return model;
//        }

//        public List<CountryModel> LookUpCountries(string sortby, string sortdir, string vWhere)
//        {
//            List<CountryModel> model = new List<CountryModel>();
//            try
//            {
//                model = _countryRepo.LookUpCountries(sortby, sortdir, vWhere).ToList();
//            }
//            catch (Exception ex)
//            {
//                LOG.Error("LookUpCountries Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
//            }

//            return model;
//        }

//        public CountryModel GetCountryInfo(int countryId)
//        {
//            CountryModel model = _countryRepo.GetCountryInfo(countryId);

//            return model;
//        }

//        public void InsertCountry(CountryModel model)
//        {
//            MstCountry newCountry = new MstCountry();
//            newCountry.Name = !String.IsNullOrEmpty(model.CountryName) ? model.CountryName.Trim().ToUpper() : "";
//            newCountry.Abbrevation = !String.IsNullOrEmpty(model.IntCountry) ? model.IntCountry.Trim().ToUpper() : "";
//            newCountry.ContinentalId = model.ContinentalId;
//            newCountry.ModifiedBy = AccountModels.GetUserId();
//            newCountry.ModifiedOn = DateTime.Now;

//            newCountry = _countryRepo.InsertCountry(newCountry);

//            if (newCountry != null)
//            {
//                newCountry.MasterCode = this.GenerateMasterCode(newCountry.Id);
//                _countryRepo.Update(newCountry);
//            }
//        }

//        public void DeleteCountry(int countryId)
//        {
//            _countryRepo.DeleteCountry(countryId);
//        }

//        public void UpdateCountry(CountryModel model)
//        {
//            MstCountry country = _countryRepo.Find(c => c.Id == model.Id);

//            country.Name = !String.IsNullOrEmpty(model.CountryName) ? model.CountryName.Trim().ToUpper() : "";
//            country.Abbrevation = !String.IsNullOrEmpty(model.IntCountry) ? model.IntCountry.Trim().ToUpper() : "";
//            country.ContinentalId = model.ContinentalId;
//            country.ModifiedBy = AccountModels.GetUserId();
//            country.ModifiedOn = DateTime.Now;

//            _countryRepo.UpdateCountry(country);
//        }

//        public bool Validation(CountryModel model, out string message)
//        {
//            bool isValid = true;
//            message = "";

//            if (String.IsNullOrEmpty(model.CountryName) || model.CountryName.Trim() == "")
//            {
//                message = "Invalid Country Name";
//                isValid = false;
//            }
//            else if (String.IsNullOrEmpty(model.IntCountry) || model.IntCountry.Trim() == "")
//            {
//                message = "Invalid Int Country";
//                isValid = false;
//            }

//            ContinentalModel selectedContinent = _continentRepo.GetContinentalInfo(model.ContinentalId);
//            if (selectedContinent == null)
//            {
//                message = "Invalid Continental Code";
//                isValid = false;
//            }

//            return isValid;
//        }

//        public bool ValidationInsert(CountryModel model, out string message)
//        {
//            return this.Validation(model, out message);
//        }

//        public bool ValidationUpdate(CountryModel model, out string message)
//        {
//            bool isValid = this.Validation(model, out message);

//            if (model.Id == 0)
//            {
//                message = "Invalid Country Code";
//                isValid = false;
//            }
//            else
//            {
//                CountryModel selectedCountry = this.GetCountryInfo(model.Id);
//                if (selectedCountry == null)
//                {
//                    message = "Invalid Country Code";
//                    isValid = false;
//                }
//            }

//            return isValid;
//        }

//        public List<string> GetCurrencyList()
//        {
//            List<string> model = _countryRepo.GetCurrencyList();

//            return model;
//        }

//        private bool disposed = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    _countryRepo.Dispose();
//                    _continentRepo.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//    }
//}