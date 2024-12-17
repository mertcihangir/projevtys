$("#profileBtn").on("click", function() {
    $.ajax({
        url: "/Home/MyProfile",
        method: "GET",
        success: function (response) {
            $("#PartialBody").html(response);
        }
    })
});

$(".userNavBtn").on("click", function () {
    if ($(this).hasClass("active")) {
        return;
    }

    $(".userNavBtn.active").removeClass("active");
    $(this).addClass("active");

    const url = $(this).data("url");

    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $("#PartialBody").html(response);
        }
    })
});