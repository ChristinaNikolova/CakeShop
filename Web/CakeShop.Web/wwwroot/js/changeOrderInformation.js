function changeOrderInformation() {
    let processing = document.getElementById("processing");
    processing.addEventListener("click", showProcessingOrders);
    processing.style.backgroundColor = "#F08632";

    let approved = document.getElementById("approved");
    approved.addEventListener("click", showApprovedOrders);

    let delivered = document.getElementById("delivered");
    delivered.addEventListener("click", showDeliveredOrders);

    let cancelled = document.getElementById("cancelled");
    cancelled.addEventListener("click", showCancelledOrders);

    function showProcessingOrders() {
        document.getElementById("processing-orders").style.display = "block";
        document.getElementById("approved-orders").style.display = "none";
        document.getElementById("cancelled-orders").style.display = "none";
        document.getElementById("delivered-orders").style.display = "none";

        processing.classList.add("active");
        approved.classList.remove("active");
        delivered.classList.remove("active");
        cancelled.classList.remove("active");

        processing.style.backgroundColor = "#F08632";
        approved.style.backgroundColor = "";
        delivered.style.backgroundColor = "";
        cancelled.style.backgroundColor = "";
    }

    function showApprovedOrders() {
        document.getElementById("processing-orders").style.display = "none";
        document.getElementById("approved-orders").style.display = "block";
        document.getElementById("delivered-orders").style.display = "none";
        document.getElementById("cancelled-orders").style.display = "none";

        processing.classList.remove("active");
        approved.classList.add("active");
        delivered.classList.remove("active");
        cancelled.classList.remove("active");

        processing.style.backgroundColor = "";
        approved.style.backgroundColor = "#F08632";
        delivered.style.backgroundColor = "";
        cancelled.style.backgroundColor = "";
    }

    function showDeliveredOrders() {
        document.getElementById("processing-orders").style.display = "none";
        document.getElementById("approved-orders").style.display = "none";
        document.getElementById("delivered-orders").style.display = "block";
        document.getElementById("cancelled-orders").style.display = "none";

        processing.classList.remove("active");
        approved.classList.remove("active");
        delivered.classList.add("active");
        cancelled.classList.remove("active");

        processing.style.backgroundColor = "";
        approved.style.backgroundColor = "";
        delivered.style.backgroundColor = "#F08632";
        cancelled.style.backgroundColor = "";
    }

    function showCancelledOrders() {
        document.getElementById("processing-orders").style.display = "none";
        document.getElementById("approved-orders").style.display = "none";
        document.getElementById("delivered-orders").style.display = "none";
        document.getElementById("cancelled-orders").style.display = "block";

        processing.classList.remove("active");
        approved.classList.remove("active");
        delivered.classList.remove("active");
        cancelled.classList.add("active");

        processing.style.backgroundColor = "";
        approved.style.backgroundColor = "";
        delivered.style.backgroundColor = "";
        cancelled.style.backgroundColor = "#F08632";
    }
}
