
//#region DATASOURCE TABEL
var vDataTrn = [];
var vSrcTrn =
{
    localdata: vDataTrn,
    datatype: "json",
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

//#endregion

function f_UpdateTblTrn() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeTraining/GetEmployeeTrainingList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            if (dt.listFamily != null && dt.listFamily.length > 0) {
                f_FillTableFamily(dt.listFamily);
            }
        }
    });
}

function f_FillTableTrn(listTrn) {
    vDataTrn.length = 0;
    for (var i = 0; i < listTrn.length; i++) {
        var row = {};
        row["employment_code"] = listTrn[i].employee_code;
        row["seq_no"] = listTrn[i].seq_no;
        row["orginizer"] = listTrn[i].orginizer;
        row["material"] = listTrn[i].material;
        row["place"] = listTrn[i].place;
        row["start_date"] = new Date(parseInt(listTrn[i].start_date.substr(6)));
        row["end_date"] = new Date(parseInt(listTrn[i].end_date.substr(6)));

        vDataTrn.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcTrn);
    $("#tblTraining").jqxGrid({ source: vAdapter });
}

function f_EmptyTrnDetail() {
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

function f_DeleteEmployeeTrn(pEmpCode) {
    $('#jqxLoader').jqxLoader('open');

    var selectedRowIndex = $("#tblTraining").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblTraining').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeTraining/DeleteEmployeeTraining",
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
    $("#btnTrainingNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnTrainingEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnTrainingDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

    //#region Table TRAINING
    function initGridTraining() {
        $("#tblTraining").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(vSrcTrn),
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

    initGridTraining();

    //#region MODAL FAMILY 
    $("#psnTrn").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnTrnCont",
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
    $("#chkTrnCompany").jqxCheckBox({ theme: vTheme });

    $("#btnTrnSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnTrnCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modTraining").jqxWindow({
        height: 280, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL TRAINING

    //#endregion

    $('#btnTrainingNew').on('click', function (event) {
        f_EmptyTrnDetail();
        $("#modTraining").jqxWindow('open');
    });

    $('#btnTrnCancel').on('click', function (event) {
        f_EmptyTrnDetail();
        $("#modTraining").jqxWindow('close');
    });

    $('#btnTrnEdit').on('click', function (event) {
        f_EmptyTrnDetail();

        var rowindex = $('#tblTraining').jqxGrid('getselectedrowindex');

        if (rowindex > 0) {
            var rd = $('#tblTraining').jqxGrid('getrowdata', rowindex);

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

    $('#btnTrnDelete').on('click', function (event) {
        var rowindex = $('#tblTraining').jqxGrid('getselectedrowindex');

        if (rowindex > 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnTrnSave').on('click', function (event) {
        if ($("#txtFamName").val() == "") {
            f_NotificationShow($("#psnTrn"), $("#psnTrnContent"), "NAMA FAMILY TIDAK BOLEH KOSONG...");

        } else {
            $('#btnModFamSave').jqxButton({ disabled: true });
            $('#jqxLoader').jqxLoader('open');

            var vAlamat = "";

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
                    url: base_url + "EmployeeTraining/UpdateEmployeeTraining",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblFamily();
                            $("#modTraining").jqxWindow('close');
                        } else {
                            alert(d.vResp['message']);
                        }
                        $('#btnTrnSave').jqxButton({ disabled: false });
                        $('#jqxLoader').jqxLoader('close');
                    }
                });
            } else {
                $.ajax({
                    url: base_url + "EmployeeTraining/InsertEmployeeTraining",
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