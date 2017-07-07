f_ShowLoaderModal();

var vSrcList = {
    url: base_url + "Title/GetTitleList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "title_code" },
                 { name: "int_title" },
                 { name: "title_name" },
                 { name: "description" }],
    cache: false,
    sortcolumn: 'title_code',
    sortdirection: 'desc',
    filter: function () { $("#tblTitle").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblTitle").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblTitle() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblTitle").jqxGrid(
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
          columns: [{ text: "Title code", dataField: "title_code", hidden: true },
                    { text: "Code", dataField: "int_title", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Title Name", dataField: "title_name", align: 'center' },
                    { text: "Description", dataField: "description", width: 300, align: 'center' }
          ]
      });
}


function f_EmptyForm() {
    $("#txtTitleCode").val("");
    $("#txtTitleCode").data("title_code", "");
    $("#txtTitleName").val("");
    $("#txtTitleDesc").val("");
}

function f_DeleteTitle(pTitleCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblTitle").jqxGrid('selectedrowindex');
    var vTitleCode = $('#tblTitle').jqxGrid('getcellvalue', selectedRowIndex, "title_code");


    if (vSeqNo > 0) {
        $.ajax({
            url: base_url + "Title/DeleteTitle",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pTitleCode: vTitleCode }),
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
    vSrcList.url = base_url + "/Title/GetTitleList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblTitle').jqxGrid({ source: vAdapter })
    $('#tblTitle').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data

    $("#txtTitleCode").jqxInput({ theme: vTheme });
    $("#txtTitleName").jqxInput({ theme: vTheme });
    $('#txtTitleDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModTitleSave").jqxButton({ theme: vTheme });
    $("#btnModTitleCancel").jqxButton({ theme: vTheme });

    $("#notifTitle").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarTitle").jqxToolBar({
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
                                        "<img style='vertical-align:middle' src='"+base_url+"/content/images/Refresh_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>RELOAD</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_ReloadData();
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='"+base_url+"/content/images/edit property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>EDIT</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm();

                        var rowindex = $('#tblTitle').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblTitle').jqxGrid('getrowdata', rowindex);

                            //$("#txtCompCode").jqxInput({ disabled:true });
                            $("#txtTitleCode").data("title_code", rd.title_code);
                            $("#txtTitleCode").val(rd.int_title);
                            $("#txtTitleName").val(rd.title_name);
                            $("#txtTitleDesc").val(rd.description);

                            $("#modTitle").jqxWindow('open');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='"+base_url+"/content/images/add property_24_grey.png'/>" +
                                        "<span style='margin-left:5px'>NEW</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_EmptyForm()
                        $("#modTitle").jqxWindow('open');

                    });
                    break;

            }
        }
    });

    initTblTitle();


    $("#modTitle").jqxWindow({
        height: 250, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModTitleSave').on('click', function (event) {

        var vTitleCode = $('#txtTitleCode').data("title_code") == undefined ? "" : $('#txtTitleCode').data("title_code");

        if ($('#txtTitleCode').val() == "") {
            f_MessageBoxShow("Please Insert International Code..... ");
            return;
        }
        var vModel = JSON.stringify({
            title_code: vTitleCode,
            int_title: $('#txtTitleCode').val(),
            title_name: $('#txtTitleName').val(),
            description: $('#txtTitleDesc').val()
        });

        if (vTitleCode != "") {

            $.ajax({
                url: base_url + "Title/UpdateTitle",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modTitle").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpSave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        } else {
            $.ajax({
                url: base_url + "Title/InsertTitle",
                type: "POST",
                contentType: "application/json",
                data: vModel,
                success: function (d) {
                    var isOke = d.vResp['isValid'];

                    if (isOke) {
                        f_ReloadData();
                        $("#modTitle").jqxWindow('close');
                    } else {
                        f_MessageBoxShow(d.vResp['message']);
                    }
                    //$('#btnExpave').jqxButton({ disabled: false });
                    f_HideLoaderModal();
                }
            });
        }
    });

    $('#btnModTitleCancel').on('click', function (event) {
        f_EmptyForm();
        $("#modTitle").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modTitle').jqxWindow({ position: { x: f_PosX($('#modTitle')), y: f_PosY($('#modTitle')) } });
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
