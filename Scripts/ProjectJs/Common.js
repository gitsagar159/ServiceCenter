$(function () {

    var isRtl = $('body').attr('dir') === 'rtl' || $('html').attr('dir') === 'rtl';

    $(".datepicker-features").each(function (e) {

        var objThis = this
        
        $(objThis).datepicker({
            format: "dd/mm/yyyy",
            calendarWeeks: true,
            todayBtn: 'linked',
            //daysOfWeekDisabled: '1',
            clearBtn: true,
            todayHighlight: true,
            //multidate: true,
            //daysOfWeekHighlighted: '1,2',
            orientation: isRtl ? 'auto left' : 'auto right',

            beforeShowMonth: function (date) {
                if (date.getMonth() === 8) {
                    return false;
                }
            },

            beforeShowYear: function (date) {
                if (date.getFullYear() === 2014) {
                    return false;
                }
            },

            /*change: function (date) {
                $(objThis).hide();
            }*/
        }).on('change', function () {
            $('.datepicker').hide();
        });;
    });

});


function fnValidateFormById(formId) {

    var IsValid = true;
    var objectId = "";

    $("#" + formId).find(".control-required").each(function () {

        var currentObj = $(this);

        var currentElementType = currentObj.prop('nodeName');

        console.log(currentObj.attr("name") + " " + currentObj.val());

        if (currentElementType === 'INPUT') {
            
            if (currentObj.val() === "") {
                currentObj.addClass("is-invalid");
                currentObj.next().addClass("invalid-feedback").show();

                objectId = objectId !== "" ? currentObj.attr("id") : objectId;

                IsValid = false;
            }
            else {
                if (currentObj.hasClass('mob-no') && currentObj.val().length !== 10) {
                    currentObj.addClass("is-invalid");
                    currentObj.next().addClass("invalid-feedback").show().html("Please Enter 10 digit Mobile Number");
                    IsValid = false;
                }
                else {
                    currentObj.removeClass("is-invalid");
                    currentObj.next().removeClass("invalid-feedback").hide().html("Please Enter Mobile Number");
                }

            }

        }
        else if (currentElementType === 'SELECT') {

            if (currentObj.val() === '' || currentObj.val() == null || currentObj.val() == undefined) {
                currentObj.addClass("is-invalid");
                var currentElementName = currentObj.attr("id");
                //currentObj.next("#" + currentElementName + "_ToolTip").addClass("invalid-feedback").show();
                $("#" + currentElementName + "_ToolTip").addClass("invalid-feedback").show();
                IsValid = false;
            }
            else {
                currentObj.removeClass("is-invalid");
                //currentObj.next().removeClass("invalid-feedback").hide();
                $("#" + currentElementName + "_ToolTip").addClass("invalid-feedback").show();
            }

        }
    });

    //$("#" + objectId).focus();

    return IsValid;


}


function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
