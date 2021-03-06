﻿
//#region DATASOURCE TABEL
var vDataExp = [];
var vSrcExp =
{
    localdata: vDataExp,
    datatype: "json",
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
         { name: 'reason_stop_working' },
         { name: 'description' }
    ]
};

//#endregion

function f_UpdateTblExperience() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeExperience/GetEmployeeExperienceList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
          //  if (dt.listExp != null && dt.listExp.length > 0) {
                f_FillTableExp(dt.listExp);
          //  }
        }
    });
}

function f_FillTableExp(listExp) {
    vDataExp.length = 0;
    for (var i = 0; i < listExp.length; i++) {
        var row = {};
        row["employment_code"] = listExp[i].employee_code;
        row["seq_no"] = listExp[i].seq_no;
        row["company_name"] = listExp[i].company_name;
        row["usaha"] = listExp[i].usaha;
        row["department_name"] = listExp[i].department_name;
        row["last_title"] = listExp[i].last_title;
        row["start_working"] = new Date(parseInt(listExp[i].start_working.substr(6)));
        row["end_working"] = new Date(parseInt(listExp[i].end_working.substr(6)));
        row["last_salary"] = listExp[i].last_salary;
        row["reason_stop_working"] = listExp[i].reason_stop_working;
        row["description"] = listExp[i].description;

        vDataExp.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcExp);
    $("#tblExperience").jqxGrid({ source: vAdapter });
}

function f_EmptyExpDetail() {
    $('#btnExpSave').jqxButton({ disabled: false });

    $("#txtExpCode").val($("#txtId").data("employee_code"));
    $("#txtExpCode").data("exp_seq_no", 0);

    $("#dtExpStart").jqxDateTimeInput('setDate', new Date());
    $("#dtExpEnd").jqxDateTimeInput('setDate', new Date());

    $("#txtExpCompanyName").val("");
    $("#txtExpBussiness").val("");
    $("#txtExpDepartment").val("");
    $("#txtExpLastTitle").val("");
    $("#txtExpSalary").val("0");
    $("#txtExpReason").val("");
    $("#txtExpDesc").val("");
}

function f_DeleteEmployeeExp(pEmpCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblExperience").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblExperience').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeExperience/DeleteEmployeeExperience",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblExperience();
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
                f_HideLoaderModal();
            }
        });
    }
}

$(document).ready(function () {
    //#region INIT FAMILY
    $("#btnExpNew").jqxButton({theme: vTheme});
    $("#btnExpEdit").jqxButton({theme: vTheme});
    $("#btnExpDelete").jqxButton({theme: vTheme});

    //#region Table EXPERIENCE

    function initGridExperience() {
        $("#tblExperience").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(vSrcExp),
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
                { text: 'Reason', datafield: 'reason_stop_working', width: 300 },
                { text: 'Description', datafield: 'description', hidden: true }
            ]
        });
    }
    //#endregion

    initGridExperience();

    //#region MODAL FAMILY 
    $("#psnExp").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnExpCont",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#txtExpCode").jqxInput({ theme: vTheme, disabled: true })
    $("#dtExpStart").jqxDateTimeInput({ theme: vTheme });
    $("#dtExpEnd").jqxDateTimeInput({ theme: vTheme });

    $("#txtExpCompanyName").jqxInput({ theme: vTheme })
    $("#txtExpBussiness").jqxInput({ theme: vTheme })
    $("#txtExpDepartment").jqxInput({ theme: vTheme })
    $("#txtExpLastTitle").jqxInput({ theme: vTheme })
    //$("#txtExpSalary").jqxInput({ theme: vTheme })
    $("#txtExpSalary").jqxNumberInput({ theme: vTheme, spinButtons: true });
    $("#txtExpReason").jqxInput({ theme: vTheme })

    $('#txtExpDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });
    $("#btnExpSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modExperience").jqxWindow({
        height: 320, width: 700,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL FAMILY

    //#endregion

    $('#btnExpNew').on('click', function (event) {
        f_EmptyExpDetail();
        $("#modExperience").jqxWindow('open');
    });

    $('#btnExpCancel').on('click', function (event) {
        f_EmptyExpDetail();
        $("#modExperience").jqxWindow('close');
    });

    $('#btnExpEdit').on('click', function (event) {
        f_EmptyExpDetail();

        var rowindex = $('#tblExperience').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblExperience').jqxGrid('getrowdata', rowindex);

            $("#txtExpCode").data("exp_seq_no", rd.seq_no);
            $("#dtExpStart").jqxDateTimeInput('setDate',rd.start_working);
            $("#dtExpEnd").jqxDateTimeInput('setDate',rd.end_working);
            $("#txtExpCompanyName").val(rd.company_name);
            $("#txtExpBussiness").val(rd.usaha);
            $("#txtExpDepartment").val(rd.department_name);
            $("#txtExpLastTitle").val(rd.last_title);
            $("#txtExpSalary").val(rd.last_salary);
            $("#txtExpReason").val(rd.reason_stop_working);
            $("#txtExpDesc").val(rd.description);

            $("#modExperience").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnExpDelete').on('click', function (event) {
        var rowindex = $('#tblExperience').jqxGrid('getselectedrowindex');
        vLookUp = "Emp";

        if (rowindex > 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnExpSave').on('click', function (event) {
        if ($("#txtExpCompanyName").val() == "") {
            f_NotificationShow($("#jqxNotification"), $("#notificationContent"), "Company Name Tidak Boleh Kosong...");

        } else {
            $('#btnExpSave').jqxButton({ disabled: true });
            $("#modExperience").jqxWindow('close');

            f_ShowLoaderModal();

            var vAlamat = "";

            var vModel = JSON.stringify({
                employee_code: $("#txtId").data("employee_code"),
                employee_name: $("#txtFullName").val(),
                seq_no: $("#txtExpCode").data("exp_seq_no"),
                start_working : $("#dtExpStart").jqxDateTimeInput('getDate'),
                end_working : $("#dtExpEnd").jqxDateTimeInput('getDate'),
                company_name : $("#txtExpCompanyName").val(),
                usaha : $("#txtExpBussiness").val(),
                department_name: $("#txtExpDepartment").val(),
                last_title: $("#txtExpLastTitle").val(),
                last_salary: $("#txtExpSalary").val(),
                reason_stop_working: $("#txtExpReason").val(),
                description: $("#txtExpDesc").val()
            });

            var vSeqNo = ($("#txtExpCode").data("exp_seq_no") == "") ? 0 : $("#txtExpCode").data("exp_seq_no");

            if (vSeqNo > 0) {

                $.ajax({
                    url: base_url + "EmployeeExperience/UpdateEmployeeExperience",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblExperience();
                        } else {
                            f_MessageBoxShow(d.vResp['message']);
                        }
                        $('#btnExpSave').jqxButton({ disabled: false });
                        f_HideLoaderModal();
                    }
                });
            } else {
                $.ajax({
                    url: base_url + "EmployeeExperience/InsertEmployeeExperience",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblExperience();
                        } else {
                            f_MessageBoxShow(d.vResp['message']);
                        }
                        $('#btnExpave').jqxButton({ disabled: false });
                        f_HideLoaderModal();
                    }
                });
            }
        }
    });

});