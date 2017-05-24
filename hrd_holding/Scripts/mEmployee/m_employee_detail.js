
$(document).ready(function () {
    Form_Load("35151269069300041", 1);

    function Form_Load(pEmployeeCode, pSeqNo) {
        $.ajax({
            dataType: "json",
            url: base_url + "Employee/GetEmployeeInfoAll?EmployeeCode=" + pEmployeeCode + "&seqno=" + pSeqNo,
            success: function (dt) {
                //alert(data.empModel.employee_code);

                if (dt.empModel.employee_code == null) {
                    window.location = base_url + "employee/index";
                } else {

                    //#region Employee Detail
                    $("#txtId").val(dt.empModel.nik);
                    $("#txtId").data("employee_code", dt.empModel.employee_code);

                    //alert($("#txtId").data("employee_code"));

                    $("#txtFullName").val(dt.empModel.employee_name);
                    $("#txtNickName").val(dt.empModel.employee_nick_name);
                    $("#txtPob").val(dt.empModel.place_birth);

                    if (dt.empModel.date_birth != null) {
                        var vDob = new Date(parseInt(dt.empModel.date_birth.substr(6)));
                        $("#dtDob").jqxDateTimeInput('setDate', vDob);
                    }

                    var vGender = dt.empModel.sex;
                    $("#cmbGender").jqxComboBox({ selectedIndex: vGender });

                    var vReligion = dt.empModel.religion;
                    $("#cmbReligion").jqxComboBox({ selectedIndex: vReligion });

                    $("#txtChild").val(dt.empModel.no_of_children);
                    $("#txtAddress").val(dt.empModel.emp_address);
                    $("#txtKdDepartement").val(dt.empModel.int_department);
                    $("#txtKdDepartement").data("department_code", dt.empModel.department_code);

                    //$("#btnKdDepartement").jqxButton({ theme: vTheme });
                    $("#txtNmDepartement").val(dt.empModel.department_name);
                    $("#txtKdJobTitle").val(dt.empModel.int_title);
                    $("#txtKdJobTitle").data("title_code", dt.empModel.title_code);

                    //$("#btnKdJobTitle").jqxButton({ theme: vTheme });
                    $("#txtNmJobTitle").val(dt.empModel.title_name);
                    $("#txtKdGrade").val(dt.empModel.int_subtitle);
                    $("#txtKdGrade").data("subtitle_code", dt.empModel.subtitle_code);

                    //$("#btnKdGrade").jqxButton({ theme: vTheme });
                    $("#txtNmGrade").val(dt.empModel.subtitle_name);
                    $("#txtKdLevel").val(dt.empModel.int_level);
                    $("#txtKdLevel").data("level_code", dt.empModel.level_code);

                    //$("#btnKdLevel").jqxButton({ theme: vTheme });
                    $("#txtNmLevel").val(dt.empModel.level_name);

                    if (dt.empModel.start_working != null) {
                        var vStartWorking = new Date(parseInt(dt.empModel.start_working.substr(6)));
                        $("#dtStartWorking").jqxDateTimeInput('setDate', vStartWorking);

                        var vToday = new Date();
                        var vAge = Math.floor((vToday - vStartWorking) / (365.25 * 24 * 60 * 60 * 1000));
                        $("#txtWorkingAge").val(vAge);

                    }

                    if (dt.empModel.appointment_date != null) {
                        var vAppointment = new Date(parseInt(dt.empModel.appointment_date.substr(6)));
                        $("#dtProbation").jqxDateTimeInput('setDate', vAppointment);
                    }

                    $("#txtPhone").val(dt.empModel.phone_number);
                    $("#txtEmail").val(dt.empModel.email);
                    $("#txtKdCountry").val(dt.empModel.country_code);
                    //$("#btnKdCountry").jqxButton({ theme: vTheme });
                    $("#txtNmCountry").val(dt.empModel.country_name);
                    $("#txtNoKtp").val(dt.empModel.identity_number);
                    $("#txtLastEducation").val(dt.empModel.last_education);
                    $("#txtLastEmployment").val(dt.empModel.last_employment);
                    $("#txtKdBank").val(dt.empModel.bank_code);
                    //$("#btnKdBank").val({ theme: vTheme });
                    $("#txtNmBank").val(dt.empModel.bank_name);
                    $("#txtBankAcc").val(dt.empModel.bank_account);
                    $("#txtBankAccName").val(dt.empModel.bank_acc_name);
                    $("#txtKdStatus").val(dt.empModel.status_code);
                    //$("#btnKdStatus").jqxButton({ theme: vTheme });
                    $("#txtNmStatus").val(dt.empModel.status_name);
                    $("#txtKdAtasan").val(dt.empModel.spv_code);
                    //$("#btnkdAtasan").jqxButton({ theme: vTheme });
                    $("#txtNmAtasan").val(dt.empModel.spv_name);

                    var vFlag = false;
                    if (dt.empModel.flag_managerial == 1) { vFlag = true; }
                    $("#chkManagerial").jqxCheckBox({ checked: vFlag });

                    var vFlag_shiftable = false;
                    if (dt.empModel.flag_shiftable == 1) { vFlag = true; }
                    $("#chkSpecialLate").jqxCheckBox({ checked: vFlag_shiftable });

                    switch (dt.empModel.Flag_active) {
                        case 0:
                            $("#optNonActive").jqxRadioButton({ checked: true });
                            $("#optActive").jqxRadioButton({ checked: false });
                            $("#optProbation").jqxRadioButton({ checked: false });
                            break;
                        case 1:
                            $("#optNonActive").jqxRadioButton({ checked: false });
                            $("#optActive").jqxRadioButton({ checked: true });
                            $("#optProbation").jqxRadioButton({ checked: false });
                            break;
                        default:
                            $("#optNonActive").jqxRadioButton({ checked: false });
                            $("#optActive").jqxRadioButton({ checked: false });
                            $("#optProbation").jqxRadioButton({ checked: true });
                    }

                    //#endregion Employee Detail

                    //#region ListFamily

                    //#region+++++++++LANGSUNG TAMBAH KE TABEL++++++++
                    //+++++++++++++++++++++++++++++++++++++++++

                    //if (dt.listFamily != null && dt.listFamily.length > 0) {
                    //    for (var i = 0; i < dt.listFamily.length; i++) {
                    //        $("#tblFamily").jqxGrid('beginupdate');

                    //        var row = {};
                    //        row["employment_code"] = dt.listFamily[i].employee_code;
                    //        row["name"] = dt.listFamily[i].name;
                    //        row["nm_rel"] = dt.listFamily[i].nm_rel;
                    //        row["date_birth"] = new Date(parseInt(dt.listFamily[i].date_birth.substr(6)));
                    //        row["education"] = dt.listFamily[i].education;
                    //        row["employment"] = dt.listFamily[i].employment;

                    //        var commit = $("#tblFamily").jqxGrid('addrow', null, row);

                    //        $("#tblFamily").jqxGrid('endupdate');
                    //    }
                    //    //$("#tblFamily").jqxGrid({ source: new $.jqx.dataAdapter(source) });
                    //}

                    //+++++++++++++++++++++++++++++++++++++++++++
                    //#endregion +++++++++++++++++++++++++++++++++++++++++++

                    if (dt.listFamily != null && dt.listFamily.length > 0) {
                        f_FillTableFamily(dt.listFamily);
                    }

                    //#endregion

                    //#region ListEducation
                    if (dt.listEducation != null) {
                        if (dt.listEducation.length > 0) {
                            for (var i = 0; i < dt.listEducation.length; i++) {
                                $("#tblEducation").jqxGrid('beginupdate');

                                var row = {};
                                row["employment_code"] = dt.listEducation[i].employee_code;
                                row["seq_no"] = dt.listEducation[i].seq_no;
                                row["nm_jenjang"] = dt.listEducation[i].nm_jenjang;
                                row["school"] = dt.listEducation[i].school;
                                row["jurusan"] = dt.listEducation[i].jurusan;
                                row["city"] = dt.listEducation[i].city;
                                row["start_year"] = new Date(parseInt(dt.listEducation[i].start_year.substr(6)));
                                row["end_year"] = new Date(parseInt(dt.listEducation[i].end_year.substr(6)));

                                var commit = $("#tblEducation").jqxGrid('addrow', null, row);

                                $("#tblEducation").jqxGrid('endupdate');
                            }
                        }
                    }
                    //#endregion

                    //#region ListSkill
                    if (dt.listSkill != null && dt.listSkill.length > 0) {
                        for (var i = 0; i < dt.listSkill.length; i++) {
                            $("#tblSkill").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listSkill[i].employee_code;
                            row["seq_no"] = dt.listSkill[i].seq_no;
                            row["skill"] = dt.listSkill[i].skill;
                            row["nm_level"] = dt.listSkill[i].nm_level;
                            row["description"] = dt.listSkill[i].description;

                            var commit = $("#tblSkill").jqxGrid('addrow', null, row);

                            $("#tblSkill").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                    //#region ListExperience
                    if (dt.listExperience != null && dt.listExperience.length > 0) {
                        for (var i = 0; i < dt.listExperience.length; i++) {
                            $("#tblExperience").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listSkill[i].employee_code;
                            row["seq_no"] = dt.listExperience[i].seq_no;
                            row["company_name"] = dt.listExperience[i].company_name;
                            row["usaha"] = dt.listExperience[i].usaha;
                            row["department_name"] = dt.listExperience[i].department_name;
                            row["last_title"] = dt.listExperience[i].last_title;
                            row["start_working"] = new Date(parseInt(dt.listExperience[i].start_working.substr(6)));
                            row["end_working"] = new Date(parseInt(dt.listExperience[i].end_working.substr(6)));
                            row["last_salary"] = dt.listExperience[i].last_salary;
                            row["reason_stop_working"] = dt.listExperience[i].reason_stop_working;

                            var commit = $("#tblExperience").jqxGrid('addrow', null, row);

                            $("#tblExperience").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                    //#region ListTraining
                    if (dt.listTraining != null && dt.listTraining.length > 0) {
                        for (var i = 0; i < dt.listTraining.length; i++) {
                            $("#tblTraining").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listExperience[i].employee_code;
                            row["seq_no"] = dt.listExperience[i].seq_no;
                            row["orginizer"] = dt.listExperience[i].orginizer;
                            row["material"] = dt.listExperience[i].material;
                            row["place"] = dt.listExperience[i].place;
                            row["start_date"] = new Date(parseInt(dt.listExperience[i].start_date.substr(6)));
                            row["end_date"] = new Date(parseInt(dt.listExperience[i].end_date.substr(6)));

                            var commit = $("#tblTraining").jqxGrid('addrow', null, row);

                            $("#tblTraining").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                    //#region ListContract
                    if (dt.listContract != null && dt.listContract.length > 0) {
                        for (var i = 0; i < dt.listContract.length; i++) {
                            $("#tblContract").jqxGrid('beginupdate');

                            var row = {};
                            row["employment_code"] = dt.listContract[i].employee_code;
                            row["seq_no"] = dt.listContract[i].seq_no;
                            row["start_date"] = new Date(parseInt(dt.listContract[i].start_date.substr(6)));
                            row["end_date"] = new Date(parseInt(dt.listContract[i].end_date.substr(6)));
                            row["no_contract"] = dt.listContract[i].no_contract;
                            row["company"] = dt.listContract[i].description;
                            row["description"] = dt.listContract[i].description;

                            var commit = $("#tblContract").jqxGrid('addrow', null, row);

                            $("#tblContract").jqxGrid('endupdate');
                        }
                    }
                    //#endregion

                }
            }
        });
    }
});
