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
var vDataFamily = [];
var vSrcFamily =
{
    localdata: vDataFamily,
    datatype: "json",
    datafields: [
            { name: 'employment_code' },
            { name: 'seq_no' },
            { name: 'name' },
            { name: 'sex' },
            { name: 'relationship' },
            { name: 'nm_rel' },
            { name: 'date_birth', type: "date" },
            { name: 'education' },
            { name: 'employment' },
            { name: 'address' },
            { name: 'chk_address' }
    ]
};
//#endregion


$(document).ready(function () {
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
    var vPesanFam = "";

    $("#jqxNotification").jqxNotification({
        width: "100%",height:"40px",theme:vTheme,
        appendContainer: "#container",
        opacity: 0.9, autoClose: true, template: "error"
    });

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
        height: 280, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#btnYes").jqxButton({ theme: vTheme, height: 30, width: 60 });
    $("#btnNo").jqxButton({ theme: vTheme, height: 30, width: 60 });

    $("#modYesNo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    //#endregion MODAL FAMILY



    //#endregion

    //#region UNTUK CENTER MODAL DIALOG
    //KEEP CENTERED

    function f_PosX(pLebar) {
        winWidth = $(window).width();
        var posX = (winWidth / 2) - ((pLebar).width() / 2) + $(window).scrollLeft();
        return posX;
    }

    function f_PosY(pTinggi) {
        winHeight = $(window).height();
        var posY = (winHeight / 2) - ((pTinggi).height() / 2) + $(window).scrollTop();
        return posY;
    }

    $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
    $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });

    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
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
                { text: 'Conn. With Emp', datafield: 'nm_rel', width: 130 },
                {
                    text: 'Date Of Birth', datafield: 'date_birth', width: 200,
                    align: 'center', cellsalign: 'center', cellsformat: 'dd-MMM-yy'
                },
                { text: 'Education', datafield: 'education' },
                { text: 'Employment', datafield: 'employment' },
                { text: 'Address', datafield: 'address', hidden: true },
                { text: 'chk_address', datafield: 'chk_address', hidden: true }
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
});
