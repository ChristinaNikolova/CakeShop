function addDessertToBasket() {
    document.getElementById("add-to-card").addEventListener("click", addToCard);

    function addToCard() {
        let dessertId = document.getElementById("dessert-id").innerHTML;
        let quantity = document.getElementById("quantity").value;

        var token = $("#order input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Orders/AddToBasket/",
            type: "POST",
            data: JSON.stringify({ dessertId, quantity }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { 'X-CSRF-TOKEN': token },
            success: function (data) {
                $('.total-price').html('$' + `${data.formatTotalPrice}`);

                if (data.isSuccess) {
                    document.getElementById('added-message-error').style.display = "none";
                    document.getElementById('added-message-success').style.display = "block";
                    $('#added-message-success').html(data.message);
                }
                else {
                    document.getElementById('added-message-success').style.display = "none";
                    document.getElementById('added-message-error').style.display = "block";
                    $('#added-message-error').html(data.message);
                }
            }
        });
    }
}