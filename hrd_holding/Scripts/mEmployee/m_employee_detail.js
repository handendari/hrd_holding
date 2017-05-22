$(document).ready(function () {
    var vDataTbl = {};
    var vCmbReligion = [
            "ISLAM",
            "KRISTEN",
            "KATOLIK",
            "HINDU",
            "BUDHA",
            "OTHERS"
    ];

    var vCmbRelation = [
            "SUAMI",
            "ISTRI",
            "ANAK",
            "ORANG TUA",
            "SAUDARA"
    ];

    var vCmbGender = [
            "LAKI LAKI",
            "PEREMPUAN"
    ];

    //#region DATASOURCE TABEL
    var vDataFamily = new Array();
    var vSrcFamily =
    {
        localdata: vDataFamily,
        datatype: "array",
        datafields: [
             { name: 'employment_code' },
             { name: 'seq_no' },
             { name: 'name' },
             { name: 'sex' },
             { name: 'relationship' },
             { name: 'nm_rel' },
             { name: 'date_birth', type: "date" },
             { name: 'education' },
             { name: 'employment' }
        ]
    };
    //#endregion

    //GET WINDOW HEIGHT AND WIDTH
    var winHeight = $(window).height();
    var winWidth = $(window).width();

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

    $("#btnFamilyNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnFamilyEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnFamilyDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#btnSkillNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnSkillEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnSkillDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#btnExpNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#btnEduNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#btnTrainingNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnTrainingEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnTrainingDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

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

    //#region MODAL FAMILY 
    $("#txtFamName").jqxInput({ theme: vTheme })
    $("#txtFamDob").jqxDateTimeInput({ theme: vTheme });
    $("#txtFamEducation").jqxInput({ theme: vTheme })
    $("#txtFamEmployment").jqxInput({ theme: vTheme })
    $("#cmbFamGender").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbGender, selectedIndex: 0
    });
    $("#cmbFamRelation").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbRelation, selectedIndex: 0
    });
    $("#chkFamAddress").jqxCheckBox({ theme: vTheme });
    $('#txtFamAddress').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Alamat Keluarga',
        height: 50, width: 200, minLength: 1
    });
    $("#btnModFamSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnModFamCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modFamily").jqxWindow({
        height: 250, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion
    //#endregion

    //#region UNTUK CENTER MODAL DIALOG
    //KEEP CENTERED
    var posX = (winWidth / 2) - ($('#modFamily').width() / 2) + $(window).scrollLeft();
    var posY = (winHeight / 2) - ($('#modFamily').height() / 2) + $(window).scrollTop();
    $('#modFamily').jqxWindow({ position: { x: posX, y: posY } });

    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        winHeight = $(window).height();
        winWidth = $(window).width();
        posX = (winWidth / 2) - ($('#modFamily').width() / 2) + $(window).scrollLeft();
        posY = (winHeight / 2) - ($('#modFamily').height() / 2) + $(window).scrollTop();
        $('#modFamily').jqxWindow({ position: { x: posX, y: posY } });
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        winHeight = $(window).height();
        winWidth = $(window).width();
        posX = (winWidth / 2) - ($('#modFamily').width() / 2) + $(window).scrollLeft();
        posY = (winHeight / 2) - ($('#modFamily').height() / 2) + $(window).scrollTop();
        $('#modFamily').jqxWindow({ position: { x: posX, y: posY } });
    });
    //#endregion


    //#region Table FAMILY
    var initGridFamily = function () {
        var dataAdapter = new $.jqx.dataAdapter(vSrcFamily);

        $("#tblFamily").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: dataAdapter,
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'Seq No', datafield: 'seq_no', width: 50, cellsalign: 'center' },
                { text: 'Name', datafield: 'name', width: 300 },
                { text: 'Gender', datafield: 'sex', width: 50, hidden: true },
                { text: 'Relationship', datafield: 'relationship', hidden: true },
                { text: 'Conn. With Emp', datafield: 'nm_rel' },
                {
                    text: 'Date Of Birth', datafield: 'date_birth', width: 200,
                    align: 'center', cellsalign: 'center', cellsformat: 'dd-MMM-yy'
                },
                { text: 'Education', datafield: 'education' },
                { text: 'Employment', datafield: 'employment' }
            ]
        });
    }
    //#endregion

    //#region Table SKILL
    var initGridSkill = function () {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'skill' },
                { name: 'nm_level' },
                { name: 'description' }
           ]
       };

        $("#tblSkill").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: new $.jqx.dataAdapter(source),
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Skill', datafield: 'skill' },
                { text: 'Level', datafield: 'nm_level' },
                { text: 'Description', datafield: 'description' }
            ]
        });
    }
    //#endregion

    //#region Table EXPERIENCE
    var initGridExperience = function () {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'company_name' },
                { name: 'usaha' },
                { name: 'department_name' },
                { name: 'last_title' },
                { name: 'start_working', type: 'date' },
                { name: 'end_working', type: 'date' },
                { name: 'last_salary' },
                { name: 'reason_stop_working' }
           ]
       };

        $("#tblExperience").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(source),
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Company Name', datafield: 'company_name', width: 200 },
                { text: 'Usaha', datafield: 'usaha', width: 200 },
                { text: 'Departement', datafield: 'department_name', width: 200 },
                { text: 'Last Title', datafield: 'last_title', width: 200 },
                {
                    text: 'Start Working', datafield: 'start_working',
                    filtertype: 'date', cellsalign: 'center',
                    cellsformat: 'dd-MMM-yy', width: 100
                },
                {
                    text: 'End Working', datafield: 'end_working',
                    filtertype: 'date', cellsalign: 'center',
                    cellsformat: 'dd-MMM-yy', width: 100
                },
                {
                    text: 'Last Salary', datafield: 'last_salary',
                    cellsalign: 'right', align: 'center',
                    cellsformat: 'd2', width: 150
                },
                { text: 'Reason', datafield: 'reason_stop_working', width: 300 }
            ]
        });
    }
    //#endregion

    //#region Table EDUCATION
    var initGridEducation = function () {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'nm_jenjang' },
                { name: 'school' },
                { name: 'jurusan' },
                { name: 'city' },
                { name: 'start_year', type: "date" },
                { name: 'end_year', type: "date" }
           ]
       };
        var dataAdapter = new $.jqx.dataAdapter(source);

        $("#tblEducation").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: dataAdapter,
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Jenjang', datafield: 'nm_jenjang' },
                { text: 'Sekolah', datafield: 'school' },
                { text: 'Jurusan', datafield: 'jurusan' },
                { text: 'Kota', datafield: 'city' },
                {
                    text: 'Start Date', datafield: 'start_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                },
                {
                    text: 'End Date', datafield: 'end_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                }
            ]
        });
    }

    //#endregion

    //#region Table TRAINING
    var initGridTraining = function () {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'orginizer' },
                { name: 'material' },
                { name: 'place' },
                { name: 'start_date' },
                { name: 'end_date' }
           ]
       };

        var dataAdapter = new $.jqx.dataAdapter(source);

        $("#tblTraining").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(source),
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Orginizer', datafield: 'orginizer' },
                { text: 'Material', datafield: 'material' },
                { text: 'Place', datafield: 'place' },
                { text: 'Start Date', datafield: 'start_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'End Date', datafield: 'end_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' }
            ]
        });
    }
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

    initGridFamily();
    initGridEducation();
    initGridSkill();
    initGridExperience();
    initGridTraining();
    initGridContract();


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
                        for (var i = 0; i < dt.listFamily.length; i++) {
                            var row = {};
                            row["employment_code"] = dt.listFamily[i].employee_code;
                            row["seq_no"] = dt.listFamily[i].seq_no;
                            row["name"] = dt.listFamily[i].name;
                            row["sex"] = dt.listFamily[i].sex;
                            row["relationship"] = dt.listFamily[i].relationship;
                            row["nm_rel"] = dt.listFamily[i].nm_rel;
                            row["date_birth"] = new Date(parseInt(dt.listFamily[i].date_birth.substr(6)));
                            row["education"] = dt.listFamily[i].education;
                            row["employment"] = dt.listFamily[i].employment;

                            vDataFamily[i] = row;
                        }
                        var vAdapter = new $.jqx.dataAdapter(vSrcFamily);
                        $("#tblFamily").jqxGrid({ source: vAdapter });
                    }

                    //#endregion

                    //#region ListEducation
                    if (dt.listEducation != null) {
                        if (dt.listEducation.length > 0) {
                            for (var i = 0; i < dt.listEducation.length; i++) {
                                $("#tblEducation").jqxGrid('beginupdate');

                                var row = {};
                                row["employment_code"] = dt.listEducation[i].employee_code;
                                row["seq_no"] = dt.listEducation[i].seq_no;
                                row["nm_jenjang"] = dt.listEducation[i].nm_jenjang;
                                row["school"] = dt.listEducation[i].school;
                                row["jurusan"] = dt.listEducation[i].jurusan;
                                row["city"] = dt.listEducation[i].city;
                                row["start_year"] = new Date(parseInt(dt.listEducation[i].start_year.substr(6)));
                                row["end_year"] = new Date(parseInt(dt.listEducation[i].end_year.substr(6)));

                                var commit = $("#tblEducation").jqxGrid('addrow', null, row);

                                $("#tblEducation").jqxGrid('endupdate');
                            }
                        }
                    }
                    //#endregion

                    //#region ListSkill
                    if (dt.listSkill != null && dt.listSkill.length > 0) {
                        for (var i = 0; i < dt.listSkill.length; i++) {
                            $("#tblSkill").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listSkill[i].employee_code;
                            row["seq_no"] = dt.listSkill[i].seq_no;
                            row["skill"] = dt.listSkill[i].skill;
                            row["nm_level"] = dt.listSkill[i].nm_level;
                            row["description"] = dt.listSkill[i].description;

                            var commit = $("#tblSkill").jqxGrid('addrow', null, row);

                            $("#tblSkill").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                    //#region ListExperience
                    if (dt.listExperience != null && dt.listExperience.length > 0) {
                        for (var i = 0; i < dt.listExperience.length; i++) {
                            $("#tblExperience").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listSkill[i].employee_code;
                            row["seq_no"] = dt.listExperience[i].seq_no;
                            row["company_name"] = dt.listExperience[i].company_name;
                            row["usaha"] = dt.listExperience[i].usaha;
                            row["department_name"] = dt.listExperience[i].department_name;
                            row["last_title"] = dt.listExperience[i].last_title;
                            row["start_working"] = new Date(parseInt(dt.listExperience[i].start_working.substr(6)));
                            row["end_working"] = new Date(parseInt(dt.listExperience[i].end_working.substr(6)));
                            row["last_salary"] = dt.listExperience[i].last_salary;
                            row["reason_stop_working"] = dt.listExperience[i].reason_stop_working;

                            var commit = $("#tblExperience").jqxGrid('addrow', null, row);

                            $("#tblExperience").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                    //#region ListTraining
                    if (dt.listTraining != null && dt.listTraining.length > 0) {
                        for (var i = 0; i < dt.listTraining.length; i++) {
                            $("#tblTraining").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listExperience[i].employee_code;
                            row["seq_no"] = dt.listExperience[i].seq_no;
                            row["orginizer"] = dt.listExperience[i].orginizer;
                            row["material"] = dt.listExperience[i].material;
                            row["place"] = dt.listExperience[i].place;
                            row["start_date"] = new Date(parseInt(dt.listExperience[i].start_date.substr(6)));
                            row["end_date"] = new Date(parseInt(dt.listExperience[i].end_date.substr(6)));

                            var commit = $("#tblTraining").jqxGrid('addrow', null, row);

                            $("#tblTraining").jqxGrid('endupdate');
                        }
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

    $('#btnFamilyNew').on('click', function (event) {
        $("#modFamily").jqxWindow('open');
    });

    $('#btnModFamCancel').on('click', function (event) {
        $("#modFamily").jqxWindow('close');
    });

    $('#btnFamilyEdit').on('click', function (event) {
        //var getselectedrowindexes = $('#tblFamily').jqxGrid('getselectedrowindexes');
        //if (getselectedrowindexes.length > 0) {
        //    // returns the selected row's data.
        //    var selectedRowData = $('#tblFamily').jqxGrid('getrowdata', getselectedrowindexes[0]);
        //    alert(JSON.stringify(selectedRowData));
        //}

        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');
        var rd = $('#tblFamily').jqxGrid('getrowdata', rowindex);
        //alert(JSON.stringify(rd));

        $("#txtFamName").val(rd.name);
        $("#txtFamName").data("fam_seq_no", rd.seq_no);

        $("#txtFamDob").jqxDateTimeInput('setDate', rd.date_birth);

        var vFamGender = rd.sex;
        $("#cmbFamGender").jqxComboBox({ selectedIndex: vFamGender });

        var vFamRelation = rd.relationship;
        $("#cmbFamRelation").jqxComboBox({ selectedIndex: vFamRelation });

        $("#txtFamEducation").val(rd.education)
        $("#txtFamEmployment").val(rd.employment)

        $("#txtFamAddress")

        $("#modFamily").jqxWindow('open');
    });

    $('#btnFamilyDelete').on('click', function (event) {
        var selectedRowIndex = $("#tblFamily").jqxGrid('selectedrowindex');
        vDataFamily.splice(selectedRowIndex, 1);

        $("#tblFamily").jqxGrid({
            source: new $.jqx.dataAdapter(vSrcFamily)
        });

    });

    $('#btnModFamSave').on('click', function (event) {
        if ($("#txtId").data("employee_code") > 0
            || $("#txtId").data("employee_code") != null
            || $("#txtId").data("employee_code") != "") {

            var vAlamat = "";
            if ($("#chkFamAddress").jqxCheckBox('checked')) {
                vAlamat = $("#txtAddress").val();
            } else {
                vAlamat = $("#txtFamAddress").val();
            }

            var vModel = JSON.stringify({
                employee_code: $("#txtId").data("employee_code"),
                employee_name: $("#txtFullName").val(),
                seq_no: $("#txtFamName").data("fam_seq_no"),
                name: $("#txtFamName").val(),
                relationship: $("#cmbFamRelation").jqxComboBox('listBox').selectedIndex,
                nm_rel: $("#cmbFamRelation").val(),
                date_birth: $("#txtFamDob").jqxDateTimeInput('getDate'),
                sex: $("#cmbFamGender").jqxComboBox('listBox').selectedIndex,
                education: $("#txtFamEducation").val(),
                employment: $("#txtFamEmployment").val(),
                chk_address: $("#chkFamAddress").jqxCheckBox('checked'),
                address: vAlamat
            });

            $.ajax({
                url: base_url + "EmployeeFamily/UpdateEmployeeFamily",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vHasil['isValid'];

                    alert(d.vHasil['message']);

                }
            });
        }
    });

});
