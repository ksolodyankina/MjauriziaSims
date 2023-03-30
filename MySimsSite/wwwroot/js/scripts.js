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
    var $selectedOptionText = $(".selected-option-text");
    $selectedOptionText.empty();
    $selectedOptionText.append(title);
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

function filterByPack(pack) {
    event.preventDefault();
    $option = $(".filter-" + pack);
    $choiceOptions = $(".pack-" + pack);
    if ($option.hasClass("filter-option--enabled")) {
        $option.removeClass("filter-option--enabled");
        $option.addClass("filter-option--disabled");
        $option.find("i").removeClass("hidden");
        for (var i = 0; i < $choiceOptions.length; i++) {
            disableChoiceOption($($choiceOptions[i]));
        }
    } else {
        $option.removeClass("filter-option--disabled");
        $option.addClass("filter-option--enabled");
        $option.find("i").addClass("hidden");
        for (var i = 0; i < $choiceOptions.length; i++) {
            enableChoiceOption($($choiceOptions[i]));
        }
    }
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

function setGoalsSelectOptions() {
    var $goalSelect = $("#goalSelect");
    if ($goalSelect.length > 0) {
        var goalInfo = JSON.parse($goalSelect.attr("data"));
        for (var i = 0; i < goalInfo["child"].length; i++) {
            $("#goalSelect option[value=" + goalInfo["child"][i] + "]").addClass("child-goal-option");
        }
        for (var i = 0; i < goalInfo["adult"].length; i++) {
            $("#goalSelect option[value=" + goalInfo["adult"][i] + "]").addClass("adult-goal-option");
        }
    }
}

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

function setBlockVisibility(block) {
    event.preventDefault();
    var $block = $("#" + block);
    var $showButton = $("#blockShow-" + block);
    var $hideButton = $("#blockHide-" + block);
    if ($block.hasClass("hidden")) {
        $block.removeClass("hidden");
        $showButton.addClass("hidden");
        $hideButton.removeClass("hidden");
    } else {
        $block.addClass("hidden");
        $hideButton.addClass("hidden");
        $showButton.removeClass("hidden");
    };
}

function setPreference(preference, isLike) {
    event.preventDefault();
    var $likeBtn = $("#like-" + preference);
    var $dislikeBtn = $("#dislike-" + preference);
    var $likesOption = $("#likesSelect option[value = " + preference + "]");
    var $dislikesOption = $("#dislikesSelect option[value = " + preference + "]");
    if (isLike) {
        var $targetBtn = $likeBtn;
        var $secondaryBtn = $dislikeBtn;
        var $targetOption = $likesOption;
        var $secondaryOption = $dislikesOption;
    } else {
        var $targetBtn = $dislikeBtn;
        var $secondaryBtn = $likeBtn;
        var $targetOption = $dislikesOption;
        var $secondaryOption = $likesOption;
    }
    if ($targetBtn.hasClass("active")) {
        $targetOption.removeAttr("selected");
        $targetBtn.removeClass("active");
        removePreferenceValue(preference);
    } else {
        $targetOption.attr("selected", true);
        $targetBtn.addClass("active");
        $secondaryOption.removeAttr("selected");
        $secondaryBtn.removeClass("active");
        removePreferenceValue(preference);
        showPreferenceValue(preference, isLike);
    }
}

function setLike(preference) {
    setPreference(preference, true);
}

function setDislike(preference) {
    setPreference(preference, false);
}

function setActivePreferences() {
    var $likesOptions = $("#likesSelect option[selected]");
    for (var i = 0; i < $likesOptions.length; i++) {
        var preference = $likesOptions[i].value;
        var $likeBtn = $("#like-" + preference);
        $likeBtn.addClass("active");
        showPreferenceValue(preference, true);
    }
    var $dislikesOptions = $("#dislikesSelect option[selected]");
    for (var i = 0; i < $dislikesOptions.length; i++) {
        var preference = $dislikesOptions[i].value;
        var $dislikeBtn = $("#dislike-" + preference);
        $dislikeBtn.addClass("active");
        showPreferenceValue(preference, false);
    }
}

function showPreferenceValue(preference, isLike) {
    var template = '<img width = "30px" height = "30px" id="blockValue-' + preference +
        '" class="rounded border border-3 border-' + (isLike ? 'success' : 'danger') + '"/>';
    var $preference = $("#preference-" + preference);
    var category = $preference.parents(".preference-block").prop("id");
    var $blockValues = $("#block" + (isLike ? "Likes" : "Dislikes") + "-" + category);
    $blockValues.append(template);
    var $blockValue = $("#blockValue-" + preference);
    $blockValue.prop("src", $preference.prop("src"));
    $blockValue.prop("title", $preference.prop("title"));
}

function removePreferenceValue(preference) {
    $("#blockValue-" + preference).remove();
}

function setRandomBlockValue(category) {
    event.preventDefault();
    var $selectedLikesOptions = $("#likesSelect option[selected]");
    for (var i = 0; i < $selectedLikesOptions.length; i++) {
        var preference = $selectedLikesOptions[i].value;
        var $blockValue = $("#blockValue-" + preference);
        if ($blockValue.parents("#blockLikes-" + category).length > 0) {
            $blockValue.remove();
            var $likeBtn = $("#like-" + preference);
            $likeBtn.removeClass("active");
            $($selectedLikesOptions[i]).removeAttr("selected");
        }
    }
    var $selectedDislikesOptions = $("#dislikesSelect option[selected]");
    for (var i = 0; i < $selectedDislikesOptions.length; i++) {
        var preference = $selectedDislikesOptions[i].value;
        var $blockValue = $("#blockValue-" + preference);
        if ($blockValue.parents("#blockDislikes-" + category).length > 0) {
            $blockValue.remove();
            var $dislikeBtn = $("#dislike-" + preference);
            $dislikeBtn.removeClass("active");
            $($selectedDislikesOptions[i]).removeAttr("selected");
        }
    }
    var $likes = $("#" + category + " .preference-like-icon");
    var $dislikes = $("#" + category + " .preference-dislike-icon");
    var count = $likes.size();
    var dislikesCount = getRandomArbitrary(0, 2);
    for (var i = 1; i <= dislikesCount; i++) {
        var random = getRandomArbitrary(0, $dislikes.length - 1);
        $($dislikes[random]).trigger("click");
    };
    var likesCount = getRandomArbitrary(1, 2);
    for (var i = 1; i <= likesCount; i++) {
        var random = getRandomArbitrary(0, $likes.length - 1);
        if (!$($likes[random]).hasClass("active")) {
            $($likes[random]).trigger("click");
        }
    };
}

function preferencesPanelCollapse(id) {
    event.preventDefault();
    var $preferencesPanel = $("#preferencesPanel-" + id);
    var isHidden = $preferencesPanel.hasClass("hidden");
    if (isHidden) {
        $preferencesPanel.removeClass("hidden");
        $("#panelShow-" + id).addClass("hidden");
        $("#panelHide-" + id).removeClass("hidden");
    } else {
        $preferencesPanel.addClass("hidden");
        $("#panelShow-" + id).removeClass("hidden");
        $("#panelHide-" + id).addClass("hidden");
    }
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

    $(".localeSwitcher").on("click",
        function() {
            event.preventDefault();
            var url = "";
            if (this.id == "localeSwitcherEn") {
                url = "/Locale/SetLocale?culture=en";
            } else {
                url = "/Locale/SetLocale?culture=ru";
            }
            $.get(url)
                .done(function () {
                    location.reload();
                });
        });

    $("#myFamiliesCheckbox").on("click",
        function() {
            location.href = $(this).attr("link");
        });

    setGoalsSelectOptions();

    var ages = ["baby", "toddler", "child", "teen", "young", "adult", "old"];

    $("#ageSelect").on("change",
        function () {
            var age = this.value;
            for (var i = 0; i <= ages.length; i++) {
                if (i <= age) {
                    $(".min-age-" + ages[i]).removeClass("hidden");
                } else {
                    $(".min-age-" + ages[i]).addClass("hidden");
                }
            };

            if (ages[age] == "child") {
                $(".child-goal-option").removeClass("hidden");
                $(".adult-goal-option").addClass("hidden");
            } else {
                $(".child-goal-option").addClass("hidden");
                $(".adult-goal-option").removeClass("hidden");
            }
        });

    $("#ageSelect").trigger("change");

    $("#parent1Select, #parent2Select, #partnerSelect").on("change",
        function () {
            var parent1 = $("#parent1Select")[0].value;
            var parent2 = $("#parent2Select")[0].value;
            var partner = $("#partnerSelect")[0].value;

            if (parent1 > 0 || parent2 > 0 || partner > 0) {
                $(".generation-select").addClass("hidden");
            } else {
                $(".generation-select").removeClass("hidden");
            }
        });

    $("#parent1Select").trigger("change");

    setActivePreferences();
});