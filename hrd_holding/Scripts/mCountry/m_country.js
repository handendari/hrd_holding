f_ShowLoaderModal();

var vSrcList = {
    url: base_url + "Country/GetCountryList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "country_code" },
                 { name: "int_code" },
                 { name: "int_country" },
                 { name: "country_name" },
                 { name: "description" }],
    cache: false,
    sortcolumn: 'country_code',
    sortdirection: 'desc',
    filter: function () { $("#tblCountry").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblCountry").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblCountry() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblCountry").jqxGrid(
      {
          theme: vTheme,
          source: vAdapter,
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
          columns: [{ text: "country code", dataField: "country_code", hidden: true },
                    { text: "Int. Code", dataField: "int_code", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Initial", dataField: "int_country", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Country Name", dataField: "country_name", align: 'center' },
                    { text: "Description", dataField: "description", align: 'center' }
          ]
      });
}


function f_EmptyForm() {
    $("#txtCountryInitial").val("");
    $("#txtCountryCode").val("");
    $("#txtCountryCode").data("country_code", "");
    $("#txtCountryName").val("");
    $("#txtCountryDesc").val("");
}

function f_DeleteCountry(pCountryCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblCountry").jqxGrid('selectedrowindex');
    var vCountryCode = $('#tblCountry').jqxGrid('getcellvalue', selectedRowIndex, "country_code");


    if (vCountryCode > 0) {
        $.ajax({
            url: base_url + "Country/DeleteCountry",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pCountryCode: vCountryCode }),
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
    vSrcList.url = base_url + "/Country/GetCountryList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblCountry').jqxGrid({ source: vAdapter })
    $('#tblCountry').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data
    $("#txtCountryInitial").jqxInput({ theme: vTheme });
    $("#txtCountryCode").jqxInput({ theme: vTheme });
    $("#txtCountryName").jqxInput({ theme: vTheme });
    $('#txtCountryDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModCountrySave").jqxButton({ theme: vTheme });
    $("#btnModCountryCancel").jqxButton({ theme: vTheme });

    $("#notifCountry").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarCountry").jqxToolBar({
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
                        f_ReloadData();
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

                        var rowindex = $('#tblCountry').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblCountry').jqxGrid('getrowdata', rowindex);

                            $("#txtCountryInitial").val(rd.int_country);
                            $("#txtCountryCode").data("country_code", rd.country_code);
                            $("#txtCountryCode").val(rd.int_code);
                            $("#txtCountryName").val(rd.country_name);
                            $("#txtCountryDesc").val(rd.description);

                            $("#modCountry").jqxWindow('open');
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
                        $("#modCountry").jqxWindow('open');

                    });
                    break;

            }
        }
    });

    initTblCountry();


    $("#modCountry").jqxWindow({
        height: 300, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModCountrySave').on('click', function (event) {
        $("#modCountry").jqxWindow('close');

        var vCountryCode = $('#txtCountryCode').data("country_code") == undefined ? "" : $('#txtCountryCode').data("country_code");

        if ($('#txtCountryCode').val() == "") {
            f_MessageBoxShow("Please Insert International Code..... ");
            return;
        }
        var vModel = JSON.stringify({
            country_code: vCountryCode,
            int_code: $('#txtCountryCode').val(),
            int_country:$('#txtCountryInitial').val(),
            country_name: $('#txtCountryName').val(),
            description: $('#txtCountryDesc').val()
        });

        if (vCountryCode != "") {

            $.ajax({
                url: base_url + "Country/UpdateCountry",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "Country/InsertCountry",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modCountry").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModCountryCancel').on('click', function (event) {
        f_EmptyForm();
        $("#modCountry").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modCountry').jqxWindow({ position: { x: f_PosX($('#modCountry')), y: f_PosY($('#modCountry')) } });
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
