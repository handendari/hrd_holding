
//#region DATASOURCE TABEL
var vDataRef = [];
var vSrcRef =
{
    localdata: vDataRef,
    datatype: "json",
    datafields: [
         { name: 'employee_code' },
         { name: 'seq_no' },
         { name: 'organizer' },
         { name: 'material' },
         { name: 'place' },
         { name: 'start_date' },
         { name: 'end_date' },
         { name: 'company' },
         { name: 'chk_company' },
         { name: 'value' }

    ]
};

//#endregion

function f_UpdateTblRef() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeTraining/GetEmployeeTrainingList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            //if (dt.listTrn != null && dt.listTrn.length > 0) {
            f_FillTableTrn(dt.listRef);
            //}
        }
    });
}

function f_FillTableRef(listRef) {
    vDataRef.length = 0;
    for (var i = 0; i < listRef.length; i++) {
        var row = {};
        row["employment_code"] = listRef[i].employee_code;
        row["seq_no"] = listRef[i].seq_no;
        row["organizer"] = listRef[i].organizer;
        row["material"] = listRef[i].material;
        row["place"] = listRef[i].place;
        row["start_date"] = new Date(parseInt(listRef[i].start_date.substr(6)));
        row["end_date"] = new Date(parseInt(listRef[i].end_date.substr(6)));
        row["company"] = listRef[i].company;
        row["chk_company"] = listRef[i].chk_company;
        row["value"] = listRef[i].value;

        vDataRef.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcRef);
    $("#tblReference").jqxGrid({ source: vAdapter });
}

function f_EmptyRefDetail() {
    $("#txtTrnCode").val($("#txtId").data("employee_code"));
    $("#txtTrnCode").data("trn_seq_no", 0);
    $("#dtTrnStart").jqxDateTimeInput('setDate', new Date());
    $("#dtTrnEnd").jqxDateTimeInput('setDate', new Date());
    $("#txtTrnSubject").val("");
    $("#txtTrnOrganizer").val("");
    $("#txtTrnPlace").val("");
    $("#txtTrnCompany").val("");
    $("#txtTrnValue").val("");
    $("#chkTrnCompany").jqxCheckBox('uncheck');

}

function f_DeleteEmployeeRef(pEmpCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblReference").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblReference').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeTraining/DeleteEmployeeTraining",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblRef();
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
    $("#btnReferenceNew").jqxButton({ theme: vTheme });
    $("#btnReferenceEdit").jqxButton({ theme: vTheme });
    $("#btnReferenceDelete").jqxButton({ theme: vTheme });

    //#region Table TRAINING
    function initGridReference() {
        $("#tblReference").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(vSrcRef),
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Organizer', datafield: 'organizer' },
                { text: 'Material', datafield: 'material' },
                { text: 'Place', datafield: 'place' },
                { text: 'Start Date', datafield: 'start_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'End Date', datafield: 'end_date', filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'Company', datafield: 'company',hidden:true },
                { text: 'chk_company', datafield: 'chk_company',hidden:true },
                { text: 'value', datafield: 'value',hidden:true }
            ]
        });
    }
    //#endregion

    initGridReference();

    ////#region MODAL FAMILY 
    //$("#psnTrn").jqxNotification({
    //    width: "100%", height: "40px", theme: vTheme,
    //    appendContainer: "#psnTrnCont",
    //    opacity: 0.9, autoClose: true, template: "error"
    //});

    //$("#txtTrnCode").jqxInput({ theme: vTheme, disabled: true });
    //$("#dtTrnStart").jqxDateTimeInput({ theme: vTheme });
    //$("#dtTrnEnd").jqxDateTimeInput({ theme: vTheme });
    //$("#txtTrnSubject").jqxInput({ theme: vTheme });
    //$("#txtTrnOrganizer").jqxInput({ theme: vTheme });
    //$("#txtTrnPlace").jqxInput({ theme: vTheme });
    //$("#txtTrnCompany").jqxInput({ theme: vTheme });
    //$("#txtTrnValue").jqxInput({ theme: vTheme });

    //$("#chkTrnCompany").jqxCheckBox({ theme: vTheme });

    //$("#btnTrnSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    //$("#btnTrnCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    //$("#modTraining").jqxWindow({
    //    height: 280, width: 700,
    //    theme: vTheme, isModal: true,
    //    autoOpen: false,
    //    resizable: false
    //});
    ////#endregion MODAL TRAINING

    ////#endregion

    //$('#btnTrainingNew').on('click', function (event) {
    //    f_EmptyTrnDetail();
    //    $("#modTraining").jqxWindow('open');
    //});

    //$('#btnTrnCancel').on('click', function (event) {
    //    f_EmptyTrnDetail();
    //    $("#modTraining").jqxWindow('close');
    //});

    //$('#btnTrainingEdit').on('click', function (event) {
    //    f_EmptyTrnDetail();

    //    var rowindex = $('#tblTraining').jqxGrid('getselectedrowindex');

    //    if (rowindex >= 0) {
    //        var rd = $('#tblTraining').jqxGrid('getrowdata', rowindex);

    //        $("#txtTrnCode").data("trn_seq_no", rd.seq_no);
    //        $("#dtTrnStart").jqxDateTimeInput('setDate', rd.start_date);
    //        $("#dtTrnEnd").jqxDateTimeInput('setDate', rd.end_date);
    //        $("#txtTrnSubject").val(rd.material);
    //        $("#txtTrnOrganizer").val(rd.organizer);
    //        $("#txtTrnPlace").val(rd.place);
    //        $("#txtTrnCompany").val(rd.company);
    //        $("#txtTrnValue").val(rd.value);

    //        if (rd.chk_company == 1) {
    //            $("#chkTrnCompany").jqxCheckBox('check');
    //        } else {
    //            $("#chkTrnCompany").jqxCheckBox('uncheck');
    //        }

    //        $("#modTraining").jqxWindow('open');
    //    } else {
    //        f_MessageBoxShow("Please Select Data...");
    //    }
    //});

    //$('#btnTrainingDelete').on('click', function (event) {
    //    var rowindex = $('#tblTraining').jqxGrid('getselectedrowindex');
    //    vLookUp = "Emp";

    //    if (rowindex >= 0) {
    //        $("#modYesNo").jqxWindow('open');
    //    } else {
    //        f_MessageBoxShow("Please Select Data...");
    //    }
    //});

    //$('#btnTrnSave').on('click', function (event) {
    //    if ($("#txtTrnSubject").val() == "") {
    //        f_NotificationShow($("#psnTrn"), $("#psnTrnContent"), "Subject Training Tidak Boleh Kosong...");
    //        return;
    //    }

    //    if ($("#txtTrnPlace").val() == "") {
    //        f_NotificationShow($("#psnTrn"), $("#psnTrnContent"), "Training Place Tidak Boleh Kosong...");
    //        return;
    //    }

    //    if ($("#txtTrnOrganizer").val() == "") {
    //        f_NotificationShow($("#psnTrn"), $("#psnTrnContent"), "Training Organizer Tidak Boleh Kosong...");
    //        return;
    //    }

    //    $("#modTraining").jqxWindow('close');
    //    f_ShowLoaderModal();

    //    var vModel = JSON.stringify({
    //        employee_code: $("#txtId").data("employee_code"),
    //        employee_name: $("#txtFullName").val(),
    //        seq_no: $("#txtTrnCode").data("trn_seq_no"),
    //        start_date: $("#dtTrnStart").jqxDateTimeInput('getDate'),
    //        end_date: $("#dtTrnEnd").jqxDateTimeInput('getDate'),
    //        material: $("#txtTrnSubject").val(),
    //        organizer: $("#txtTrnOrganizer").val(),
    //        place: $("#txtTrnPlace").val(),
    //        company: $("#txtTrnCompany").val(),
    //        value: $("#txtTrnValue").val(),
    //        chk_company: $("#chkTrnCompany").jqxCheckBox('checked')

    //    });

    //    var vSeqNo = ($("#txtTrnCode").data("trn_seq_no") == "") ? 0 : $("#txtTrnCode").data("trn_seq_no");

    //    if (vSeqNo > 0) {

    //        $.ajax({
    //            url: base_url + "EmployeeTraining/UpdateEmployeeTraining",
    //            type: "POST",
    //            contentType: "application/json",
    //            data: vModel,
    //            success: function (d) {
    //                var isOke = d.vResp['isValid'];

    //                if (isOke) {
    //                    f_UpdateTblTrn();
    //                } else {
    //                    f_MessageBoxShow(d.vResp['message']);
    //                }
    //                f_HideLoaderModal();
    //            }
    //        });
    //    } else {
    //        $.ajax({
    //            url: base_url + "EmployeeTraining/InsertEmployeeTraining",
    //            type: "POST",
    //            contentType: "application/json",
    //            data: vModel,
    //            success: function (d) {
    //                var isOke = d.vResp['isValid'];

    //                if (isOke) {
    //                    f_UpdateTblTrn();
    //                } else {
    //                    f_MessageBoxShow(d.vResp['message']);
    //                }
    //                f_HideLoaderModal();
    //            }
    //        });
    //    }

    //});

});