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

var SrcCountryLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "country_code" },
                 { name: "int_country" },
                 { name: "country_name" }],
    cache: false,
    filter: function () { $("#tblCountryLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCountryLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcCountryLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "country_code",
    root: 'Rows'
}

function initGridCountryLookUp() {
    $("#tblCountryLookUp").jqxGrid(
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
              { text: 'Code', dataField: 'country_code', hidden: true },
              { text: 'Int Code', dataField: 'int_country', width: 100, cellsalign: 'center', align: 'center' },
              { text: 'Name', dataField: 'country_name', width: 300 }
          ]
      });
}

var vSrcList = {
    //url: base_url + "/Company/GetCompanyList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "branch_code" },
                 { name: "int_branch" },
                 { name: "branch_name" },
                 { name: "company_code" },
                 { name: "int_company" },
                 { name: "company_name" },
                 { name: "address" },
                 { name: "postal_code" },
                 { name: "city_name" },
                 { name: "state" },
                 { name: "phone_number" },
                 { name: "fax_number" },
                 { name: "web_address" },
                 { name: "email_address" },
                 { name: "country_code" },
                 { name: "int_country" },
                 { name: "country_name" },
                 { name: "npwp" },
                 { name: "pimpinan" },
                 { name: "pimpinan_npwp" },
                 { name: "npp" },
                 { name: "entry_date" },
                 { name: "entry_user" },
                 { name: "edit_date" },
                 { name: "edit_user" }],
    cache: false,
    filter: function () { $("#tblCompany").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCompany").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblBranch() {
    //var vAdapter = new $.jqx.dataAdapter(vSrcList, {
    //    downloadComplete: function (data, status, xhr) {
    //        if (!vSrcList.TotalRows) {
    //            vSrcList.TotalRows = data.length;
    //        }
    //    }
    //});

    $("#tblBranch").jqxGrid(
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
          columns: [{ text: "Branch Code", dataField: "branch_code", hidden: true, },
                    { text: "Code", dataField: "int_branch", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Name", dataField: "branch_name", width: 270, align: 'center' },
                    { text: "Company Code", dataField: "company_code", hidden: true, },
                    { text: "intCode", dataField: "int_company", hidden: true, },
                    { text: "Company Name", dataField: "company_name", width: 270, align: 'center' },
                    { text: "Address", dataField: "address", width: 300, align: 'center' },
                    { text: "Postal Code", dataField: "postal_code", hidden: true },
                    { text: "City", dataField: "city_name", align: 'center' },
                    { text: "State", dataField: "state", align: 'center' },
                    { text: "Phone", dataField: "phone_number", align: 'center', hidden: true },
                    { text: "Fax", dataField: "fax_number", align: 'center', hidden: true },
                    { text: "Web", dataField: "web_address", hidden: true },
                    { text: "Email", dataField: "email_address", align: 'center', hidden: true },
                    { text: "country_code", dataField: "country_code", hidden: true },
                    { text: "int_country", dataField: "int_country", hidden: true },
                    { text: "country_name", dataField: "country_name", hidden: true },
                    { text: "npwp", dataField: "npwp", hidden: true },
                    { text: "pimpinan", dataField: "pimpinan", hidden: true },
                    { text: "pimpinan_npwp", dataField: "pimpinan_npwp", hidden: true },
                    { text: "npp", dataField: "npp", hidden: true },
                    { text: "entry_date", dataField: "entry_date", hidden: true },
                    { text: "entry_user", dataField: "entry_user", hidden: true },
                    { text: "edit_date", dataField: "edit_date", hidden: true },
                    { text: "edit_user", dataField: "edit_user", hidden: true }
          ]
      });
}


function f_EmptyForm() {
    $("#txtBranchCode").val("");
    $("#txtBranchCode").data("branch_code", "");
    $("#txtBranchName").val("");
    $("#txtBranchAddress").val("");
    $("#txtBranchCity").val("");
    $("#txtBranchState").val("");
    $("#txtBranchIntCountry").val("");
    $("#txtBranchCountryName").val("");
    $("#txtBranchEmail").val("");
    $("#txtBranchZip").val("");
    $("#txtBranchWebSite").val("");
    $("#txtBranchPhone").val("");
    $("#txtBranchFax").val("");
    $("#txtBranchNpwp").val("");
    $("#txtBranchNpp").val("");
    $("#txtBranchWhTax").val("");
    $("#txtBranchWhNpwp").val("");
}

function f_DeleteBranchOffice(pBranchCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblBranch").jqxGrid('selectedrowindex');
    var vBranchCode = $('#tblBranch').jqxGrid('getcellvalue', selectedRowIndex, "branch_code");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "BranchOffice/DeleteBranchOffice",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pBranchCode: vBranchCode }),
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
    var vCompanyCode = $("#txtBranchIntCompany").data("company_code");

    vSrcList.url = base_url + "/BranchOffice/GetBranchOfficeList?pCompanyCode=" + vCompanyCode;

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblBranch').jqxGrid({ source: vAdapter })
    $('#tblBranch').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data
    $("#txtBranchIntCompany").jqxInput({ theme: vTheme, width: 70, disabled: true });
    $("#btnBranchCompany").jqxButton({ theme: vTheme });
    $("#txtBranchCompanyName").jqxInput({ theme: vTheme, width: 300, disabled: true });

    $("#txtBranchCode").jqxInput({ theme: vTheme, width: 70 });
    $("#txtBranchName").jqxInput({ theme: vTheme, width: 300 });
    $('#txtBranchAddress').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Alamat',
        height: 50, width: 300, minLength: 1
    });
    $("#txtBranchCity").jqxInput({ theme: vTheme });
    $("#txtBranchState").jqxInput({ theme: vTheme });
    $("#txtBranchIntCountry").jqxInput({ theme: vTheme, disabled: true });
    $("#btnBranchCountry").jqxButton({ theme: vTheme });
    $("#txtBranchCountryName").jqxInput({ theme: vTheme, width: 200, disabled: true });
    $("#txtBranchEmail").jqxInput({ theme: vTheme, width: 200 });
    $("#txtBranchZip").jqxInput({ theme: vTheme });
    $("#txtBranchWebSite").jqxInput({ theme: vTheme, width: 200 });
    $("#txtBranchPhone").jqxInput({ theme: vTheme });
    $("#txtBranchFax").jqxInput({ theme: vTheme });
    $("#txtBranchNpwp").jqxInput({ theme: vTheme });
    $("#txtBranchNpp").jqxInput({ theme: vTheme });
    $("#txtBranchWhTax").jqxInput({ theme: vTheme });
    $("#txtBranchWhNpwp").jqxInput({ theme: vTheme });

    $("#btnModBranchSave").jqxButton({ theme: vTheme });
    $("#btnModBranchCancel").jqxButton({ theme: vTheme });

    $("#notifBranch").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarBranch").jqxToolBar({
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
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Refresh_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>RELOAD</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        var vCompanyCode = $("#txtBranchIntCompany").data("company_code") == undefined ? "" : $("#txtBranchIntCompany").data("company_code");
                        if (vCompanyCode == "") {
                            f_MessageBoxShow("Please Select Company...");
                        } else {
                            f_ReloadData();
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/edit property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>EDIT</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm();

                        var rowindex = $('#tblBranch').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblBranch').jqxGrid('getrowdata', rowindex);

                            //$("#txtCompCode").jqxInput({ disabled:true });
                            $("#txtBranchCode").val(rd.int_branch);
                            $("#txtBranchCode").data("branch_code", rd.branch_code);
                            $("#txtBranchName").val(rd.branch_name);
                            $("#txtBranchAddress").val(rd.address);
                            $("#txtBranchCity").val(rd.city_name);
                            $("#txtBranchState").val(rd.state);
                            $("#txtBranchIntCountry").val(rd.int_country);
                            $("#txtBranchIntCountry").data("country_code", rd.country_code);
                            $("#txtBranchCountryName").val(rd.country_name);
                            $("#txtBranchEmail").val(rd.email_address);
                            $("#txtBranchZip").val(rd.postal_code);
                            $("#txtBranchWebSite").val(rd.web_address);
                            $("#txtBranchPhone").val(rd.phone_number);
                            $("#txtBranchFax").val(rd.fax_number);
                            $("#txtBranchNpwp").val(rd.npwp);
                            $("#txtBranchNpp").val(rd.npp);
                            $("#txtBranchWhTax").val(rd.pimpinan);
                            $("#txtBranchWhNpwp").val(rd.pimpinan_npwp);

                            $("#modBranch").jqxWindow('open');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/add property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>NEW</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm()
                        $("#modBranch").jqxWindow('open');

                    });
                    break;

            }
        }
    });


    initGridCountryLookUp();
    initGridCompanyLookUp();
    initTblBranch();

    $("#CountryLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == 'button') {
                tool.height("25px")
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Checked_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Select Data</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCountryLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCountryLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtBranchIntCountry").val(rd.int_country);
                            $("#txtBranchIntCountry").data("country_code", rd.country_code);

                            $("#txtBranchCountryName").val(rd.country_name);
                            $("#modCountryLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/exit_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Cancel</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("80px");
                    tool.on("click", function () {
                        $("#modCountryLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnBranchCountry').on('click', function (event) {
        SrcCountryLookUp.url = base_url + "/Country/GetCountryLookUp";
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

    $("#modCountryLookUp").jqxWindow({
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
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Checked_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Select Data</span> " +
                                   "</div>");
                    tool.append(button);
                    //tool.height("25px");
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCompanyLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCompanyLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtBranchIntCompany").val(rd.int_company);
                            $("#txtBranchIntCompany").data("company_code", rd.company_code);

                            $("#txtBranchCompanyName").val(rd.company_name);
                            $("#modCompanyLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/exit_16_grey.png'/>" +
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

    $('#btnBranchCompany').on('click', function (event) {
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

    $("#modBranch").jqxWindow({
        height: 400, width: 1000,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModBranchSave').on('click', function (event) {

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

    $('#btnModBranchCancel').on('click', function (event) {
        $("#modBranch").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modCountryLookUp').jqxWindow({ position: { x: f_PosX($('#modCountryLookUp')), y: f_PosY($('#modCountryLookUp')) } });
        $('#modCompanyLookUp').jqxWindow({ position: { x: f_PosX($('#modCompanyLookUp')), y: f_PosY($('#modCompanyLookUp')) } });
        $('#modBranch').jqxWindow({ position: { x: f_PosX($('#modBranch')), y: f_PosY($('#modBranch')) } });
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
