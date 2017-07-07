f_ShowLoaderModal();


var SrcJobTitleLookUp = {
    datatype: "json",
    type: "Post",
    datafields: [{ name: "title_code" },
                 { name: "int_title" },
                 { name: "title_name" }],
    cache: false,
    filter: function () { $("#tblJobTitleLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblJobTitleLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcJobTitleLookUp.totalrecords = data["TotalRows"]; },
    sortcolumn: "job_code",
    root: 'Rows'
}

function initGridJobTitleLookUp() {
    $("#tblJobTitleLookUp").jqxGrid(
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
              { text: 'title code', dataField: 'title_code', hidden:true },
              { text: 'Code', dataField: 'int_title', width: 90 },
              { text: 'Name', dataField: 'title_name'}
          ]
      });
}

var vSrcList = {
    url: base_url + "/SubTitle/GetSubTitleList",
    datatype: "json",
    type: "Post",
    datafields: [{ name: "subtitle_code" },
                 { name: "int_subtitle" },
                 { name: "subtitle_name" },
                 { name: "title_code" },
                 { name: "int_title" },
                 { name: "title_name" },
                 { name: "description" }],
    cache: false,
    filter: function () { $("#tblSubTitle").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblSubTitle").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { vSrcList.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initTblSubTitle() {
    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });

    $("#tblSubTitle").jqxGrid(
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
          columns: [{ text: "SubTitle Code", dataField: "subtitle_code", hidden: true, },
                    { text: "Code", dataField: "int_subtitle", width: 90, cellsalign: 'center', align: 'center' },
                    { text: "Name", dataField: "subtitle_name", width: 270, align: 'center' },
                    { text: "title code", dataField: "title_code", hidden: true, },
                    { text: "Int Title", dataField: "int_title", hidden: true, },
                    { text: "Title Name", dataField: "title_name", width: 300, align: 'center' },
                    { text: "Description", dataField: "description", align: 'center' }]
      });
}


function f_EmptyForm() {
    $("#txtSubTitleCode").val("");
    $("#txtSubTitleCode").data("subtitle_code","");
    $("#txtSubTitleName").val("");
    $("#txtJobTitleCode").data("title_code", "");
    $("#txtJobTitleCode").val("");
    $("#txtJobTitleName").val("");
    $("#txtSubTitleDesc").val("");
}

function f_DeleteSubTitle(pCode) {
    $("#modYesNo").jqxWindow('close');
    f_ShowLoaderModal();

    var selectedRowIndex = $("#tblSubTitle").jqxGrid('selectedrowindex');
    var vSubTitleCode = $('#tblSubTitle').jqxGrid('getcellvalue', selectedRowIndex, "subtitle_code");


    if (vSubTitleCode > 0) {
        $.ajax({
            url: base_url + "SubTitle/DeleteSubTitle",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ pSubTitleCode: vSubTitleCode }),
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
    vSrcList.url = base_url + "/SubTitle/GetSubTitleList";

    var vAdapter = new $.jqx.dataAdapter(vSrcList, {
        downloadComplete: function (data, status, xhr) {
            if (!vSrcList.TotalRows) {
                vSrcList.TotalRows = data.length;
            }
        }
    });
    $('#tblSubTitle').jqxGrid({ source: vAdapter })
    $('#tblSubTitle').jqxGrid('gotopage', 0);
}

$(document).ready(function () {
    // prepare the data

    $("#txtSubTitleCode").jqxInput({ theme: vTheme, width: 70 });
    $("#txtSubTitleName").jqxInput({ theme: vTheme, width: 300 });

    $("#txtJobTitleCode").jqxInput({ theme: vTheme, width: 70,disabled:true });
    $("#btnJobTitle").jqxButton({ theme: vTheme });
    $("#txtJobTitleName").jqxInput({ theme: vTheme, width: 300 });
    $('#txtSubTitleDesc').jqxTextArea({
        theme: vTheme, placeHolder: 'Masukkan Keterangan',
        height: 50, width: 300, minLength: 1
    });

    $("#btnModSubTitleSave").jqxButton({ theme: vTheme });
    $("#btnModSubTitleCancel").jqxButton({ theme: vTheme });

    $("#notifSubTitle").jqxNotification({
        width: "100%", height: "40px", theme: vTheme,
        appendContainer: "#notifContainer",
        opacity: 0.9, autoClose: true, template: "error"
    });

    $("#toolBarSubTitle").jqxToolBar({
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

                        var rowindex = $('#tblSubTitle').jqxGrid('getselectedrowindex');

                        if (rowindex >= 0) {
                            var rd = $('#tblSubTitle').jqxGrid('getrowdata', rowindex);

                            $("#txtSubTitleCode").val(rd.int_subtitle);
                            $("#txtSubTitleCode").data("subtitle_code", rd.subtitle_code);
                            $("#txtSubTitleName").val(rd.subtitle_name);

                            $("#txtJobTitleCode").val(rd.int_title);
                            $("#txtJobTitleCode").data("title_code",rd.title_code);
                            $("#txtJobTitleName").val(rd.title_name);

                            $("#txtSubTitleDesc").val(rd.description);

                            $("#modSubTitle").jqxWindow('open');
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
                        $("#modSubTitle").jqxWindow('open');

                    });
                    break;

            }
        }
    });


    initGridJobTitleLookUp();
    initTblSubTitle();

    $("#JobTitleLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == 'button') {
                tool.height("25px")
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='"+base_url+"/content/images/Checked_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Select Data</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("100px");
                    tool.on("click", function () {
                        var rowindex = $('#tblJobTitleLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblJobTitleLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtJobTitleCode").val(rd.int_title);
                            $("#txtJobTitleCode").data("title_code",rd.title_code);
                            $("#txtJobTitleName").val(rd.title_name);

                            $("#modJobTitleLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='"+base_url+"/content/images/exit_16_grey.png'/>" +
                                        "<span style='margin-left:5px'>Cancel</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.width("80px");
                    tool.on("click", function () {
                        $("#modJobTitleLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    $('#btnJobTitle').on('click', function (event) {
        SrcJobTitleLookUp.url = base_url + "/Title/GetTitleList";

        var vAdapter = new $.jqx.dataAdapter(SrcJobTitleLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcJobTitleLookUp.TotalRows) {
                    SrcJobTitleLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblJobTitleLookUp').jqxGrid({ source: vAdapter })
        $('#tblJobTitleLookUp').jqxGrid('gotopage', 0);
        $("#modJobTitleLookUp").jqxWindow('open');

    });

    $("#modJobTitleLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });


    $("#modSubTitle").jqxWindow({
        height: 300, width: 600,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnModSubTitleSave').on('click', function (event) {

        if ($('#txtJobTitleCode').val() == "") {
            f_MessageBoxShow("Please Select Title...");
            return;
        }

        if ($('#txtSubTitleCode').val() == "" || $('#txtSubTitleName').val() == "") {
            f_MessageBoxShow("Please Insert Status Code And Status Name...");
            return;
        }

        $("#modSubTitle").jqxWindow('close');

        var vSubTitleCode = $('#txtSubTitleCode').data("subtitle_code") == undefined ? "" : $('#txtSubTitleCode').data("subtitle_code");
        
        var vModel = JSON.stringify({
            subtitle_code: vSubTitleCode,
            int_subtitle: $('#txtSubTitleCode').val(),
            subtitle_name: $('#txtSubTitleName').val(),
            title_code: $('#txtJobTitleCode').data("title_code"),
            description: $('#txtSubTitleDesc').val()
        });

        if (vSubTitleCode != "") {

            $.ajax({
                url: base_url + "SubTitle/UpdateSubTitle",
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
                url: base_url + "SubTitle/InsertSubTitle",
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

    $('#btnModSubTitleCancel').on('click', function (event) {
        $("#modSubTitle").jqxWindow('close');
    });

    function f_PosisiModalDialog() {
        $('#modJobTitleLookUp').jqxWindow({ position: { x: f_PosX($('#modJobTitleLookUp')), y: f_PosY($('#modJobTitleLookUp')) } });
        $('#modSubTitle').jqxWindow({ position: { x: f_PosX($('#modSubTitle')), y: f_PosY($('#modSubTitle')) } });
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
