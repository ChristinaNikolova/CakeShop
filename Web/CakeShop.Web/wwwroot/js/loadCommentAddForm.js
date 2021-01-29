function loadCommentAddForm() {
    document.getElementById("show-comment-form").addEventListener("click", showCommentForm);

    function showCommentForm() {
        let commentForm = document.getElementById("comment-form");

        if (commentForm.style.display === "none") {
            commentForm.style.display = "block";
        } else {
            commentForm.style.display = "none";
        }
    }
}