function removeTagFromDessert(event) {
    if (event === undefined) {
        return;
    }

    var tagName = event.target.parentElement.parentElement.innerText;
    var dessertId = document.getElementById("dessert-id").innerHTML;

    var token = $("#form-delete-tag input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/Administration/Desserts/RemoveTagFromDessert",
        type: "POST",
        data: JSON.stringify({ tagName, dessertId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            var result = "";

            data.dessertTags.forEach((element) => {
                result += `<li class="remove-tag" onclick="removeTagFromDessert(event)">${element.tagName}<span class="cart__close"><span class="icon_close"></span></span>
                            </li>`
            });

            $('#tags-list').html(result);
        }
    });
};
