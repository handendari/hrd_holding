f_ShowLoaderModal();

var vCmbSource = [
        "INTERNAL",
        "EXTERNAL"
];

function f_empty_form() {

    $("#dtTglReq").jqxDateTimeInput('setDate', new Date());
    $("#txtNoReq").val("");
    $("#txtNoReq").data("id_req", "");

    $("#txtBagian").val("");
    $('#txtAlasan').val("");

    $("#cmbKelamin").jqxComboBox({ selectedIndex: 0 });

    $("#txtMinUsia").val(0);
    $("#cmbPendidikan").jqxComboBox({ selectedIndex: 0 });
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
    //f_ShowLoaderModal();

    var vIdReq = $("#txtNoReq").data("id_req") == undefined ? "" : $("#txtNoReq").data("id_req");
    var vBranch = $("#txtIntBranch").data("branch_code");

    var vModel = JSON.stringify({        
        id : vIdReq,//$("#txtNoReq").data("id_req"),
        company_code : $("#txtIntCompany").data("company_code"),
        branch_code : vBranch,
        date_req : $("#dtTglReq").jqxDateTimeInput('getDate'),
        no_req : $("#txtNoReq").val(),
        position_need : $("#txtBagian").val(),
        reason : $("#txtAlasan").val(),
        sex : $("#cmbKelamin").jqxComboBox('listBox').selectedIndex,
        age_min : $("#txtMinUsia").val(),
        education: $("#cmbPendidikan").jqxComboBox('listBox').selectedIndex,
        job_experience : $("#txtPengalaman").val(),
        english_skill : $("#txtBInggris").val(),
        certificate : $("#txtSertifikasi").val(),
        marital_status : $("#cmbStatusKawin").jqxComboBox('listBox').selectedIndex,
        job_title : $("#txtNamaJabatan").val(),
        job_purpose : $("#txtTujuanJabatan").val(),
        responsibility : $("#txtBertanggungJawab").val(),
        count_staff : $("#txtJmlBawahan").val(),
        authority : $("#txtWewenang").val(),
        job_relationship : $("#txtHubungan").val(),
        job_self : $("#txtKerjaSendiri").val(),
        source_employee : $("#cmbSumber").jqxComboBox('listBox').selectedIndex,
        work_plan : $("#dtWorkPlan").jqxDateTimeInput('getDate'),
        note : $("#txtCatatan").val(),
        count_needed : $("#txtJmlButuh").val()
        //request_by,
        //flag_status,
        //flag_approval,
        //user_approval,
    });

    if (vIdReq != "") {

        $.ajax({
            url: base_url + "ReqRecruitment/UpdateRequest",
            type: "POST",
            contentType: "application/json",
            data: vModel,
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    Form_Load(vBranch, vIdReq);
                    f_HideLoaderModal();
                }
                f_MessageBoxShow(d.vResp['message']);
            }
        });
    } else {
        $.ajax({
            url: base_url + "ReqRecruitment/InsertRequest",
            type: "POST",
            contentType: "application/json",
            data: vModel,
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    Form_Load(vBranch, vIdReq);
                    f_HideLoaderModal();
                }
                f_MessageBoxShow(d.vResp['message']);
            }
        });
    }

}

function Form_Load(pBranch, pIdReq) {
    $.ajax({
        dataType: "json",
        url: base_url + "ReqRecruitment/GetRequestInfo",
        data: {'pBranchCode': pBranch, 'pIdCode': pIdReq },
        success: function (dt) {
            //alert(data.empModel.employee_code);

            //if (dt.empModel.employee_code == null) {
            //    window.location = base_url + "employee/index";
            //} else {

            $("#txtIntCompany").val(dt.int_company);
            $("#txtIntCompany").data("company_code", dt.company_code);
            $("#txtCompanyName").val(dt.company_name);

            $("#txtIntBranch").val(dt.int_branch);
            $("#txtIntBranch").data("branch_code", dt.branch_code);
            $("#txtBranchName").val(dt.branch_name);

            if (dt.date_req != null) {
                var vTglReq = new Date(parseInt(dt.date_req.substr(6)));
                $("#dtTglReq").jqxDateTimeInput('setDate', vTglReq);
            }
            $("#txtNoReq").val(dt.no_req);
            $("#txtNoReq").data("id_req", dt.id);

            $("#txtBagian").val(dt.position_need);
            $('#txtAlasan').val(dt.reason);

            var vGender = dt.sex;
            $("#cmbKelamin").jqxComboBox({ selectedIndex: vGender });

            $("#txtMinUsia").val(dt.age_min);
            $("#txtPendidikan").val(dt.education);
            $("#txtPengalaman").val(dt.job_experience);
            $("#txtBInggris").val(dt.english_skill);
            $("#txtSertifikasi").val(dt.certificate);

            var vMarital = dt.marital_status;
            $("#cmbStatusKawin").jqxComboBox({ selectedIndex: vMarital });

            $("#txtNamaJabatan").val(dt.job_title);
            $("#txtTujuanJabatan").val(dt.job_purpose);
            $("#txtBertanggungJawab").val(dt.responsibility);
            $("#txtJmlBawahan").val(dt.count_staff);
            $("#txtWewenang").val(dt.authority);
            $("#txtHubungan").val(dt.job_relationship);
            $("#txtKerjaSendiri").val(dt.job_self);

            var vSumber = dt.source_employee;
            $("#cmbSumber").jqxComboBox({ selectedIndex: vSumber });

            $("#txtJmlButuh").val(dt.count_needed);

            if (dt.work_plan != null) {
                var vWorkPlan = new Date(parseInt(dt.work_plan.substr(6)));
                $("#dtWorkPlan").jqxDateTimeInput('setDate', vWorkPlan);
            }
            $("#txtCatatan").val(dt.note);
            f_HideLoaderModal();
        }
    });
}

$(document).ready(function () {
    //#region INIT COMPONENT
    $("#txtIntCompany").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#txtCompanyName").jqxInput({ theme: vTheme, width: 300, disabled: true });
    $("#txtIntBranch").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#txtBranchName").jqxInput({ theme: vTheme, width: 300, disabled: true });

    $("#dtTglReq").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtNoReq").jqxInput({ theme: vTheme, width: 200 });


    $("#txtBagian").jqxInput({ theme: vTheme, width: 300 });
    $('#txtAlasan').jqxTextArea({
        theme: vTheme, height: 50, width: 300, minLength: 1
    });

    $("#cmbKelamin").jqxComboBox({ theme: vTheme, width: 150, source: vCmbGender, selectedIndex: 0 });
    $("#cmbKelamin input").attr('disabled', true);

    $("#txtMinUsia").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });

    $("#cmbPendidikan").jqxComboBox({ theme: vTheme, width: 150, source: vCmbEducation, selectedIndex: 0 });
    $("#cmbPendidikan input").attr('disabled', true);

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

    $("#cmbSumber").jqxComboBox({ theme: vTheme, width: 150, source: vCmbSource, selectedIndex: 0 });
    $("#cmbSumber input").attr('disabled', true);

    $("#txtJmlButuh").jqxNumberInput({ theme: vTheme, width: 70, decimalDigits: 0, digits: 2, min: 0, spinButtons: true });

    $("#dtWorkPlan").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtCatatan").jqxTextArea({
        theme: vTheme, height: 50, width: 400, minLength: 1
    });


    var vBranch = $.urlParam('pBranch')
    var vIdRequest = $.urlParam('pIdCode')

    Form_Load(vBranch, vIdRequest);

    //#endregion


    $("#ReqToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button | button | button', rtl: true,
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == "button") {
                tool.height("25px");
                tool.width("110px");
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Delete Property_24_grey.png'/>" +
                                        "<span>DELETE</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.css('margin-left', '50px')
                    tool.on("click", function () {
                        f_MessageBoxShow("Karyawan di aktifkan...");
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Submit Resume_24_grey.png'/>" +
                                        "<span>APPROVE</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_MessageBoxShow("Karyawan di aktifkan...");
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Save as_24_grey.png'/>" +
                                        "<span>SAVE DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        // set timeout
                        f_ShowLoaderModal();
                        setTimeout(f_InsertRequest(), 600000);
                    });
                    break;
                case 3:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/add property_24_grey.png'/>" +
                                        "<span>NEW DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_empty_form();
                    });
                    break;

            }
        }
    });

    //f_HideLoaderModal();
});
