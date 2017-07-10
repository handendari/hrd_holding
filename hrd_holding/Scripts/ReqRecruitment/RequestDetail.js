
function f_empty_form() {

    $("#dtTglReq").jqxDateTimeInput('setDate', new Date());
    $("#txtNoReq").val("");
    $("#txtNoReq").data("id_req", "");

    $("#txtBagian").val("");
    $('#txtAlasan').val("");

    $("#cmbKelamin").jqxComboBox({ selectedIndex: 0 });

    $("#txtMinUsia").val(0);
    $("#txtPendidikan").val("");
    $("#txtPengalaman").val("");
    $("#txtBInggris").val("");
    $("#txtSertifikasi").val("");

    $("#cmbStatusKawin").jqxComboBox({ selectedIndex: 0 });

    $("#txtNamaJabatan").val("");
    $("#txtTujuanJabatan").val("");
    $("#txtBertanggungJawab").val("");
    $("#txtJmlBawahan").val(0);
    $("#txtWewenang").val("");
    $("#txtHubungan").val("");
    $("#txtKerjaSendiri").val("");

    $("#cmbSumber").jqxComboBox({ selectedIndex: 0 });

    $("#txtJmlButuh").val(0);

    $("#dtWorkPlan").jqxDateTimeInput('setDate', new Date());
    $("#txtCatatan").val("");
}

function f_InsertRequest() {

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
        place_birth: $("#txtPob").val(),
        date_birth: $("#dtDob").jqxDateTimeInput('getDate'),
        sex: $("#cmbGender").jqxComboBox('listBox').selectedIndex,
        religion: $("#cmbReligion").jqxComboBox('listBox').selectedIndex,
        no_of_children: $("#txtChild").val(),
        emp_address: $("#txtAddress").val(),
        bank_code: $("#txtKdBank").val(),
        bank_account: $("#txtBankAcc").val(),
        bank_acc_name: $("#txtBankAccName").val(),
        start_working: $("#dtStartWorking").jqxDateTimeInput('getDate'),
        appointment_date: $("#dtProbation").jqxDateTimeInput('getDate'),
        phone_number: $("#txtPhone").val(),
        email: $("#txtEmail").val(),
        country_code: $("#txtKdCountry").data("country_code"),
        country_name: $("#txtNmCountry").val(),
        identity_number: $("#txtNoKtp").val(),
        last_education: $("#txtLastEducation").val(),
        last_employment: $("#txtLastEmployment").val(),
        description: $("#txtDescription").val(),
        flag_active: vActive,
        salary_type: 0,
        flag_managerial: $("#chkManagerial").val() == true ? 1 : 0,
        spv_code: $("#txtKdAtasan").val(),
        spv_name: $("#txtNmAtasan").val()
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

function Form_Load(pIdReq) {
    //    $.ajax({
    //        dataType: "json",
    //        url: base_url + "Employee/GetRequestInfo?pIdCode=" + pIdReq,
    //        success: function (dt) {
    //            //alert(data.empModel.employee_code);

    //            //if (dt.empModel.employee_code == null) {
    //            //    window.location = base_url + "employee/index";
    //            //} else {

    //            //#region Employee Detail
    //            $("#txtIntCompany").val(dt.empModel.int_company);
    //            $("#txtIntCompany").data("company_code", dt.empModel.company_code);
    //            $("#txtCompany").val(dt.empModel.company_name);

    //            $("#txtIntBranch").val(dt.empModel.int_branch);
    //            $("#txtIntBranch").data("branch_code", dt.empModel.branch_code);
    //            $("#txtBranch").val(dt.empModel.branch_name);

    //            $("#txtId").val(dt.empModel.nik);
    //            $("#txtId").data("employee_code", dt.empModel.employee_code);

    //            $("#txtSeqNo").val(dt.empModel.seq_no);

    //            $("#txtFullName").val(dt.empModel.employee_name);
    //            $("#txtNickName").val(dt.empModel.employee_nick_name);
    //            $("#txtPob").val(dt.empModel.place_birth);

    //            if (dt.empModel.date_birth != null) {
    //                var vDob = new Date(parseInt(dt.empModel.date_birth.substr(6)));
    //                $("#dtDob").jqxDateTimeInput('setDate', vDob);
    //            }

    //            var vGender = dt.empModel.sex;
    //            $("#cmbGender").jqxComboBox({ selectedIndex: vGender });

    //            var vMarital = dt.empModel.marital_status;
    //            $("#cmbMarital").jqxComboBox({ selectedIndex: vMarital });

    //            var vReligion = dt.empModel.religion;
    //            $("#cmbReligion").jqxComboBox({ selectedIndex: vReligion });

    //            $("#txtChild").val(dt.empModel.no_of_children);
    //            $("#txtAddress").val(dt.empModel.emp_address);
    //            $("#txtKdDepartement").val(dt.empModel.int_department);
    //            $("#txtKdDepartement").data("department_code", dt.empModel.department_code);

    //            //$("#btnKdDepartement").jqxButton({ theme: vTheme });
    //            $("#txtNmDepartement").val(dt.empModel.department_name);
    //            $("#txtKdJobTitle").val(dt.empModel.int_title);
    //            $("#txtKdJobTitle").data("title_code", dt.empModel.title_code);

    //            //$("#btnKdJobTitle").jqxButton({ theme: vTheme });
    //            $("#txtNmJobTitle").val(dt.empModel.title_name);
    //            $("#txtKdGrade").val(dt.empModel.int_subtitle);
    //            $("#txtKdGrade").data("subtitle_code", dt.empModel.subtitle_code);

    //            //$("#btnKdGrade").jqxButton({ theme: vTheme });
    //            $("#txtNmGrade").val(dt.empModel.subtitle_name);
    //            $("#txtKdLevel").val(dt.empModel.int_level);
    //            $("#txtKdLevel").data("level_code", dt.empModel.level_code);

    //            //$("#btnKdLevel").jqxButton({ theme: vTheme });
    //            $("#txtNmLevel").val(dt.empModel.level_name);

    //            if (dt.empModel.start_working != null) {
    //                var vStartWorking = new Date(parseInt(dt.empModel.start_working.substr(6)));
    //                $("#dtStartWorking").jqxDateTimeInput('setDate', vStartWorking);

    //                var vToday = new Date();
    //                var vAge = Math.floor((vToday - vStartWorking) / (365.25 * 24 * 60 * 60 * 1000));
    //                $("#txtWorkingAge").val(vAge);

    //            }

    //            if (dt.empModel.appointment_date != null) {
    //                var vAppointment = new Date(parseInt(dt.empModel.appointment_date.substr(6)));
    //                $("#dtProbation").jqxDateTimeInput('setDate', vAppointment);
    //            }

    //            $("#txtPhone").val(dt.empModel.phone_number);
    //            $("#txtEmail").val(dt.empModel.email);
    //            $("#txtKdCountry").val(dt.empModel.int_country);
    //            $("#txtKdCountry").data("country_code", dt.empModel.country_code);
    //            $("#txtNmCountry").val(dt.empModel.country_name);

    //            $("#txtNoKtp").val(dt.empModel.identity_number);
    //            $("#txtLastEducation").val(dt.empModel.last_education);
    //            $("#txtLastEmployment").val(dt.empModel.last_employment);
    //            $("#txtKdBank").val(dt.empModel.bank_code);
    //            //$("#btnKdBank").val({ theme: vTheme });
    //            $("#txtNmBank").val(dt.empModel.bank_name);
    //            $("#txtBankAcc").val(dt.empModel.bank_account);
    //            $("#txtBankAccName").val(dt.empModel.bank_acc_name);
    //            $("#txtKdStatus").val(dt.empModel.int_status);
    //            $("#txtKdStatus").data("status_code", dt.empModel.status_code);
    //            //$("#btnKdStatus").jqxButton({ theme: vTheme });
    //            $("#txtNmStatus").val(dt.empModel.status_name);
    //            $("#txtKdAtasan").val(dt.empModel.spv_code);
    //            //$("#btnkdAtasan").jqxButton({ theme: vTheme });
    //            $("#txtNmAtasan").val(dt.empModel.spv_name);

    //            var vFlag = false;
    //            if (dt.empModel.flag_managerial == 1) { vFlag = true; }
    //            $("#chkManagerial").jqxCheckBox({ checked: vFlag });

    //            var vFlag_shiftable = false;
    //            if (dt.empModel.flag_shiftable == 1) { vFlag = true; }
    //            $("#chkSpecialLate").jqxCheckBox({ checked: vFlag_shiftable });

    //            //#endregion Employee Detail
    //            f_HideLoaderModal();
    //        }
    //    });
}

$(document).ready(function () {
    //#region INIT COMPONENT
    $("#dtTglReq").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtNoReq").jqxInput({ theme: vTheme, width: 200 });


    $("#txtBagian").jqxInput({ theme: vTheme, width: 300 });
    $('#txtAlasan').jqxTextArea({
        theme: vTheme, height: 50, width: 300, minLength: 1
    });

    $("#cmbKelamin").jqxComboBox({ theme: vTheme, width: 150, source: vCmbGender, selectedIndex: 0 });
    $("#cmbKelamin input").attr('disabled', true);

    $("#txtMinUsia").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });
    $("#txtPendidikan").jqxInput({ theme: vTheme, width: 300 });
    $("#txtPengalaman").jqxInput({ theme: vTheme, width: 300 });
    $("#txtBInggris").jqxInput({ theme: vTheme, width: 300 });
    $("#txtSertifikasi").jqxInput({ theme: vTheme, width: 300 });

    $("#cmbStatusKawin").jqxComboBox({ theme: vTheme, width: 150, source: vCmbMarital, selectedIndex: 0 });
    $("#cmbStatusKawin input").attr('disabled', true);

    $("#txtNamaJabatan").jqxInput({ theme: vTheme, width: 300 });
    $("#txtTujuanJabatan").jqxInput({ theme: vTheme, width: 300 });
    $("#txtBertanggungJawab").jqxInput({ theme: vTheme, width: 300 });
    $("#txtJmlBawahan").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });
    $("#txtWewenang").jqxInput({ theme: vTheme, width: 300 });
    $("#txtHubungan").jqxInput({ theme: vTheme, width: 300 });
    $("#txtKerjaSendiri").jqxInput({ theme: vTheme, width: 300 });

    $("#cmbSumber").jqxComboBox({ theme: vTheme, width: 150, source: vCmbGender, selectedIndex: 0 });
    $("#cmbSumber input").attr('disabled', true);

    $("#txtJmlButuh").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });

    $("#dtWorkPlan").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtCatatan").jqxTextArea({
        theme: vTheme, height: 50, width: 300, minLength: 1
    });


    var vIdRequest = $.urlParam('pIdRequest')

    if (vIdRequest == "") {
        f_empty_form();
        f_HideLoaderModal();
    } else {
        Form_Load(vIdRequest);
    }

    //#endregion


    //$("#ReqToolBar").jqxToolBar({
    //    theme: vTheme,
    //    width: '100%', height: 35, tools: 'button | button | button', rtl: true,
    //    initTools: function (type, index, tool, menuToolIninitialization) {
    //        if (type == "button") {
    //            tool.height("25px");
    //            tool.width("110px");
    //        }
    //        switch (index) {
    //            case 0:
    //                var button = $("<div>" +
    //                                    "<img style='vertical-align:middle' src='" + base_url + "/content/images/Submit Resume_24_grey.png'/>" +
    //                                    "<span>ACTIVATE</span> " +
    //                               "</div>");
    //                tool.append(button);
    //                tool.on("click", function () {
    //                    f_MessageBoxShow("Karyawan di aktifkan...");
    //                });
    //                break;
    //            case 1:
    //                var button = $("<div>" +
    //                                    "<img style='vertical-align:middle' src='" + base_url + "/content/images/Save as_24_grey.png'/>" +
    //                                    "<span>SAVE DATA</span> " +
    //                               "</div>");
    //                tool.append(button);
    //                tool.on("click", function () {
    //                    f_InsertEmployee();
    //                });
    //                break;
    //            case 2:
    //                var button = $("<div>" +
    //                                    "<img style='vertical-align:middle' src='" + base_url + "/content/images/add property_24_grey.png'/>" +
    //                                    "<span>NEW DATA</span> " +
    //                               "</div>");
    //                tool.append(button);
    //                tool.on("click", function () {
    //                    f_empty_employee_form();
    //                });
    //                break;

    //        }
    //    }
    //});


});
