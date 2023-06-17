
$(function () {

});




/*----------------------------------------*/



var TechnicanArray = [];
var TechnicanTypeArray = [];
var ItemNameArray = [];
var UserNameArray = [];
var FaultTypeArray = [];
var AreaArray = [];


$(function () {

    $('#ItemName').select2({
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
        placeholder: "Item Name",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/OldData/GetItemList",
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
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
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
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
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
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
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


    $('#FaultType').select2({
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
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

    $('#Area').select2({
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
        placeholder: "Area",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/OldData/GetAreaList",
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

    $('#Company').select2({
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
        placeholder: "Company",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/OldData/GetCompanyList",
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

    $('#Brand').select2({
        dropdownParent: $('#OldJobListAdvanceFilter_modal'),
        placeholder: "Brand",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/OldData/GetBrandList",
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


});


$('#Technician').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        TechnicanArray.push(data);
    }
});

$('#Technician').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        TechnicanArray.pop(data);
    }
});



$('#TechnicianType').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        TechnicanTypeArray.push(data);
    }
});

$('#TechnicianType').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        TechnicanTypeArray.pop(data);
    }
});



$('#ItemName').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        ItemNameArray.push(data);
    }
});

$('#ItemName').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        ItemNameArray.pop(data);
    }
});




$('#UserName').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        UserNameArray.push(data);
    }
});

$('#UserName').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        UserNameArray.pop(data);
    }
});


$('#FaultType').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        FaultTypeArray.push(data);
    }
});

$('#FaultType').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        FaultTypeArray.pop(data);
    }
});



$('#Area').on('select2:select', function (e) {
    var data = e.params.data;
    //console.log(data); 

    if (data !== null) {
        console.log(data.text);
        AreaArray.push(data);
    }
});

$('#Area').on('select2:unselect', function (e) {
    var data = e.params.data;
    //console.log(data);
    if (data !== null) {
        console.log(data.text);
        AreaArray.pop(data);
    }
});


function fnCallRegistrationDataForTechnician() {

    if (checkboxes !== null) {

        var callIds = checkboxes.join(", ");

        $.ajax({
            type: "POST",
            url: "/Report/ExportCallRegisterDataForTechnician",
            data: { CallIds: callIds },
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

        if (confirm('Do you want to create new call from Old Job No?')) {

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

                        $("#JobnNo_modal").find("#OldJobNo").val("");
                        $("#JobnNo_modal").modal("hide");

                        table.draw();
                    }

                }
            });
        }

    }


}


function fnSelectAllCheckboxes(objThis) {

    if ($(objThis).is(':checked') && CallIdsForSelectAll !== "") {
        var Oid_Array = CallIdsForSelectAll.split(', ');

        if (Oid_Array !== null && Oid_Array.length > 0) {
            checkboxes = [];
            checkboxes = Oid_Array;
            $(".selection").prop("checked", true);

        }

    }

    if (!$(objThis).is(':checked')) {
        checkboxes = [];
        $(".selection").prop("checked", false)
    }

    fnButtonShowHide();
}

function fnButtonShowHide() {
    if (checkboxes.length > 0) {
        $("#btnAssignCall").show();
        $("#btnExportCalls").show();
        $("#btnPrintMultipleWorkorder").show();
    }
    else {
        $("#btnAssignCall").hide();
        $("#btnExportCalls").hide();
        $("#btnPrintMultipleWorkorder").hide();
    }
}

function fnManageJobListSearchJson(btnId) {



    objAdvanceFilter_old = new Object();

    if (btnId === "btnAdvanceSearch") {
        objAdvanceFilter_old.CustomerName = "";
        objAdvanceFilter_old.CallType = "";
        objAdvanceFilter_old.ServType = "";
        objAdvanceFilter_old.Technician = "";
        objAdvanceFilter_old.TechnicianText = "";
        objAdvanceFilter_old.TechnicianType = "";
        objAdvanceFilter_old.TechnicianTypeText = "";
        objAdvanceFilter_old.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter_old.CallAttn = "";
        objAdvanceFilter_old.JobDone = "";
        objAdvanceFilter_old.GoAfterCall = "";
        objAdvanceFilter_old.JobNo = "";
        objAdvanceFilter_old.SrNo = "";
        objAdvanceFilter_old.ItemName = "";
        objAdvanceFilter_old.ItemNameText = "";
        objAdvanceFilter_old.ItemKeyword = "";
        objAdvanceFilter_old.Deliver = "";
        objAdvanceFilter_old.Canceled = "";
        objAdvanceFilter_old.PartPanding = "";
        objAdvanceFilter_old.RepeatFromTech = "";
        objAdvanceFilter_old.CallCategory = $('#CallCategory').val();
        objAdvanceFilter_old.FromDate = "";
        objAdvanceFilter_old.ToDate = "";
        objAdvanceFilter_old.CompComplaintNo = "";
        objAdvanceFilter_old.IsCompComplaintNo = "";
        objAdvanceFilter_old.CallAssignFromDate = "";
        objAdvanceFilter_old.CallAssignToDate = "";
        objAdvanceFilter_old.CallBack = "";
        objAdvanceFilter_old.WorkShopIN = "";
        objAdvanceFilter_old.PaymentPanding = "";
        objAdvanceFilter_old.UserName = "";
        objAdvanceFilter_old.UserNameText = "";
        objAdvanceFilter_old.FaultType = "";
        objAdvanceFilter_old.FaultTypeText = "";
        objAdvanceFilter_old.Area = "";
        objAdvanceFilter_old.AreaText = "";
        objAdvanceFilter_old.FaultDesc = "";
        objAdvanceFilter_old.ModifyFromDate = "";
        objAdvanceFilter_old.ModifyToDate = "";
        objAdvanceFilter_old.TechnicanArray = [];
        objAdvanceFilter_old.TechnicanTypeArray = [];
        objAdvanceFilter_old.ItemNameArray = [];
        objAdvanceFilter_old.UserNameArray = [];
        objAdvanceFilter_old.FaultTypeArray = [];
        objAdvanceFilter_old.AreaArray = [];


        localStorage.setItem('OldJobListAdvanceFilter', JSON.stringify(objAdvanceFilter_old));
    }

    if (btnId === "btnFilter") {
        objAdvanceFilter_old.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter_old.CallType = $("#CallType").val()
        objAdvanceFilter_old.ServType = $("#ServType").val();

        objAdvanceFilter_old.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter_old.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter_old.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter_old.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter_old.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter_old.CallAttn = $("#CallAttn").val();
        objAdvanceFilter_old.JobDone = $("#JobDone").val();
        objAdvanceFilter_old.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter_old.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter_old.SrNo = $("#SrNo").val().trim();

        objAdvanceFilter_old.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter_old.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter_old.ItemKeyword = $("#ItemKeyword").val();;

        objAdvanceFilter_old.Deliver = $("#Deliver").val();
        objAdvanceFilter_old.Canceled = $("#Canceled").val();
        objAdvanceFilter_old.PartPanding = $("#PartPanding").val();
        objAdvanceFilter_old.RepeatFromTech = $("#RepeatFromTech").val();
        objAdvanceFilter_old.CallCategory = $('#CallCategory').val();
        objAdvanceFilter_old.FromDate = $("#FromDate").val();
        objAdvanceFilter_old.ToDate = $('#ToDate').val();
        objAdvanceFilter_old.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter_old.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter_old.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter_old.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter_old.CallBack = $("#CallBack").val();
        objAdvanceFilter_old.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter_old.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter_old.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id'); // $("#UserName").val();
        objAdvanceFilter_old.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter_old.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id');
        objAdvanceFilter_old.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text');

        objAdvanceFilter_old.Area = fnGetSearchArrayIntoString(AreaArray, 'id'); // $("#UserName").val();
        objAdvanceFilter_old.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter_old.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter_old.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter_old.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter_old.TechnicanArray = TechnicanArray;
        objAdvanceFilter_old.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter_old.ItemNameArray = ItemNameArray;
        objAdvanceFilter_old.UserNameArray = UserNameArray;
        objAdvanceFilter_old.FaultTypeArray = FaultTypeArray;
        objAdvanceFilter_old.AreaArray = AreaArray;


        localStorage.setItem('OldJobListAdvanceFilter', JSON.stringify(objAdvanceFilter_old));
    }

    if (btnId === "btnFilterClear" || btnId === "btnClearSearch") {

        fnClearAllInputOfAdvanceFilter();

        objAdvanceFilter_old.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter_old.CallType = $("#CallType").val()
        objAdvanceFilter_old.ServType = $("#ServType").val();

        objAdvanceFilter_old.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter_old.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter_old.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter_old.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter_old.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter_old.CallAttn = $("#CallAttn").val();
        objAdvanceFilter_old.JobDone = $("#JobDone").val();
        objAdvanceFilter_old.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter_old.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter_old.SrNo = $("#SrNo").val().trim();

        objAdvanceFilter_old.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter_old.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter_old.ItemKeyword = $("#ItemKeyword").val();;

        objAdvanceFilter_old.Deliver = $("#Deliver").val();
        objAdvanceFilter_old.Canceled = $("#Canceled").val();
        objAdvanceFilter_old.PartPanding = $("#PartPanding").val();
        objAdvanceFilter_old.RepeatFromTech = $("#RepeatFromTech").val();
        objAdvanceFilter_old.CallCategory = "0";//$('#CallCategory').val();
        objAdvanceFilter_old.FromDate = $("#FromDate").val();
        objAdvanceFilter_old.ToDate = $('#ToDate').val();
        objAdvanceFilter_old.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter_old.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter_old.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter_old.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter_old.CallBack = $("#CallBack").val();
        objAdvanceFilter_old.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter_old.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter_old.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id'); // $("#UserName").val();
        objAdvanceFilter_old.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter_old.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id');
        objAdvanceFilter_old.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text');

        objAdvanceFilter_old.Area = fnGetSearchArrayIntoString(AreaArray, 'id'); // $("#UserName").val();
        objAdvanceFilter_old.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter_old.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter_old.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter_old.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter_old.TechnicanArray = TechnicanArray;
        objAdvanceFilter_old.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter_old.ItemNameArray = ItemNameArray;
        objAdvanceFilter_old.UserNameArray = UserNameArray;
        objAdvanceFilter_old.FaultTypeArray = FaultTypeArray;
        objAdvanceFilter_old.AreaArray = AreaArray;



        localStorage.setItem('OldJobListAdvanceFilter', JSON.stringify(objAdvanceFilter_old));
    }


    var localStorageAdvanceFilter = JSON.parse(localStorage.getItem('OldJobListAdvanceFilter'));

    if (localStorageAdvanceFilter !== null && Object.keys(localStorageAdvanceFilter).length !== 0) {
        objAdvanceFilter = localStorageAdvanceFilter;

        $("#CustomerName").val(objAdvanceFilter_old.CustomerName)
        $("#CallType").val(objAdvanceFilter_old.CallType)
        $("#ServType").val(objAdvanceFilter_old.ServType);

        TechnicanArray = objAdvanceFilter_old.TechnicanArray;

        var technicianIds = $("#Technician").val();

        $.each(objAdvanceFilter_old.TechnicanArray, function (i, item) {
            var currentItemId = item.id;
            var currentItemText = item.text;


            if ($.inArray(currentItemId, technicianIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var TechnicianOption = new Option(item.text, item.id, true, true);
                $('#Technician').append(TechnicianOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }

        });

        TechnicanTypeArray = objAdvanceFilter_old.TechnicanTypeArray;

        var technicianTypeIds = $("#TechnicianType").val();

        $.each(objAdvanceFilter_old.TechnicanTypeArray, function (i, item) {

            var currentItemId = item.id;
            var currentItemText = item.text;

            if ($.inArray(currentItemId, technicianTypeIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var TechnicianTypeOption = new Option(item.text, item.id, true, true);
                $('#TechnicianType').append(TechnicianTypeOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }

        });

        $("#MobileNo").val(objAdvanceFilter_old.MobileNo);
        $("#CallAttn").val(objAdvanceFilter_old.CallAttn);
        $("#JobDone").val(objAdvanceFilter_old.JobDone);
        $("#GoAfterCall").val(objAdvanceFilter_old.GoAfterCall);
        $("#JobNo").val(objAdvanceFilter_old.JobNo);
        $("#SrNo").val(objAdvanceFilter_old.SrNo);

        ItemNameArray = objAdvanceFilter_old.ItemNameArray;

        var itemNameIds = $("#ItemName").val();

        $.each(objAdvanceFilter_old.ItemNameArray, function (i, item) {

            var currentItemId = item.id;
            var currentItemText = item.text;

            if ($.inArray(currentItemId, itemNameIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var ItemNameOption = new Option(item.text, item.id, true, true);
                $("#ItemName").append(ItemNameOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }


        });

        $("#ItemKeyword").val(objAdvanceFilter_old.ItemKeyword);

        $("#Deliver").val(objAdvanceFilter_old.Deliver);
        $("#Canceled").val(objAdvanceFilter_old.Canceled);
        $("#PartPanding").val(objAdvanceFilter_old.PartPanding);
        $("#RepeatFromTech").val(objAdvanceFilter_old.RepeatFromTech);
        $('#CallCategory').val(objAdvanceFilter_old.CallCategory);
        $("#FromDate").val(objAdvanceFilter_old.FromDate);
        $('#ToDate').val(objAdvanceFilter_old.ToDate);
        $("#CompComplaintNo").val(objAdvanceFilter_old.CompComplaintNo);
        $("#IsCompComplaintNo").val(objAdvanceFilter_old.IsCompComplaintNo);
        $("#CallAssignFromDate").val(objAdvanceFilter_old.CallAssignFromDate);
        $("#CallAssignToDate").val(objAdvanceFilter_old.CallAssignToDate);
        $("#CallBack").val(objAdvanceFilter_old.CallBack);
        $("#WorkShopIN").val(objAdvanceFilter_old.WorkShopIN);
        $("#PaymentPanding").val(objAdvanceFilter_old.PaymentPanding);

        UserNameArray = objAdvanceFilter_old.UserNameArray;

        var userNameIds = $("#UserName").val();

        $.each(objAdvanceFilter_old.UserNameArray, function (i, item) {

            var currentItemId = item.id;
            var currentItemText = item.text;

            if ($.inArray(currentItemId, userNameIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var UserNameOption = new Option(item.text, item.id, true, true);
                $("#UserName").append(UserNameOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }

        });


        AreaArray = objAdvanceFilter_old.AreaArray;

        var areaIds = $("#Area").val();

        $.each(objAdvanceFilter_old.AreaArray, function (i, item) {

            var currentItemId = item.id;
            var currentItemText = item.text;

            if ($.inArray(currentItemId, areaIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var AreaOption = new Option(item.text, item.id, true, true);
                $("#Area").append(AreaOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }

        });

        FaultTypeArray = objAdvanceFilter_old.FaultTypeArray;

        var faultTypeIds = $("#FaultType").val();

        $.each(objAdvanceFilter_old.FaultTypeArray, function (i, item) {

            var currentItemId = item.id;
            var currentItemText = item.text;

            if ($.inArray(currentItemId, faultTypeIds) === -1) {
                console.log(currentItemId + " " + currentItemText + "  Add");

                var FaultTypeOption = new Option(item.text, item.id, true, true);
                $("#FaultType").append(FaultTypeOption).trigger('change');
            }
            else {
                console.log(currentItemId + " " + currentItemText + " Not Add");
            }

        });

        $("#FaultDesc").val(objAdvanceFilter_old.FaultDesc);

        $("#ModifyFromDate").val(objAdvanceFilter_old.ModifyFromDate);
        $('#ModifyToDate').val(objAdvanceFilter_old.ModifyToDate);

        $("#btnAdvanceFiter").removeClass("btn-primary");
        $("#btnAdvanceFiter").addClass("btn-success");
    }
    else {

        objAdvanceFilter_old.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter_old.CallType = $("#CallType").val()
        objAdvanceFilter_old.ServType = $("#ServType").val();

        objAdvanceFilter_old.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter_old.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter_old.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter_old.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter_old.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter_old.CallAttn = $("#CallAttn").val();
        objAdvanceFilter_old.JobDone = $("#JobDone").val();
        objAdvanceFilter_old.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter_old.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter_old.SrNo = $("#SrNo").val().trim();

        objAdvanceFilter_old.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter_old.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter_old.ItemKeyword = $("#ItemKeyword").val();

        objAdvanceFilter_old.Deliver = $("#Deliver").val();
        objAdvanceFilter_old.Canceled = $("#Canceled").val();
        objAdvanceFilter_old.PartPanding = $("#PartPanding").val();
        objAdvanceFilter_old.RepeatFromTech = $("#RepeatFromTech").val();
        objAdvanceFilter_old.CallCategory = $('#CallCategory').val();
        objAdvanceFilter_old.FromDate = $("#FromDate").val();
        objAdvanceFilter_old.ToDate = $('#ToDate').val();
        objAdvanceFilter_old.CompComplaintNo = $("#CompComplaintNo").val();
        objAdvanceFilter_old.IsCompComplaintNo = $("#IsCompComplaintNo").val();
        objAdvanceFilter_old.CallAssignFromDate = $("#CallAssignFromDate").val();
        objAdvanceFilter_old.CallAssignToDate = $("#CallAssignToDate").val();
        objAdvanceFilter_old.CallBack = $("#CallBack").val();
        objAdvanceFilter_old.WorkShopIN = $("#WorkShopIN").val();
        objAdvanceFilter_old.PaymentPanding = $("#PaymentPanding").val();

        objAdvanceFilter_old.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id');
        objAdvanceFilter_old.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text');

        objAdvanceFilter_old.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id');
        objAdvanceFilter_old.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text');

        objAdvanceFilter_old.Area = fnGetSearchArrayIntoString(AreaArray, 'id');
        objAdvanceFilter_old.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text');

        objAdvanceFilter_old.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter_old.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter_old.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter_old.TechnicanArray = TechnicanArray;
        objAdvanceFilter_old.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter_old.ItemNameArray = ItemNameArray;
        objAdvanceFilter_old.UserNameArray = UserNameArray;
        objAdvanceFilter_old.FaultTypeArray = FaultTypeArray;
        objAdvanceFilter_old.AreaArray = AreaArray;

    }

    localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter_old));
}

function fnClearAllInputOfAdvanceFilter() {

    $('#frmJobListFilter').find('input:text').val('');
    $("#frmJobListFilter").find("select").each(function () {

        $(this).prop('selectedIndex', "0");

    });

    $('#frmAdvanceFilter').find('input:text').val('');
    $("#frmAdvanceFilter").find("select").each(function () {

        $(this).prop('selectedIndex', "");

    });

    $("#Technician").val('').trigger('change');
    $("#TechnicianType").val('').trigger('change');
    $("#ItemName").val('').trigger('change');
    $("#UserName").val('').trigger('change');
    $("#FaultType").val('').trigger('change');
    $("#Area").val('').trigger('change');

    TechnicanArray = [];
    TechnicanTypeArray = [];
    ItemNameArray = [];
    UserNameArray = [];
    FaultTypeArray = [];
    AreaArray = [];
}

function fnGenerateMultipleWordOrderPDF() {

    if (checkboxes !== null) {

        var callIds = checkboxes.join(", ");

        $(".loading").show();

        $.ajax({
            type: "POST",
            url: "/Print/MultipleWorkorderPrintPreview",
            data: { CallIds: callIds },
            cache: true,
            async: true,
            success: function (result) {
                $(".loading").hide();
                if (result !== null) {
                    var objFileData = result.data;

                    if (objFileData.Base64String !== "" || objFileData.Base64String !== undefined) {

                        var fileName = objFileData.FileName;
                        var bytes = Base64ToBytes(objFileData.Base64String);
                        var blob = new Blob([bytes], { type: "application/pdf" });

                        var fileURL = URL.createObjectURL(blob);
                        window.open(fileURL);

                    }
                    else {
                        toastr["error"]("Somthing Went Wrong");
                    }
                }

            }
        });

    }


}

function fnGetSearchArrayIntoString(arrayParam, paramName) {
    var commaString = "";


    if (arrayParam !== null) {
        if (paramName === "id") {
            $.each(arrayParam, function (i, item) {
                console.log(item.id);
                commaString += item.id + ",";
            });
        }

        if (paramName === "text") {
            $.each(arrayParam, function (i, item) {
                console.log(item.text);
                commaString += item.text + ",";
            });
        }


    }

    return commaString;

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


