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

function checkFieldIsNotEmpty(formId, fieldId) {
    var result = true;
    var $field = $("#" + formId + " #" + fieldId);
    if ($field.length > 0 && $field[0].value == "") {
        var $fieldHelp = $("#" + formId + " #" + fieldId + "Help");
        $fieldHelp.removeClass("hidden");
        result = false;
    }
    return result;
}

function validateEmail(formId) {
    var result = true;
    var $email = $("#" + formId + " #email");
    if ($email.length > 0) {
        var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
        if (!$email[0].value.match(validRegex)) {
            var $emailHelp = $("#" + formId + " #emailHelp");
            $emailHelp.removeClass("hidden");
            result = false;
        }
    }
    return result;
}

function confirmPassword(formId) {
    var result = true;
    var $confirmPassword = $("#" + formId + " #confirmPassword");
    if ($confirmPassword.length > 0) {
        var $password = $("#" + formId + " #password");
        if ($password[0].value != $confirmPassword[0].value) {
            var $passwordHelp = $("#" + formId + " #passwordConformationHelp");
            $passwordHelp.removeClass("hidden");
            result = false;
        };
    };
    return result;
}

function postForm(formId, url) {
    $.post(url, $("#" + formId).serialize())
        .done(function (response) {
            if (response.isSuccess) {
                var $successMsg = $("#" + formId + " #successMsg");
                $successMsg.removeClass("hidden");
            } else {
                var $errMsg = $("#" + formId + " #errMsg");
                $errMsg.text(response.errorMsg);
                $errMsg.removeClass("hidden");
            }
        });
}

function setActiveTab(tabId) {
    $activeTab = $(".nav-link#" + tabId);
    $activeTab.addClass("active");
}

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

    $("#loginBtn").on("click",
        function () {
            event.preventDefault();
            var isOk = true;
            var formId = "loginForm";

            var fields = ["login", "password"];

            for (var i = 0; i < 2; i++) {
                isOk = checkFieldIsNotEmpty(formId, fields[i]) && isOk;
            }
            
            if (isOk) {
                var url = "/Account/Login";
                
                $.post(url, $("#" + formId).serialize())
                    .done(function (response) {
                        if (response.isSuccess) {
                            location.reload();
                        } else {
                            var $errMsg = $("#" + formId + " #errMsg");
                            $errMsg.text(response.errorMsg);
                            $errMsg.removeClass("hidden");
                        }
                    });
            }
        });

    $("#registrationBtn").on("click",
        function () {
            event.preventDefault();
            var isOk = true;
            var formId = "registrationForm";
            
            var $successMsg = $("#successMsg");
            $successMsg.addClass("hidden");
            
            var fields = ["login", "email", "password"];
            for (var i = 0; i < 3; i++) {
                isOk = checkFieldIsNotEmpty(formId, fields[i]) && isOk;
            };
            isOk = confirmPassword(formId) && isOk;
            isOk = validateEmail(formId) && isOk;

            var url = "/Account/Registration";

            if (isOk) {
                postForm(formId, url);
            }
        });

    $("#recoverySendBtn").on("click",
        function () {
            event.preventDefault();
            var isOk = true;
            var formId = "recoveryForm";

            isOk = checkFieldIsNotEmpty(formId, "email");
            isOk = validateEmail(formId) && isOk;

            var url = "/Account/Recovery";

            if (isOk) {
                postForm(formId, url);
            }
        });


    $("#resetPassBtn").on("click",
        function () {
            event.preventDefault();
            var isOk = true;
            var formId = "resetPassForm";

            isOk = checkFieldIsNotEmpty(formId, "password");
            isOk = confirmPassword(formId) && isOk;

            var url = "/Account/ResetPassword";

            if (isOk) {
                postForm(formId, url);
            }
        });

    $("#login").on("change",
        function() {
            var $loginHelp = $("#loginHelp");
            $loginHelp.addClass("hidden");
        });

    $("#email").on("change",
        function() {
            var $emailHelp = $("#emailHelp");
            $emailHelp.addClass("hidden");
        });
    
    $("#password").on("change",
        function() {
            var $passwordHelp = $("#passwordHelp");
            $passwordHelp.addClass("hidden");
            var $passwordHelp = $("#passwordConformationHelp");
            $passwordHelp.addClass("hidden");
        });

    $("#closeBtn").on("click",
        function() {
            var $loginHelp = $("#loginHelp");
            $loginHelp.addClass("hidden");
            var $passwordHelp = $("#passwordEmptyHelp");
            $passwordHelp.addClass("hidden");
            var $passwordHelp = $("#passwordConformationHelp");
            $passwordHelp.addClass("hidden");
            var $emailHelp = $("#emailHelp");
            $emailHelp.addClass("hidden");
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