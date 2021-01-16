function likeDessert() {
    document.getElementById("like-dessert").addEventListener("click", likeDessert);

    function likeDessert() {
        let dessertId = document.getElementById("dessert-id").innerHTML;
        var token = $("#likes input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Desserts/Like/",
            type: "POST",
            data: JSON.stringify(dessertId),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { 'X-CSRF-TOKEN': token },
            success: function (data) {
                let result = "";

                if (data.isAdded) {
                    result = '<span><i class="fa fa-heart"></i></span>';
                } else {
                    result = '<span><i class="far fa-heart"></i></span>';
                }

                $("#like-dessert").html(result);
            }
        });
    }
}