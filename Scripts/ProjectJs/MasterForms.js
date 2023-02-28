

(function () {

    $('#City').select2({
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

});


function InsertUpdateArea() {

    var blnIsValidForm = true;

    var areaName = $("#AreaName").val();
    var areaPincode = $("#AreaPincode").val();

    if (areaName === "") {
        $("#AreaName").addClass("is-invalid");
        $("#AreaNameToolTip").addClass("invalid-feedback").html("Please Enter Area Name");

        blnIsValidForm = false;
    }
    else if (areaPincode === "") {
        $("#AreaPincode").addClass("is-invalid");
        $("#AreaPincodeToolTip").addClass("invalid-feedback").html("Please Enter Area Pincode");

        blnIsValidForm = false;
    }
    else {

        var objArea = new Object();

        objArea.AreaId = $("#AreaId").val();
        objArea.AreaName = areaName;
        objArea.AreaPincode = areaPincode;

        $.ajax({
            type: "POST",
            url: "/Master/AreaForm",
            data: JSON.stringify(objArea),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#AreaName").addClass("is-invalid");
                        $("#AreaNameToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/AreaList";
                        }, 2000);
                        
                    }
                }
            },
            failure: function (result) {
                alert(data.responseText);
            },
            error: function (result) {
                alert(data.responseText);
            }
        });
    }

    return blnIsValidForm;
}


function fnDeleteArea(areaId) {

    if (confirm('Are you sure you want to Delete Area?')) {

        if (areaId === "") {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeleteAreaById",
                data: { AreaId: areaId },
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log("return");
                    if (result.data !== null) {
                        if (result.data.Responce === false) {
                            toastr["error"](result.data.Message);
                        }
                        else {
                            toastr["success"](result.data.Message);

                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                    }
                },
                failure: function (result) {
                    console.log(result.responseText);
                },
                error: function (result) {
                    console.log(result.responseText);
                }
            });
        }
    }
    
}



function InsertUpdateItem() {

    var blnIsValidForm = true;

    var ItemName = $("#ItemName").val();
    var ItemKeyword = $("#ItemKeyword").val();

    if (ItemName === "") {
        $("#ItemName").addClass("is-invalid");
        $("#ItemNameToolTip").addClass("invalid-feedback").html("Please Enter Item Name");

        blnIsValidForm = false;
    }
    else {

        var objItem = new Object();

        objItem.ItemId = $("#ItemId").val();
        objItem.ItemName = ItemName;
        objItem.TechnicianId = $("#Technician").val();
        objItem.ItemKeyword = ItemKeyword;


        $.ajax({
            type: "POST",
            url: "/Master/ItemForm",
            data: JSON.stringify(objItem),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#ItemName").addClass("is-invalid");
                        $("#ItemNameToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/ItemList";
                        }, 2000);

                    }
                }
            },
            failure: function (result) {
                alert(data.responseText);
            },
            error: function (result) {
                alert(data.responseText);
            }
        });
    }

    return blnIsValidForm;
}


function fnDeleteItem(ItemId) {

    if (confirm('Are you sure you want to Delete Item?')) {

        if (ItemId === "") {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeleteItemById",
                data: { ItemId: ItemId },
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log("return");
                    if (result.data !== null) {
                        if (result.data.Responce === false) {
                            toastr["error"](result.data.Message);
                        }
                        else {
                            toastr["success"](result.data.Message);

                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                    }
                },
                failure: function (result) {
                    console.log(result.responseText);
                },
                error: function (result) {
                    console.log(result.responseText);
                }
            });
        }
    }

}





function InsertUpdateIPAddress() {

    var blnIsValidForm = true;

    var IPAddress = $("#IP").val();

    if (IPAddress === "") {
        $("#IP").addClass("is-invalid");
        $("#IPAddressToolTip").addClass("invalid-feedback").html("Please IP Address");

        blnIsValidForm = false;
    }
    else {

        var objItem = new Object();

        objItem.Id = $("#Id").val();
        objItem.IP = IPAddress;

        $.ajax({
            type: "POST",
            url: "/Master/IPAddressForm",
            data: JSON.stringify(objItem),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#IP").addClass("is-invalid");
                        $("#IPAddressToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/IPAddressList";
                        }, 2000);

                    }
                }
            },
            failure: function (result) {
                alert(data.responseText);
            },
            error: function (result) {
                alert(data.responseText);
            }
        });
    }

    return blnIsValidForm;
}


function fnDeleteIPAddress(ipAddressId) {

    if (confirm('Are you sure you want to Delete IP Address?')) {

        if (ipAddressId === 0) {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeleteIPAddressById",
                data: { IPAddressId: ipAddressId },
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log("return");
                    if (result.data !== null) {
                        if (result.data.Responce === false) {
                            toastr["error"](result.data.Message);
                        }
                        else {
                            toastr["success"](result.data.Message);

                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                    }
                },
                failure: function (result) {
                    console.log(result.responseText);
                },
                error: function (result) {
                    console.log(result.responseText);
                }
            });
        }
    }

}