f_ShowLoaderModal();

var vSrcList = {
    url: base_url + "/Level/GetLevelList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "level_code" },
                 { name: "int_level" },
                 { name: "level_name" },
                 { name: "description" },
                 { name: "entry_date" },
                 { name: "entry_user" },
                 { name: "edit_date" },
                 { name: "edit_user" }],
    cache: false,
    filter: function () { $("#tblLevel").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblLevel").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblLevel() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblLevel").jqxGrid(
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
          columns: [{ text: "level code", dataField: "level_code", hidden: true },
                    { text: "Code", dataField: "int_level", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Level Name", dataField: "level_name", align: 'center' },
                    { text: "Description", dataField: "description", width: 300, align: 'center' },
                    { text: "entry_date", dataField: "entry_date", hidden: true },
                    { text: "entry_user", dataField: "entry_user", hidden: true },
                    { text: "edit_date", dataField: "edit_date", hidden: true },
                    { text: "edit_user", dataField: "edit_user", hidden: true }
          ]
      });
}


function f_EmptyForm() {
    $("#txtLevelCode").val("");
    $("#txtLevelCode").data("level_code", "");
    $("#txtLevelName").val("");
    $("#txtLevelDesc").val("");
}

function f_DeleteLevel(pLevelCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblLevel").jqxGrid('selectedrowindex');
    var vLevelCode = $('#tblLevel').jqxGrid('getcellvalue', selectedRowIndex, "level_code");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "Level/DeleteLevel",
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
    vSrcList.url = base_url + "/Level/GetLevelList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblLevel').jqxGrid({ source: vAdapter })
    $('#tblLevel').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data

    $("#txtLevelCode").jqxInput({ theme: vTheme });
    $("#txtLevelName").jqxInput({ theme: vTheme });
    $('#txtLevelDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModLevelSave").jqxButton({ theme: vTheme });
    $("#btnModLevelCancel").jqxButton({ theme: vTheme });

    $("#notifLevel").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarLevel").jqxToolBar({
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

                        var rowindex = $('#tblLevel').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblLevel').jqxGrid('getrowdata', rowindex);

                            //$("#txtCompCode").jqxInput({ disabled:true });
                            $("#txtLevelCode").data("level_code",rd.level_code);
                            $("#txtLevelCode").val(rd.int_level);
                            $("#txtLevelName").val(rd.level_name);
                            $("#txtLevelDesc").val(rd.description);

                            $("#modLevel").jqxWindow('open');
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
                        $("#modLevel").jqxWindow('open');

                    });
                    break;

            }
        }
    });

    initTblLevel();


    $("#modLevel").jqxWindow({
        height: 250, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModLevelSave').on('click', function (event) {

        var vLevelCode = $('#txtLevelCode').data("level_code") == undefined ? "" : $('#txtLevelCode').data("level_code");

        if ($('#txtLevelCode').val() == "") {
            f_MessageBoxShow("Please Insert International Code..... ");
            return;
        }
        var vModel = JSON.stringify({
            level_code: vLevelCode,
            int_level: $('#txtLevelCode').val(),
            level_name: $('#txtLevelName').val(),
            description: $('#txtLevelDesc').val()
        });

        if (vLevelCode != "") {

            $.ajax({
                url: base_url + "Level/UpdateLevel",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modLevel").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "Level/InsertLevel",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modLevel").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModLevelCancel').on('click', function (event) {
        $("#modLevel").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modLevel').jqxWindow({ position: { x: f_PosX($('#modLevel')), y: f_PosY($('#modLevel')) } });
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
