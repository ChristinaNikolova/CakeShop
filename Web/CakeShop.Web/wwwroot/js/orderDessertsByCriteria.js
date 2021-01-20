function orderDessertsByCriteria() {
    var targetCriteria = "";
    var criterias = [...document.getElementById("order-dessert").children];

    criterias.forEach((criteria) => {
        if (criteria.selected) {
            targetCriteria = criteria.value;
        }
    });

    var token = $("#order-criterias input[name=__RequestVerificationToken]").val();
    var categoryId = document.getElementById("category-id").innerHTML;

    $.ajax({
        url: "/Shop/OrderByCriteria/",
        type: "POST",
        data: JSON.stringify({ targetCriteria, categoryId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data.targetCriteria === "removeFilter") {
                window.location.replace(`/Shop/GetAllCurrentCategory/${categoryId}`);
            }

            var result = "";

            data.orderedDesserts.forEach((dessert) => {
                result += `<div class="col-lg-3 col-md-6 col-sm-6">
                    <div class="product__item">
                        <div class="product__item__pic set-bg" style="background-image:url(${dessert.picture});">
                            <div class="product__label">
                                <span>${dessert.categoryName}</span>
                            </div>
                        </div>
                        <div class="product__item__text">
                            <h6><a href="/Shop/DessertDetails/${dessert.id}">${dessert.name}</a></h6>
                            <div class="product__item__price">$ ${dessert.formattedPrice}</div>
                            <div class="cart_add">
                                <a href="/Shop/DessertDetails/${dessert.id}">See details</a>
                            </div>
                        </div>
                    </div>
                </div>`;
            });

            $("#desserts").html(result);
            document.getElementById("pagination").style.display = "none";
        }
    });
}
