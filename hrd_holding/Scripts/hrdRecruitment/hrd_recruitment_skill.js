
//#region DATASOURCE TABEL
var vDataSkill = [];
var vSrcSkill =
{
    localdata: vDataSkill,
    datatype: "json",
    datafields: [
         { name: 'id' },
         { name: 'request_id' },
         { name: 'recruitment_id' },
         { name: 'seq_no' },
         { name: 'skill' },
         { name: 'flag_level' },
         { name: 'name_level' },
         { name: 'description' }
    ]
};
//#endregion

function f_UpdateTblSkill() {
    var vRequestId = $("#txtNoReq").data("request_id");

    $.ajax({
        url: base_url + "hrdRecruitmentSkill/GetRecruitmentSkillList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pRequestId: vRequestId }),
        success: function (dt) {
             if (dt.listSkill.length > 0) {
            f_FillTableSkill(dt.listSkill);
             }
        }
    });
}

function f_FillTableSkill(listSkill) {
    vDataSkill.length = 0;
    for (var i = 0; i < listSkill.length; i++) {
        var row = {};
        row["id"] = listSkill[i].id,
        row["request_id"] = listSkill[i].request_id,
        row["recruitment_id"] = listSkill[i].recruitment_id,
        row["seq_no"] = listSkill[i].seq_no;
        row["skill"] = listSkill[i].skill;
        row["flag_level"] = listSkill[i].flag_level;
        row["name_level"] = listSkill[i].name_level;
        row["description"] = listSkill[i].description;

        vDataSkill.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcSkill);
    $("#tblSkill").jqxGrid({ source: vAdapter });
}

function f_EmptySkill() {
    $("#txtSkillNoReq").val($("#txtNoReq").val());
    $("#txtSkillNoReq").data("skill_id", 0);

    $("#txtSkillName").val("");
    $("#cmbSkillLevel").jqxComboBox({ selectedIndex: 0 });
    $("#txtSkillDesc").val("");
}

function f_DeleteSkill(pId) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblSkill").jqxGrid('selectedrowindex');
    var vSkillId = $('#tblSkill').jqxGrid('getcellvalue', selectedRowIndex, "id");


    if (vSkillId >= 0) {
        $.ajax({
            url: base_url + "hrdRecruitmentSkill/DeleteRecruitmentSkill",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pId: vSkillId }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_UpdateTblSkill();
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
                f_HideLoaderModal();
            }
        });
    }
}

$(document).ready(function () {
    $("#btnSkillNew").jqxButton({ theme: vTheme });
    $("#btnSkillEdit").jqxButton({ theme: vTheme });
    $("#btnSkillDelete").jqxButton({ theme: vTheme });

    //#region Table SKILL
    function initGridSkill() {
        $("#tblSkill").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: new $.jqx.dataAdapter(vSrcSkill),
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'id', datafield: 'id', hidden: true },
                { text: 'Request_id', datafield: 'request_id', hidden: true },
                { text: 'Recruitment Id', datafield: 'recruitment_id', hidden: true },
                { text: 'No', datafield: 'seq_no', width: 70 },
                { text: 'Skill', datafield: 'skill' },
                { text: 'Level', datafield: 'flag_level', hidden: true },
                { text: 'Level', datafield: 'name_level' },
                { text: 'Description', datafield: 'description' }
            ]
        });
    }
    //#endregion

    initGridSkill();


    //#region MODAL FAMILY 
    $("#txtSkillNoReq").jqxInput({ theme: vTheme, disabled: true })
    $("#txtSkillName").jqxInput({ theme: vTheme })
    $("#cmbSkillLevel").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbSkillLevel, selectedIndex: 0
    });
    $('#txtSkillDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Keterangan',
        height: 50, width: 200, minLength: 1
    });
    $("#btnSkillSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnSkillCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modSkill").jqxWindow({
        height: 230, width: 400,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL FAMILY

    //#endregion

    $('#btnSkillNew').on('click', function (event) {
        f_EmptySkill();
        $("#modSkill").jqxWindow('open');
    });

    $('#btnSkillCancel').on('click', function (event) {
        f_EmptySkill();
        $("#modSkill").jqxWindow('close');
    });

    $('#btnSkillEdit').on('click', function (event) {
        f_EmptySkill();

        var rowindex = $('#tblSkill').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblSkill').jqxGrid('getrowdata', rowindex);

            //alert(JSON.stringify(rd));
            $("#txtSkillNoReq").data("skill_id", rd.id);

            $("#txtSkillName").val(rd.skill);

            var vSkillLevel = rd.flag_level;
            $("#cmbSkillLevel").jqxComboBox({ selectedIndex: vSkillLevel });

            $("#txtSkillDesc").val(rd.description);

            $("#modSkill").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnSkillDelete').on('click', function (event) {
        var rowindex = $('#tblSkill').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnSkillSave').on('click', function (event) {
        if ($("#txtSkillName").val() == "") {
            f_MessageBoxShow("Nama Skill Tidak Boleh Kosong...");

        } else {
            //$('#btnSkillSave').jqxButton({ disabled: true });
            $("#modSkill").jqxWindow('close');
            f_ShowLoaderModal();


            var vModel = JSON.stringify({
                request_id: $("#txtNoReq").data("request_id"),
                id: $("#txtSkillNoReq").data("skill_id"),
                skill: $("#txtSkillName").val(),
                level: $("#cmbSkillLevel").jqxComboBox('listBox').selectedIndex,
                name_level: $("#cmbSkillLevel").val(),
                description: $("#txtSkillDesc").val()
            });

            var vSkillId = ($("#txtSkillNoReq").data("skill_id") == "") ? 0 : $("#txtSkillNoReq").data("skill_id");

            if (vSkillId > 0) {

                $.ajax({
                    url: base_url + "hrdRecruitmentSkill/UpdateRecruitmentSkill",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblSkill();
                        } else {
                            f_MessageBoxShow(d.vResp['message']);
                        }
                        //$('#btnSkillSave').jqxButton({ disabled: false });
                        f_HideLoaderModal();
                    }
                });
            } else {
                $.ajax({
                    url: base_url + "hrdRecruitmentSkill/InsertRecruitmentSkill",
                    type: "POST",
                    contentType: "application/json",
                    data: vModel,
                    success: function (d) {
                        var isOke = d.vResp['isValid'];

                        if (isOke) {
                            f_UpdateTblSkill();
                        } else {
                            f_MessageBoxShow(d.vResp['message']);
                        }
                        //$('#btnSkillSave').jqxButton({ disabled: false });
                        f_HideLoaderModal();
                    }
                });
            }
        }
    });

});