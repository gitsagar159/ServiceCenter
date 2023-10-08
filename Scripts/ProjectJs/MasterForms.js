

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




function InsertUpdatePart() {

    var blnIsValidForm = true;

    var PartName = $("#PartName").val();
    var PartKeyword = $("#PartKeyword").val();

    if (PartName === "") {
        $("#PartName").addClass("is-invalid");
        $("#PartNameToolTip").addClass("invalid-feedback").html("Please Enter Part Name");

        blnIsValidForm = false;
    }
    else {

        var objPart = new Object();

        objPart.PartId = $("#PartId").val();
        objPart.PartName = PartName;
        objPart.Company = $("#Company").val();
        objPart.PartKeyword = PartKeyword;


        $.ajax({
            type: "POST",
            url: "/Master/PartForm",
            data: JSON.stringify(objPart),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#PartName").addClass("is-invalid");
                        $("#PartNameToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/PartList";
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


function fnDeletePart(PartId) {

    if (confirm('Are you sure you want to Delete Part?')) {

        if (PartId === "") {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeletePartById",
                data: { PartId: PartId },
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


// Category - Start


function InsertUpdateCategory() {

    var blnIsValidForm = true;

    var CategoryName = $("#CategoryName").val();
    var CategoryKeyword = $("#CategoryKeyword").val();

    if (CategoryName === "") {
        $("#CategoryName").addClass("is-invalid");
        $("#CategoryNameToolTip").addClass("invalid-feedback").html("Please Enter Category Name");

        blnIsValidForm = false;
    }
    else {

        var objCategory = new Object();

        objCategory.CategoryId = $("#CategoryId").val();
        objCategory.CategoryName = CategoryName;
        objCategory.Company = $("#Company").val();
        objCategory.CategoryKeyword = CategoryKeyword;


        $.ajax({
            type: "POST",
            url: "/Master/CategoryForm",
            data: JSON.stringify(objCategory),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#CategoryName").addClass("is-invalid");
                        $("#CategoryNameToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/CategoryList";
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


function fnDeleteCategory(CategoryId) {

    if (confirm('Are you sure you want to Delete Category?')) {

        if (CategoryId === "") {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeleteCategoryById",
                data: { CategoryId: CategoryId },
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

// Category - End




// Company - Start


function InsertUpdateCompany() {

    var blnIsValidForm = true;

    var companyName = $("#CompanyName").val();
    var categoryId = $("#CategoryId").val();
    var serviceName = $("#ServiceName").val();
    var serviceChargeLocal = $("#ServiceChargeLocal").val();
    var serviceChargeOutStation = $("#ServiceChargeOutStation").val();

    if (companyName === "") {
        $("#CompanyName").addClass("is-invalid");
        $("#CompanyNameToolTip").addClass("invalid-feedback").html("Please Enter Company Name");

        blnIsValidForm = false;
    }

    if (categoryId === "") {
        $("#CategoryId").addClass("is-invalid");
        $("#CategoryIdToolTip").addClass("invalid-feedback").html("Please Enter Category Name");

        blnIsValidForm = false;
    }

    if (serviceName === "") {
        $("#ServiceName").addClass("is-invalid");
        $("#ServiceNameToolTip").addClass("invalid-feedback").html("Please Enter Service Name");

        blnIsValidForm = false;
    }

    if (serviceChargeLocal === "") {
        $("#ServiceChargeLocal").addClass("is-invalid");
        $("#ServiceChargeLocalToolTip").addClass("invalid-feedback").html("Please Enter Service Charge Local");

        blnIsValidForm = false;
    }

    if (serviceChargeOutStation === "") {
        $("#ServiceChargeOutStation").addClass("is-invalid");
        $("#ServiceChargeOutStationToolTip").addClass("invalid-feedback").html("Please Enter Service Charge Out-Station Name");

        blnIsValidForm = false;
    }

    if (blnIsValidForm) {

        var objCompany = new Object();

        objCompany.CompanyName = companyName;
        objCompany.CategoryId = categoryId;
        objCompany.ServiceName = serviceName;
        objCompany.serviceChargeLocal = serviceChargeLocal;
        objCompany.serviceChargeOutStation = serviceChargeOutStation;


        $.ajax({
            type: "POST",
            url: "/Master/CompanyForm",
            data: JSON.stringify(objCompany),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("return");
                if (result.data !== null) {
                    if (result.data.Responce === false) {
                        $("#CompanyName").addClass("is-invalid");
                        $("#CompanyNameToolTip").addClass("invalid-feedback").html(result.data.Message);
                        blnIsValidForm = false;
                    }
                    else {
                        toastr["success"](result.data.Message);

                        setTimeout(function () {
                            window.location.href = "/Master/CompanyList";
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


function fnDeleteCompany(companyId) {

    if (confirm('Are you sure you want to Delete Company?')) {

        if (companyId === "") {

            toastr["error"]("Something went wrong");
        }
        else {

            $.ajax({
                type: "POST",
                url: "/Master/DeleteCompanyById",
                data: { CompanyId: companyId },
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

// Company - End