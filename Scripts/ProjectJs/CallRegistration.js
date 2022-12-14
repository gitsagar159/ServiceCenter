
/*

$("#Customer").select2({
    minimumInputLength: 3,
    tags: [],
    ajax: {
        url: "/Job/GetCustomerCodeList",
        dataType: 'json',
        type: "GET",
        quietMillis: 50,
        data: function (term) {
            return {
                term: term
            };
        },
        results: function (data) {
            return {
                results: $.map(data, function (item) {
                    return {
                        text: item.completeName,
                        slug: item.slug,
                        id: item.id
                    }
                })
            };
        }
    }
});

*/


$(function () {
    $('#CallAssignDate').on('change', function (e) {

        $("#CallDate").val($(this).val());
    });
});


function LoadCustomerCodeDropDown() {

    $('#Customer').select2({
        placeholder: "Customer code",
        minimumInputLength: 6,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetCustomerCodeList",
            dataType: 'json',
            type: 'Get',
            data: function (params) {
                var query = {
                    match: params.term,
                    page: params.page || 1,
                    pageSize: params.pageSize || 5
                }
                return query;
            },
            processResults: function (data, params) {
                console.log(params);
                return {
                    results: data.items,
                    page: params.page,
                    pagination: {
                        more: (params.page * 5) < data.total_count
                    }
                }
            },
        },
    });
}

$(function () {

    $('#ItemName').select2({
        placeholder: "Item Name",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetItemList",
            dataType: 'json',
            type: 'Get',
            data: function (params) {
                var query = {
                    match: params.term,
                    page: params.page || 1,
                    pageSize: params.pageSize || 5
                }
                return query;
            },
            processResults: function (data, params) {
                console.log(params);
                return {
                    results: data.items,
                    page: params.page,
                    pagination: {
                        more: (params.page * 5) < data.total_count
                    }
                }
            },
        },
    });


    $('#CityName').select2({
        dropdownParent: $('#customer_modal'),
        placeholder: "City Name",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetCityList",
            dataType: 'json',
            type: 'Get',
            data: function (params) {
                var query = {
                    match: params.term,
                    page: params.page || 1,
                    pageSize: params.pageSize || 5
                }
                return query;
            },
            processResults: function (data, params) {
                console.log(params);
                return {
                    results: data.items,
                    page: params.page,
                    pagination: {
                        more: (params.page * 5) < data.total_count
                    }
                }
            },
        },
    });


    $('#Technician').select2({
        placeholder: "Technician Name",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetTechnicianList",
            dataType: 'json',
            type: 'Get',
            data: function (params) {
                var query = {
                    match: params.term,
                    page: params.page || 1,
                    pageSize: params.pageSize || 5
                }
                return query;
            },
            processResults: function (data, params) {
                console.log(params);
                return {
                    results: data.items,
                    page: params.page,
                    pagination: {
                        more: (params.page * 5) < data.total_count
                    }
                }
            },
        },
    });

    $('#FaultType').select2({
        placeholder: "Fault Type",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetFaultTypeList",
            dataType: 'json',
            type: 'Get',
            data: function (params) {
                var query = {
                    match: params.term,
                    page: params.page || 1,
                    pageSize: params.pageSize || 5
                }
                return query;
            },
            processResults: function (data, params) {
                console.log(params);
                return {
                    results: data.items,
                    page: params.page,
                    pagination: {
                        more: (params.page * 5) < data.total_count
                    }
                }
            },
        },
    });


})


$(function () {
    $('#Customer').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var customerId = this.value;

        console.log(customerId);
        LoadCustomerDetailsByCustomerId(customerId);
    });

    $('#CallType').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var callTypeId = this.value;
        var serviceTypeId = $('#ServType').val();

        GetGeneratedJobNo(serviceTypeId, callTypeId);
    });

    $('#ServType').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var servTypeId = this.value;
        var callTypeId = $('#CallType').val();

        GetGeneratedJobNo(servTypeId, callTypeId);
    });
});

function LoadCustomerDetailsByCustomerId(customerId) {
    $.ajax({
        type: "POST",
        url: "/Job/GetCustomerDetailsByCustomerId",
        data: { CustomerId: customerId },
        cache: false,
        success: function (data) {
            if (data !== null) {
                $("#CustomerName").val(data.FirstName + " " + data.LastName);
                $("#Address").val(data.Address);
                $("#MobileNo").val(data.MobileNo);
            }

        }
    });
}

function GetGeneratedJobNo(serviceTypeId, callTypeId) {

    var LatestJobIdNo = 1;

    $.ajax({
        type: "POST",
        url: "/Job/GetLatestIdForJobNo",
        data: {},
        cache: false,
        success: function (result) {
            if (result !== null && result.JobIdNo !== "") {
                LatestJobIdNo = result.JobIdNo;
                $("#ID").val(LatestJobIdNo);
                UpdateJobNo(serviceTypeId, callTypeId, LatestJobIdNo);
            }
            else {
                console.log("GetGeneratedJobNo No resonce");
            }

        }
    });
    return LatestJobIdNo;
}

function UpdateJobNo(serviceTypeId, callTypeId, jobIdNo) {

    var JobNo = "";

    var LatestJobIdNo = jobIdNo;


    var currentDate = new Date()

    var currentYear = currentDate.getFullYear().toString().substr(-2);
    var currentMonth = currentDate.getMonth();

    var strMonthCode = "";

    switch (currentMonth) {
        case 1:
            strMonthCode = "A";
            break;
        case 2:
            strMonthCode = "B";
            break;
        case 3:
            strMonthCode = "C";
            break;
        case 4:
            strMonthCode = "D";
            break;
        case 5:
            strMonthCode = "E";
            break;
        case 6:
            strMonthCode = "F";
            break;
        case 7:
            strMonthCode = "G";
            break;
        case 8:
            strMonthCode = "H";
            break;
        case 9:
            strMonthCode = "I";
            break;
        case 10:
            strMonthCode = "J";
            break;
        case 11:
            strMonthCode = "K";
            break;
        case 12:
            strMonthCode = "L";
            break;
    }

    var strServiceTypeCode = "IW";

    switch (!isNaN(serviceTypeId) ? parseInt(serviceTypeId) : 0) {
        case 0:
            strServiceTypeCode = "IW";
            break;
        case 1:
            strServiceTypeCode = "OW";
            break;
        case 2:
            strServiceTypeCode = "IN";
            break;
    }

    var strCallTypeCode = "";

    switch (!isNaN(callTypeId) ? parseInt(callTypeId) : 0) {
        case 0:
            strCallTypeCode = "L";
            break;
        case 1:
            strCallTypeCode = "W";
            break;
        case 2:
            strCallTypeCode = "O";
            break;
    }

    JobNo = currentYear + strMonthCode + strCallTypeCode + strServiceTypeCode + LatestJobIdNo;


    $("#JobNo").val(JobNo);


}

function fnCustomerEditModalShow(thisObj) {

    

    var customerId = $(thisObj).attr("data-Oid");

    if (customerId !== "" || customerId !== undefined) {

        $.ajax({
            type: "POST",
            url: "/Job/GetCustomerDetailsByCustomerId",
            data: { CustomerId: customerId },
            cache: false,
            success: function (data) {
                if (data !== null) {
                    $("#customer_modal").find("#Oid").val(customerId);

                    $("#customer_modal").find("#FirstName").val(data.FirstName);
                    $("#customer_modal").find("#LastName").val(data.LastName);
                    $("#customer_modal").find("#Address").val(data.Address);

                    $("#customer_modal").find("#EMail").val(data.EMail);
                    $("#customer_modal").find("#MobileNo").val(data.MobileNo);
                    $("#customer_modal").find("#PhoneH").val(data.PhoneH);
                    $("#customer_modal").find("#PhoneO").val(data.PhoneO);
                    $("#customer_modal").find("#SpDay").val(data.SpDay);
                    $("#customer_modal").find("#OtherInfo").val(data.OtherInfo);

                    var select2JsonData = data.Select2JSON;

                    var objSelect2Json = JSON.parse(select2JsonData.replace(/&quot;/g, '"')); //JSON.parse(select2JsonData);

                    var cityOption = new Option(objSelect2Json.Select2City.text, objSelect2Json.Select2City.id, false, false);
                    $("#customer_modal").find('#CityName').append(cityOption).trigger('change');
                    
                }
                $("#customer_modal").modal("show");

            }
        });


        

    }

    

}


function fnSaveCustomerDetails() {

    var custFormId = "frmCustomerMaster";

    var IsValidForm = fnValidateFormById(custFormId);

    if (IsValidForm) {

        var customerForm = $("#" + custFormId);

        var url = customerForm.attr('action');

        $.ajax({
            type: "POST",
            url: url,
            data: customerForm.serialize(),
            success: function (result) {
                if (result !== null) {
                    var customerObj = result.data;
                    //console.log(customerObj);
                    if (customerObj.Responce) {
                        toastr["success"](customerObj.Message);
                        var data = { id: customerObj.Oid, text: (customerObj.FirstName + " " + customerObj.LastName)};

                        var newOption = new Option(data.text, data.id, false, false);
                        $('#Customer').append(newOption).trigger('change');

                        $("#customer_modal").modal("hide");
                    }
                    else {
                        toastr["error"](customerObj.Message);
                    }
                }
                // Ajax call completed successfully
                //alert("Form Submited Successfully");
            },
            error: function (data) {

                // Some error in ajax call
                alert("some Error");
            }
        });
    }
}


function fnSaveCallReisterDetails(formId) {


    var IsValidForm = fnValidateFormById(formId);

    /*
    if (IsValidForm) {

        var callRegisterForm = $("#" + formId);

        var url = callRegisterForm.attr('action');

        $.ajax({
            type: "POST",
            url: url,
            data: customerForm.serialize(),
            success: function (result) {
                if (result !== null) {
                    var callRegisterResponce = result.data;
                    if (callRegisterResponce.Responce) {
                        toastr["success"](callRegisterResponce.Responce);
                    }
                    else {
                        toastr["error"](callRegisterResponce.Responce);
                    }
                }
            },
            error: function (data) {

                // Some error in ajax call
                console.log("some Error");
            }
        });
    }*/

    return IsValidForm;
}

