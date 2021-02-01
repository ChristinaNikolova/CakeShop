function loadBasketTotalPrice() {
    var token = $("#load-basket-total-price input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Orders/GetTotalPrice/",
        type: "POST",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            $('.total-price').html('$' + `${data.formatTotalPrice}`);
        }
    });
};

function checkForBellNotification() {
    var token = $("#check-for-bell-notification input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Users/BellNotification/",
        type: "POST",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var bell = document.getElementById("bell-layout");
            var smallBell = document.getElementById("bell-layout-small");
            if (data) {
                bell.style.display = "inline-block";
                smallBell.style.display = "inline-block";
            } else {
                bell.style.display = "none";
                smallBell.style.display = "none";
            }
        }
    });
}