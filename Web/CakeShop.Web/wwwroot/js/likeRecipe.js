function likeRecipe() {
    document.getElementById("like-recipe").addEventListener("click", like);

    function like() {
        let recipeId = document.getElementById("recipe-id").innerHTML;
        var token = $("#favourite-recipes input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Recipes/Like/",
            type: "POST",
            data: JSON.stringify(recipeId),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { 'X-CSRF-TOKEN': token },
            success: function (data) {
                console.log(data);
                let result = "";

                if (data.isAdded) {
                    result = '<a class="primary-btn hover-effect-orange"><i class="far fa-heart"></i>Add to favs</a>';
                } else {
                    result = '<a class="primary-btn hover-effect-orange"><i class="fa fa-heart"></i>Remove from favs</a>';
                }
                console.log(result);
                $("#like-recipe").html(result);
            }
        });
    }
}