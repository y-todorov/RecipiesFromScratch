kendo.culture("en-IE");

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}


// this is for the rad menu so root item cannot be clicked.
function OnClientItemClicking(sender, args) {
    if (args.get_item().get_items().get_count() != 0 && args.get_item().get_level() == 0) {
        args.set_cancel(true); // Cancel the event 
    }
}


function OnRequestStart() {
    isInRequest = true;
}

function OnResponseEnd() {
    isInRequest = false;
}

function exportGridData(sender) {

    var gridDiv = ($(sender)).parents('div[class~="k-grid"]').first(); // да точно така е!!!
    var grid = gridDiv.data("kendoGrid");

    $.ajax({
        type: "POST",
        url: "/Download/ExportWithOpenXML",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            html: grid.table.context.innerHTML
        }),
        success: function (data) {
        },
        error: function (result) {
            alert('Oh no: ' + result.responseText);
        },
        async: false
    });
}

function getWeekString(weekInt) {
    //debugger;
    var res;
    $.ajax({
        type: "POST",
        url: "/Chart/GetWeekString",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            dateEncoded: weekInt
        }),
        success: function (data) {
            res = data;
        },
        error: function (result) {
            alert('Oh no: ' + result.responseText);
        },
        async: false
    });

    return res;
}