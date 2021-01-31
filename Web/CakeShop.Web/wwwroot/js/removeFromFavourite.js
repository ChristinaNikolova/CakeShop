function removeFromFavourite(event) {
    if (event === undefined) {
        return;
    }

    var token = $("#remove-from-favourite input[name=__RequestVerificationToken]").val();
    var dessertId = event.target.parentNode.id;

    $.ajax({
        url: "/Desserts/RemoveFromFavouriteDesserts/",
        type: "POST",
        data: JSON.stringify(dessertId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.favouriteDesserts.forEach((dessert) => {
                result += `<tr><td class="product__cart__item"><div class="product__cart__item__pic"><img class="small-pic" src="${dessert.picture}" alt="dessert"></div><div class="product__cart__item__text"><h6>${dessert.name}</h6></div></td><td class="cart__price">$ ${dessert.formattedPrice}</td><td class="cart__btn"><a href="/Shop/GetAllCurrentCategory/${dessert.categoryId}" class="color-black">${dessert.categoryName}</a></td><td class="cart__btn"><a href="/Shop/DessertDetails/${dessert.id}" class="site-btn">See details</a></td><td id="${dessert.id}" class="cart__close" onclick="removeFromFavourite(event)"><span class="icon_close"></span></td></tr>`;
            });

            if (result === "") {
                $('#empty-favourite-desserts').html(`<form id="remove-from-favourite" method="post"></form><h4>You don't have any favourite desserts yet! <a href="/Shop/GetAllCategories" class="color-black">Go</a> and check our desserts!</h4>`);
            }
            else {

                $('#table-body').html(result);
            }
        }
    });
}
