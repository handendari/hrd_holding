f_ShowLoaderModal();

var SrcCompanyLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "company_code" },
                 { name: "int_company" },
                 { name: "company_name" }],
    cache: false,
    filter: function () { $("#tblCompanyLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCompanyLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcCompanyLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "country_code",
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
          pagesize: 20,
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
              { text: 'Int Code', dataField: 'int_company', width: 100, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'company_name', width: 300 }
          ]
      });
}

var SrcBranchLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "branch_code" },
                 { name: "int_branch" },
                 { name: "branch_name" }],
    cache: false,
    filter: function () { $("#tblBranchLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblBranchLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcBranchLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "country_code",
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
          pagesize: 20,
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
              { text: 'Int Code', dataField: 'int_branch', width: 100, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'branch_name', width: 300 }
          ]
      });
}

var vSrcList = {
    //url: base_url + "/Company/GetCompanyList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "department_code" },
                 { name: "int_department" },
                 { name: "department_name" },
                 { name: "branch_code" },
                 { name: "int_branch" },
                 { name: "branch_name" },
                 { name: "company_code" },
                 { name: "int_company" },
                 { name: "company_name" },
                 { name: "description" },
                 { name: "entry_date" },
                 { name: "entry_user" },
                 { name: "edit_date" },
                 { name: "edit_user" }],
    cache: false,
    filter: function () { $("#tblDepartment").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblDepartment").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblDepartment() {

    $("#tblDepartment").jqxGrid(
      {
          theme: vTheme,
          //source: vAdapter,
          width: '100%',
          height: 500,
          filterable: true,
          sortable: true,
          pageable: true,
          pagesize: 20,
          pagesizeoptions: ['20', '30', '50'],
          rowsheight: 20,
          autorowheight: true,
          columnsresize: true,
          virtualmode: true,
          autoshowfiltericon: true,
          rendergridrows: function (obj) {
              return obj.data;
          },
          columns: [{ text: "dept code", dataField: "department_code", hidden: true },
                    { text: "Code", dataField: "int_department", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Dept. Name", dataField: "department_name", align: 'center' },
                    { text: "Branch Code", dataField: "branch_code", hidden: true, },
                    { text: "int branch", dataField: "int_branch", hidden:true},
                    { text: "Branch Name", dataField: "branch_name", width: 270, align: 'center' },
                    { text: "Company Code", dataField: "company_code", hidden: true, },
                    { text: "int company", dataField: "int_company", hidden: true, },
                    { text: "Company Name", dataField: "company_name", width: 270, align: 'center' },
                    { text: "Description", dataField: "address", width: 300, align: 'center' },
                    { text: "entry_date", dataField: "entry_date", hidden: true },
                    { text: "entry_user", dataField: "entry_user", hidden: true },
                    { text: "edit_date", dataField: "edit_date", hidden: true },
                    { text: "edit_user", dataField: "edit_user", hidden: true }
          ]
      });
}


function f_EmptyForm() {
    $("#txtDeptCode").val("");
    $("#txtDeptCode").data("dept_code", "");
    $("#txtDeptName").val("");
    $("#txtDeptDesc").val("");
}

function f_DeleteDepartment(pDeptCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblDepartment").jqxGrid('selectedrowindex');
    var vDeptCode = $('#tblDepartment').jqxGrid('getcellvalue', selectedRowIndex, "branch_code");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "Department/DeleteDepartment",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pDepartmentCode: vDeptCode }),
            success: function (d) {
                var isOke = d.vResp['isValid'];

                if (isOke) {
                    f_EmptyForm();

                    f_ReloadData();
                    f_HideLoaderModal();
                } else {
                    f_MessageBoxShow(d.vResp['message']);
                }
            }
        });
    }
}

function f_ReloadData() {
    var vCompanyCode = $("#txtIntCompany").data("company_code");
    var vBranchCode = $("#txtIntBranch").data("branch_code");

    vSrcList.url = base_url + "/Department/GetDepartmentList?pCompanyCode=" + vCompanyCode + "&pBranchCode=" + vBranchCode;

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblDepartment').jqxGrid({ source: vAdapter })
    $('#tblDepartment').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data
    $("#txtIntCompany").jqxInput({ theme: vTheme, width: 70, disabled: true });
    $("#btnCompany").jqxButton({ theme: vTheme });
    $("#txtCompanyName").jqxInput({ theme: vTheme, width: 300, disabled: true });

    $("#txtIntBranch").jqxInput({ theme: vTheme, width: 70, disabled: true });
    $("#btnBranch").jqxButton({ theme: vTheme });
    $("#txtBranchName").jqxInput({ theme: vTheme, width: 300, disabled: true });

    $("#txtBranchCode_Detail").jqxInput({ theme: vTheme, width: 70, disabled: true });
    $("#txtBranchName_Detail").jqxInput({ theme: vTheme, width: 300,disabled:true });
    $("#txtDeptCode").jqxInput({ theme: vTheme });
    $("#txtDeptName").jqxInput({ theme: vTheme });
    $('#txtDeptDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModDeptSave").jqxButton({ theme: vTheme });
    $("#btnModDeptCancel").jqxButton({ theme: vTheme });

    $("#notifDept").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarDepartment").jqxToolBar({
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
                                        "<span style='margin-left:5px'>RELOAD</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        var vCompanyCode = $("#txtIntCompany").data("company_code") == undefined ? "" : $("#txtIntCompany").data("company_code");
                        if (vCompanyCode == "") {
                            f_MessageBoxShow("Please Select Company...");
                        } else {
                            f_ReloadData();
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/edit property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>EDIT</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm();

                        var rowindex = $('#tblDepartment').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblDepartment').jqxGrid('getrowdata', rowindex);

                            //$("#txtCompCode").jqxInput({ disabled:true });
                            $("#txtBranchCode_Detail").val(rd.int_branch);
                            $("#txtBranchCode_Detail").data("branch_code", rd.branch_code);
                            $("#txtBranchName_Detail").val(rd.branch_name);
                            $("#txtDeptCode").val(rd.int_department);
                            $("#txtDeptCode").data("dept_code", rd.department_code);
                            $("#txtDeptName").val(rd.department_name);
                            $("#txtDeptDesc").val(rd.description);

                            $("#modDepartment").jqxWindow('open');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/add property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>NEW</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm()
                        $("#modDepartment").jqxWindow('open');

                    });
                    break;

            }
        }
    });


    initGridBranchLookUp();
    initGridCompanyLookUp();
    initTblDepartment();

    $("#BranchLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if(type=='button'){
                tool.height("25px")
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Checked_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Select Data</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblBranchLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblBranchLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtIntBranch").val(rd.int_branch);
                            $("#txtIntBranch").data("branch_code", rd.branch_code);

                            $("#txtBranchName").val(rd.branch_name);
                            $("#modBranchLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/exit_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Cancel</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("80px");
                    tool.on("click", function () {
                        $("#modBranchLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnBranch').on('click', function (event) {
        var vCompanyCode = $('#txtIntCompany').data("company_code") == undefined ? "" : $('#txtIntCompany').data("company_code");

        if (vCompanyCode == "") {
            f_MessageBoxShow("Please Select Company Data...");

        } else {
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
        }
    });

    $("#modBranchLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#CompanyLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == 'button') {
                tool.height("25px")
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/Checked_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Select Data</span> " +
                                   "</div>");
                    tool.append(button);
                    //tool.height("25px");
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCompanyLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCompanyLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtIntCompany").val(rd.int_company);
                            $("#txtIntCompany").data("company_code", rd.company_code);

                            $("#txtCompanyName").val(rd.company_name);
                            $("#modCompanyLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/exit_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Cancel</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("80px");
                    tool.on("click", function () {
                        $("#modCompanyLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnCompany').on('click', function (event) {
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

    $("#modCompanyLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#modDepartment").jqxWindow({
        height: 300, width: 800,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModDeptSave').on('click', function (event) {

        var vBranchCode = $('#txtBranchCode').data("branch_code") == undefined ? "" : $('#txtBranchCode').data("branch_code");

        var vModel = JSON.stringify({
            branch_code: vBranchCode,
            int_branch: $('#txtBranchCode').val(),
            branch_name: $('#txtBranchName').val(),
            company_code: $('#txtBranchIntCompany').data("company_code"),
            int_company: $('#txtBranchIntCompany').val(),
            company_name: $('#txtBranchName').val(),
            country_code: $('#txtBranchIntCountry').data("country_code"),
            int_country: $('#txtBranchIntCountry').val(),
            country_name: $('#txtBranchCountryName').val(),
            address: $('#txtBranchAddress').val(),
            postal_code: $('#txtBranchZip').val(),
            city_name: $('#txtBranchCity').val(),
            state: $('#txtBranchState').val(),
            phone_number: $('#txtBranchPhone').val(),
            fax_number: $('#txtBranchFax').val(),
            web_address: $('#txtBranchWebSite').val(),
            email_address: $('#txtBranchEmail').val(),
            //picture { get; set; }
            npwp: $('#txtBranchNpwp').val(),
            pimpinan: $('#txtBranchWhTax').val(),
            pimpinan_npwp: $('#txtBranchWhNpwp').val(),
            npp: $('#txtBranchNpp').val()
            //jhk { get; set; }
        });

        if (vBranchCode != "") {

            $.ajax({
                url: base_url + "BranchOffice/UpdateBranchOffice",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modBranch").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "BranchOffice/InsertBranchOffice",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modBranch").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModDeptCancel').on('click', function (event) {
        $("#modDepartment").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modBranchLookUp').jqxWindow({ position: { x: f_PosX($('#modBranchLookUp')), y: f_PosY($('#modBranchLookUp')) } });
        $('#modCompanyLookUp').jqxWindow({ position: { x: f_PosX($('#modCompanyLookUp')), y: f_PosY($('#modCompanyLookUp')) } });
        $('#modDepartment').jqxWindow({ position: { x: f_PosX($('#modDepartment')), y: f_PosY($('#modDepartment')) } });
    }

    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        f_PosisiModalDialog();
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        f_PosisiModalDialog();
    });
    //#endregion

    f_HideLoaderModal();
});
