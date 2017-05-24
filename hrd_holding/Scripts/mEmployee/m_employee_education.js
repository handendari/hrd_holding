function f_UpdateTblFamily() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeEducation/GetEmployeeEducationList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            if (dt.listEdu != null && dt.listEdu.length > 0) {
                f_FillTableEdu(dt.listEdu);
            }
        }
    });
}

function f_FillTableEducation(listEdu) {
    vDataEdu.length = 0;
    for (var i = 0; i < listEdu.length; i++) {
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

        vDataEdu.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcEdu);
    $("#tblEducation").jqxGrid({ source: vAdapter });
}

function f_EmptyEduDetail() {
    $("#dtEduStartYear").jqxDateTimeInput('setDate', new Date());
    $("#dtEduStartYear").data("edu_seq_no", 0);

    $("#dtEduEndYear").jqxDateTimeInput('setDate', new Date());

    $("#cmbEduLevel").jqxComboBox({ selectedIndex: 0 });
    $("#txtEduMajors").val("")
    $("#txtEduSchool").val("")
    $("#txtEduCity").val("")

    $("#txtEduCountryCode").val("")
    $("#txtEduCountryName").val("")

}

function f_DeleteEmployeeEdu(pEmpCode) {
    $('#jqxLoader').jqxLoader('open');

    var selectedRowIndex = $("#tblEducation").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblEducation').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeEducation/DeleteEmployeeEducation",
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

    //#region INIT EDUCATION
    //#region Table EDUCATION
    function initGridEducation() {
        var source =
       {
           localdata: vDataTbl,
           datatype: "local",
           datafields: [
                { name: 'employee_code' },
                { name: 'seq_no' },
                { name: 'nm_jenjang' },
                { name: 'school' },
                { name: 'jurusan' },
                { name: 'city' },
                { name: 'start_year', type: "date" },
                { name: 'end_year', type: "date" }
           ]
       };
        var dataAdapter = new $.jqx.dataAdapter(source);

        $("#tblEducation").jqxGrid(
        {
            width: '100%',
            height: 200,
            theme: vTheme,
            source: dataAdapter,
            columnsresize: true,
            rowsheight: 25,
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Jenjang', datafield: 'nm_jenjang' },
                { text: 'Sekolah', datafield: 'school' },
                { text: 'Jurusan', datafield: 'jurusan' },
                { text: 'Kota', datafield: 'city' },
                {
                    text: 'Start Date', datafield: 'start_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                },
                {
                    text: 'End Date', datafield: 'end_year',
                    filtertype: 'date', cellsformat: 'dd-MMM-yy',
                    cellsalign: 'center', align: 'center', width: 150
                }
            ]
        });
    }

    initGridEducation();
    //#endregion


    $("#psnEducation").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnEduContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#dtEduStartYear").jqxDateTimeInput({ theme: vTheme });
    $("#dtEduEndYear").jqxDateTimeInput({ theme: vTheme });

    $("#cmbEduLevel").jqxComboBox({
        theme: vTheme, width: 120,
        source: vCmbEducation, selectedIndex: 0
    });
    $("#txtEduMajors").jqxInput({ theme: vTheme })
    $("#txtEduSchool").jqxInput({ theme: vTheme })
    $("#txtEduCity").jqxInput({ theme: vTheme })

    $("#txtEduCountryCode").jqxInput({ theme: vTheme })
    $("#btnEduCountry").jqxButton({ theme: vTheme });
    $("#txtEduCountryName").jqxInput({ theme: vTheme })


    $("#btnEduSave").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduCancel").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modEducation").jqxWindow({
        height: 280, width: 600,
        theme: vTheme, isModal: true,
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
        if (rowindex > 0) {
            var rd = $('#tblEducation').jqxGrid('getrowdata', rowindex);

            //$("#txtFamName").val(rd.name);
            //$("#txtFamName").data("fam_seq_no", rd.seq_no);

            //$("#txtFamDob").jqxDateTimeInput('setDate', rd.date_birth);

            //var vFamGender = rd.sex;
            //$("#cmbFamGender").jqxComboBox({ selectedIndex: vFamGender });

            //var vFamRelation = rd.relationship;
            //$("#cmbFamRelation").jqxComboBox({ selectedIndex: vFamRelation });

            //$("#txtFamEducation").val(rd.education);
            //$("#txtFamEmployment").val(rd.employment);
            //$("#txtFamAddress").val(rd.address);

            $("#modEducation").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnEduDelete').on('click', function (event) {
        var rowindex = $('#tblEducation').jqxGrid('getselectedrowindex');

        if (rowindex > 0) {
            $("#modYesNo").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnEduSave').on('click', function (event) {
        $('#btnEduSave').jqxButton({ disabled: true });
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
    });

});