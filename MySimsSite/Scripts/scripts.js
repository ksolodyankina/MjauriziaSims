

// A $( document ).ready() block.
$(document).ready(function () {
    $("#goalRandomizeButton").click(function () {
        var count = $("#goalSelect option").size();
        var random = getRandomArbitrary(1, count - 1);
        $("#goalSelect option[value=" + random +"]").prop("selected", true);
    });

    function getRandomArbitrary(min, max) {
        return Math.round(Math.random() * (max - min) + min);
    }
});