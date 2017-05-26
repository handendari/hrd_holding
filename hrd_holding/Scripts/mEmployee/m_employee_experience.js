
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
         { name: 'reason_stop_working' }
    ]
};

//#endregion

function f_UpdateTblExp() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeExperience/GetEmployeeExperienceList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            if (dt.listExp != null && dt.listExp.length > 0) {
                f_FillTableExp(dt.listExp);
            }
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

        vDataExp.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcExp);
    $("#tblExperience").jqxGrid({ source: vAdapter });
}

function f_EmptyExpDetail() {
    $("#txtExpCode").val($("#txtId").val());
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
    $('#jqxLoader').jqxLoader('open');

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
                    f_UpdateTblExp();
                    $("#modYesNo").jqxWindow('close');
                } else {
                    alert(d.vResp['message']);
                }
                $('#jqxLoader').jqxLoader('close');
            }
        });
    }
}

$(document).ready(function () {
    //#region INIT FAMILY
    $("#btnExpNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

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
                { text: 'Reason', datafield: 'reason_stop_working', width: 300 }
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

    $("#txtExpCode").jqxInput({ theme: vTheme,disabled:true })
    $("#dtExpStart").jqxDateTimeInput({ theme: vTheme });
    $("#dtExpEnd").jqxDateTimeInput({ theme: vTheme });

    $("#txtExpCompanyName").jqxInput({ theme: vTheme })
    $("#txtExpBussiness").jqxInput({ theme: vTheme })
    $("#txtExpDepartment").jqxInput({ theme: vTheme })
    $("#txtExpLastTitle").jqxInput({ theme: vTheme })
    $("#txtExpSalary").jqxInput({ theme: vTheme })
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

        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');

        if (rowindex > 0) {
            var rd = $('#tblExperience').jqxGrid('getrowdata', rowindex);

            //alert(JSON.stringify(rd));

            $("#txtFamName").val(rd.name);
            $("#txtFamName").data("fam_seq_no", rd.seq_no);

            $("#txtFamDob").jqxDateTimeInput('setDate', rd.date_birth);

            var vFamGender = rd.sex;
            $("#cmbFamGender").jqxComboBox({ selectedIndex: vFamGender });

            var vFamRelation = rd.relationship;
            $("#cmbFamRelation").jqxComboBox({ selectedIndex: vFamRelation });

            $("#txtFamEducation").val(rd.education);
            $("#txtFamEmployment").val(rd.employment);
            $("#txtFamAddress").val(rd.address);

            if (rd.chk_address == 1) {
                $("#chkFamAddress").jqxCheckBox('check');
            } else {
                $("#chkFamAddress").jqxCheckBox('uncheck');
            }

            $("#modFamily").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnExpDelete').on('click', function (event) {
        var rowindex = $('#tblExperience').jqxGrid('getselectedrowindex');

        if (rowindex > 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnExpSave').on('click', function (event) {
        if ($("#txtFamName").val() == "") {
            f_NotificationShow($("#jqxNotification"), $("#notificationContent"), "NAMA FAMILY TIDAK BOLEH KOSONG...");

        } else {
            $('#btnModFamSave').jqxButton({ disabled: true });
            $('#jqxLoader').jqxLoader('open');

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

            var vSeqNo = ($("#txtFamName").data("fam_seq_no") == null
                       || $("#txtFamName").data("fam_seq_no") == "") ? 0 : $("#txtFamName").data("fam_seq_no");

            if (vSeqNo > 0) {

                $.ajax({
                    url: base_url + "EmployeeFamily/UpdateEmployeeFamily",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblFamily();
                            $("#modFamily").jqxWindow('close');
                        } else {
                            alert(d.vResp['message']);
                        }
                        $('#btnModFamSave').jqxButton({ disabled: false });
                        $('#jqxLoader').jqxLoader('close');
                    }
                });
            } else {
                $.ajax({
                    url: base_url + "EmployeeFamily/InsertEmployeeFamily",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblFamily();
                            $("#modFamily").jqxWindow('close');
                        } else {
                            alert(d.vResp['message']);
                        }
                        $('#btnModFamSave').jqxButton({ disabled: false });
                        $('#jqxLoader').jqxLoader('close');
                    }
                });
            }
        }
    });

});