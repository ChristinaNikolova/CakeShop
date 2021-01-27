function getUserDataButton() {
    var button = document.getElementById("user-data");
    button.addEventListener("click", showUserData);

    function showUserData() {
        var information = document.getElementById("show-data");

        if (information.style.display === "none") {
            information.style.display = "block";
            button.innerText = "Close client's data";
        } else {
            information.style.display = "none";
            button.innerText = "Show client's data";
        }
    }
}