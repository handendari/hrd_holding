var vDataEdu = [];
var vSrcEdu =
{
    localdata: vDataEdu,
    datatype: "json",
    datafields: [
         { name: 'id' },
         { name: 'request_id' },
         { name: 'recruitment_id' },
         { name: 'seq_no' },
         { name: 'school' },
         { name: 'start_year', type: "date" },
         { name: 'end_year', type: "date" },
         { name: 'flag_achieved'},
         { name: 'name_achieved' }
    ]
};

function f_UpdateTblEducation() {
    var vRequestId = $("#txtNoReq").data("request_id");

    $.ajax({
        url: base_url + "hrdRecruitmentEdu/GetRecruitmentEduList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pRequestId: vRequestId }),
        success: function (dt) {
            // if (dt.listEdu != null && dt.listEdu.length > 0) {
            f_FillTableEducation(dt.listEdu);
            // }
        }
    });
}

function f_FillTableEducation(listEdu) {
    vDataEdu.length = 0;
    for (var i = 0; i < listEdu.length; i++) {
        var row = {};
        row["id"] = listEdu[i].id;
        row["request_id"] = listEdu[i].request_id;
        row["recruitment_id"] = listEdu[i].recruitment_id;
        row["seq_no"] = listEdu[i].seq_no;
        row["school"] = listEdu[i].school;
        row["start_year"] = new Date(parseInt(listEdu[i].start_year.substr(6)));
        row["end_year"] = new Date(parseInt(listEdu[i].end_year.substr(6)));
        row["flag_achieved"] = listEdu[i].flag_achieved;
        row["name_achieved"] = listEdu[i].name_achieved;

        vDataEdu.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcEdu);
    $("#tblEducation").jqxGrid({ source: vAdapter });
}

function f_EmptyEduDetail() {
    $('#txtEduNoReq').val($('#txtNoReq').val());
    $('#txtEduNoReq').data("edu_id", 0);

    $("#txtEduSchool").val("")
    $("#dtEduStartYear").jqxDateTimeInput('setDate', new Date());
    $("#dtEduEndYear").jqxDateTimeInput('setDate', new Date());

    $("#cmbEduLevel").jqxComboBox({ selectedIndex: 0 });

}

function f_DeleteEducation(pId) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblEducation").jqxGrid('selectedrowindex');
    var vId = $('#tblEducation').jqxGrid('getcellvalue', selectedRowIndex, "id");


    if (vId >= 0) {
        $.ajax({
            url: base_url + "hrdRecruitmentEdu/DeleteRecruitmentEdu",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pId: vId }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblEducation();
                } else {
                    f_MessageBoxShow(d.vResp['message']); //alert(d.vResp['message']);
                }
                f_HideLoaderModal();
            }
        });
    }
}

$(document).ready(function () {

    //#region INIT EDUCATION
    $("#btnEduNew").jqxButton({ theme: vTheme });
    $("#btnEduEdit").jqxButton({ theme: vTheme });
    $("#btnEduDelete").jqxButton({ theme: vTheme });

    //#region Table EDUCATION
    function initGridEducation() {
        $("#tblEducation").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: new $.jqx.dataAdapter(vSrcEdu),
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'id', datafield: 'id', hidden: true },
                { text: 'Request_id', datafield: 'request_id', hidden: true },
                { text: 'Recruitment Id', datafield: 'recruitment_id', hidden: true },
                { text: 'No', datafield: 'seq_no', width: 70 },
                { text: 'School', datafield: 'school' },
                {
                    text: 'Start Date', datafield: 'start_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                },
                {
                    text: 'End Date', datafield: 'end_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                },
                { text: 'flag_achieved', datafield: 'flag_achieved',hidden:true },
                { text: 'Achieved', datafield: 'name_achieved' }

            ]
        });
    }

    initGridEducation();
    //#endregion

    $("#txtEduNoReq").jqxInput({ theme: vTheme, disabled: true })

    $("#txtEduSchool").jqxInput({ theme: vTheme, width: 400 })
    $("#dtEduStartYear").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#dtEduEndYear").jqxDateTimeInput({ theme: vTheme, width: 150 });

    $("#cmbEduLevel").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbEducation, selectedIndex: 0
    });

    $("#btnEduSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modEducation").jqxWindow({
        height: 220, width: 750,
        theme: vTheme,
        isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion INIT EDUCATION

    $('#btnEduNew').on('click', function (event) {
        f_EmptyEduDetail();
        $("#modEducation").jqxWindow('open');
    });

    $('#btnEduCancel').on('click', function (event) {
        f_EmptyEduDetail();
        $("#modEducation").jqxWindow('close');
    });


    $('#btnEduEdit').on('click', function (event) {
        f_EmptyEduDetail();

        var rowindex = $('#tblEducation').jqxGrid('getselectedrowindex');
        if (rowindex >= 0) {
            var rd = $('#tblEducation').jqxGrid('getrowdata', rowindex);

            $("#txtEduNoReq").data("edu_id", rd.id);

            $("#txtEduSchool").val(rd.school);
            $("#dtEduStartYear").jqxDateTimeInput('setDate', rd.start_year);
            $("#dtEduEndYear").jqxDateTimeInput('setDate', rd.end_year);

            var vEduLevel = rd.flag_achieved;
            $("#cmbEduLevel").jqxComboBox({ selectedIndex: vEduLevel });

            $("#modEducation").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnEduDelete').on('click', function (event) {
        var rowindex = $('#tblEducation').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnEduSave').on('click', function (event) {
        $('#btnEduSave').jqxButton({ disabled: true });
        f_ShowLoaderModal();

        var vModel = JSON.stringify({
            id: $("#txtEduNoReq").data("edu_id"),
            request_id: $("#txtNoReq").data("request_id"),
            school: $("#txtEduSchool").val(),
            start_year : $("#dtEduStartYear").jqxDateTimeInput('getDate'),
            end_year :$("#dtEduEndYear").jqxDateTimeInput('getDate'),
            flag_achieved : $("#cmbEduLevel").jqxComboBox('listBox').selectedIndex,
            name_achieved : $("#cmbEduLevel").val()
        });

        var vId = ($("#txtEduNoReq").data("edu_id") == "") ? 0 : $("#txtEduNoReq").data("edu_id");

        if (vId >= 0) {

            $.ajax({
                url: base_url + "hrdRecruitmentEdu/UpdateRecruitmentEdu",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_UpdateTblEducation();
                        $("#modEducation").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    $('#btnEduSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "hrdRecruitmentEdu/InsertRecruitmentEdu",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_UpdateTblEducation();
                        $("#modEducation").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    $('#btnEduSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

});