function seeBasket() {
    [...document.getElementsByClassName("remove-from-basket")].forEach((element) => {
        element.addEventListener("click", removeFromBasket);
    });

    function removeFromBasket(event) {
        var id = event.target.id;

        var token = $("#remove-dessert-from-basket input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Orders/RemoveFromBasket/",
            type: "POST",
            data: JSON.stringify({ id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { 'X-CSRF-TOKEN': token },
            success: function (data) {
                var result = "";

                data.dessertsInBasket.forEach((dessert) => {
                    result += `<tr>
                                    <td class="product__cart__item">
                                        <div class="product__cart__item__pic">
                                            <a href="/Shop/DessertDetails/${dessert.dessertId}">
                                                <img class="small-pic" src="${dessert.dessertPicture}" alt="dessert">
                                            </a>
                                        </div>
                                        <div class="product__cart__item__text">
                                            <h6><a href="/Shop/DessertDetails/${dessert.dessertId}" class="color-black">${dessert.dessertName}</a></h6>
                                            <h5>$ ${dessert.formattedDessertPrice}</h5>
                                        </div>
                                    </td>
                                    <td class="quantity__item">
                                        <div class="quantity">
                                            <div class="dessert-quantity">
                                                ${dessert.quantity}
                                            </div>
                                        </div>
                                    </td>
                                    <td class="cart__price">$ ${dessert.formattedTotalPrice}</td>
                                    <td class="cart__close"><span id="${dessert.id}" class="icon_close remove-from-basket" onclick="removeFromBasket(event);"></span></td>
                                </tr>`
                });

                if (!result) {
                    $('#empty-result').html(`<h5>You don't have any desserts in your basket yet!<a href="/Shop/GetAllCategories" class="color-black"> Go</a> and check out desserts!</h5>`);
                }
                else {
                    $('#desserts-in-basket').html(result);
                }
            }
        });
    };
}