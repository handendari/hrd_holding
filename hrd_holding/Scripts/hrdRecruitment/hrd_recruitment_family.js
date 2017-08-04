
//#region DATASOURCE TABEL
var vDataFamily = [];
var vSrcFamily =
{
    localdata: vDataFamily,
    datatype: "json",
    datafields: [
            { name: 'fam_id' },
            { name: 'request_id' },
            { name: 'recruitment_id' },
            { name: 'seq_no' },
            { name: 'name' },
            { name: 'flag_relationship' },
            { name: 'name_relationship' },
            { name: 'date_birth', type: "date" },
            { name: 'flag_gender' },
            { name: 'education' },
            { name: 'occupation' },
            { name: 'name_employer' },
            { name: 'address' }
    ]
};
//#endregion

function f_UpdateTblFamily() {
    var vRequestId = $("#txtNoReq").data("request_id");

    $.ajax({
        url: base_url + "hrdRecruitmentFam/GetRecruitmentFamList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pRequestId: vRequestId }),
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

        row["fam_id"] = listFamily[i].fam_id;
        row["request_id"] = listFamily[i].request_id;
        row["recruitment_id"] = listFamily[i].recruitment_id;
        row["seq_no"] = listFamily[i].seq_no;
        row["name"] = listFamily[i].name;
        row["flag_relationship"] = listFamily[i].flag_relationship;
        row["name_relationship"] = listFamily[i].name_relationship;
        row["date_birth"] = new Date(parseInt(listFamily[i].date_birth.substr(6)));
        row["flag_gender"] = listFamily[i].flag_gender;
        row["education"] = listFamily[i].education;
        row["occupation"] = listFamily[i].occupation;
        row["name_employer"] = listFamily[i].name_employer;
        row["address"] = listFamily[i].address;

        vDataFamily.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcFamily);
    $("#tblFamily").jqxGrid({ source: vAdapter });
}

function f_EmptyFamilyDetail() {
    $("#txtFamNoReq").val("");
    $("#txtFamNoReq").data("fam_id", 0);
    $("#txtFamName").val("");
    $("#txtFamOccupation").val("");
    $("#txtFamDob").jqxDateTimeInput('setDate', new Date());
    $("#cmbFamGender").jqxComboBox({ selectedIndex: 0 });
    $("#cmbFamRelation").jqxComboBox({ selectedIndex: 0 });
    $("#txtFamEducation").val("");
    $("#txtFamEmployer").val("");
    $("#txtFamAddress").val("");
}

function f_DeleteRecruitmentFamily() {
    $('#jqxLoader').jqxLoader('open');

    var selectedRowIndex = $("#tblFamily").jqxGrid('selectedrowindex');
    var vFamId = $('#tblFamily').jqxGrid('getcellvalue', selectedRowIndex, "fam_id");


    if (vFamId > 0) {
        $.ajax({
            url: base_url + "hrdRecruitmentFam/DeleteRecruitmentFam",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pId:vFamId }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblFamily();
                    $("#modYesNo").jqxWindow('close');
                } else {
                    f_MessageBoxShow(d.vResp['message']);
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
                { text: 'fam_id', datafield: 'fam_id', hidden: true },
                { text: 'Request_id', datafield: 'request_id', hidden: true },
                { text: 'Recruitment Id', datafield: 'recruitment_id', hidden: true },
                { text: 'No', datafield: 'seq_no', width: 70 },
                { text: 'Name', datafield: 'name', width: 300 },
                { text: 'flag_Relationship', datafield: 'flag_relationship', hidden: true },
                { text: 'Relationship', datafield: 'name_relationship', width: 100 },
                { text: 'flag_Gender', datafield: 'flag_gender', hidden: true },
                { text: 'Gender', datafield: 'sex', width: 50, hidden: true },
                { text: 'Education', datafield: 'education' },
                { text: 'Occupation', datafield: 'occupation' },
                { text: 'Employer', datafield: 'name_employer' },
                { text: 'Address', datafield: 'address', hidden: true }
            ]
        });
    }

    initGridFamily();

    //#region MODAL FAMILY 
    $("#txtFamNoReq").jqxInput({ theme: vTheme, disabled: true })
    $("#txtFamName").jqxInput({ theme: vTheme, width: 300 })
    $("#txtFamDob").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtFamEducation").jqxInput({ theme: vTheme })
    $("#txtFamEmployer").jqxInput({ theme: vTheme, width: 250 })
    $("#cmbFamGender").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbGender, selectedIndex: 0
    });
    $("#cmbFamRelation").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbRelation, selectedIndex: 0
    });
    $("#txtFamOccupation").jqxInput({ theme: vTheme })
    $('#txtFamAddress').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Alamat Keluarga',
        height: 50, width: 250, minLength: 1
    });
    $("#btnModFamSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnModFamCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modFamily").jqxWindow({
        height: 320, width: 1000,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL FAMILY

    ////#endregion

    $('#btnFamilyNew').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#txtFamNoReq").val($("#txtNoReq").val());
        $("#modFamily").jqxWindow('open');
    });

    $('#btnModFamCancel').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#modFamily").jqxWindow('close');
    });


    $('#btnFamilyEdit').on('click', function (event) {
        f_EmptyFamilyDetail();

        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblFamily').jqxGrid('getrowdata', rowindex);

            //alert(JSON.stringify(rd));


            $("#txtFamNoReq").val($("#txtNoReq").val());
            $("#txtFamNoReq").data("fam_id", rd.fam_id);

            $("#txtFamName").val(rd.name);
            $("#txtFamDob").jqxDateTimeInput('setDate', rd.date_birth);

            var vFamGender = rd.flag_gender;
            $("#cmbFamGender").jqxComboBox({ selectedIndex: vFamGender });

            var vFamRelation = rd.flag_relationship;
            $("#cmbFamRelation").jqxComboBox({ selectedIndex: vFamRelation });

            $("#txtFamEducation").val(rd.education);
            $("#txtFamOccupation").val(rd.occupation);
            $("#txtFamEmployer").val(rd.name_employer);
            $("#txtFamAddress").val(rd.address);

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
            f_MessageBoxShow("NAMA FAMILY TIDAK BOLEH KOSONG...");
            return;
        }

        $('#btnModFamSave').jqxButton({ disabled: true });
        $('#jqxLoader').jqxLoader('open');

        var vModel = JSON.stringify({
            fam_id: $("#txtFamNoReq").data("fam_id"),
            request_id: $("#txtNoReq").data("request_id"),
            name: $("#txtFamName").val(),
            occupation: $("#txtFamOccupation").val(),
            date_birth: $("#txtFamDob").jqxDateTimeInput('getDate'),
            flag_gender: $("#cmbFamGender").jqxComboBox('listBox').selectedIndex,
            flag_relationship: $("#cmbFamRelation").jqxComboBox('listBox').selectedIndex,
            name_relationship: $("#cmbFamRelation").val(),
            education: $("#txtFamEducation").val(),
            name_employer: $("#txtFamEmployer").val(),
            address: $("#txtFamAddress").val()
        });

        var vFamId = ($("#txtFamNoReq").data("fam_id") == null
                   || $("#txtFamNoReq").data("fam_id") == "") ? 0 : $("#txtFamNoReq").data("fam_id");

        if (vFamId > 0) {

            $.ajax({
                url: base_url + "hrdRecruitmentFam/UpdateRecruitmentFam",
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
                url: base_url + "hrdRecruitmentFam/InsertRecruitmentFam",
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
    });

});