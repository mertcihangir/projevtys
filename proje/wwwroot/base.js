$("#profileBtn").on("click", function () {
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

$(document).on("click", ".courseSelectBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");

    const selectedCourse = $("#unselectedCourse_" + id);
    selectedCourse.detach();
    selectedCourse.attr("id", "selectedCourse_" + id);
    selectedCourse.find("button").removeClass("btn-success courseSelectBtn").addClass("btn-danger courseUnselectBtn");
    selectedCourse.find("button").text("Çıkart");

    $("#courseSelectedTable").append(selectedCourse);
});

$(document).on("click", ".courseUnselectBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");

    const selectedCourse = $("#selectedCourse_" + id);
    selectedCourse.detach();
    selectedCourse.attr("id", "unselectedCourse_" + id);
    selectedCourse.find("button").removeClass("btn-danger courseUnselectBtn").addClass("btn-success courseSelectBtn");
    selectedCourse.find("button").text("Ekle")

    $("#courseUnselectedTable").append(selectedCourse);
});

$(document).on("click", "#selectedCoursesSave", function (e) {
    e.preventDefault();
    debugger;
    var selectedCoursesIds = [];
    $("#courseSelectedTable").find("tr").each(function () {
        var trTag = $(this);
        const selectedCourseId = trTag.data("id");
        selectedCoursesIds.push(selectedCourseId);
    });

    $.ajax({
        url: "/Students/SelectCourses",
        method: "POST",
        data: { SelectedCoursesIds: JSON.stringify(selectedCoursesIds) },
        success: function (response) {
            if (response.success) {
                $(".courseSelectBtn").each(function () {
                    $(this).attr("disabled", true);
                });
                $(".courseUnselectBtn").each(function () {
                    $(this).attr("disabled", true);
                });

                $("#selectedCoursesSave").attr("disabled", true);
                $("#confirmMessage").show();
            }

            alert(response.message);
        }
    })
});

$(document).on("click", ".studentCourseConfirmBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");

    $.ajax({
        url: "Teacher/ConfirmCourses",
        method: "POST",
        data: { Id: id },
        success: function (response) {
            if (response.success) {
                $("#studentCourse_" + id).remove();
            }

            alert(response.message);
        }
    })
});

$(document).on("click", ".studentCourseRejectBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");

    $.ajax({
        url: "Teacher/RejectCourses",
        method: "POST",
        data: { Id: id },
        success: function (response) {
            if (response.success) {
                $("#studentCourse_" + id).remove();
            }

            alert(response.message);
        }
    })
});