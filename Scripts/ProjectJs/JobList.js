

$(function () {

    $('#ItemName').select2({
        dropdownParent: $('#JobListAdvanceFilter_modal'),
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


    $('#Technician').select2({
        dropdownParent: $('#JobListAdvanceFilter_modal'),
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

    $('#TechnicianType').select2({
        dropdownParent: $('#JobListAdvanceFilter_modal'),
        placeholder: "Technician Type",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetTechnicianTypeList",
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


    $('#TechnicianId').select2({
        dropdownParent: $('#TechnicianCallAssign_modal'),
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

    $('#UserName').select2({
        dropdownParent: $('#JobListAdvanceFilter_modal'),
        placeholder: "User Name",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetUserList",
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


function fnCallRegistrationDataForTechnician() {

    if (checkboxes !== null) {

        var callIds = checkboxes.join(", ");

        $.ajax({
            type: "POST",
            url: "/Report/ExportCallRegisterDataForTechnician",
            data: { CallIds: callIds},
            cache: true,
            async: true,
            success: function (result) {

                if (result !== null) {
                    var objFileData = result.data;

                    if (objFileData.Base64String !== "" || objFileData.Base64String !== undefined) {

                        var fileName = objFileData.FileName;
                        var bytes = Base64ToBytes(objFileData.Base64String);
                        var blob = new Blob([bytes], { type: "application/octetstream" });

                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        a[0].click();
                        
                    }
                    else {
                        toastr["error"]("Somthing Went Wrong");
                    }
                }

            }
        });

    }


}


function fnCreateNewCallFromOldJonNo() {

    if (fnValidateFormById('frmJobNo')) {

        if (confirm('Do you want to create new call from Old Job No?'))
        {

            var oldJobNo = $("#OldJobNo").val();

            $(".loading").show();

            $.ajax({
                type: "POST",
                url: "/Job/CreateNewCallFromOldJonNo",
                data: { OldJobNo: oldJobNo },
                cache: true,
                async: true,
                success: function (result) {
                    $(".loading").hide();
                    if (result !== null) {
                        var objResponce = result.data;

                        if (objResponce.Responce) {

                            toastr["success"](objResponce.Message);
                        }
                        else {
                            toastr["error"](objResponce.Message);
                        }

                        $("#JobnNo_modal").modal("hide");

                        table.draw();
                    }

                }
            });
        }

    }


}


function fnSelectAllCheckboxes(objThis) {

    if ($(objThis).is(':checked') && CallIdsForSelectAll !== "")
    {
        var Oid_Array = CallIdsForSelectAll.split(', ');

        if (Oid_Array !== null && Oid_Array.length > 0)
        {
            checkboxes = [];
            checkboxes = Oid_Array;
            $(".selection").prop("checked", true);

        }

    }

    if (!$(objThis).is(':checked'))
    {
        checkboxes = [];
        $(".selection").prop("checked", false)
    }

    fnButtonShowHide();
}

function fnButtonShowHide()
{
    if (checkboxes.length > 0) {
        $("#btnAssignCall").show();
        $("#btnExportCalls").show();
    }
    else {
        $("#btnAssignCall").hide();
        $("#btnExportCalls").hide();
    }
}

function fnManageJobListSearchJson(btnId)
{

    

    objAdvanceFilter = new Object();

    if (btnId === "btnAdvanceSearch")
    {
        objAdvanceFilter.CustomerName = "";
        objAdvanceFilter.CallType = "";
        objAdvanceFilter.ServType = "";
        objAdvanceFilter.Technician = "";
        objAdvanceFilter.TechnicianText = "";
        objAdvanceFilter.TechnicianType = "";
        objAdvanceFilter.TechnicianTypeText = "";
        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = "";
        objAdvanceFilter.JobDone = "";
        objAdvanceFilter.JobNo = "";
        objAdvanceFilter.ItemName = "";
        objAdvanceFilter.ItemNameText = "";
        objAdvanceFilter.Deliver = "";
        objAdvanceFilter.Canceled = "";
        objAdvanceFilter.PartPanding = "";
        objAdvanceFilter.CallCategory = $('#CallCategory').val();
        objAdvanceFilter.FromDate = "";
        objAdvanceFilter.ToDate = "";
        objAdvanceFilter.CompComplaintNo = "";
        objAdvanceFilter.IsCompComplaintNo = "";
        objAdvanceFilter.CallAssignFromDate = "";
        objAdvanceFilter.CallAssignToDate = "";
        objAdvanceFilter.CallBack = "";
        objAdvanceFilter.WorkShopIN = "";
        objAdvanceFilter.PaymentPanding = "";
        objAdvanceFilter.UserName = "";
        objAdvanceFilter.UserNameText = "";


        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }

    if (btnId === "btnFilter")
    {
        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = $("#Technician").val();
        objAdvanceFilter.TechnicianText = $("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = $("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();

        objAdvanceFilter.ItemName = $("#ItemName").val();
        objAdvanceFilter.ItemNameText = $("#ItemName option:selected").text();

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.CallCategory = $('#CallCategory').val();
        objAdvanceFilter.FromDate = $("#FromDate").val();
        objAdvanceFilter.ToDate = $('#ToDate').val();
        objAdvanceFilter.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter.CallBack = $("#CallBack").val();
        objAdvanceFilter.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter.UserName = $("#UserName").val();
        objAdvanceFilter.UserNameText = $("#UserName option:selected").text();

        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }

    if (btnId === "btnFilterClear")
    {

        fnClearAllInputOfAdvanceFilter();

        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = $("#Technician").val();
        objAdvanceFilter.TechnicianText = $("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = $("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();

        objAdvanceFilter.ItemName = $("#ItemName").val();
        objAdvanceFilter.ItemNameText = $("#ItemName option:selected").text();

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.CallCategory = "0";//$('#CallCategory').val();
        objAdvanceFilter.FromDate = $("#FromDate").val();
        objAdvanceFilter.ToDate = $('#ToDate').val();
        objAdvanceFilter.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter.CallBack = $("#CallBack").val();
        objAdvanceFilter.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter.UserName = $("#UserName").val();
        objAdvanceFilter.UserNameText = $("#UserName option:selected").text();

        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }


    var localStorageAdvanceFilter = JSON.parse(localStorage.getItem('JobListAdvanceFilter'));

    if (localStorageAdvanceFilter !== null && Object.keys(localStorageAdvanceFilter).length !== 0)
    {
        objAdvanceFilter = localStorageAdvanceFilter;

        $("#CustomerName").val(objAdvanceFilter.CustomerName)
        $("#CallType").val(objAdvanceFilter.CallType)
        $("#ServType").val(objAdvanceFilter.ServType);

        var TechnicianOption = new Option(objAdvanceFilter.TechnicianText, objAdvanceFilter.Technician === null ? "" : objAdvanceFilter.Technician, false, false);
        $('#Technician').append(TechnicianOption).trigger('change');
        
        var TechnicianTypeOption = new Option(objAdvanceFilter.TechnicianTypeText, objAdvanceFilter.TechnicianType === null ? "" : objAdvanceFilter.TechnicianType , false, false);
        $("#TechnicianType").append(TechnicianTypeOption).trigger('change');

        $("#MobileNo").val(objAdvanceFilter.MobileNo);
        $("#CallAttn").val(objAdvanceFilter.CallAttn);
        $("#JobDone").val(objAdvanceFilter.JobDone);
        $("#JobNo").val().trim(objAdvanceFilter.JobNo);

        var ItemNameOption = new Option(objAdvanceFilter.ItemNameText, objAdvanceFilter.ItemName === null ? "" : objAdvanceFilter.ItemName, false, false);
        $("#ItemName").append(ItemNameOption).trigger('change');

        $("#Deliver").val(objAdvanceFilter.Deliver);
        $("#Canceled").val(objAdvanceFilter.Canceled);
        $("#PartPanding").val(objAdvanceFilter.PartPanding);
        $('#CallCategory').val(objAdvanceFilter.CallCategory);
        $("#FromDate").val(objAdvanceFilter.FromDate);
        $('#ToDate').val(objAdvanceFilter.ToDate);
        $("#CompComplaintNo").val(objAdvanceFilter.CompComplaintNo);
        $("#IsCompComplaintNo").val(objAdvanceFilter.IsCompComplaintNo);
        $("#CallAssignFromDate").val(objAdvanceFilter.CallAssignFromDate);
        $("#CallAssignToDate").val(objAdvanceFilter.CallAssignToDate);
        $("#CallBack").val(objAdvanceFilter.CallBack);
        $("#WorkShopIN").val(objAdvanceFilter.WorkShopIN);
        $("#PaymentPanding").val(objAdvanceFilter.PaymentPanding);

        var UserNameOption = new Option(objAdvanceFilter.UserNameText, objAdvanceFilter.UserName === null ? "" : objAdvanceFilter.UserName, false, false);
        $("#UserName").append(UserNameOption).trigger('change');

        $("#btnAdvanceFiter").removeClass("btn-primary");
        $("#btnAdvanceFiter").addClass("btn-success");
    }
    else
    {
        
        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = $("#Technician").val();
        objAdvanceFilter.TechnicianText = $("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = $("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();

        objAdvanceFilter.ItemName = $("#ItemName").val();
        objAdvanceFilter.ItemNameText = $("#ItemName option:selected").text();

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.CallCategory = $('#CallCategory').val();
        objAdvanceFilter.FromDate = $("#FromDate").val();
        objAdvanceFilter.ToDate = $('#ToDate').val();
        objAdvanceFilter.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter.CallBack = $("#CallBack").val();
        objAdvanceFilter.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter.UserName = $("#UserName").val();
        objAdvanceFilter.UserNameText = $("#UserName option:selected").text();

    }

    localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
}

function fnClearAllInputOfAdvanceFilter()
{
    $('#frmAdvanceFilter').find('input:text').val('');

    $("#frmAdvanceFilter").find("select").each(function () {

        $(this).prop('selectedIndex', "");

    });

    $("#Technician").val('').trigger('change');
    $("#TechnicianType").val('').trigger('change');
    $("#ItemName").val('').trigger('change');
    $("#UserName").val('').trigger('change');
}


/*
$("#chkSelectionAll").change(function (event) {
    event.preventDefault();

    var objThis = $("#chkSelectionAll");

    if ($(objThis).is(':checked') && CallIdsForSelectAll !== "") {
        var Oid_Array = CallIdsForSelectAll.split(', ');

        if (Oid_Array !== null && Oid_Array.length > 0) {
            checkboxes = [];
            checkboxes = Oid_Array;
            $(".selection").prop("checked", true)
        }

    }

    if (!$(objThis).is(':checked')) {
        checkboxes = [];
        $(".selection").prop("checked", false)
    }

});

*/


