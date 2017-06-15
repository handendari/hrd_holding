var vDataTbl = {};
var vLookUp = "";

f_ShowLoaderModal();

var SrcCountryLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "country_code" },
                 { name: "int_code" },
                 { name: "int_country" },
                 { name: "country_name" }],
    cache: false,
    filter: function () { $("#tblCountryLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCountryLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcCountryLookUp.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initGridCountryLookUp() {
    $("#tblCountryLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'country_code', cellsalign: 'center' },
              { text: 'Int Code', dataField: 'int_code', hidden: true },
              { text: 'Int Code', dataField: 'int_country', cellsalign: 'center' },
              { text: 'Country Name', dataField: 'country_name' }
          ]
      });
}

var SrcDeptLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "department_code" },
                 { name: "int_department" },
                 { name: "department_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblDeptLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblDeptLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcCountryLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "department_code",
    root: 'Rows'
}

function initGridDeptLookUp() {
    $("#tblDeptLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'dapertment_code', hidden: true },
              { text: 'Int Code', dataField: 'int_department', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'department_name', width: 200 },
              { text: 'Description', dataField: 'description' }
          ]
      });
}

var SrcTitleLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "title_code" },
                 { name: "int_title" },
                 { name: "title_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblTitleLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblTitleLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcTitleLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "title_code",
    root: 'Rows'
}

function initGridTitleLookUp() {
    $("#tblTitleLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'title_code', hidden: true },
              { text: 'Int Code', dataField: 'int_title', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'title_name', width: 200 },
              { text: 'Description', dataField: 'description' }
          ]
      });
}

var SrcLvlLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "level_code" },
                 { name: "int_level" },
                 { name: "level_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblLvlLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblLvlLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcLvlLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "title_code",
    root: 'Rows'
}

function initGridLvlLookUp() {
    $("#tblLvlLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'level_code', hidden: true },
              { text: 'Int Code', dataField: 'int_level', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'level_name', width: 200 },
              { text: 'Description', dataField: 'description' }
          ]
      });
}

var SrcBankLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "bank_code" },
                 { name: "bank_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblBankLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblBankLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcBankLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "bank_code",
    root: 'Rows'
}

function initGridBankLookUp() {
    $("#tblBankLookUp").jqxGrid(
      {
          theme: vTheme,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'bank_code', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'bank_name', width: 200 },
              { text: 'Description', dataField: 'description' }
          ]
      });
}

var SrcStatusLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "status_code" },
                 { name: "int_status" },
                 { name: "status_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblStatusLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblStatusLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcStatusLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "status_code",
    root: 'Rows'
}

function initGridStatusLookUp() {
    $("#tblStatusLookUp").jqxGrid(
      {
          theme: vTheme,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'status_code', hidden: true },
              { text: 'Code', dataField: 'int_status', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'status_name', width: 200 },
              { text: 'Description', dataField: 'description' }
          ]
      });
}

var SrcSupervisorLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "employee_code" },
                 { name: "nik" },
                 { name: "employee_name" },
                 { name: "department_name" }],
    cache: false,
    filter: function () { $("#tblSupervisorLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblSupervisorLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcSupervisorLookUp.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initGridSupervisorLookUp() {
    $("#tblSupervisorLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'employee_code', hidden: true },
              { text: 'NIK', dataField: 'nik', cellsalign: 'center', align: 'center', width: 150 },
              { text: 'Name', dataField: 'employee_name', width: 300 },
              { text: 'Department', dataField: 'department_name' }
          ]
      });
}

function f_empty_employee_form() {
    $("#txtId").val("");
    $("#txtId").data("employee_code", "");
    $("#txtSeqNo").val("");
    $("#txtFullName").val("");
    $("#txtNickName").val("");
    $("#txtPob").val("");
    $("#dtDob").jqxDateTimeInput('setDate', new Date());
    $("#cmbGender").jqxComboBox({ selectedIndex: 0 });
    $("#cmbMarital").jqxComboBox({ selectedIndex: 0 });
    $("#cmbReligion").jqxComboBox({ selectedIndex: 0 });
    $("#txtChild").val(0);
    $("#txtAddress").val("");
    $("#txtKdDepartement").val("");
    $("#txtKdDepartement").data("dept_code", "");
    $("#txtNmDepartement").val("");
    $("#txtKdJobTitle").val("");
    $("#txtKdJobTitle").data("title_code", "");
    $("#txtNmJobTitle").val("");
    $("#txtKdGrade").val("");
    $("#txtNmGrade").val("");
    $("#txtKdLevel").val("");
    $("#txtKdLevel").data("level_code", "");
    $("#txtNmLevel").val("");
    $("#dtStartWorking").jqxDateTimeInput('setDate', new Date());
    $("#dtProbation").jqxDateTimeInput('setDate', new Date());
    $("#txtWorkingAge").val("0");
    $("#txtPhone").val("");
    $("#txtEmail").val("");
    $("#txtKdCountry").val("");
    $("#txtKdCountry").data("country_code", "");
    $("#txtNmCountry").val("");
    $("#txtNoKtp").val("");
    $("#txtLastEducation").val("");
    $("#txtLastEmployment").val("");
    $("#txtKdBank").val("");
    $("#txtKdBank").data("bank_code", "");
    $("#txtNmBank").val("");
    $("#txtBankAcc").val("");
    $("#txtBankAccName").val("");
    $("#txtKdStatus").val("");
    $("#txtKdStatus").data("status_code", "");
    $("#txtNmStatus").val("");
    $("#txtKdAtasan").val("");
    $("#txtNmAtasan").val("");

    $("#chkManagerial").jqxCheckBox({ checked: false });
    $("#chkSpecialLate").jqxCheckBox({ checked: false });

    $("#optProbation").jqxRadioButton({ checked: false });
    $("#optActive").jqxRadioButton({ checked: false });
    $("#optNonActive").jqxRadioButton({ checked: false });


    vDataCompany.length = 0;
    $("#tblCompany").jqxGrid({ source: new $.jqx.dataAdapter(vSrcCompany) });

    vDataFamily.length = 0;
    $("#tblFamily").jqxGrid({ source: new $.jqx.dataAdapter(vSrcFamily) });

    vDataEdu.length = 0;
    $("#tblEducation").jqxGrid({ source: new $.jqx.dataAdapter(vSrcEdu) });

    vDataExp.length = 0;
    $("#tblExperience").jqxGrid({ source: new $.jqx.dataAdapter(vSrcExp) });

    vDataSkill.length = 0;
    $("#tblSkill").jqxGrid({ source: new $.jqx.dataAdapter(vSrcSkill) });
}

function f_InsertEmployee() {
    var vActive = 0;
    if ($('#optActive').jqxRadioButton('checked')) {
        vActive = 1;
    }
    if ($('#optNonActive').jqxRadioButton('checked')) {
        vActive = 0;
    }
    if ($('#optProbation').jqxRadioButton('checked')) {
        vActive = 2;
    }

    var vModel = JSON.stringify({
        employee_code: $('#txtId').data("employee_code"),
        seq_no: $('#txtSeqNo').val(),
        nik: $('#txtId').val(),
        nip: "",
        employee_name: $("#txtFullName").val(),
        employee_nick_name: $("#txtNickName").val(),
        company_code: $("#txtIntCompany").data("company_code"),
        company_name: $("#txtCompany").val(),
        branch_code: $("#txtIntBranch").data("branch_code"),
        branch_name: $("#txtBranch").val(),
        department_code: $("#txtKdDepartement").data("department_code"),
        department_name: $("#txtNmDepartement").val(),
        division_code: 0,
        title_code: $("#txtKdJobTitle").data("title_code"),
        title_name: $("#txtNmJobTitle").val(),
        subtitle_code: $("#txtKdGrade").data("grade_code"),
        subtitle_name: $("#txtNmGrade").val(),
        level_code: $("#txtKdLevel").data("level_code"),
        level_name: $("#txtNmLevel").val(),
        status_code: $("#txtKdStatus").data("status_code"),
        status_name: $("#txtNmStatus").val(),
        flag_shiftable: $("#chkSpecialLate").val() == true ? 1 : 0,
        //flag_transport = aa.GetInt16("flag_transport"),
        place_birth: $("#txtPob").val(),
        date_birth: $("#dtDob").jqxDateTimeInput('getDate'),
        sex: $("#cmbGender").jqxComboBox('listBox').selectedIndex,
        religion: $("#cmbReligion").jqxComboBox('listBox').selectedIndex,
        //marital_status = aa.GetInt16("marital_status"),
        no_of_children: $("#txtChild").val(),
        emp_address: $("#txtAddress").val(),
        //npwp = aa.GetString("npwp"),
        //kode_pajak = aa.GetString("kode_pajak"),
        //npwp_method = aa.GetInt16("npwp_method"),
        //npwp_registered_date = (aa["npwp_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["npwp_registered_date"]),
        //npwp_address = aa.GetString("npwp_address"),
        //no_jamsostek = aa.GetString("no_jamsostek"),
        //jstk_registered_date = (aa["jstk_registered_date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["jstk_registered_date"]),
        bank_code: $("#txtKdBank").val(),
        bank_account: $("#txtBankAcc").val(),
        bank_acc_name: $("#txtBankAccName").val(),
        start_working: $("#dtStartWorking").jqxDateTimeInput('getDate'),
        appointment_date: $("#dtProbation").jqxDateTimeInput('getDate'),
        phone_number: $("#txtPhone").val(),
        //hp_number = aa.GetString("hp_number"),
        email: $("#txtEmail").val(),
        country_code: $("#txtKdCountry").data("country_code"),
        country_name: $("#txtNmCountry").val(),
        identity_number: $("#txtNoKtp").val(),
        last_education: $("#txtLastEducation").val(),
        last_employment: $("#txtLastEmployment").val(),
        description: $("#txtDescription").val(),
        flag_active: vActive,
        //end_working = (aa["end_working"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["end_working"]),
        //reason = aa.GetString("reason"),
        //picture = aa.GetString("picture"),
        salary_type: 0,
        //tgl_mutasi = (aa["tgl_mutasi"] == DBNull.Value) ? (DateTime?)null : ((DateTime)aa["tgl_mutasi"]),
        flag_managerial: $("#chkManagerial").val() == true ? 1 : 0,
        spv_code: $("#txtKdAtasan").val(),
        spv_name: $("#txtNmAtasan").val()
        //note1 = aa.GetString("note1"),
        //note2 = aa.GetString("note2"),
        //note3 = aa.GetString("note3")
    });

    var vEmployeeCode = $("#txtId").data("employee_code");
    var vSeqNo = $("#txtSeqNo").val();

    if (vEmployeeCode != "") {

        $.ajax({
            url: base_url + "Employee/UpdateEmployee",
            type: "POST",
            contentType: "application/json",
            data: vModel,
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    Form_Load(vEmployeeCode, vSeqNo);
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
                //$('#btnExpSave').jqxButton({ disabled: false });
                f_HideLoaderModal();
            }
        });
    } else {
        $.ajax({
            url: base_url + "Employee/InsertEmployee",
            type: "POST",
            contentType: "application/json",
            data: vModel,
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    Form_Load(vEmployeeCode, vSeqNo);
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
                //$('#btnExpave').jqxButton({ disabled: false });
                f_HideLoaderModal();
            }
        });
    }

}

function Form_Load(pEmployeeCode, pSeqNo) {
    $.ajax({
        dataType: "json",
        url: base_url + "Employee/GetEmployeeInfoAll?EmployeeCode=" + pEmployeeCode + "&seqno=" + pSeqNo,
        success: function (dt) {
            //alert(data.empModel.employee_code);

            //if (dt.empModel.employee_code == null) {
            //    window.location = base_url + "employee/index";
            //} else {

            //#region Employee Detail
            $("#txtIntCompany").val(dt.empModel.int_company);
            $("#txtIntCompany").data("company_code", dt.empModel.company_code);
            $("#txtCompany").val(dt.empModel.company_name);

            $("#txtIntBranch").val(dt.empModel.int_branch);
            $("#txtIntBranch").data("branch_code", dt.empModel.branch_code);
            $("#txtBranch").val(dt.empModel.branch_name);

            $("#txtId").val(dt.empModel.nik);
            $("#txtId").data("employee_code", dt.empModel.employee_code);

            $("#txtSeqNo").val(dt.empModel.seq_no);

            $("#txtFullName").val(dt.empModel.employee_name);
            $("#txtNickName").val(dt.empModel.employee_nick_name);
            $("#txtPob").val(dt.empModel.place_birth);

            if (dt.empModel.date_birth != null) {
                var vDob = new Date(parseInt(dt.empModel.date_birth.substr(6)));
                $("#dtDob").jqxDateTimeInput('setDate', vDob);
            }

            var vGender = dt.empModel.sex;
            $("#cmbGender").jqxComboBox({ selectedIndex: vGender });

            var vMarital = dt.empModel.marital_status;
            $("#cmbMarital").jqxComboBox({ selectedIndex: vMarital });

            var vReligion = dt.empModel.religion;
            $("#cmbReligion").jqxComboBox({ selectedIndex: vReligion });

            $("#txtChild").val(dt.empModel.no_of_children);
            $("#txtAddress").val(dt.empModel.emp_address);
            $("#txtKdDepartement").val(dt.empModel.int_department);
            $("#txtKdDepartement").data("department_code", dt.empModel.department_code);

            //$("#btnKdDepartement").jqxButton({ theme: vTheme });
            $("#txtNmDepartement").val(dt.empModel.department_name);
            $("#txtKdJobTitle").val(dt.empModel.int_title);
            $("#txtKdJobTitle").data("title_code", dt.empModel.title_code);

            //$("#btnKdJobTitle").jqxButton({ theme: vTheme });
            $("#txtNmJobTitle").val(dt.empModel.title_name);
            $("#txtKdGrade").val(dt.empModel.int_subtitle);
            $("#txtKdGrade").data("subtitle_code", dt.empModel.subtitle_code);

            //$("#btnKdGrade").jqxButton({ theme: vTheme });
            $("#txtNmGrade").val(dt.empModel.subtitle_name);
            $("#txtKdLevel").val(dt.empModel.int_level);
            $("#txtKdLevel").data("level_code", dt.empModel.level_code);

            //$("#btnKdLevel").jqxButton({ theme: vTheme });
            $("#txtNmLevel").val(dt.empModel.level_name);

            if (dt.empModel.start_working != null) {
                var vStartWorking = new Date(parseInt(dt.empModel.start_working.substr(6)));
                $("#dtStartWorking").jqxDateTimeInput('setDate', vStartWorking);

                var vToday = new Date();
                var vAge = Math.floor((vToday - vStartWorking) / (365.25 * 24 * 60 * 60 * 1000));
                $("#txtWorkingAge").val(vAge);

            }

            if (dt.empModel.appointment_date != null) {
                var vAppointment = new Date(parseInt(dt.empModel.appointment_date.substr(6)));
                $("#dtProbation").jqxDateTimeInput('setDate', vAppointment);
            }

            $("#txtPhone").val(dt.empModel.phone_number);
            $("#txtEmail").val(dt.empModel.email);
            $("#txtKdCountry").val(dt.empModel.int_country);
            $("#txtKdCountry").data("country_code", dt.empModel.country_code);
            $("#txtNmCountry").val(dt.empModel.country_name);

            $("#txtNoKtp").val(dt.empModel.identity_number);
            $("#txtLastEducation").val(dt.empModel.last_education);
            $("#txtLastEmployment").val(dt.empModel.last_employment);
            $("#txtKdBank").val(dt.empModel.bank_code);
            //$("#btnKdBank").val({ theme: vTheme });
            $("#txtNmBank").val(dt.empModel.bank_name);
            $("#txtBankAcc").val(dt.empModel.bank_account);
            $("#txtBankAccName").val(dt.empModel.bank_acc_name);
            $("#txtKdStatus").val(dt.empModel.int_status);
            $("#txtKdStatus").data("status_code", dt.empModel.status_code);
            //$("#btnKdStatus").jqxButton({ theme: vTheme });
            $("#txtNmStatus").val(dt.empModel.status_name);
            $("#txtKdAtasan").val(dt.empModel.spv_code);
            //$("#btnkdAtasan").jqxButton({ theme: vTheme });
            $("#txtNmAtasan").val(dt.empModel.spv_name);

            var vFlag = false;
            if (dt.empModel.flag_managerial == 1) { vFlag = true; }
            $("#chkManagerial").jqxCheckBox({ checked: vFlag });

            var vFlag_shiftable = false;
            if (dt.empModel.flag_shiftable == 1) { vFlag = true; }
            $("#chkSpecialLate").jqxCheckBox({ checked: vFlag_shiftable });

            switch (dt.empModel.Flag_active) {
                case 0:
                    $("#optNonActive").jqxRadioButton({ checked: true });
                    $("#optActive").jqxRadioButton({ checked: false });
                    $("#optProbation").jqxRadioButton({ checked: false });
                    break;
                case 1:
                    $("#optNonActive").jqxRadioButton({ checked: false });
                    $("#optActive").jqxRadioButton({ checked: true });
                    $("#optProbation").jqxRadioButton({ checked: false });
                    break;
                default:
                    $("#optNonActive").jqxRadioButton({ checked: false });
                    $("#optActive").jqxRadioButton({ checked: false });
                    $("#optProbation").jqxRadioButton({ checked: true });
            }

            //#endregion Employee Detail

            //#region ListFamily

            //#region+++++++++LANGSUNG TAMBAH KE TABEL++++++++
            //+++++++++++++++++++++++++++++++++++++++++

            //if (dt.listFamily != null && dt.listFamily.length > 0) {
            //    for (var i = 0; i < dt.listFamily.length; i++) {
            //        $("#tblFamily").jqxGrid('beginupdate');

            //        var row = {};
            //        row["employment_code"] = dt.listFamily[i].employee_code;
            //        row["name"] = dt.listFamily[i].name;
            //        row["nm_rel"] = dt.listFamily[i].nm_rel;
            //        row["date_birth"] = new Date(parseInt(dt.listFamily[i].date_birth.substr(6)));
            //        row["education"] = dt.listFamily[i].education;
            //        row["employment"] = dt.listFamily[i].employment;

            //        var commit = $("#tblFamily").jqxGrid('addrow', null, row);

            //        $("#tblFamily").jqxGrid('endupdate');
            //    }
            //    //$("#tblFamily").jqxGrid({ source: new $.jqx.dataAdapter(source) });
            //}

            //+++++++++++++++++++++++++++++++++++++++++++
            //#endregion +++++++++++++++++++++++++++++++++++++++++++

            if (dt.listFamily != null && dt.listFamily.length > 0) {
                f_FillTableFamily(dt.listFamily);
            }

            //#endregion

            //#region ListEducation
            if (dt.listEducation != null && dt.listEducation.length > 0) {
                f_FillTableEducation(dt.listEducation);
            }
            //#endregion

            //#region ListSkill
            if (dt.listSkill != null && dt.listSkill.length > 0) {
                f_FillTableSkill(dt.listSkill);
            }
            //#endregion

            //#region ListExperience
            if (dt.listExperience != null && dt.listExperience.length > 0) {
                f_FillTableExp(dt.listExperience);
            }
            //#endregion

            //#region ListTraining
            if (dt.listTrain != null && dt.listTrain.length > 0) {
                f_FillTableTrn(dt.listTrain);
            }
            //#endregion

            //#region ListEducation
            if (dt.listCompany != null && dt.listCompany.length > 0) {
                f_FillTableCompany(dt.listCompany);
            }
            //#endregion

            //#region ListContract
            if (dt.listContract != null && dt.listContract.length > 0) {
                for (var i = 0; i < dt.listContract.length; i++) {
                    $("#tblContract").jqxGrid('beginupdate');

                    var row = {};
                    row["employment_code"] = dt.listContract[i].employee_code;
                    row["seq_no"] = dt.listContract[i].seq_no;
                    row["start_date"] = new Date(parseInt(dt.listContract[i].start_date.substr(6)));
                    row["end_date"] = new Date(parseInt(dt.listContract[i].end_date.substr(6)));
                    row["no_contract"] = dt.listContract[i].no_contract;
                    row["company"] = dt.listContract[i].description;
                    row["description"] = dt.listContract[i].description;

                    var commit = $("#tblContract").jqxGrid('addrow', null, row);

                    $("#tblContract").jqxGrid('endupdate');
                }
            }
            //#endregion

            //}

            f_HideLoaderModal();
        }
    });
}

$(document).ready(function () {
    //#region INIT COMPONENT
    $("#txtIntBranch").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtBranch").jqxInput({ theme: vTheme, disabled: true, width: 300 });
    $("#txtIntCompany").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtCompany").jqxInput({ theme: vTheme, disabled: true, width: 200 });
    $("#btnChangeComp").jqxButton({ theme: vTheme, height: 20 });
    $("#txtId").jqxInput({ theme: vTheme });
    $("#txtSeqNo").jqxInput({ theme: vTheme, width: 30, disabled: true });
    $("#txtFullName").jqxInput({ theme: vTheme, width: 250 });
    $("#txtNickName").jqxInput({ theme: vTheme });
    $("#txtPob").jqxInput({ theme: vTheme });
    $("#dtDob").jqxDateTimeInput({ theme: vTheme, width: 150 });

    $("#cmbGender").jqxComboBox({ theme: vTheme, width: 150, source: vCmbGender, selectedIndex: 0 });
    $("#cmbGender input").attr('disabled', true);

    $("#cmbMarital").jqxComboBox({ theme: vTheme, width: 150, source: vCmbMarital, selectedIndex: 0 });
    $("#cmbMarital input").attr('disabled', true);

    $("#cmbReligion").jqxComboBox({ theme: vTheme, width: 150, source: vCmbReligion, selectedIndex: 0 });
    $("#cmbReligion input").attr('disabled', true);

    $("#txtChild").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });
    $("#txtAddress").jqxInput({ theme: vTheme, width: 300 });
    $("#txtKdDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdDepartement").jqxButton({ theme: vTheme });
    $("#txtNmDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdJobTitle").jqxButton({ theme: vTheme });
    $("#txtNmJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdGrade").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdGrade").jqxButton({ theme: vTheme });
    $("#txtNmGrade").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdLevel").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdLevel").jqxButton({ theme: vTheme });
    $("#txtNmLevel").jqxInput({ theme: vTheme, width: 250, disabled: true });
    $("#dtStartWorking").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#dtProbation").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtWorkingAge").jqxInput({ theme: vTheme, width: 50 });
    $("#txtPhone").jqxInput({ theme: vTheme });
    $("#txtEmail").jqxInput({ theme: vTheme, width: 250 });
    $("#txtKdCountry").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdCountry").jqxButton({ theme: vTheme });
    $("#txtNmCountry").jqxInput({ theme: vTheme, disabled: true });
    $("#txtNoKtp").jqxInput({ theme: vTheme });
    $("#txtLastEducation").jqxInput({ theme: vTheme });
    $("#txtLastEmployment").jqxInput({ theme: vTheme });
    $("#txtKdBank").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdBank").jqxButton({ theme: vTheme });
    $("#txtNmBank").jqxInput({ theme: vTheme, disabled: true });
    $("#txtBankAcc").jqxInput({ theme: vTheme });
    $("#txtBankAccName").jqxInput({ theme: vTheme, width: 250 });
    $("#txtKdStatus").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdStatus").jqxButton({ theme: vTheme });
    $("#txtNmStatus").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdAtasan").jqxInput({ theme: vTheme, width: 150, disabled: true });
    $("#btnkdAtasan").jqxButton({ theme: vTheme });
    $("#txtNmAtasan").jqxInput({ theme: vTheme, width: 300, disabled: true });
    $('#txtDescription').jqxTextArea({
        theme: vTheme, placeHolder: 'Description Master Employee',
        height: 50, width: 300, minLength: 1
    });


    $("#btnContractNew").jqxButton({ theme: vTheme });
    $("#btnContractEdit").jqxButton({ theme: vTheme });
    $("#btnContractDelete").jqxButton({ theme: vTheme });

    $("#chkManagerial").jqxCheckBox({ theme: vTheme });
    $("#chkSpecialLate").jqxCheckBox({ theme: vTheme });

    $("#optProbation").jqxRadioButton({ theme: vTheme, groupName: "status", disabled: true });
    $("#optActive").jqxRadioButton({ theme: vTheme, groupName: "status", disabled: true });
    $("#optNonActive").jqxRadioButton({ theme: vTheme, groupName: "status", disabled: true });

    $('#jqxTabs').jqxTabs({
        width: '100%', height: 300, theme: vTheme,
        position: 'top', selectionTracker: 1, animationType: 'fade'
    });

    $("#modYesNo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false,
        modalZIndex: 999
    });

    //#endregion

    $("#modCountryLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modDeptLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modTitleLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modLvlLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modBankLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modStatusLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modSupervisorLookUp").jqxWindow({
        height: 500, width: 730,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#EmpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button | button', rtl: true,
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == "button") {
                tool.height("25px");
                tool.width("110px");
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Submit Resume_24_grey.png'/>" +
                                        "<span>ACTIVATE</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_MessageBoxShow("Karyawan di aktifkan...");
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Save as_24_grey.png'/>" +
                                        "<span>SAVE DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_InsertEmployee();
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/add property_24_grey.png'/>" +
                                        "<span>NEW DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_empty_employee_form();
                    });
                    break;

            }
        }
    });

    $("#CountryLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data"); tool.height("25px"); tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCountryLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCountryLookUp').jqxGrid('getrowdata', rowindex);
                            if (vLookUp == "edu") {
                                $("#txtEduCountryCode").val(rd.int_country);
                                $("#txtEduCountryCode").data("edu_country_code", rd.country_code);

                                $("#txtEduCountryName").val(rd.country_name);
                            } else {
                                $("#txtKdCountry").val(rd.int_country);
                                $("#txtKdCountry").data("emp_country_code", rd.country_code);

                                $("#txtNmCountry").val(rd.country_name);
                            }
                            $("#modCountryLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel"); tool.height("25px"); tool.width("50px");
                    tool.on("click", function () {
                        $("#modCountryLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#DeptLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblDeptLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblDeptLookUp').jqxGrid('getrowdata', rowindex);
                            if (vLookUp == "Emp") {
                                $("#txtKdDepartement").val(rd.int_department);
                                $("#txtKdDepartement").data("dept_code", rd.department_code);

                                $("#txtNmDepartement").val(rd.department_name);
                            } else {
                                $("#txtDeptCode_Company").val(rd.int_department);
                                $("#txtDeptCode_Company").data("dept_code", rd.department_code);

                                $("#txtDeptName_Company").val(rd.department_name);
                            }
                            $("#modDeptLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modDeptLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#TitleLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblTitleLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblTitleLookUp').jqxGrid('getrowdata', rowindex);
                            if (vLookUp == "Emp") {
                                $("#txtKdJobTitle").val(rd.int_title);
                                $("#txtKdJobTitle").data("title_code", rd.title_code);

                                $("#txtNmJobTitle").val(rd.title_name);
                            } else {
                                $("#txtTitleCode_Company").val(rd.int_title);
                                $("#txtTitleCode_Company").data("title_code", rd.title_code);

                                $("#txtTitleName_Company").val(rd.title_name);
                            }

                            $("#modTitleLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modTitleLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#LvlLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblLvlLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblLvlLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtKdLevel").val(rd.int_level);
                            $("#txtKdLevel").data("level_code", rd.level_code);

                            $("#txtNmLevel").val(rd.level_name);
                            $("#modLvlLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modLvlLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#BankLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblBankLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblBankLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtKdBank").val(rd.bank_code);

                            $("#txtNmBank").val(rd.bank_name);
                            $("#modBankLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modBankLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#StatusLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblStatusLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblStatusLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtKdStatus").val(rd.int_status);
                            $("#txtKdStatus").data("status_code", rd.status_code);

                            $("#txtNmStatus").val(rd.bank_name);
                            $("#modStatusLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modStatusLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#SupervisorLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 40, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == "button") {
                tool.height("30px");
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Checked Checkbox_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>SELECT DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("130px");
                    tool.on("click", function () {
                        var rowindex = $('#tblSupervisorLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblSupervisorLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtKdAtasan").val(rd.employee_code);

                            $("#txtNmAtasan").val(rd.employee_name);
                            $("#modSupervisorLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Close Pane_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>CANCEL</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("110px");
                    tool.on("click", function () {
                        $("#modSupervisorLookUp").jqxWindow('close')
                    });
                    break;
            }
        }
    });

    //#region UNTUK CENTER MODAL DIALOG
    function f_PosisiModalDialog() {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modEducation').jqxWindow({ position: { x: f_PosX($('#modEducation')), y: f_PosY($('#modEducation')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
        $('#modSkill').jqxWindow({ position: { x: f_PosX($('#modSkill')), y: f_PosY($('#modSkill')) } });
        $('#modExperience').jqxWindow({ position: { x: f_PosX($('#modExperience')), y: f_PosY($('#modExperience')) } });
        $('#modTraining').jqxWindow({ position: { x: f_PosX($('#modTraining')), y: f_PosY($('#modTraining')) } });
        $('#modCountryLookUp').jqxWindow({ position: { x: f_PosX($('#modCountryLookUp')), y: f_PosY($('#modCountryLookUp')) } });
        $('#modDeptLookUp').jqxWindow({ position: { x: f_PosX($('#modDeptLookUp')), y: f_PosY($('#modDeptLookUp')) } });
        $('#modTitleLookUp').jqxWindow({ position: { x: f_PosX($('#modTitleLookUp')), y: f_PosY($('#modTitleLookUp')) } });
        $('#modLvlLookUp').jqxWindow({ position: { x: f_PosX($('#modLvlLookUp')), y: f_PosY($('#modLvlLookUp')) } });
        $('#modBankLookUp').jqxWindow({ position: { x: f_PosX($('#modBankLookUp')), y: f_PosY($('#modBankLookUp')) } });
        $('#modStatusLookUp').jqxWindow({ position: { x: f_PosX($('#modStatusLookUp')), y: f_PosY($('#modStatusLookUp')) } });
        $('#modCompanyList').jqxWindow({ position: { x: f_PosX($('#modCompanyList')), y: f_PosY($('#modCompanyList')) } });
        $('#modCompany').jqxWindow({ position: { x: f_PosX($('#modCompany')), y: f_PosY($('#modCompany')) } });
        $('#modSupervisorLookUp').jqxWindow({ position: { x: f_PosX($('#modSupervisorLookUp')), y: f_PosY($('#modSupervisorLookUp')) } });
    }

    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        f_PosisiModalDialog();
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        f_PosisiModalDialog();
    });
    //#endregion

    //#region Table CONTRACT
    var initGridContract = function () {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'start_date' },
                { name: 'end_date' },
                { name: 'no_contract' },
                { name: 'company' },
                { name: 'description' }
           ]
       };

        var dataAdapter = new $.jqx.dataAdapter(source, {
            async: false,
            loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); }
        });

        $("#tblContract").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            rowsheight: 25,
            columnsresize: true,
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Start Date', datafield: 'start_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'End Date', datafield: 'end_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'No Contract', datafield: 'no_contract' },
                { text: 'Company', datafield: 'company' },
                { text: 'Description', datafield: 'description' }
            ]
        });
    }
    //#endregion

    initGridContract();
    initGridCountryLookUp();
    initGridDeptLookUp();
    initGridTitleLookUp();
    initGridLvlLookUp();
    initGridBankLookUp();
    initGridStatusLookUp();
    initGridSupervisorLookUp();

    $("#btnYes").jqxButton({ theme: vTheme, height: 30, width: 60 });
    $("#btnNo").jqxButton({ theme: vTheme, height: 30, width: 60 });

    Form_Load("35151269069300041", 1);

    $('#btnYes').on('click', function (event) {
        var vEmpCode = $("#txtId").data("employee_code");

        if (vLookUp == "Comp") {
            f_DeleteEmployeeCompany(vEmpCode);

        } else {
            var selectedTab = $('#jqxTabs').jqxTabs('selectedItem');

            switch (selectedTab) {
                case 0:
                    f_DeleteEmployeeFamily(vEmpCode);
                    break;
                case 1:
                    f_DeleteEmployeeEducation(vEmpCode);
                    break;
                case 2:
                    f_DeleteEmployeeSkill(vEmpCode);
                    break;
                case 3:
                    f_DeleteEmployeeExp(vEmpCode);
                    break;
                case 4:
                    f_DeleteEmployeeTrn(vEmpCode);
                    break;
            }
        }
    });

    $('#btnNo').on('click', function (event) {
        $("#modYesNo").jqxWindow("close");
    });

    $('#btnKdCountry').on('click', function (event) {
        SrcCountryLookUp.url = base_url + "/Country/GetCountryList";
        vLookUp = "emp";

        var vAdapter = new $.jqx.dataAdapter(SrcCountryLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcCountryLookUp.TotalRows) {
                    SrcCountryLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblCountryLookUp').jqxGrid({ source: vAdapter })
        $('#tblCountryLookUp').jqxGrid('gotopage', 0);
        $("#modCountryLookUp").jqxWindow('open');
    });

    $('#btnKdDepartement').on('click', function (event) {
        var vCompanyCode = $("#txtIntCompany").data("company_code");
        var vBranchCode = $("#txtIntBranch").data("branch_code");

        vLookUp = "Emp";
        SrcDeptLookUp.url = base_url + "/Department/GetDepartmentList?pCompanyCode=" + vCompanyCode + "&pBranchCode=" + vBranchCode;
        var vAdapter = new $.jqx.dataAdapter(SrcDeptLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcDeptLookUp.TotalRows) {
                    SrcDeptLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblDeptLookUp').jqxGrid({ source: vAdapter })
        $('#tblDeptLookUp').jqxGrid('gotopage', 0);
        $("#modDeptLookUp").jqxWindow('open');
    });

    $('#btnKdJobTitle').on('click', function (event) {
        vLookUp = "Emp";

        SrcTitleLookUp.url = base_url + "/Title/GetTitleList";

        var vAdapter = new $.jqx.dataAdapter(SrcTitleLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcTitleLookUp.TotalRows) {
                    SrcTitleLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblTitleLookUp').jqxGrid({ source: vAdapter })
        $('#tblTitleLookUp').jqxGrid('gotopage', 0);
        $("#modTitleLookUp").jqxWindow('open');
    });

    $('#btnKdLevel').on('click', function (event) {
        SrcLvlLookUp.url = base_url + "/Level/GetLevelLookUp";

        var vAdapter = new $.jqx.dataAdapter(SrcLvlLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcLvlLookUp.TotalRows) {
                    SrcLvlLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblLvlLookUp').jqxGrid({ source: vAdapter })
        $('#tblLvlLookUp').jqxGrid('gotopage', 0);
        $("#modLvlLookUp").jqxWindow('open');
    });

    $('#btnKdBank').on('click', function (event) {
        SrcBankLookUp.url = base_url + "/Bank/GetBankList";

        var vAdapter = new $.jqx.dataAdapter(SrcBankLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcBankLookUp.TotalRows) {
                    SrcBankLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblBankLookUp').jqxGrid({ source: vAdapter })
        $('#tblBankLookUp').jqxGrid('gotopage', 0);
        $("#modBankLookUp").jqxWindow('open');
    });

    $('#btnKdStatus').on('click', function (event) {
        SrcStatusLookUp.url = base_url + "/Status/GetStatusLookUp";

        var vAdapter = new $.jqx.dataAdapter(SrcStatusLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcStatusLookUp.TotalRows) {
                    SrcStatusLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblStatusLookUp').jqxGrid({ source: vAdapter })
        $('#tblStatusLookUp').jqxGrid('gotopage', 0);
        $("#modStatusLookUp").jqxWindow('open');
    });

    $('#btnChangeComp').on('click', function (event) {
        $("#modCompanyList").jqxWindow('open');
    });

    $('#btnkdAtasan').on('click', function (event) {
        var vCompanyCode = $("#txtIntCompany").data("company_code");
        var vBranchCode = $("#txtIntBranch").data("branch_code");

        vLookUp = "Emp";
        SrcSupervisorLookUp.url = base_url + "/Employee/GetEmployeeLookUp?pCompanyCode=" + vCompanyCode + "&pBranchCode=" + vBranchCode;
        var vAdapter = new $.jqx.dataAdapter(SrcSupervisorLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcSupervisorLookUp.TotalRows) {
                    SrcSupervisorLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblSupervisorLookUp').jqxGrid({ source: vAdapter })
        $('#tblSupervisorLookUp').jqxGrid('gotopage', 0);
        $("#modSupervisorLookUp").jqxWindow('open');
    });

});
