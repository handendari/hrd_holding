
//#region DATASOURCE TABEL
var vDataExp = [];
var vSrcExp =
{
    localdata: vDataExp,
    datatype: "json",
    datafields: [
         { name: 'id' },
         { name: 'request_id' },
         { name: 'recruitment_id' },
         { name: 'seq_no' },
         { name: 'name_employer' },
         { name: 'business' },
         { name: 'position_held' },
         { name: 'start_date', type: 'date' },
         { name: 'end_date', type: 'date' },
         { name: 'last_salary' },
         { name: 'reason_leave' }
    ]
};

//#endregion

function f_UpdateTblExperience() {
    var vRequestId = $("#txtNoReq").data("request_id");

    $.ajax({
        url: base_url + "hrdRecruitmentExp/GetRecruitmentExpList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pRequestId: vRequestId }),
        success: function (dt) {
            if (dt.listExp.length > 0) {
                f_FillTableExp(dt.listExp);
            }
        }
    });
}

function f_FillTableExp(listExp) {
    vDataExp.length = 0;
    for (var i = 0; i < listExp.length; i++) {
        var row = {};

        row["id"] = listExp[i].id;
        row["request_id"] = listExp[i].request_id;
        row["recruitment_id"] = listExp[i].recruitment_id;
        row["seq_no"] = listExp[i].seq_no;
        row["name_employer"] = listExp[i].name_employer;
        row["business"] = listExp[i].business;
        row["position_held"] = listExp[i].position_held;
        row["start_date"] = new Date(parseInt(listExp[i].start_date.substr(6)));
        row["end_date"] = new Date(parseInt(listExp[i].end_date.substr(6)));
        row["last_salary"] = listExp[i].last_salary;
        row["reason_leave"] = listExp[i].reason_leave;

        vDataExp.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcExp);
    $("#tblExperience").jqxGrid({ source: vAdapter });
}

function f_EmptyExpDetail() {
    $('#btnExpSave').jqxButton({ disabled: false });

    $("#txtExpNoReq").val($("#txtNoReq").val());
    $("#txtExpNoReq").data("exp_id", 0);

    $("#dtExpStart").jqxDateTimeInput('setDate', new Date());
    $("#dtExpEnd").jqxDateTimeInput('setDate', new Date());

    $("#txtExpCompanyName").val("");
    $("#txtExpBussiness").val("");
    $("#txtExpLastTitle").val("");
    $("#txtExpSalary").val("0");
    $("#txtExpReason").val("");
}

function f_DeleteExp() {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblExperience").jqxGrid('selectedrowindex');
    var vExpId = $('#tblExperience').jqxGrid('getcellvalue', selectedRowIndex, "id");


    if (vExpId > 0) {
        $.ajax({
            url: base_url + "hrdRecruitmentExp/DeleteRecruitmentExp",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pId: vExpId }),
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
                { text: 'Id', datafield: 'id', hidden: true },
                { text: 'Request Id', datafield: 'request_id', hidden: true },
                { text: 'Recruitment Id', datafield: 'recruitment_id', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Employer Name', datafield: 'name_employer', width: 200 },
                { text: 'Business', datafield: 'business', width: 200 },
                { text: 'Position Held', datafield: 'position_held', width: 200 },
                {
                    text: 'Start Working', datafield: 'start_date',
                    filtertype: 'date', cellsalign: 'center',
                    cellsformat: 'dd-MMM-yy', width: 100
                },
                {
                    text: 'End Working', datafield: 'end_date',
                    filtertype: 'date', cellsalign: 'center',
                    cellsformat: 'dd-MMM-yy', width: 100
                },
                {
                    text: 'Last Salary', datafield: 'last_salary',
                    cellsalign: 'right', align: 'center',
                    cellsformat: 'd2', width: 150
                },
                { text: 'Reason', datafield: 'reason_leave' }
            ]
        });
    }
    //#endregion

    initGridExperience();

    //#region MODAL FAMILY 

    $("#txtExpNoReq").jqxInput({ theme: vTheme, disabled: true })
    $("#dtExpStart").jqxDateTimeInput({ theme: vTheme });
    $("#dtExpEnd").jqxDateTimeInput({ theme: vTheme });

    $("#txtExpCompanyName").jqxInput({ theme: vTheme,width:400 })
    $("#txtExpBussiness").jqxInput({ theme: vTheme, width: 400 })
    $("#txtExpLastTitle").jqxInput({ theme: vTheme, width: 400 })
    $("#txtExpSalary").jqxNumberInput({ theme: vTheme, spinButtons: true });
    $("#txtExpReason").jqxInput({ theme: vTheme,width:400 })

    $("#btnExpSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnExpCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modExperience").jqxWindow({
        height: 300, width: 750,
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

            $("#txtExpNoReq").data("exp_id", rd.id);
            $("#dtExpStart").jqxDateTimeInput('setDate',rd.start_date);
            $("#dtExpEnd").jqxDateTimeInput('setDate',rd.end_date);
            $("#txtExpCompanyName").val(rd.name_employer);
            $("#txtExpBussiness").val(rd.business);
            $("#txtExpLastTitle").val(rd.position_held);
            $("#txtExpSalary").val(rd.last_salary);
            $("#txtExpReason").val(rd.reason_leave);

            $("#modExperience").jqxWindow('open');
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
        if ($("#txtExpCompanyName").val() == "") {
            f_MessageBoxShow("Employer Name Tidak Boleh Kosong...");

        } else {
            $('#btnExpSave').jqxButton({ disabled: true });
            $("#modExperience").jqxWindow('close');

            f_ShowLoaderModal();

            var vAlamat = "";

            var vModel = JSON.stringify({
                id: $("#txtExpNoReq").data("exp_id"),
                request_id: $("#txtNoReq").data("request_id"),
                //recruitment_id: $("#txtExpCompanyName").val(),
                seq_no: $("#txtExpCode").data("exp_seq_no"),
                name_employer: $("#txtExpCompanyName").val(),
                business : $("#txtExpBussiness").val(),
                start_date : $("#dtExpStart").jqxDateTimeInput('getDate'),
                end_date : $("#dtExpEnd").jqxDateTimeInput('getDate'),
                position_held: $("#txtExpLastTitle").val(),
                last_salary: $("#txtExpSalary").val(),
                reason_leave: $("#txtExpReason").val()
            });

            var vExpId = ($("#txtExpNoReq").data("exp_id") == "") ? 0 : $("#txtExpNoReq").data("exp_id");

            if (vExpId > 0) {

                $.ajax({
                    url: base_url + "hrdRecruitmentExp/UpdateRecruitmentExp",
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
                    url: base_url + "hrdRecruitmentExp/InsertRecruitmentExp",
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
            }
        }
    });

});