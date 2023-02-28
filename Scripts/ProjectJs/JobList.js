
var TechnicanArray = [];
var TechnicanTypeArray = [];
var ItemNameArray = [];
var UserNameArray = [];
var FaultTypeArray = [];
var AreaArray = [];


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


    $('#FaultType').select2({
        dropdownParent: $('#JobListAdvanceFilter_modal'),
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
        dropdownParent: $('#JobListAdvanceFilter_modal'),
        placeholder: "Area",
        minimumInputLength: 3,
        allowClear: true,
        delay: 250,
        cache: true,
        ajax: {
            url: "/Job/GetAreaList",
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



    objAdvanceFilter = new Object();

    if (btnId === "btnAdvanceSearch") {
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
        objAdvanceFilter.GoAfterCall = "";
        objAdvanceFilter.JobNo = "";
        objAdvanceFilter.SrNo = "";
        objAdvanceFilter.ItemName = "";
        objAdvanceFilter.ItemNameText = "";
        objAdvanceFilter.ItemKeyword = "";
        objAdvanceFilter.Pincode = "";
        objAdvanceFilter.Deliver = "";
        objAdvanceFilter.Canceled = "";
        objAdvanceFilter.PartPanding = "";
        objAdvanceFilter.RepeatFromTech = "";
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
        objAdvanceFilter.FaultType = "";
        objAdvanceFilter.FaultTypeText = "";
        objAdvanceFilter.Area = "";
        objAdvanceFilter.AreaText = "";
        objAdvanceFilter.FaultDesc = ""; 
        objAdvanceFilter.ModifyFromDate = "";
        objAdvanceFilter.ModifyToDate = "";
        objAdvanceFilter.TechnicanArray = [];
        objAdvanceFilter.TechnicanTypeArray = [];
        objAdvanceFilter.ItemNameArray = [];
        objAdvanceFilter.UserNameArray = [];
        objAdvanceFilter.FaultTypeArray = []; 
        objAdvanceFilter.AreaArray = [];


        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }

    if (btnId === "btnFilter") {
        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter.SrNo = $("#SrNo").val().trim();
        objAdvanceFilter.Pincode = $("#Pincode").val().trim();

        objAdvanceFilter.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter.ItemKeyword = $("#ItemKeyword").val();;

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.RepeatFromTech = $("#RepeatFromTech").val();
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

        objAdvanceFilter.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id'); // $("#UserName").val();
        objAdvanceFilter.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id'); 
        objAdvanceFilter.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text'); 

        objAdvanceFilter.Area = fnGetSearchArrayIntoString(AreaArray, 'id'); // $("#UserName").val();
        objAdvanceFilter.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter.TechnicanArray = TechnicanArray;
        objAdvanceFilter.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter.ItemNameArray = ItemNameArray;
        objAdvanceFilter.UserNameArray = UserNameArray;
        objAdvanceFilter.FaultTypeArray = FaultTypeArray; 
        objAdvanceFilter.AreaArray = AreaArray;


        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }

    if (btnId === "btnFilterClear" || btnId === "btnClearSearch") {

        fnClearAllInputOfAdvanceFilter();

        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter.SrNo = $("#SrNo").val().trim();
        objAdvanceFilter.Pincode = $("#Pincode").val().trim();

        objAdvanceFilter.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter.ItemKeyword = $("#ItemKeyword").val();;

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.RepeatFromTech = $("#RepeatFromTech").val();
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

        objAdvanceFilter.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id'); // $("#UserName").val();
        objAdvanceFilter.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id');
        objAdvanceFilter.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text'); 

        objAdvanceFilter.Area = fnGetSearchArrayIntoString(AreaArray, 'id'); // $("#UserName").val();
        objAdvanceFilter.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text'); // $("#UserName option:selected").text();

        objAdvanceFilter.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter.TechnicanArray = TechnicanArray;
        objAdvanceFilter.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter.ItemNameArray = ItemNameArray;
        objAdvanceFilter.UserNameArray = UserNameArray;
        objAdvanceFilter.FaultTypeArray = FaultTypeArray; 
        objAdvanceFilter.AreaArray = AreaArray;



        localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
    }


    var localStorageAdvanceFilter = JSON.parse(localStorage.getItem('JobListAdvanceFilter'));

    if (localStorageAdvanceFilter !== null && Object.keys(localStorageAdvanceFilter).length !== 0) {
        objAdvanceFilter = localStorageAdvanceFilter;

        $("#CustomerName").val(objAdvanceFilter.CustomerName)
        $("#CallType").val(objAdvanceFilter.CallType)
        $("#ServType").val(objAdvanceFilter.ServType);

        TechnicanArray = objAdvanceFilter.TechnicanArray;

        var technicianIds = $("#Technician").val();

        $.each(objAdvanceFilter.TechnicanArray, function (i, item)
        {
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

        TechnicanTypeArray = objAdvanceFilter.TechnicanTypeArray;

        var technicianTypeIds = $("#TechnicianType").val();

        $.each(objAdvanceFilter.TechnicanTypeArray, function (i, item) {

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

        $("#MobileNo").val(objAdvanceFilter.MobileNo);
        $("#CallAttn").val(objAdvanceFilter.CallAttn);
        $("#JobDone").val(objAdvanceFilter.JobDone);
        $("#GoAfterCall").val(objAdvanceFilter.GoAfterCall);
        $("#JobNo").val(objAdvanceFilter.JobNo);
        $("#SrNo").val(objAdvanceFilter.SrNo);
        $("#Pincode").val(objAdvanceFilter.Pincode);

        ItemNameArray = objAdvanceFilter.ItemNameArray;

        var itemNameIds = $("#ItemName").val();

        $.each(objAdvanceFilter.ItemNameArray, function (i, item) {

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

        $("#ItemKeyword").val(objAdvanceFilter.ItemKeyword);

        $("#Deliver").val(objAdvanceFilter.Deliver);
        $("#Canceled").val(objAdvanceFilter.Canceled);
        $("#PartPanding").val(objAdvanceFilter.PartPanding);
        $("#RepeatFromTech").val(objAdvanceFilter.RepeatFromTech);
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

        UserNameArray = objAdvanceFilter.UserNameArray;

        var userNameIds = $("#UserName").val();

        $.each(objAdvanceFilter.UserNameArray, function (i, item) {

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


        AreaArray = objAdvanceFilter.AreaArray;

        var areaIds = $("#Area").val();

        $.each(objAdvanceFilter.AreaArray, function (i, item) {

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

        FaultTypeArray = objAdvanceFilter.FaultTypeArray;

        var faultTypeIds = $("#FaultType").val();

        $.each(objAdvanceFilter.FaultTypeArray, function (i, item) {

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

         $("#FaultDesc").val(objAdvanceFilter.FaultDesc);

        $("#ModifyFromDate").val(objAdvanceFilter.ModifyFromDate);
        $('#ModifyToDate').val(objAdvanceFilter.ModifyToDate);

        $("#btnAdvanceFiter").removeClass("btn-primary");
        $("#btnAdvanceFiter").addClass("btn-success");
    }
    else {

        objAdvanceFilter.CustomerName = $("#CustomerName").val().trim();
        objAdvanceFilter.CallType = $("#CallType").val()
        objAdvanceFilter.ServType = $("#ServType").val();

        objAdvanceFilter.Technician = fnGetSearchArrayIntoString(TechnicanArray, 'id'); //$("#Technician").val();
        objAdvanceFilter.TechnicianText = fnGetSearchArrayIntoString(TechnicanArray, 'text'); //$("#Technician option:selected").text()

        objAdvanceFilter.TechnicianType = fnGetSearchArrayIntoString(TechnicanTypeArray, 'id'); //$("#TechnicianType").val();
        objAdvanceFilter.TechnicianTypeText = fnGetSearchArrayIntoString(TechnicanTypeArray, 'text');  // $("#TechnicianType option:selected").text();

        objAdvanceFilter.MobileNo = $("#MobileNo").val().trim();
        objAdvanceFilter.CallAttn = $("#CallAttn").val();
        objAdvanceFilter.JobDone = $("#JobDone").val();
        objAdvanceFilter.GoAfterCall = $("#GoAfterCall").val();
        objAdvanceFilter.JobNo = $("#JobNo").val().trim();
        objAdvanceFilter.SrNo = $("#SrNo").val().trim();
        objAdvanceFilter.Pincode = $("#Pincode").val();

        objAdvanceFilter.ItemName = fnGetSearchArrayIntoString(ItemNameArray, 'id');  // $("#ItemName").val();
        objAdvanceFilter.ItemNameText = fnGetSearchArrayIntoString(ItemNameArray, 'text'); // $("#ItemName option:selected").text();

        objAdvanceFilter.ItemKeyword = $("#ItemKeyword").val();

        objAdvanceFilter.Deliver = $("#Deliver").val();
        objAdvanceFilter.Canceled = $("#Canceled").val();
        objAdvanceFilter.PartPanding = $("#PartPanding").val();
        objAdvanceFilter.RepeatFromTech = $("#RepeatFromTech").val();
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

        objAdvanceFilter.UserName = fnGetSearchArrayIntoString(UserNameArray, 'id'); 
        objAdvanceFilter.UserNameText = fnGetSearchArrayIntoString(UserNameArray, 'text'); 

        objAdvanceFilter.FaultType = fnGetSearchArrayIntoString(FaultTypeArray, 'id');
        objAdvanceFilter.FaultTypeText = fnGetSearchArrayIntoString(FaultTypeArray, 'text'); 

        objAdvanceFilter.Area = fnGetSearchArrayIntoString(AreaArray, 'id');
        objAdvanceFilter.AreaText = fnGetSearchArrayIntoString(AreaArray, 'text'); 

        objAdvanceFilter.FaultDesc = $("#FaultDesc").val();

        objAdvanceFilter.ModifyFromDate = $("#ModifyFromDate").val();
        objAdvanceFilter.ModifyToDate = $("#ModifyToDate").val();
        objAdvanceFilter.TechnicanArray = TechnicanArray;
        objAdvanceFilter.TechnicanTypeArray = TechnicanTypeArray;
        objAdvanceFilter.ItemNameArray = ItemNameArray;
        objAdvanceFilter.UserNameArray = UserNameArray;
        objAdvanceFilter.FaultTypeArray = FaultTypeArray;
        objAdvanceFilter.AreaArray = AreaArray;

    }

    localStorage.setItem('JobListAdvanceFilter', JSON.stringify(objAdvanceFilter));
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


