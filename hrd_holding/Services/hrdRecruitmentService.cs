using hrd_holding.GeneralFunction;
using hrd_holding.Models;
using hrd_holding.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hrd_holding.Services
{
    public class hrdRecruitmentService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("hrdRecruitmentService");
        private readonly hrdRecruitmentRepo _repoRec;
        private readonly hrdRecruitmentEduService _recEdu;
        private readonly hrdRecruitmentExpService _recExp;
        private readonly hrdRecruitmentFamService _recFam;
        private readonly hrdRecruitmentMemService _recMem;
        private readonly hrdRecruitmentRefService _recRef;
        private readonly hrdRecruitmentSkillService _recSkill;

        public hrdRecruitmentService()
        {
            _repoRec = new hrdRecruitmentRepo();
            _recEdu = new hrdRecruitmentEduService();
            _recExp = new hrdRecruitmentExpService();
            _recFam = new hrdRecruitmentFamService();
            _recMem = new hrdRecruitmentMemService();
            _recRef = new hrdRecruitmentRefService();
            _recSkill = new hrdRecruitmentSkillService();
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

        public ResponseModel GetRecruitmentList(int pCompany,int pBranchCode, int? pPageNum = 0, int? pPageSize = 0, string pWhere = "", string pOrderBy = "")
        {
            Log.Debug(DateTime.Now + " pPage : " + pPageNum + " pRows : " + pPageSize);

            var vRows = pPageSize == 0 ? 10 : pPageSize;
            var vStart = (pPageNum) * vRows;

            Log.Debug(DateTime.Now + " vRows : " + vRows + " vStart : " + vStart);

            var vModel = new ResponseModel();
            try
            {
                vModel = _repoRec.getRecruitmentList(pCompany,pBranchCode, vStart, vRows, pWhere, pOrderBy);
            }
            catch (Exception ex)
            {
                //Log.Error("GetEmployeeList Failed, UserId:" + AccountModels.GetUserId() + ", CompanyId:" + AccountModels.GetCompanyId(), ex);
            }

            return vModel;
        }

        public RecruitmentModel_All GetRecuitmentInfo(int pRecId)
        {
            var vModel = _repoRec.getRecruitmentInfo(pRecId);

            Log.Debug(DateTime.Now + " SERVICE Rec NIK : " + vModel.nik);

            var vtblEdu = _recEdu.GetRecruitmentEduList(pRecId);
            var vtblExp = _recExp.GetRecruitmentExpList(pRecId);
            var vtblFam = _recFam.GetRecruitmentFamList(pRecId);
            var vtblMem = _recMem.GetRecruitmentMemList(pRecId);
            var vtblRef = _recRef.GetRecruitmentRefList(pRecId);
            var vtblSkill = _recSkill.GetRecruitmentSkillList(pRecId);

            var vEmpModel = new RecruitmentModel_All
            {
                recModel = vModel,
                listEdu = vtblEdu,
                listExp = vtblExp,
                listFams = vtblFam,
                listMem = vtblMem,
                listSkill = vtblSkill,
                listRef = vtblRef
            };

            return vEmpModel;
        }

        public ResponseModel InsertRecruitment(hrdRecruitmentModel pModel)
        {
            //pModel.seq_no = _repoRec.getEmployeeSeqNo(pModel.employee_code, pModel.company_code);
            //pModel.employee_code = pModel.nik + pModel.seq_no.ToString();
            pModel.entry_date = DateTime.Now;
            pModel.entry_user = "it"; 

            var vResp = _repoRec.InsertRecruitment(pModel);

            return vResp;
        }

        public ResponseModel DeleteRecruitment(int pRecId)
        {
            var vResp = _repoRec.DeleteRecruitment(pRecId);

            return vResp;
        }

        public ResponseModel UpdateRecruitment(hrdRecruitmentModel pModel)
        {
            //pModel.edit_date = DateTime.Now;
            //pModel.edit_user = "it";


            var vResp = _repoRec.UpdateRecruitment(pModel);

            return vResp;
        }

    }
}