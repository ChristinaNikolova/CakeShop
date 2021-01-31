function removeFromFavouriteRecipes(event) {
    if (event === undefined) {
        return;
    }

    var token = $("#remove-from-favourite-recipes input[name=__RequestVerificationToken]").val();
    var recipeId = event.target.parentNode.id;

    $.ajax({
        url: "/Recipes/RemoveFromFavouriteRecipes/",
        type: "POST",
        data: JSON.stringify(recipeId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.favouriteRecipes.forEach((recipe) => {
                result += `<tr><td class="product__cart__item"><div class="product__cart__item__pic"><img class="small-pic" src="${recipe.picture}" alt="recipe-pic"></div><div class="product__cart__item__text"><h6>${recipe.title}</h6></div></td><td class="cart__price">${recipe.categoryName}</td><td class="cart__btn"><a href="/Recipes/RecipeDetails/${recipe.id}" class="site-btn">Read more</a></td><td id="${recipe.id}" class="cart__close" onclick="removeFromFavouriteRecipes(event)"><span class="icon_close"></span></td></tr>`;
            });

            if (result === "") {
                $('#empty-favourite-recipes').html(`<form id="remove-from-favourite-recipes" method="post"></form><h4>You don't have any favourite recipes yet! <a href="/Recipes/GetAll" class="color-black">Go</a> and check our recipes!</h4>`);
            }
            else {

                $('#table-body').html(result);
            }
        }
    });
}