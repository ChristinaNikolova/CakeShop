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