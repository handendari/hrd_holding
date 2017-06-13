
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

function f_UpdateTblFamily() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeFamily/GetEmployeeFamilyList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            //  if (dt.listFamily != null && dt.listFamily.length > 0) {
            f_FillTableFamily(dt.listFamily);
            //  }
        }
    });
}

function f_FillTableFamily(listFamily) {
    vDataFamily.length = 0;
    for (var i = 0; i < listFamily.length; i++) {
        var row = {};
        row["employment_code"] = listFamily[i].employee_code;
        row["seq_no"] = listFamily[i].seq_no;
        row["name"] = listFamily[i].name;
        row["sex"] = listFamily[i].sex;
        row["relationship"] = listFamily[i].relationship;
        row["nm_rel"] = listFamily[i].nm_rel;
        row["date_birth"] = new Date(parseInt(listFamily[i].date_birth.substr(6)));
        row["education"] = listFamily[i].education;
        row["employment"] = listFamily[i].employment;
        row["address"] = listFamily[i].address;
        row["chk_address"] = listFamily[i].chk_address;

        vDataFamily.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcFamily);
    $("#tblFamily").jqxGrid({ source: vAdapter });
}

function f_EmptyFamilyDetail() {
    $("#txtFamName").val("");
    $("#txtFamName").data("fam_seq_no", 0);

    $("#txtFamDob").jqxDateTimeInput('setDate', new Date());
    $("#cmbFamGender").jqxComboBox({ selectedIndex: 0 });
    $("#cmbFamRelation").jqxComboBox({ selectedIndex: 0 });
    $("#txtFamEducation").val("");
    $("#txtFamEmployment").val("");
    $("#txtFamAddress").val("");
    $("#chkFamAddress").jqxCheckBox('uncheck');
}

function f_DeleteEmployeeFamily(pEmpCode) {
    $('#jqxLoader').jqxLoader('open');

    var selectedRowIndex = $("#tblFamily").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblFamily').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeFamily/DeleteEmployeeFamily",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblFamily();
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
    $("#btnFamilyNew").jqxButton({ theme: vTheme });
    $("#btnFamilyEdit").jqxButton({ theme: vTheme });
    $("#btnFamilyDelete").jqxButton({ theme: vTheme });

    function initGridFamily() {
        $("#tblFamily").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: new $.jqx.dataAdapter(vSrcFamily),
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

    initGridFamily();

    //#region MODAL FAMILY 
    $("#jqxNotification").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
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
    //#endregion MODAL FAMILY

    //#endregion

    $('#btnFamilyNew').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#modFamily").jqxWindow('open');
    });

    $('#btnModFamCancel').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#modFamily").jqxWindow('close');
    });

    $("#chkFamAddress").bind('change', function (event) {
        var checked = event.args.checked;
        if (checked) {
            $("#txtFamAddress").val($("#txtAddress").val());
        }
    });

    $('#btnFamilyEdit').on('click', function (event) {
        f_EmptyFamilyDetail();

        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblFamily').jqxGrid('getrowdata', rowindex);

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

    $('#btnFamilyDelete').on('click', function (event) {
        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');
        vLookUp = "Emp";

        if (rowindex > 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnModFamSave').on('click', function (event) {
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