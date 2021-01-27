function removeIngredientFromRecipe(event) {
    if (event === undefined) {
        return;
    }

    var ingredientName = event.target.parentElement.parentElement.innerText;
    var index = ingredientName.indexOf(":");
    ingredientName = ingredientName.substring(0, index);

    var recipeId = document.getElementById("recipe-id").innerText;

    var token = $("#form-delete-ingredient input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Administration/Recipes/RemoveIngredientFromRecipe",
        type: "POST",
        data: JSON.stringify({ ingredientName, recipeId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.recipeIngredients.forEach((element) => {
                result += `<li class="remove-ingredient" onclick="removeIngredientFromRecipe(event)">
                            ${element.ingredientName}: ${element.quantity}
                            <span class="cart__close"><span class="icon_close"></span></span>
                        </li>`
            });

            $('#ingredients-list').html(result);
        }
    });
};
