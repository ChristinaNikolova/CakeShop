function removeIngredientFromDessert(event) {
    if (event === undefined) {
        return;
    }

    var ingredientName = event.target.parentElement.parentElement.innerText;
    var dessertId = document.getElementById("dessert-id").innerHTML;

    var token = $("#form-delete-ingredient input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Administration/Desserts/RemoveIngredientFromDessert",
        type: "POST",
        data: JSON.stringify({ ingredientName, dessertId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.dessertIngredients.forEach((element) => {
                result += `<li class="remove-ingredient" onclick="removeIngredientFromDessert(event)">${element.ingredientName}<span class="cart__close"><span class="icon_close"></span></span>
                            </li>`
            });

            $('#ingredients-list').html(result);
        }
    });
};
