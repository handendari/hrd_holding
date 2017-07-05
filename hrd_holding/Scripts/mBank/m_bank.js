f_ShowLoaderModal();

var vSrcList = {
    url: base_url + "/Bank/GetBankList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "bank_code" },
                 { name: "bank_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblBank").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblBank").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblBank() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblBank").jqxGrid(
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
          columns: [{ text: "Bank Code", dataField: "bank_code", width: 100, cellsalign: 'center', align: 'center' },
                    { text: "Bank Name", dataField: "bank_name", align: 'center' },
                    { text: "Description", dataField: "description", align: 'center' }
          ]
      });
}


function f_EmptyForm() {
    $("#txtBankCode").jqxInput({ disabled: false });
    $("#txtBankCode").val("");
    $("#txtBankCode").data("bank_code","");
    $("#txtBankName").val("");
    $("#txtBankDesc").val("");
}

function f_DeleteBank(pBankCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblBank").jqxGrid('selectedrowindex');
    var vLevelCode = $('#tblBank').jqxGrid('getcellvalue', selectedRowIndex, "bank_code");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "Bank/DeleteBank",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pLevelCode: vLevelCode }),
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
    vSrcList.url = base_url + "/Bank/GetBankList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblBank').jqxGrid({ source: vAdapter })
    $('#tblBank').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data

    $("#txtBankCode").jqxInput({ theme: vTheme });
    $("#txtBankName").jqxInput({ theme: vTheme });
    $('#txtBankDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModBankSave").jqxButton({ theme: vTheme });
    $("#btnModBankCancel").jqxButton({ theme: vTheme });

    $("#notifBank").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarBank").jqxToolBar({
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

                        var rowindex = $('#tblBank').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblBank').jqxGrid('getrowdata', rowindex);

                            $("#txtBankCode").jqxInput({ disabled: true }); 

                            $("#txtBankCode").val(rd.bank_code);
                            $("#txtBankCode").data("bank_code",rd.bank_code);
                            $("#txtBankName").val(rd.bank_name);
                            $("#txtBankDesc").val(rd.description);

                            $("#modBank").jqxWindow('open');
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
                        $("#modBank").jqxWindow('open');

                    });
                    break;

            }
        }
    });

    initTblBank();


    $("#modBank").jqxWindow({
        height: 250, width: 500,
        theme: vTheme,
        isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModBankSave').on('click', function (event) {

        var vBankCode = $('#txtBankCode').data("bank_code") == undefined ? "" : $('#txtBankCode').data("bank_code");

        if ($('#txtBankCode').val() == "") {
            f_MessageBoxShow("Please Insert Bank Code..... ");
            return;
        }
        var vModel = JSON.stringify({
            bank_code: $('#txtBankCode').val(),
            bank_name: $('#txtBankName').val(),
            description: $('#txtBankDesc').val()
        });

        if (vBankCode != "") {

            $.ajax({
                url: base_url + "Bank/UpdateBank",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modBank").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "Bank/InsertBank",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modBank").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModBankCancel').on('click', function (event) {
        $("#modBank").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modBank').jqxWindow({ position: { x: f_PosX($('#modBank')), y: f_PosY($('#modBank')) } });
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
