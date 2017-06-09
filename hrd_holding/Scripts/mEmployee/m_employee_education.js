var vDataEdu = [];
var vSrcEdu =
{
    localdata: vDataEdu,
    datatype: "json",
    datafields: [
         { name: 'employee_code' },
         { name: 'seq_no' },
         { name: 'jenjang' },
         { name: 'nm_jenjang' },
         { name: 'school' },
         { name: 'jurusan' },
         { name: 'city' },
         { name: 'country_code' },
         { name: 'int_country' },
         { name: 'country_name' },
         { name: 'start_year', type: "date" },
         { name: 'end_year', type: "date" }
    ]
};

function f_UpdateTblEducation() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeEducation/GetEmployeeEducationList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
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
        row["employment_code"] = listEdu[i].employee_code;
        row["seq_no"] = listEdu[i].seq_no;
        row["jenjang"] = listEdu[i].jenjang;
        row["nm_jenjang"] = listEdu[i].nm_jenjang;
        row["school"] = listEdu[i].school;
        row["jurusan"] = listEdu[i].jurusan;
        row["city"] = listEdu[i].city;
        row["country_code"] = listEdu[i].country_code;
        row["int_country"] = listEdu[i].int_country,
        row["country_name"] = listEdu[i].country_name;
        row["start_year"] = new Date(parseInt(listEdu[i].start_year.substr(6)));
        row["end_year"] = new Date(parseInt(listEdu[i].end_year.substr(6)));

        vDataEdu.push(row);
    }
    var vAdapter = new $.jqx.dataAdapter(vSrcEdu);
    $("#tblEducation").jqxGrid({ source: vAdapter });
}

function f_EmptyEduDetail() {
    $('#txtEduCode').val($('#txtId').data("employee_code"));
    $("#dtEduCode").data("edu_seq_no", 0);

    $("#dtEduStartYear").jqxDateTimeInput('setDate', new Date());
    $("#dtEduEndYear").jqxDateTimeInput('setDate', new Date());

    $("#cmbEduLevel").jqxComboBox({ selectedIndex: 0 });
    $("#txtEduMajors").val("")
    $("#txtEduSchool").val("")
    $("#txtEduCity").val("")

    $("#txtEduCountryCode").val("")
    $("#txtEduCountryName").val("")

}

function f_DeleteEmployeeEducation(pEmpCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblEducation").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblEducation').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo >= 0) {
        $.ajax({
            url: base_url + "EmployeeEducation/DeleteEmployeeEducation",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
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
    $("#btnEduNew").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduEdit").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnEduDelete").jqxButton({ theme: vTheme, height: 30, width: 100 });

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
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'jenjang Code', datafield: 'jenjang', hidden: true },
                { text: 'Jenjang', datafield: 'nm_jenjang' },
                { text: 'Sekolah', datafield: 'school' },
                { text: 'Jurusan', datafield: 'jurusan' },
                { text: 'Kota', datafield: 'city' },
                { text: 'country_code', datafield: 'country_code', hidden: true },
                { text: 'int_country', datafield: 'int_country',hidden:true},
                { text: 'Country', datafield: 'country_name' },

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

    $("#txtEduCode").jqxInput({ theme: vTheme, disabled: true })
    $("#psnEducation").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnEduContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#dtEduStartYear").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#dtEduEndYear").jqxDateTimeInput({ theme: vTheme, width: 150 });

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
        height: 280, width: 750,
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

            $("#txtEduCode").data("edu_seq_no", rd.seq_no);

            $("#dtEduStartYear").jqxDateTimeInput('setDate', rd.start_year);
            $("#dtEduEndYear").jqxDateTimeInput('setDate', rd.end_year);

            var vEduLevel = rd.jenjang;
            $("#cmbEduLevel").jqxComboBox({ selectedIndex: vEduLevel });

            //var vFamRelation = rd.relationship;
            //$("#cmbFamRelation").jqxComboBox({ selectedIndex: vFamRelation });

            $("#txtEduMajors").val(rd.jurusan);
            $("#txtEduSchool").val(rd.school);
            $("#txtEduCity").val(rd.city);
            $("#txtEduCountryCode").val(rd.int_country);
            $("#txtEduCountryCode").data("edu_country_code", rd.country_code);
            $("#txtEduCountryName").val(rd.country_name);

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
            employee_code: $("#txtId").data("employee_code"),
            employee_name: $("#txtFullName").val(),
            seq_no: $("#txtEduCode").data("edu_seq_no"),
            start_year : $("#dtEduStartYear").jqxDateTimeInput('getDate'),
            end_year :$("#dtEduEndYear").jqxDateTimeInput('getDate'),
            jenjang : $("#cmbEduLevel").jqxComboBox('listBox').selectedIndex,
            nm_jenjang : $("#cmbEduLevel").val(),
            jurusan : $("#txtEduMajors").val(),
            school : $("#txtEduSchool").val(),
            city : $("#txtEduCity").val(),
            country_code : $("#txtEduCountryCode").data("edu_country_code"),
            int_country : $("#txtEduCountryCode").val(),
            country_name : $("#txtEduCountryName").val()
        });

        var vSeqNo = ($("#txtEduCode").data("edu_seq_no") == "") ? 0 : $("#txtEduCode").data("edu_seq_no");

        if (vSeqNo >= 0) {

            $.ajax({
                url: base_url + "EmployeeEducation/UpdateEmployeeEducation",
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
                url: base_url + "EmployeeEducation/InsertEmployeeEducation",
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

    $('#btnEduCountry').on('click', function (event) {
        SrcCountryLookUp.url = base_url + "/Country/GetCountryList";
        vLookUp = "edu";

        var vAdapter = new $.jqx.dataAdapter(SrcCountryLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcCountryLookUp.TotalRows) {
                    SrcCountryLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblCountryLookUp').jqxGrid({ source: vAdapter })

        $('#tblCountryLookUp').jqxGrid('gotopage', 0);

        $("#modCountryLookUp").jqxWindow('open');
    });

});