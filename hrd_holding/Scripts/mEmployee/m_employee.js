var vDataTbl = {};

$(document).ready(function () {
    //#region INIT COMPONENT
    $("#txtId").jqxInput({ theme: vTheme });
    $("#txtFullName").jqxInput({ theme: vTheme });
    $("#txtNickName").jqxInput({ theme: vTheme });
    $("#txtPob").jqxInput({ theme: vTheme });
    $("#dtDob").jqxDateTimeInput({ theme: vTheme });
    $("#cmbGender").jqxComboBox({ theme: vTheme, source: vCmbGender, selectedIndex: 0 });
    $("#cmbReligion").jqxComboBox({ theme: vTheme, source: vCmbReligion, selectedIndex: 0 });
    $("#txtChild").jqxInput({ theme: vTheme });
    $("#txtAddress").jqxInput({ theme: vTheme });
    $("#txtKdDepartement").jqxInput({ theme: vTheme });
    $("#btnKdDepartement").jqxButton({ theme: vTheme });
    $("#txtNmDepartement").jqxInput({ theme: vTheme });
    $("#txtKdJobTitle").jqxInput({ theme: vTheme });
    $("#btnKdJobTitle").jqxButton({ theme: vTheme });
    $("#txtNmJobTitle").jqxInput({ theme: vTheme });
    $("#txtKdGrade").jqxInput({ theme: vTheme });
    $("#btnKdGrade").jqxButton({ theme: vTheme });
    $("#txtNmGrade").jqxInput({ theme: vTheme });
    $("#txtKdLevel").jqxInput({ theme: vTheme });
    $("#btnKdLevel").jqxButton({ theme: vTheme });
    $("#txtNmLevel").jqxInput({ theme: vTheme });
    $("#dtStartWorking").jqxDateTimeInput({ theme: vTheme });
    $("#dtProbation").jqxDateTimeInput({ theme: vTheme });
    $("#txtWorkingAge").jqxInput({ theme: vTheme });
    $("#txtPhone").jqxInput({ theme: vTheme });
    $("#txtEmail").jqxInput({ theme: vTheme });
    $("#txtKdCountry").jqxInput({ theme: vTheme });
    $("#btnKdCountry").jqxButton({ theme: vTheme });
    $("#txtNmCountry").jqxInput({ theme: vTheme });
    $("#txtNoKtp").jqxInput({ theme: vTheme });
    $("#txtLastEducation").jqxInput({ theme: vTheme });
    $("#txtLastEmployment").jqxInput({ theme: vTheme });
    $("#txtKdBank").jqxInput({ theme: vTheme });
    $("#btnKdBank").jqxButton({ theme: vTheme });
    $("#txtNmBank").jqxInput({ theme: vTheme });
    $("#txtBankAcc").jqxInput({ theme: vTheme });
    $("#txtBankAccName").jqxInput({ theme: vTheme });
    $("#txtKdStatus").jqxInput({ theme: vTheme });
    $("#btnKdStatus").jqxButton({ theme: vTheme });
    $("#txtNmStatus").jqxInput({ theme: vTheme });
    $("#txtKdAtasan").jqxInput({ theme: vTheme });
    $("#btnkdAtasan").jqxButton({ theme: vTheme });
    $("#txtNmAtasan").jqxInput({ theme: vTheme });

    $("#btnContractNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnContractEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnContractDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#chkManagerial").jqxCheckBox({ theme: vTheme });
    $("#chkSpecialLate").jqxCheckBox({ theme: vTheme });

    $("#optProbation").jqxRadioButton({ theme: vTheme, groupName: "status" });
    $("#optActive").jqxRadioButton({ theme: vTheme, groupName: "status" });
    $("#optNonActive").jqxRadioButton({ theme: vTheme, groupName: "status" });

    $('#jqxTabs').jqxTabs({
        width: '100%', height: 300, theme: vTheme,
        position: 'top', selectionTracker: 1, animationType: 'fade'
    });

    $("#modYesNo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false,
        modalZIndex:999
    });

    //#endregion

    //#region UNTUK CENTER MODAL DIALOG
    function f_PosisiModalDialog() {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modEducation').jqxWindow({ position: { x: f_PosX($('#modEducation')), y: f_PosY($('#modEducation')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
        $('#modSkill').jqxWindow({ position: { x: f_PosX($('#modSkill')), y: f_PosY($('#modSkill')) } });
        $('#modExperience').jqxWindow({ position: { x: f_PosX($('#modExperience')), y: f_PosY($('#modExperience')) } });
        $('#modTraining').jqxWindow({ position: { x: f_PosX($('#modTraining')), y: f_PosY($('#modTraining')) } });
        $('#modLookUp').jqxWindow({ position: { x: f_PosX($('#modLookUp')), y: f_PosY($('#modLookUp')) } });
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

    $("#btnYes").jqxButton({ theme: vTheme, height: 30, width: 60 });
    $("#btnNo").jqxButton({ theme: vTheme, height: 30, width: 60 });
    
    Form_Load("35151269069300041", 1);

    function Form_Load(pEmployeeCode, pSeqNo) {
        $.ajax({
            dataType: "json",
            url: base_url + "Employee/GetEmployeeInfoAll?EmployeeCode=" + pEmployeeCode + "&seqno=" + pSeqNo,
            success: function (dt) {
                //alert(data.empModel.employee_code);

                if (dt.empModel.employee_code == null) {
                    window.location = base_url + "employee/index";
                } else {

                    //#region Employee Detail
                    $("#txtId").val(dt.empModel.nik);
                    $("#txtId").data("employee_code", dt.empModel.employee_code);

                    //alert($("#txtId").data("employee_code"));

                    $("#txtFullName").val(dt.empModel.employee_name);
                    $("#txtNickName").val(dt.empModel.employee_nick_name);
                    $("#txtPob").val(dt.empModel.place_birth);

                    if (dt.empModel.date_birth != null) {
                        var vDob = new Date(parseInt(dt.empModel.date_birth.substr(6)));
                        $("#dtDob").jqxDateTimeInput('setDate', vDob);
                    }

                    var vGender = dt.empModel.sex;
                    $("#cmbGender").jqxComboBox({ selectedIndex: vGender });

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
                    $("#txtKdCountry").val(dt.empModel.country_code);
                    //$("#btnKdCountry").jqxButton({ theme: vTheme });
                    $("#txtNmCountry").val(dt.empModel.country_name);
                    $("#txtNoKtp").val(dt.empModel.identity_number);
                    $("#txtLastEducation").val(dt.empModel.last_education);
                    $("#txtLastEmployment").val(dt.empModel.last_employment);
                    $("#txtKdBank").val(dt.empModel.bank_code);
                    //$("#btnKdBank").val({ theme: vTheme });
                    $("#txtNmBank").val(dt.empModel.bank_name);
                    $("#txtBankAcc").val(dt.empModel.bank_account);
                    $("#txtBankAccName").val(dt.empModel.bank_acc_name);
                    $("#txtKdStatus").val(dt.empModel.status_code);
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
                    if (dt.listTraining != null && dt.listTraining.length > 0) {
                        f_FIllTableTrn(dt.listTraining);
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

                }
            }
        });
    }

    $('#btnYes').on('click', function (event) {
        var vEmpCode = $("#txtId").data("employee_code");

        var selectedTab = $('#jqxTabs').jqxTabs('selectedItem');

        switch (selectedTab) {
            case 0:
                f_DeleteEmployeeFamily(vEmpCode);
                break;
            case 1:
                f_DeleteEmployeeEducation(vEmpCode);
                break;

        }
    });

    $('#btnNo').on('click', function (event) {
        $("#modYesNo").jqxWindow("close");
    });
});
