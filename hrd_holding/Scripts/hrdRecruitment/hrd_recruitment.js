$(document).ready(function () {
    //#region INIT COMPONENT
    $("#txtIntBranch").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtBranch").jqxInput({ theme: vTheme, disabled: true, width: 300 });
    $("#txtIntCompany").jqxInput({ theme: vTheme, disabled: true, width: 50 });
    $("#txtCompany").jqxInput({ theme: vTheme, disabled: true, width: 200 });

    $("#txtNoReq").jqxInput({ theme: vTheme });
    $("#btnRequest").jqxButton({ theme: vTheme, height: 20 });

    $("#txtIdCard").jqxInput({ theme: vTheme});
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

    $("#chkDLicense").jqxCheckBox({ theme: vTheme});

    $("#txtLicClass").jqxComboBox({ theme: vTheme, width: 150, source: vCmbReligion, selectedIndex: 0 });
    $("#txtLicClass input").attr('disabled', true);

    $("#txtAddress").jqxInput({ theme: vTheme});
    $("#txtKdDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdDepartement").jqxButton({ theme: vTheme });
    $("#txtNmDepartement").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdJobTitle").jqxButton({ theme: vTheme });
    $("#txtNmJobTitle").jqxInput({ theme: vTheme, disabled: true });
    $("#txtKdStatus").jqxInput({ theme: vTheme, disabled: true });
    $("#btnKdStatus").jqxButton({ theme: vTheme });
    $("#txtNmStatus").jqxInput({ theme: vTheme, disabled: true });
});