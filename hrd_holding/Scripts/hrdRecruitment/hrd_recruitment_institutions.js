
//#region DATASOURCE TABEL
var vDataMem = [];
var vSrcMem =
{
    localdata: vDataMem,
    datatype: "json",
    datafields: [
         { name: 'id' },
         { name: 'request_id' },
         { name: 'recruitment_id' },
         { name: 'seq_no' },
         { name: 'name' },
         { name: 'year_from', type: 'date' },
         { name: 'year_to', type: 'date' },
         { name: 'level' }
    ]
};

//#endregion

function f_PopulateTblMember() {
    var vMemId = $("#txtNoReq").data("request_id");

    $.ajax({
        url: base_url + "hrdRecruitmentMem/GetRecruitmentMemList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pRequestId: vMemId }),
        success: function (dt) {
            //if (dt.listTrn != null && dt.listTrn.length > 0) {
            f_FillTableMember(dt.listMem);
            //}
        }
    });
}

function f_FillTableMember(listMem) {
    vDataMem.length = 0;
    for (var i = 0; i < listMem.length; i++) {
        var row = {};
        row["id"] = listMem[i].id;
        row["request_id"] = listMem[i].request_id;
        row["recruitment_id"] = listMem[i].recruitment_id;
        row["seq_no"] = listMem[i].seq_no;
        row["name"] = listMem[i].name;
        row["year_from"] = new Date(parseInt(listMem[i].year_from.substr(6)));
        row["year_to"] = new Date(parseInt(listMem[i].year_to.substr(6)));
        row["level"] = listMem[i].level;

        vDataMem.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcMem);
    $("#tblMember").jqxGrid({ source: vAdapter });
}

function f_EmptyMemberDetail() {
    $("#txtMemNoReq").val($("#txtNoReq").val());
    $("#txtMemNoReq").data("mem_id", 0);

    $("#txtMemName").val("");
    $("#dtMemStart").jqxDateTimeInput('setDate', new Date());
    $("#dtMemEnd").jqxDateTimeInput('setDate', new Date());
    $("#txtMemLevel").val("");
}

function f_DeleteMember() {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblMember").jqxGrid('selectedrowindex');
    var vMemId = $('#tblMember').jqxGrid('getcellvalue', selectedRowIndex, "id");


    if (vMemId > 0) {
        $.ajax({
            url: base_url + "hrdRecruitmentMem/DeleteRecruitmentMem",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pId: vMemId }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_PopulateTblMember();
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
    $("#btnMemberNew").jqxButton({ theme: vTheme });
    $("#btnMemberEdit").jqxButton({ theme: vTheme });
    $("#btnMemberDelete").jqxButton({ theme: vTheme });

    //#region Table TRAINING
    function initGridMember() {
        $("#tblMember").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(vSrcMem),
            columns: [
                { text: 'id', datafield: 'id', hidden: true },
                { text: 'request id', datafield: 'request_id', hidden: true },
                { text: 'recruitment id', datafield: 'recruitment_id', hidden: true },
                { text: 'No', datafield: 'seq_no', width: 100, align: 'center' },
                { text: 'Name', datafield: 'name', width: 350, align: 'center' },
                {
                    text: 'Start Date', datafield: 'year_from', width: 150,
                    filtertype: 'date', cellsalign: 'center',
                    align: 'center', cellsformat: 'dd-MMM-yy'
                },
                {
                    text: 'End Date', datafield: 'year_to', width: 150,
                    filtertype: 'date', cellsalign: 'center',
                    align: 'center', cellsformat: 'dd-MMM-yy'
                },
                { text: 'Level', datafield: 'level', align: 'center' }
            ]
        });
    }
    //#endregion

    initGridMember();

    //#region MODAL FAMILY 
    $("#txtMemNoReq").jqxInput({ theme: vTheme, disabled: true });
    $("#dtMemStart").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#dtMemEnd").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtMemName").jqxInput({ theme: vTheme, width: 400 });
    $("#txtMemLevel").jqxInput({ theme: vTheme,width:300 });

    $("#btnMemberSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnMemberCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modMember").jqxWindow({
        height: 220, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL TRAINING

    //#endregion

    $('#btnMemberNew').on('click', function (event) {
        f_EmptyMemberDetail();
        $("#modMember").jqxWindow('open');
    });

    $('#btnMemberCancel').on('click', function (event) {
        f_EmptyMemberDetail();
        $("#modMember").jqxWindow('close');
    });

    $('#btnMemberEdit').on('click', function (event) {
        f_EmptyMemberDetail();

        var rowindex = $('#tblMember').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblMember').jqxGrid('getrowdata', rowindex);

            $("#txtMemNoReq").val($("#txtNoReq").val());

            $("#txtMemNoReq").data("mem_id", rd.id);

            $("#txtMemName").val(rd.name);
            $("#dtMemStart").jqxDateTimeInput('setDate', rd.year_from);
            $("#dtMemEnd").jqxDateTimeInput('setDate', rd.year_to);
            $("#txtMemLevel").val(rd.level);

            $("#modMember").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnMemberDelete').on('click', function (event) {
        var rowindex = $('#tblMember').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnMemberSave').on('click', function (event) {
        if ($("#txtMemName").val() == "") {
            f_MessageBoxShow("Nama Tidak Boleh Kosong...");
            return;
        }

        if ($("#txtMemLevel").val() == "") {
            f_MessageBoxShow("Level Tidak Boleh Kosong...");
            return;
        }

        $("#modMember").jqxWindow('close');
        f_ShowLoaderModal();

        var vModel = JSON.stringify({
            id: $("#txtMemNoReq").data("mem_id"),
            request_id: $("#txtNoReq").data("request_id"),
            name: $("#txtMemName").val(),
            year_from: $("#dtMemStart").jqxDateTimeInput('getDate'),
            year_to: $("#dtMemEnd").jqxDateTimeInput('getDate'),
            level: $("#txtMemLevel").val()

        });

        var vMemId = ($("#txtMemNoReq").data("mem_id") == "") ? 0 : $("#txtMemNoReq").data("mem_id");

        if (vMemId > 0) {

            $.ajax({
                url: base_url + "hrdRecruitmentMem/UpdateRecruitmentMem",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_PopulateTblMember();
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "hrdRecruitmentMem/InsertRecruitmentMem",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_PopulateTblMember();
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    f_HideLoaderModal();
                }
            });
        }

    });

});