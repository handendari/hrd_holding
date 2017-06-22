f_ShowLoaderModal();

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
    url: base_url + "/Company/GetCompanyList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "company_code" },
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
                 { name: "pimpinan" },
                 { name: "country_code" },
                 { name: "int_country" },
                 { name: "country_name" },
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

function initTblCompany() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblCompany").jqxGrid(
      {
          theme: vTheme,
          source: vAdapter,
          width: '100%',
          height: 550,
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
          columns: [{ text: "Company Code", dataField: "company_code", hidden: true, },
                    { text: "Code", dataField: "int_company", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Name", dataField: "company_name", width: 270, align: 'center' },
                    { text: "Address", dataField: "address", width: 300, align: 'center' },
                    { text: "Postal Code", dataField: "postal_code", hidden: true },
                    { text: "City", dataField: "city_name", align: 'center' },
                    { text: "State", dataField: "state", align: 'center' },
                    { text: "Phone", dataField: "phone_number", align: 'center' },
                    { text: "Fax", dataField: "fax_number", align: 'center' },
                    { text: "Web", dataField: "web_address", hidden: true },
                    { text: "Email", dataField: "email_address", align: 'center' },
                    { text: "pimpinan", dataField: "pimpinan", hidden: true },
                    { text: "country_code", dataField: "country_code", hidden: true },
                    { text: "int_country", dataField: "int_country", hidden: true },
                    { text: "country_name", dataField: "country_name", hidden: true },
                    { text: "entry_date", dataField: "entry_date", hidden: true },
                    { text: "entry_user", dataField: "entry_user", hidden: true },
                    { text: "edit_date", dataField: "edit_date", hidden: true },
                    { text: "edit_user", dataField: "edit_user", hidden: true }
          ]
      });
}


function f_EmptyForm() {
    $("#txtCompCode").val("");
    $("#txtCompCode").data("company_code","");
    $("#txtCompName").val("");
    $("#txtCompAddress").val("");
    $("#txtCompCity").val("");
    $("#txtCompState").val("");
    $("#txtCompIntCountry").val("");
    $("#txtCompCountryName").val("");
    $("#txtCompEmail").val("");
    $("#txtCompZip").val("");
    $("#txtCompWebSite").val("");
    $("#txtCompPhone").val("");
    $("#txtCompFax").val("");
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

function f_ReloadData() {
    vSrcList.url = base_url + "/Company/GetCompanyList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblCompany').jqxGrid({ source: vAdapter })
    $('#tblCompany').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data
    $("#txtCompCode").jqxInput({ theme: vTheme,width:70 });
    $("#txtCompName").jqxInput({ theme: vTheme,width:300 });
    $('#txtCompAddress').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });
    $("#txtCompCity").jqxInput({ theme: vTheme });
    $("#txtCompState").jqxInput({ theme: vTheme });
    $("#txtCompIntCountry").jqxInput({ theme: vTheme, disabled: true });
    $("#btnCompCountry").jqxButton({ theme: vTheme });
    $("#txtCompCountryName").jqxInput({ theme: vTheme, width:200,disabled: true });
    $("#txtCompEmail").jqxInput({ theme: vTheme, width: 200 });
    $("#txtCompZip").jqxInput({ theme: vTheme });
    $("#txtCompWebSite").jqxInput({ theme: vTheme,width:200 });
    $("#txtCompPhone").jqxInput({ theme: vTheme });
    $("#txtCompFax").jqxInput({ theme: vTheme });

    $("#btnModCompSave").jqxButton({ theme: vTheme });
    $("#btnModCompCancel").jqxButton({ theme: vTheme });

    $("#notifCompany").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarCompany").jqxToolBar({
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
                        f_ReloadData();
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='../content/images/edit property_24_grey.png'/>" +
                                        "<span>EDIT</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm();

                        var rowindex = $('#tblCompany').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblCompany').jqxGrid('getrowdata', rowindex);

                            //$("#txtCompCode").jqxInput({ disabled:true });
                            $("#txtCompCode").val(rd.int_company);
                            $("#txtCompCode").data("company_code", rd.company_code);
                            $("#txtCompName").val(rd.company_name);
                            $("#txtCompAddress").val(rd.address);
                            $("#txtCompCity").val(rd.city_name);
                            $("#txtCompState").val(rd.state);
                            $("#txtCompIntCountry").val(rd.int_country);
                            $("#txtCompIntCountry").data("country_code",rd.country_code);
                            $("#txtCompCountryName").val(rd.country_name);
                            $("#txtCompEmail").val(rd.email_address);
                            $("#txtCompZip").val(rd.postal_code);
                            $("#txtCompWebSite").val(rd.web_address);
                            $("#txtCompPhone").val(rd.phone_number);
                            $("#txtCompFax").val(rd.fax_number);

                            $("#modCompany").jqxWindow('open');
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
                        //vUrl = base_url + "/employee/employeedetail";
                        //window.location.replace(vUrl);
                        //window.open(vUrl);

                        f_EmptyForm()
                        $("#modCompany").jqxWindow('open');

                    });
                    break;

            }
        }
    });


    initGridCountryLookUp();
    initTblCompany();

    $("#CountryLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data");
                    tool.height("25px");
                    tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblCountryLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblCountryLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtCompIntCountry").val(rd.int_country);
                            $("#txtCompIntCountry").data("country_code", rd.country_code);

                            $("#txtCompCountryName").val(rd.country_name);
                            $("#modCountryLookUp").jqxWindow('close');
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
                        $("#modCountryLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnCompCountry').on('click', function (event) {
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

    $("#modCompany").jqxWindow({
        height: 350, width: 1000,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModCompSave').on('click', function (event) {

        var vCompanyCode = $('#txtCompCode').data("company_code");

        var vModel = JSON.stringify({
            company_code : vCompanyCode,
            int_company : $('#txtCompCode').val(),
            company_name : $('#txtCompName').val(),
            country_code : $('#txtCompIntCountry').data("country_code"),
            int_country : $('#txtCompIntCountry').val(),
            country_name : $('#txtCompCountryName').val(),
            address : $('#txtCompAddress').val(),
            postal_code : $('#txtCompZip').val(),
            city_name : $('#txtCompCity').val(),
            state : $('#txtCompState').val(),
            phone_number : $('#txtCompPhone').val(),
            fax_number : $('#txtCompFax').val(),
            web_address : $('#txtCompWebSite').val(),
            email_address : $('#txtCompEmail').val()
            //picture { get; set; }
            //npwp { get; set; }
            //pimpinan { get; set; }
            //pimpinan_npwp { get; set; }
            //npp { get; set; }
            //jhk { get; set; }
        });

        if (vCompanyCode != "") {
            
            $.ajax({
                url: base_url + "Company/UpdateCompany",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modCompany").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "Company/InsertCompany",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modCompany").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModCompCancel').on('click', function (event) {
        $("#modCompany").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modCountryLookUp').jqxWindow({ position: { x: f_PosX($('#modCountryLookUp')), y: f_PosY($('#modCountryLookUp')) } });
        $('#modCompany').jqxWindow({ position: { x: f_PosX($('#modCompany')), y: f_PosY($('#modCompany')) } });
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
