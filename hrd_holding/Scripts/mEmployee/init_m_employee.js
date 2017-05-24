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

    $("#modYesNo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    
    //#endregion

    //#region UNTUK CENTER MODAL DIALOG
    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modEducation').jqxWindow({ position: { x: f_PosX($('#modEducation')), y: f_PosY($('#modEducation')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        $('#modFamily').jqxWindow({ position: { x: f_PosX($('#modFamily')), y: f_PosY($('#modFamily')) } });
        $('#modEducation').jqxWindow({ position: { x: f_PosX($('#modEducation')), y: f_PosY($('#modEducation')) } });
        $('#modYesNo').jqxWindow({ position: { x: f_PosX($('#modYesNo')), y: f_PosY($('#modYesNo')) } });
    });
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

    initGridSkill();
    initGridExperience();
    initGridTraining();
    initGridContract();

    $("#btnYes").jqxButton({ theme: vTheme, height: 30, width: 60 });
    $("#btnNo").jqxButton({ theme: vTheme, height: 30, width: 60 });

});
