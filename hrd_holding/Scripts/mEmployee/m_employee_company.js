﻿
//#region DATASOURCE TABEL
var vDataCompany = [];
var vSrcCompany =
{
    localdata: vDataCompany,
    datatype: "json",
    datafields: [
         { name: 'employee_code' },
         { name: 'seq_no' },
         { name: 'date_company', type: "date" },
         { name: 'company_code' },
         { name: 'int_company' },
         { name: 'company_name' },
         { name: 'branch_code' },
         { name: 'int_branch' },
         { name: 'branch_name' },
         { name: 'department_code' },
         { name: 'int_department' },
         { name: 'department_name' },
         { name: 'title_code' },
         { name: 'int_title' },
         { name: 'title_name' },
         { name: 'subtitle_code' },
         { name: 'description' }
    ]
};

//#endregion

function f_UpdateTblEmployeeCompany() {
    var vEmpCode = $("#txtId").data("employee_code");

    $.ajax({
        url: base_url + "EmployeeCompany/GetEmployeeCompanyList",
        type: "POST",
        dataType: "json",
        data: jQuery.param({ pEmployeeCode: vEmpCode }),
        success: function (dt) {
            f_FillTableCompany(dt.listCompany);
        }
    });
}

function f_FillTableCompany(listCompany) {
    vDataCompany.length = 0;
    for (var i = 0; i < listCompany.length; i++) {
        var row = {};
        row["employee_code"] = listCompany[i].employee_code;
        row["seq_no"] = listCompany[i].seq_no;
        row["date_company"] = new Date(parseInt(listCompany[i].date_company.substr(6)));
        row["company_code"] = listCompany[i].company_code;
        row["int_company"] = listCompany[i].int_company;
        row["company_name"] = listCompany[i].company_name;
        row["branch_code"] = listCompany[i].branch_code;
        row["int_branch"] = listCompany[i].int_branch;
        row["branch_name"] = listCompany[i].branch_name;
        row["department_code"] = listCompany[i].department_code;
        row["int_department"] = listCompany[i].int_department;
        row["department_name"] = listCompany[i].department_name;
        row["title_code"] = listCompany[i].title_code;
        row["int_title"] = listCompany[i].int_title;
        row["title_name"] = listCompany[i].title_name;
        row["subtitle_code"] = listCompany[i].subtitle_code;
        row["description"] = listCompany[i].description;

        vDataCompany.push(row);
    }

    var vAdapter = new $.jqx.dataAdapter(vSrcCompany);
    $("#tblCompany").jqxGrid({ source: vAdapter });
}

function f_EmptyCompanyDetail() {
    $("#txtCompCode").val($("#txtId").data("employee_code"));
    $("#txtCompCode").data("comp_seq_no", 0);
    $("#dtTgl_Company").jqxDateTimeInput('setDate', new Date());
    $("#txtCompanyCode_Company").val("");
    $("#txtCompanyName_Company").val("");
    $("#txtBranchCode_Company").val("");
    $("#txtBranchName_Company").val("");
    $("#txtDeptCode_Company").val("");
    $("#txtDeptName_Company").val("");
    $("#txtTitleCode_Company").val("");
    $("#txtTitleName_Company").val("");
    $("#txtGradeCode_Company").val("");
    $("#txtGradeName_Company").val("");
    $("#txtDesc_Company").val("");
}

function f_DeleteEmployeeCompany(pEmpCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblCompany").jqxGrid('selectedrowindex');
    var vSeqNo = $('#tblCompany').jqxGrid('getcellvalue', selectedRowIndex, "seq_no");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "EmployeeCompany/DeleteEmployeeCompany",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pEmployeeCode: pEmpCode, pSeqNo: vSeqNo }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    Form_Load($("#txtCompCode").val(), 1);

                    f_UpdateTblEmployeeCompany();
                    f_HideLoaderModal();
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
            }
        });
    }
}

var SrcBranchLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "branch_code" },
                 { name: "int_branch" },
                 { name: "branch_name" },
                 { name: "address" }],
    cache: false,
    filter: function () { $("#tblBranchLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblBranchLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcBranchLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "branch_code",
    root: 'Rows'
}

function initGridBranchLookUp() {
    $("#tblBranchLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'branch_code', hidden: true },
              { text: 'Int Code', dataField: 'int_branch', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'branch_name', width: 200 },
              { text: 'Address', dataField: 'address' }
          ]
      });
}

var SrcCompanyLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "company_code" },
                 { name: "int_company" },
                 { name: "company_name" },
                 { name: "city_name" }],
    cache: false,
    filter: function () { $("#tblCompanyLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCompanyLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcCompanyLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "company_code",
    root: 'Rows'
}

function initGridCompanyLookUp() {
    $("#tblCompanyLookUp").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: 420,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 15,
          pagesizeoptions: ['15', '20', '30'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
              { text: 'Code', dataField: 'company_code', hidden: true },
              { text: 'Int Code', dataField: 'int_company', width: 80, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'company_name', width: 200 },
              { text: 'City', dataField: 'city_name' }
          ]
      });
}
$(document).ready(function () {
    //#region Table TRAINING
    function initGridCompany() {
        $("#tblCompany").jqxGrid(
        {
            width: '100%',
            height: 250,
            theme: vTheme,
            columnsresize: true,
            rowsheight: 25,
            source: new $.jqx.dataAdapter(vSrcTrn),
            columns: [
                { text: 'Emp. Code', datafield: 'employee_code', hidden: true },
                { text: 'sequence', datafield: 'seq_no', hidden: true },
                { text: 'Date', datafield: 'date_company',width:100, filtertype: 'date', cellsalign: 'center', cellsformat: 'dd-MMM-yy' },
                { text: 'company_code', datafield: 'company_code', hidden: true },
                { text: 'int_company', datafield: 'int_company', hidden: true },
                { text: 'Company', datafield: 'company_name', width: 250},
                { text: 'branch_code', datafield: 'branch_code', hidden: true },
                { text: 'int_branch', datafield: 'int_branch', hidden: true },
                { text: 'Branch', datafield: 'branch_name', width: 300},
                { text: 'department_code', datafield: 'department_code', hidden: true },
                { text: 'int_department', datafield: 'int_department', hidden: true },
                { text: 'Department', datafield: 'department_name'},
                { text: 'title_code', datafield: 'title_code', hidden: true },
                { text: 'int_title', datafield: 'int_title', hidden: true },
                { text: 'Title', datafield: 'title_name', width: 200},
                { text: 'subtitle_code', datafield: 'subtitle_code', hidden: true },
                { text: 'Description', datafield: 'description'}
            ]
        });
    }
    //#endregion

    initGridCompany();
    initGridCompanyLookUp();
    initGridBranchLookUp();

    //#region MODAL COMPANY
    $("#psnTrn").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#psnCompCont",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#txtCompCode").jqxInput({ theme: vTheme, disabled: true });
    $("#dtTgl_Company").jqxDateTimeInput({ theme: vTheme, width: 150 });
    $("#txtCompanyCode_Company").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#btnCompanyCode_Company").jqxButton({ theme: vTheme });
    $("#txtCompanyName_Company").jqxInput({ theme: vTheme, disabled: true });
    $("#txtBranchCode_Company").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#btnBranchCode_Company").jqxButton({ theme: vTheme });
    $("#txtBranchName_Company").jqxInput({ theme: vTheme, disabled: true });
    $("#txtDeptCode_Company").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#btnDeptCode_Company").jqxButton({ theme: vTheme });
    $("#txtDeptName_Company").jqxInput({ theme: vTheme, disabled: true });
    $("#txtTitleCode_Company").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#btnTitleCode_Company").jqxButton({ theme: vTheme });
    $("#txtTitleName_Company").jqxInput({ theme: vTheme, disabled: true });
    $("#txtGradeCode_Company").jqxInput({ theme: vTheme, width: 50, disabled: true });
    $("#btnGradeCode_Company").jqxButton({ theme: vTheme });
    $("#txtGradeName_Company").jqxInput({ theme: vTheme, disabled: true });
    $("#txtDesc_Company").jqxInput({ theme: vTheme,width:350 });


    $("#btnSave_Company").jqxButton({ theme: vTheme, height: 30, width: 100 });
    $("#btnCancel_Company").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#btnNew_Company").jqxButton({theme: vTheme});
    $("#btnEdit_Company").jqxButton({theme: vTheme});
    $("#btnDelete_Company").jqxButton({theme: vTheme});
    $("#btnClose_Company").jqxButton({theme: vTheme});

    $("#modCompany").jqxWindow({
        height: 330, width: 1000,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modCompanyList").jqxWindow({
        height: 340, width: 1100,
        maxWidth: 1100,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modBranchLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modCompanyLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#BranchLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblBranchLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblBranchLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtBranchCode_Company").val(rd.int_branch);
                            $("#txtBranchCode_Company").data("branch_code", rd.branch_code);

                            $("#txtBranchName_Company").val(rd.branch_name);
                            $("#modBranchLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modBranchLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $("#CompanyLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCompanyLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCompanyLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtCompanyCode_Company").val(rd.int_company);
                            $("#txtCompanyCode_Company").data("company_code", rd.company_code);

                            $("#txtCompanyName_Company").val(rd.company_name);
                            $("#modCompanyLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel");
                    tool.height("25px");
                    tool.width("50px");
                    tool.on("click", function () {
                        $("#modCompanyLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });
    //#endregion MODAL COMPANY


    $('#btnNew_Company').on('click', function (event) {
        f_EmptyCompanyDetail();
        $("#modCompany").jqxWindow('open');
    });

    $('#btnCancel_Company').on('click', function (event) {
        //f_EmptyCompanyDetail();
        $("#modCompany").jqxWindow('close');
    });

    $('#btnEdit_Company').on('click', function (event) {
        f_EmptyCompanyDetail();

        var rowindex = $('#tblCompany').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            var rd = $('#tblCompany').jqxGrid('getrowdata', rowindex);

            $("#txtCompCode").val($("#txtId").data("employee_code"));
            $("#txtCompCode").data("comp_seq_no", rd.seq_no);
            $("#dtTgl_Company").jqxDateTimeInput('setDate', rd.date_company);
            $("#txtCompanyCode_Company").val(rd.int_company);
            $("#txtCompanyCode_Company").data("company_code",rd.company_code);
            $("#txtCompanyName_Company").val(rd.company_name);
            $("#txtBranchCode_Company").val(rd.int_branch);
            $("#txtBranchCode_Company").data("branch_code",rd.branch_code);
            $("#txtBranchName_Company").val(rd.branch_name);
            $("#txtDeptCode_Company").val(rd.int_department);
            $("#txtDeptCode_Company").data("dept_code",rd.department_code);
            $("#txtDeptName_Company").val(rd.department_name);
            $("#txtTitleCode_Company").val(rd.int_title);
            $("#txtTitleCode_Company").data("title_code",rd.title_code);
            $("#txtTitleName_Company").val(rd.title_name);
            $("#txtGradeCode_Company").val(rd.int_subtitle);
            $("#txtGradeCode_Company").data("grade_code",rd.subtitle_code);
            $("#txtGradeName_Company").val(rd.subtitle_name);
            $("#txtDesc_Company").val(rd.description);

            $("#modCompany").jqxWindow('open');
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnDelete_Company').on('click', function (event) {
        var rowindex = $('#tblCompany').jqxGrid('getselectedrowindex');

        if (rowindex >= 0) {
            $("#modYesNo").jqxWindow('open');
            vLookUp = "Comp";
        } else {
            f_MessageBoxShow("Please Select Data...");
        }
    });

    $('#btnSave_Company').on('click', function (event) {
        if ($("#txtCompanyCode_Company").val() == "") {
            f_NotificationShow($("#psnComp"), $("#psnCompContent"), "Company Code Tidak Boleh Kosong...");
            return;
        }

        if ($("#txtBranchCode_Company").val() == "") {
            f_NotificationShow($("#psnComp"), $("#psnCompContent"), "Branch Code Tidak Boleh Kosong...");
            return;
        }

        if ($("#txtDeptCode_Company").val() == "") {
            f_NotificationShow($("#psnComp"), $("#psnCompContent"), "Department Code Tidak Boleh Kosong...");
            return;
        }

        $("#modCompany").jqxWindow('close');
        f_ShowLoaderModal();

        var vModel = JSON.stringify({
            employee_code: $("#txtCompCode").val(),
            seq_no: $("#txtCompCode").data("comp_seq_no"),
            date_company: $("#dtTgl_Company").jqxDateTimeInput('getDate'),
            company_code: $("#txtCompanyCode_Company").data("company_code"),
            branch_code: $("#txtBranchCode_Company").data("branch_code"),
            department_code: $("#txtDeptCode_Company").data("dept_code"),
            title_code: $("#txtTitleCode_Company").data("title_code"),
            subtitle_code: $("#txtGradeCode_Company").data("grade_code"),
            description: $("#txtDesc_Company").val()
        });

        var vSeqNo = ($("#txtCompCode").data("comp_seq_no") == "") ? 0 : $("#txtCompCode").data("comp_seq_no");

        if (vSeqNo > 0) {
            $.ajax({
                url: base_url + "EmployeeCompany/UpdateEmployeeCompany",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_UpdateTblEmployeeCompany();

                        Form_Load($("#txtCompCode").val(), 1);
                        f_HideLoaderModal();

                    } else {
                        f_HideLoaderModal();
                        f_MessageBoxShow(d.vResp['message']);
                    }
                }
            });
        } else {
            $.ajax({
                url: base_url + "EmployeeCompany/InsertEmployeeCompany",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_UpdateTblEmployeeCompany();

                        Form_Load($("#txtCompCode").val(), 1);
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    f_HideLoaderModal();
                }
            });
        }

    });

    $('#btnCompanyCode_Company').on('click', function (event) {
        SrcCompanyLookUp.url = base_url + "/Company/GetCompanyLookUp";
        var vAdapter = new $.jqx.dataAdapter(SrcCompanyLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcCompanyLookUp.TotalRows) {
                    SrcCompanyLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblCompanyLookUp').jqxGrid({ source: vAdapter })
        $('#tblCompanyLookUp').jqxGrid('gotopage', 0);
        $("#modCompanyLookUp").jqxWindow('open');

    });

    $('#btnBranchCode_Company').on('click', function (event) {
        var vCompanyCode = $("#txtCompanyCode_Company").data("company_code");


        if (vCompanyCode == "" || vCompanyCode == undefined) {
            f_MessageBoxShow("Please Select Company....");
            return;
        }

        //vLookUp = "Comp";
        SrcBranchLookUp.url = base_url + "/BranchOffice/GetBranchOfficeLookUp?pCompanyCode=" + vCompanyCode;
        var vAdapter = new $.jqx.dataAdapter(SrcBranchLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcBranchLookUp.TotalRows) {
                    SrcBranchLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblBranchLookUp').jqxGrid({ source: vAdapter })
        $('#tblBranchLookUp').jqxGrid('gotopage', 0);
        $("#modBranchLookUp").jqxWindow('open');
    });

    $('#btnDeptCode_Company').on('click', function (event) {
        var vCompanyCode = $("#txtCompanyCode_Company").data("company_code");
        var vBranchCode = $("#txtBranchCode_Company").data("branch_code");


        if (vBranchCode == "" || vBranchCode == undefined) {
            f_MessageBoxShow("Please Select Branch Office....");
            return;
        }

        vLookUp = "Comp";
        SrcDeptLookUp.url = base_url + "/Department/GetDepartmentList?pCompanyCode=" + vCompanyCode + "&pBranchCode=" + vBranchCode;
        var vAdapter = new $.jqx.dataAdapter(SrcDeptLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcDeptLookUp.TotalRows) {
                    SrcDeptLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblDeptLookUp').jqxGrid({ source: vAdapter })
        $('#tblDeptLookUp').jqxGrid('gotopage', 0);
        $("#modDeptLookUp").jqxWindow('open');
    });

    $('#btnTitleCode_Company').on('click', function (event) {
        vLookUp = "Comp";
        SrcTitleLookUp.url = base_url + "/Title/GetTitleList";

        var vAdapter = new $.jqx.dataAdapter(SrcTitleLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcTitleLookUp.TotalRows) {
                    SrcTitleLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblTitleLookUp').jqxGrid({ source: vAdapter })
        $('#tblTitleLookUp').jqxGrid('gotopage', 0);
        $("#modTitleLookUp").jqxWindow('open');
    });

    $('#btnGradeCode_Company').on('click', function (event) {
    });

    $('#btnClose_Company').on('click', function (event) {
        $("#modCompanyList").jqxWindow('close');
    });
    
});