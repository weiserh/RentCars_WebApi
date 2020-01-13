$(document).ready(function () {
    var a = $(".manufacturer").toArray();
    var availableTags = [
        "Mazda",
        "Kia",
        "Hyundai","abcd"
    ];
        $("#manufacturer" ).autocomplete({
            source: availableTags
        });
});