using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class mEmployeeTrainingService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("EmployeeTrainingService");
        private readonly mEmployeeTrainingRepo _repoTrain;

        public mEmployeeTrainingService()
        {
            _repoTrain = new mEmployeeTrainingRepo();
        }

        public List<mEmployeeTrainingModel> GetEmployeeTrainingList(string pEmployeeCode)
        {
            var vList = new List<mEmployeeTrainingModel>();
            vList = _repoTrain.getEmployeeTrainingList(pEmployeeCode);
            return vList;
        }

        public mEmployeeTrainingModel GetEmployeeTrainingInfo(string pEmployeeCode, int pSeqNo)
        {
            var vModel = new mEmployeeTrainingModel();
            vModel = _repoTrain.getEmployeeTrainingInfo(pEmployeeCode, pSeqNo);
            return vModel;
        }

        public ResponseModel UpdateEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            pModel.edit_user = "it";
            pModel.edit_date = DateTime.Now;

            var vModel = _repoTrain.UpdateEmployeeTraining(pModel);
            return vModel;
        }

        public ResponseModel InsertEmployeeTraining(mEmployeeTrainingModel pModel)
        {
            pModel.entry_user = "it";
            pModel.entry_date = DateTime.Now;
            pModel.seq_no = _repoTrain.getEmployeeTrainingSeqNo(pModel.employee_code);

            Log.Debug(DateTime.Now + " Seq No Training : " + pModel.seq_no);

            var vModel = _repoTrain.InsertEmployeeTraining(pModel);
            return vModel;
        }

        public ResponseModel DeleteEmployeeTraining(string pEmployeeCode, int pSeqNo)
        {
            var vModel = _repoTrain.DeleteEmployeeTraining(pEmployeeCode, pSeqNo);
            return vModel;
        }

    }
}