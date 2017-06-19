﻿f_ShowLoaderModal();

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

var vSrcList =
  {
      //url: base_url + "/Employee/GetEmployeeList?pCompanyCode=1&pBranchCode=1",
      datatype: "json",
      type: "Post",
      datafields: [{ name: "employee_code" },
                   { name: "seq_no" },
                   { name: "employee_name" },
                   { name: "emp_address" },
                   { name: "company_name" },
                   { name: "departement_name" },
                   { name: "division_name" },
                   { name: "level_name" },
                   { name: "entry_date", type: "date" }],
      cache: false,
      filter: function () {
          $("#jqxgrid").jqxGrid('updatebounddata', 'filter');
      },
      sort: function () {
          $("#jqxgrid").jqxGrid('updatebounddata', 'sort');
      },
      beforeprocessing: function (data) {
          vSrcList.totalrecords = data["TotalRows"];
      },
      root: 'Rows'
  }

$(document).ready(function () {
    // prepare the data
    $("#txtIntCompany").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdCompany").jqxButton({ theme: vTheme });
    $("#txtCompany").jqxInput({ theme: vTheme, disabled: true });

    $("#txtIntBranch").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdBranch").jqxButton({ theme: vTheme });
    $("#txtBranch").jqxInput({ theme: vTheme, disabled: true });

    var theme = vTheme;

    $("#jqxToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == "button") {
                tool.height("25px");
                tool.width("90px");
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Refresh_24_grey.png'/>" +
                                        "<span>RELOAD</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        var vCompanyCode = $("#txtIntCompany").data("company_code");
                        var vBranchCode = $("#txtIntBranch").data("branch_code");

                        vSrcList.url = base_url + "/Employee/GetEmployeeList?pCompanyCode=" + vCompanyCode + "&pBranchCode=" + vBranchCode;

                        var vAdapter = new $.jqx.dataAdapter(vSrcList, {
                            downloadComplete: function (data, status, xhr) {
                                if (!vSrcList.TotalRows) {
                                    vSrcList.TotalRows = data.length;
                                }
                            }
                        });
                        $('#jqxgrid').jqxGrid({ source: vAdapter })
                        $('#jqxgrid').jqxGrid('gotopage', 0);
                        $("#jqxgrid").jqxWindow('open');
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/edit property_24_grey.png'/>" +
                                        "<span>EDIT</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        var rowindex = $('#jqxgrid').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#jqxgrid').jqxGrid('getrowdata', rowindex);
                            var vEmployee_code = rd.employee_code;
                            var vSeqNo = rd.seq_no;

                            vUrl = base_url + "/employee/employeedetail?pEmployeeCode=" + vEmployee_code + "&pSeqNo=" + vSeqNo;
                            //window.location.replace(vUrl);
                            window.open(vUrl);

                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/add property_24_grey.png'/>" +
                                        "<span>NEW</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        vUrl = base_url + "/employee/employeedetail";
                        //window.location.replace(vUrl);
                        window.open(vUrl);
                    });
                    break;

            }
        }
    });


    var filterChanged = false;

    $("#jqxgrid").jqxGrid(
      {
          theme: vTheme,
          //source: dataAdapter,
          width: '100%',
          height: '480px',
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 20,
          pagesizeoptions: ['20', '30','50'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [
            { text: 'emp. code', dataField: 'employee_code', width: 180, cellsalign: 'center' },
            { text: 'seq no', dataField: 'seq_no', hidden: true },
            { text: 'employee name', dataField: 'employee_name', width: 200 },
            { text: 'address', dataField: 'emp_address', width: 450 },
            { text: 'company name', dataField: 'company_name', width: 150, cellsalign: 'right', hidden: 'true' },
            { text: 'division name', dataField: 'division_name', width: 90, cellsalign: 'right', hidden: 'true' },
            { text: 'level name', dataField: 'level_name', minwidth: 200 },
            { text: 'entry date', dataField: 'entry_date', filtertype: 'date', cellsalign: 'center', width: 100, cellsformat: 'dd-MMM-yy' }
          ]
      });

    initGridCompanyLookUp();
    initGridBranchLookUp();

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
                            $("#txtIntBranch").val(rd.int_branch);
                            $("#txtIntBranch").data("branch_code", rd.branch_code);

                            $("#txtBranch").val(rd.branch_name);
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
                            $("#txtIntCompany").val(rd.int_company);
                            $("#txtIntCompany").data("company_code", rd.company_code);

                            $("#txtCompany").val(rd.company_name);
                            $("#modCompanyLookUp").jqxWindow('close');

                            $("#txtIntBranch").val("");
                            $("#txtIntBranch").data("branch_code", "");
                            $("#txtBranch").val("");

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

    $('#btnKdCompany').on('click', function (event) {
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

    $('#btnKdBranch').on('click', function (event) {
        var vCompanyCode = $("#txtIntCompany").data("company_code");


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

    f_HideLoaderModal();
});
