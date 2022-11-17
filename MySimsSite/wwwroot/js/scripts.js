

$(document).ready(function () {
    $(".removed-character").addClass("hidden");
});

function setRemovedCharactersVisibility() {
    event.preventDefault();
    var $removedCharacters = $(".removed-character");
    var $toggle = $("#RemovedCharactersToggle");
    if ($toggle.hasClass("bi-toggle-off")) {
        $removedCharacters.removeClass("hidden");
        $toggle.removeClass("bi-toggle-off");
        $toggle.addClass("bi-toggle-on");
    } else {
        $removedCharacters.addClass("hidden");
        $toggle.removeClass("bi-toggle-on");
        $toggle.addClass("bi-toggle-off");
    }
}

function setRandomSelection(id) {
    var $selector = $("#" + id + " option")
    var count = $selector.size();
    var random = getRandomArbitrary(1, count - 1);
    var value = $selector[random].value;
    $("#" + id + " option[value=" + value + "]").prop("selected", true);
}

function setRandomChronotype() {
    var random = getRandomArbitrary(1, 100);

    var selected = 0;
    if (1 < random && random <= 50) {
        selected = 1;
    } else if (50 < random && random < 100) {
        selected = 2;
    } else {
        selected = 3;
    }

    $("#chronotypeSelect option[value=" + selected + "]").prop("selected", true);
}

function getRandomArbitrary(min, max) {
    return Math.round(Math.random() * (max - min) + min);
}