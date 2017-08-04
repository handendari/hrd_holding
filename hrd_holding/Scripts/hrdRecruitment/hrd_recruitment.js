var vTab = "";

var SrcRequestLookUp = {
    //url: vUrlCountry,
    datatype: "json",
    type: "Post",
    datafields: [{ name: "id" },
                 { name: "no_req" },
                 { name: "position_need" },
                 { name: "reason" }],
    cache: false,
    filter: function () { $("#tblRequestLookUp").jqxGrid('updatebounddata', 'filter'); },
    sort: function () { $("#tblRequestLookUp").jqxGrid('updatebounddata', 'sort'); },
    beforeprocessing: function (data) { SrcRequestLookUp.totalrecords = data["TotalRows"]; },
    root: 'Rows'
}

function initGridRequestLookUp() {
    $("#tblRequestLookUp").jqxGrid(
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
              { text: 'Id', dataField: 'id', hidden: true },
              { text: 'No Req', dataField: 'no_req', cellsalign: 'center', align: 'center' },
              { text: 'Position', dataField: 'position_need', align: 'center' },
              { text: 'Reason', dataField: 'reason', align: 'center' }
          ]
      });
}

function Form_Load(pBranchId, pRecId) {
    $.ajax({
        dataType: "json",
        url: base_url + "hrdRecruitment/GetRecruitmentInfoAll",
        data: { 'pBranchId': pBranchId, 'pRecId': pRecId },
        success: function (dt) {

            $("#txtIntCompany").val(dt.recModel.int_company);
            $("#txtIntCompany").data("company_code", dt.recModel.company_code);
            $("#txtCompanyName").val(dt.recModel.company_name);

            $("#txtIntBranch").val(dt.recModel.int_branch);
            $("#txtIntBranch").data("branch_code", dt.recModel.branch_code);
            $("#txtBranchName").val(dt.recModel.branch_name);

            $("#txtIdCard").data("recruitment_id", dt.recModel.id);

            if (dt.recModel.date_birth != null) {
                var vTglReq = new Date(parseInt(dt.recModel.date_birth.substr(6)));
                $("#dtDob").jqxDateTimeInput('setDate', vTglReq);
            }

            var vGender = dt.recModel.flag_gender;
            $("#cmbGender").jqxComboBox({ selectedIndex: vGender });

            var vMarital = dt.recModel.flag_marital_status;
            $("#cmbMarital").jqxComboBox({ selectedIndex: vMarital });

            var vReligion = dt.recModel.flag_religion;
            $("#cmbReligion").jqxComboBox({ selectedIndex: vReligion });

            var vLicense = dt.recModel.flag_license;
            $("#cmbLicClass").jqxComboBox({ selectedIndex: vLicense });

            f_HideLoaderModal();
        }
    });
}

$(document).ready(function () {
    //#region INIT COMPONENT
    $("#txtIntBranch").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtBranchName").jqxInput({ theme: vTheme, disabled: true, width: 300 });
    $("#txtIntCompany").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtCompanyName").jqxInput({ theme: vTheme, disabled: true, width: 200 });

    $("#txtNoReq").jqxInput({ theme: vTheme, disabled: true });
    $("#btnRequest").jqxButton({ theme: vTheme, height: 20 });

    $("#txtIdCard").jqxInput({ theme: vTheme });
    $("#txtFullName").jqxInput({ theme: vTheme, width: 250 });
    $("#txtPob").jqxInput({ theme: vTheme });
    $("#dtDob").jqxDateTimeInput({ theme: vTheme, width: 150 });

    $("#cmbGender").jqxComboBox({ theme: vTheme, width: 150, source: vCmbGender, selectedIndex: 0 });
    $("#cmbGender input").attr('disabled', true);

    $("#cmbMarital").jqxComboBox({ theme: vTheme, width: 150, source: vCmbMarital, selectedIndex: 0 });
    $("#cmbMarital input").attr('disabled', true);

    $("#cmbReligion").jqxComboBox({ theme: vTheme, width: 150, source: vCmbReligion, selectedIndex: 0 });
    $("#cmbReligion input").attr('disabled', true);

    $("#txtTelepon").jqxInput({ theme: vTheme, width: 300 });
    $("#txtDialect").jqxInput({ theme: vTheme, width: 300 });

    $("#chkDLicense").jqxCheckBox({ theme: vTheme });

    $("#cmbLicClass").jqxComboBox({ theme: vTheme, width: 150, source: vCmbReligion, selectedIndex: 0 });
    $("#cmbLicClass input").attr('disabled', true);

    $("#txtAddress").jqxInput({ theme: vTheme });
    $("#txtKdDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdDepartement").jqxButton({ theme: vTheme });
    $("#txtNmDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdJobTitle").jqxButton({ theme: vTheme });
    $("#txtNmJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdStatus").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdStatus").jqxButton({ theme: vTheme });
    $("#txtNmStatus").jqxInput({ theme: vTheme, disabled: true });

    $('#jqxTabs').jqxTabs({
        width: '100%', height: 300, theme: vTheme,
        position: 'top', selectionTracker: 1, animationType: 'fade'
    });

    $("#RecToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button | button', rtl: true,
        initTools: function (type, index, tool, menuToolIninitialization) {
            if (type == "button") {
                tool.height("25px");
                tool.width("110px");
            }
            switch (index) {
                case 0:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Submit Resume_24_grey.png'/>" +
                                        "<span>ACTIVATE</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_MessageBoxShow("Karyawan di aktifkan...");
                    });
                    break;
                case 1:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/Save as_24_grey.png'/>" +
                                        "<span>SAVE DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_InsertEmployee();
                    });
                    break;
                case 2:
                    var button = $("<div>" +
                                        "<img style='vertical-align:middle' src='" + base_url + "/content/images/add property_24_grey.png'/>" +
                                        "<span>NEW DATA</span> " +
                                   "</div>");
                    tool.append(button);
                    tool.on("click", function () {
                        f_empty_employee_form();
                    });
                    break;

            }
        }
    });

    initGridRequestLookUp();

    $('#btnRequest').on('click', function (event) {
        var vCompanyId = $('#txtIntCompany').data("company_code");
        var vBranchId = $('#txtIntBranch').data("branch_code");

        SrcRequestLookUp.url = base_url + "/ReqRecruitment/GetRequestLookUp?pCompanyId=" + vCompanyId + "&pBranchId=" + vBranchId;

        var vAdapter = new $.jqx.dataAdapter(SrcRequestLookUp, {
            downloadComplete: function (data, status, xhr) {
                if (!SrcRequestLookUp.TotalRows) {
                    SrcRequestLookUp.TotalRows = data.length;
                }
            }
        });

        $('#tblRequestLookUp').jqxGrid({ source: vAdapter })
        $('#tblRequestLookUp').jqxGrid('gotopage', 0);
        $("#modRequestLookUp").jqxWindow('open');
    });

    $("#modYesNo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false,
        modalZIndex: 999
    });


    $("#modRequestLookUp").jqxWindow({
        height: 500, width: 430,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $("#ReqLookUpToolBar").jqxToolBar({
        theme: vTheme,
        width: '100%', height: 35, tools: 'button | button',
        initTools: function (type, index, tool, menuToolIninitialization) {
            switch (index) {
                case 0:
                    tool.text("Select Data"); tool.height("25px"); tool.width("80px");
                    tool.on("click", function () {
                        var rowindex = $('#tblRequestLookUp').jqxGrid('getselectedrowindex');
                        if (rowindex >= 0) {
                            var rd = $('#tblRequestLookUp').jqxGrid('getrowdata', rowindex);
                            $("#txtNoReq").val(rd.no_req);
                            $("#txtNoReq").data("request_id", rd.id);

                            f_UpdateTblFamily();
                            f_UpdateTblEducation();
                            f_UpdateTblSkill();
                            f_UpdateTblExperience();

                            $("#modRequestLookUp").jqxWindow('close');
                        } else {
                            f_MessageBoxShow("Please Select Data...");
                        }
                    });
                    break;
                case 1:
                    tool.text("Cancel"); tool.height("25px"); tool.width("50px");
                    tool.on("click", function () {
                        $("#modCountryLookUp").jqxWindow('close');
                    });
                    break;
            }
        }
    });

    var vBranch = $.urlParam('pBranchId');
    var vIdRec = $.urlParam('pRecId') == "" ? 0 : $.urlParam('pRecId');

    Form_Load(vBranch, vIdRec);


    $("#btnYes").jqxButton({ theme: vTheme, height: 30, width: 60 });
    $("#btnNo").jqxButton({ theme: vTheme, height: 30, width: 60 });

    $('#btnYes').on('click', function (event) {
//        var vRequestId = $("#txtNoReq").data("request_id");

        var selectedTab = $('#jqxTabs').jqxTabs('selectedItem');

        switch (selectedTab) {
            case 0:
                f_DeleteRecruitmentFamily();
                break;
            case 1:
                f_DeleteEducation();
                break;
            case 2:
                f_DeleteSkill();
                break;
            case 3:
                f_DeleteExp();
                break;
            case 4:
                //f_DeleteEmployeeTrn(vEmpCode);
                break;
        }
    });

    $('#btnNo').on('click', function (event) {
        $("#modYesNo").jqxWindow("close");
    });
});