f_ShowLoaderModal();

var vSrcPriodStatus = [
    "NON PERIOD",
    "PERIOD"
];

var SrcPajakLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "kode_pajak" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblPajakLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblPajakLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcPajakLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "kode_pajak",
    root: 'Rows'
}

function initGridPajakLookUp() {
    $("#tblPajakLookUp").jqxGrid(
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
              { text: 'Code', dataField: 'kode_pajak', width: 80 },
              { text: 'Name', dataField: 'description'}
          ]
      });
}

var vSrcList = {
    url: base_url + "/Status/GetStatusList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "status_code" },
                 { name: "int_status" },
                 { name: "status_name" },
                 { name: "flag_period" },
                 { name: "kode_pajak" },
                 { name: "nama_pajak" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblStatus").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblStatus").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblStatus() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblStatus").jqxGrid(
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
          columns: [{ text: "status Code", dataField: "status_code", hidden: true, },
                    { text: "Code", dataField: "int_status", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Name", dataField: "status_name", width: 270, align: 'center' },
                    { text: "Flag Period", dataField: "flag_period", hidden: true, },
                    { text: "Kode Pajak", dataField: "kode_pajak", hidden: true, },
                    { text: "Pajak Desc.", dataField: "nama_pajak", width: 300, align: 'center' },
                    { text: "Description", dataField: "description", align: 'center' }]
      });
}


function f_EmptyForm() {
    $("#txtStatusCode").val("");
    $("#txtStatusCode").data("status_code", "");
    $("#txtStatusName").val("");
    $("#txtStatusPjkCode").val("");
    $("#txtStatusPjkName").val("");
    $("#txtStatusPeriod").val("");
}

function f_DeleteStatus(pStatusCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblStatus").jqxGrid('selectedrowindex');
    var vStatusCode = $('#tblStatus').jqxGrid('getcellvalue', selectedRowIndex, "status_code");


    if (vStatusCode > 0) {
        $.ajax({
            url: base_url + "Status/DeleteStatus",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pStatusCode: vStatusCode }),
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
    vSrcList.url = base_url + "/Status/GetStatusList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblStatus').jqxGrid({ source: vAdapter })
    $('#tblStatus').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data

    $("#txtStatusCode").jqxInput({ theme: vTheme, width: 70 });
    $("#txtStatusName").jqxInput({ theme: vTheme, width: 300 });
    $("#cmbStatusPeriod").jqxComboBox({
        source: vSrcPriodStatus,
        width: '200px',
        theme: vTheme
    });

    $("#txtStatusPjkCode").jqxInput({ theme: vTheme, width: 70,disabled:true });
    $("#btnStatusPjk").jqxButton({ theme: vTheme });
    $("#txtStatusPjkName").jqxInput({ theme: vTheme, width: 300 });
    $('#txtStatusDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModStatusSave").jqxButton({ theme: vTheme });
    $("#btnModStatusCancel").jqxButton({ theme: vTheme });

    $("#notifStatus").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarStatus").jqxToolBar({
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

                        var rowindex = $('#tblStatus').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblStatus').jqxGrid('getrowdata', rowindex);

                            $("#txtStatusCode").val(rd.int_status);
                            $("#txtStatusCode").data("status_code", rd.status_code);
                            $("#txtStatusName").val(rd.status_name);
                            var vFlagStatus = rd.flag_period;
                            $("#cmbStatusPeriod").jqxComboBox({ selectedIndex: vFlagStatus });
                            $("#txtStatusPjkCode").val(rd.kode_pajak);
                            $("#txtStatusPjkName").val(rd.nama_pajak);
                            $("#txtStatusDesc").val(rd.description);

                            $("#modStatus").jqxWindow('open');
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
                        $("#modStatus").jqxWindow('open');

                    });
                    break;

            }
        }
    });


    initGridPajakLookUp();
    initTblStatus();

    $("#PajakLookUpToolBar").jqxToolBar({
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
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblPajakLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblPajakLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtStatusPjkCode").val(rd.kode_pajak);

                            $("#txtStatusPjkName").val(rd.description);
                            $("#modPajakLookUp").jqxWindow('close');
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
                        $("#modPajakLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnStatusPjk').on('click', function (event) {
        SrcPajakLookUp.url = base_url + "/KodePajak/GetKodePajakList";

        var vAdapter = new $.jqx.dataAdapter(SrcPajakLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcPajakLookUp.TotalRows) {
                    SrcPajakLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblPajakLookUp').jqxGrid({ source: vAdapter })
        $('#tblPajakLookUp').jqxGrid('gotopage', 0);
        $("#modPajakLookUp").jqxWindow('open');

    });

    $("#modPajakLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });


    $("#modStatus").jqxWindow({
        height: 300, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModStatusSave').on('click', function (event) {
        if ($('#cmbStatusPeriod').jqxComboBox('selectedIndex') < 0) {
            f_MessageBoxShow("Please Select Period Status...");
            return;
        }

        if ($('#txtStatusPjkCode').val() == "") {
            f_MessageBoxShow("Please Select Kode Pajak...");
            return;
        }

        if ($('#txtStatusCode').val() == "" || $('#txtStatusName').val() == "") {
            f_MessageBoxShow("Please Insert Status Code And Status Name...");
            return;
        }

        $("#modStatus").jqxWindow('close');

        var vStatusCode = $('#txtStatusCode').data("status_code") == undefined ? "" : $('#txtStatusCode').data("status_code");

        var vModel = JSON.stringify({
            status_code: vStatusCode,
            int_status: $('#txtStatusCode').val(),
            status_name: $('#txtStatusName').val(),
            flag_period: $('#cmbStatusPeriod').jqxComboBox('selectedIndex'),
            kode_pajak: $('#txtStatusPjkCode').val(),
            description: $('#txtStatusDesc').val()
        });

        if (vStatusCode != "") {

            $.ajax({
                url: base_url + "Status/UpdateStatus",
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
                url: base_url + "Status/InsertStatus",
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
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModStatusCancel').on('click', function (event) {
        $("#modStatus").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modPajakLookUp').jqxWindow({ position: { x: f_PosX($('#modPajakLookUp')), y: f_PosY($('#modPajakLookUp')) } });
        $('#modStatus').jqxWindow({ position: { x: f_PosX($('#modStatus')), y: f_PosY($('#modStatus')) } });
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
