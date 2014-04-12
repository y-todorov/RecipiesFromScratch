kendo.culture("en-IE");

$(document).ready(function () {
    $("label").addClass("k-label");
    $("input[type=text]").addClass("k-textbox");
    $("input[type=password]").addClass("k-textbox");
    $("input[type=submit]").addClass("k-button");
});

function onKendoGridDataSourceError(e) {
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

function onKendoGridSave(e) {
    debugger;
    var popupNotification = $("#popupNotification").data("kendoNotification");
    for (var key in e.values) {
        var value = e.values[key];
        popupNotification.show("The value of '" + key + "' changed to '" + value + "'!");
    }
}

function onKendoGridSaveChanges(e) {
    debugger;
    var popupNotification = $("#popupNotification").data("kendoNotification");
    popupNotification.show("All pending changes are successfully saved!");
}

function onKendoGridRemove(e) {
    debugger;
    var popupNotification = $("#popupNotification").data("kendoNotification");
    popupNotification.show("Item removed! Press 'Save Changes' to delete the item from the database!");
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

