

$(document).ready(function () {
    $(".removed-character").addClass("hidden");

    $(".choice-option").on("click", function () {
        event.preventDefault();
        $option = $(this);
        if ($option.hasClass("choice-option--enabled")) {
            disableChoiceOption($option);
        } else {
            enableChoiceOption($option);
        }
    });

    $(".choice-option-switch").on("click", function () {
        event.preventDefault();
        $switch = $(this);
        var isDisable = $switch.hasClass("choice-option-switch--enabled")
        var currentState = isDisable ? "enabled" : "disabled"
        var newState = isDisable ? "disabled" : "enabled"
        $(".choice-option-switch__i--" + currentState).addClass("hidden");
        $(".choice-option-switch__text--" + currentState).addClass("hidden");
        $(".choice-option-switch__i--" + newState).removeClass("hidden");
        $(".choice-option-switch__text--" + newState).removeClass("hidden");
        $switch.removeClass("choice-option-switch--" + currentState)
        $switch.addClass("choice-option-switch--" + newState)
        var $options = $(".choice-option--" + currentState);
        for (i = 0; i < $options.size(); i++) {
            var $option = $($options[i]);
            if (isDisable) {
                disableChoiceOption($option);
            } else {
                enableChoiceOption($option);
            }
        }
    });
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

function showRandomizedValue() {
    $activeOptions = $(".choice-option--enabled");
    var count = $activeOptions.size();
    var random = getRandomArbitrary(0, count - 1);
    var selectedOption = $($activeOptions[random]).find("img")[0];
    var img = selectedOption.src;
    var title = selectedOption.title;
    var $selectedOptionBox = $(".selected-option-box");
    $selectedOptionBox.find("i").addClass("hidden");
    var $img = $selectedOptionBox.find("img");
    $img.removeClass("hidden");
    $img[0].src = img;
    $img[0].title = title;
}

function enableChoiceOption($option) {
    $option.removeClass("choice-option--disabled");
    $option.addClass("choice-option--enabled");
    $option.find("i").addClass("hidden");
    $option.find("img").removeClass("hidden");
}

function disableChoiceOption($option) {
    $option.removeClass("choice-option--enabled");
    $option.addClass("choice-option--disabled");
    $option.find("i").removeClass("hidden");
    $option.find("img").addClass("hidden");
}