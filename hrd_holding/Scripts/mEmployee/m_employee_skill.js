
//#region DATASOURCE TABEL
var vDataSkill = [];
var vSrcSkill =
{
    localdata: vDataSkill,
    datatype: "json",
    datafields: [
         { name: 'employee_code' },
         { name: 'seq_no' },
         { name: 'skill' },
         { name: 'level' },
         { name: 'nm_level' },
         { name: 'description' }
    ]
};
//#endregion

function f_UpdateTblSkill() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeSkill/GetEmployeeSkillList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
           // if (dt.listSkill != null && dt.listSkill.length > 0) {
                f_FillTableSkill(dt.listSkill);
           // }
        }
    });
}

function f_FillTableSkill(listSkill) {
    vDataSkill.length = 0;
    for (var i = 0; i < listSkill.length; i++) {
        var row = {};
        row["employment_code"] = listSkill[i].employee_code;
        row["seq_no"] = listSkill[i].seq_no;
        row["skill"] = listSkill[i].skill;
        row["level"] = listSkill[i].level;
        row["nm_level"] = listSkill[i].nm_level;
        row["description"] = listSkill[i].description;

        vDataSkill.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcSkill);
    $("#tblSkill").jqxGrid({ source: vAdapter });
}

function f_EmptyFamilySkill() {
    $("#txtSkillCode").val($("#txtId").data("employee_code"));
    $("#txtSkillCode").data("skill_seq_no", 0);

    $("#txtSkillName").val("");
    $("#cmbSkillLevel").jqxComboBox({ selectedIndex: 0 });
    $("#txtSkillDesc").val("");
}

function f_DeleteEmployeeSkill(pEmpCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblSkill").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblSkill').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo >= 0) {
        $.ajax({
            url: base_url + "EmployeeSkill/DeleteEmployeeSkill",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
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
    $("#btnSkillNew").jqxButton({theme: vTheme});
    $("#btnSkillEdit").jqxButton({theme: vTheme});
    $("#btnSkillDelete").jqxButton({theme: vTheme});

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
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Skill', datafield: 'skill' },
                { text: 'Level', datafield: 'level', hidden: true },
                { text: 'Level', datafield: 'nm_level' },
                { text: 'Description', datafield: 'description' }
            ]
        });
    }
    //#endregion

    initGridSkill();

    //#region MODAL FAMILY 
    $("#psnSkill").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnSkillContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#txtSkillCode").jqxInput({ theme: vTheme, disabled: true })
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
        height: 280, width: 350,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });
    //#endregion MODAL FAMILY

    //#endregion

    $('#btnSkillNew').on('click', function (event) {
        f_EmptyFamilySkill();
        $("#modSkill").jqxWindow('open');
    });

    $('#btnSkillCancel').on('click', function (event) {
        f_EmptyFamilySkill();
        $("#modSkill").jqxWindow('close');
    });

    $('#btnSkillEdit').on('click', function (event) {
        f_EmptyFamilySkill();

        var rowindex = $('#tblSkill').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblSkill').jqxGrid('getrowdata', rowindex);

            //alert(JSON.stringify(rd));
            $("#txtSkillCode").data("skill_seq_no", rd.seq_no);

            $("#txtSkillName").val(rd.skill);

            var vSkillLevel = rd.level;
            $("#cmbSkillLevel").jqxComboBox({ selectedIndex: vSkillLevel });

            $("#txtSkillDesc").val(rd.description);

            $("#modSkill").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnSkillDelete').on('click', function (event) {
        var rowindex = $('#tblSkill').jqxGrid('getselectedrowindex');
        vLookUp = "Emp";

        if (rowindex >= 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnSkillSave').on('click', function (event) {
        if ($("#txtSkillName").val() == "") {
            f_NotificationShow($("#psnSkill"), $("#psnSkillIsi"), "Nama Skill Tidak Boleh Kosong...");

        } else {
            //$('#btnSkillSave').jqxButton({ disabled: true });
            $("#modSkill").jqxWindow('close');
            f_ShowLoaderModal();


            var vModel = JSON.stringify({
                employee_code: $("#txtId").data("employee_code"),
                employee_name: $("#txtFullName").val(),
                seq_no: $("#txtSkillCode").data("skill_seq_no"),
                skill: $("#txtSkillName").val(),
                level: $("#cmbSkillLevel").jqxComboBox('listBox').selectedIndex,
                nm_level: $("#cmbSkillLevel").val(),
                description: $("#txtSkillDesc").val()
            });

            var vSeqNo = ($("#txtSkillCode").data("skill_seq_no") == "") ? 0 : $("#txtSkillCode").data("skill_seq_no");

            if (vSeqNo >= 0) {

                $.ajax({
                    url: base_url + "EmployeeSkill/UpdateEmployeeSkill",
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
                    url: base_url + "EmployeeSkill/InsertEmployeeSkill",
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