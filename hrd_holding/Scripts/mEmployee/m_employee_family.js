
$(document).ready(function () {
    $('#btnFamilyNew').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#modFamily").jqxWindow('open');
    });

    $('#btnModFamCancel').on('click', function (event) {
        f_EmptyFamilyDetail();
        $("#modFamily").jqxWindow('close');
    });

    $('#btnFamilyEdit').on('click', function (event) {
        //var getselectedrowindexes = $('#tblFamily').jqxGrid('getselectedrowindexes');
        //if (getselectedrowindexes.length > 0) {
        //    // returns the selected row's data.
        //    var selectedRowData = $('#tblFamily').jqxGrid('getrowdata', getselectedrowindexes[0]);
        //    alert(JSON.stringify(selectedRowData));
        //}

        f_EmptyFamilyDetail();

        var rowindex = $('#tblFamily').jqxGrid('getselectedrowindex');
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
    });

    $('#btnFamilyDelete').on('click', function (event) {
        $("#modYesNo").jqxWindow('open');
    });

    $('#btnModFamSave').on('click', function (event) {
        vPesanFam = "NAMA FAMILY TIDAK BOLEH KOSONG";

        $("#notificationContent").html(vPesanFam);
        $("#jqxNotification").jqxNotification("open");

        //$('#btnModFamSave').jqxButton({ disabled: true });
        //$('#jqxLoader').jqxLoader('open');

        //var vAlamat = "";

        //if ($("#chkFamAddress").jqxCheckBox('checked')) {
        //    vAlamat = $("#txtAddress").val();
        //} else {
        //    vAlamat = $("#txtFamAddress").val();
        //}

        //var vModel = JSON.stringify({
        //    employee_code: $("#txtId").data("employee_code"),
        //    employee_name: $("#txtFullName").val(),
        //    seq_no: $("#txtFamName").data("fam_seq_no"),
        //    name: $("#txtFamName").val(),
        //    relationship: $("#cmbFamRelation").jqxComboBox('listBox').selectedIndex,
        //    nm_rel: $("#cmbFamRelation").val(),
        //    date_birth: $("#txtFamDob").jqxDateTimeInput('getDate'),
        //    sex: $("#cmbFamGender").jqxComboBox('listBox').selectedIndex,
        //    education: $("#txtFamEducation").val(),
        //    employment: $("#txtFamEmployment").val(),
        //    chk_address: $("#chkFamAddress").jqxCheckBox('checked'),
        //    address: vAlamat
        //});

        //var vSeqNo = ($("#txtFamName").data("fam_seq_no") == null
        //           || $("#txtFamName").data("fam_seq_no") == "") ? 0 : $("#txtFamName").data("fam_seq_no");

        //if (vSeqNo > 0) {

        //    $.ajax({
        //        url: base_url + "EmployeeFamily/UpdateEmployeeFamily",
        //        type: "POST",
        //        contentType: "application/json",
        //        data: vModel,
        //        success: function (d) {
        //            var isOke = d.vResp['isValid'];

        //            if (isOke) {
        //                f_UpdateTblFamily();
        //                $("#modFamily").jqxWindow('close');
        //            } else {
        //                alert(d.vResp['message']);
        //            }
        //            $('#btnModFamSave').jqxButton({ disabled: false });
        //            $('#jqxLoader').jqxLoader('close');
        //        }
        //    });
        //} else {
        //    $.ajax({
        //        url: base_url + "EmployeeFamily/InsertEmployeeFamily",
        //        type: "POST",
        //        contentType: "application/json",
        //        data: vModel,
        //        success: function (d) {
        //            var isOke = d.vResp['isValid'];

        //            if (isOke) {
        //                f_UpdateTblFamily();
        //                $("#modFamily").jqxWindow('close');
        //            } else {
        //                alert(d.vResp['message']);
        //            }
        //            $('#btnModFamSave').jqxButton({ disabled: false });
        //            $('#jqxLoader').jqxLoader('close');
        //        }
        //    });
        //}
    });

    function f_UpdateTblFamily() {
        var vEmpCode = $("#txtId").data("employee_code");

        $.ajax({
            url: base_url + "EmployeeFamily/GetEmployeeFamilyList",
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

    $('#btnYes').on('click', function (event) {
        var vEmpCode = $("#txtId").data("employee_code");

        var selectedRowIndex = $("#tblFamily").jqxGrid('selectedrowindex');
        var vSeqNo = $('#tblFamily').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


        if (vSeqNo > 0) {
            $.ajax({
                url: base_url + "EmployeeFamily/DeleteEmployeeFamily",
                type: "POST",
                contentType: "application/json",
                data: jQuery.param({ pEmployeeCode: vEmpCode, pSeqNo: vSeqNo }),
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