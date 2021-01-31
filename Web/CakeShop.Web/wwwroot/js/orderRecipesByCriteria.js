function orderRecipesByCriteria() {
    var criteria = "";
    var criterias = [...document.getElementById("order-recipes").children];

    criterias.forEach((element) => {
        if (element.selected) {
            criteria = element.value;
        }
    });

    if (criteria === "removeFilter") {
        window.location.replace(`/Recipes/GetAll`);
    }

    var token = $("#order-criterias-recipes input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Recipes/OrderByCriteria/",
        type: "POST",
        data: JSON.stringify(criteria),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.repices.forEach((recipe) => {
                result += `<div class="blog__item">
                                  <div class="blog__item__pic set-bg" style="background-image:url(${recipe.picture});">
                                     <div class="blog__pic__inner">
                                         <div class="label">${recipe.categoryName}</div>
                                             <ul>
                                                <li>By <span>${recipe.author}</span></li>
                                                <li>${recipe.createdOn}</li>
                                                <li><i class="far fa-thumbs-up"></i> ${recipe.recipeLikesCount}</li>
                                                <li><i class="far fa-comments"></i> ${recipe.commentsCount}</li>
                                             </ul>
                                          </div>
                                      </div>
                                      <div class="blog__item__text">
                                          <h2>${recipe.title}</h2>
                                          <p>${recipe.shortContent}</p>
                                          <a href="/Recipes/RecipeDetails/${recipe.id}">READ MORE</a>
                                      </div>
                                   </div>`;
            });

            $("#all-recipes").html(result);
        }
    });
}