$(document).ready(function () {
    $('#RegisterForm').submit(function () {
        var canSubmit = true;
        var userNameVMessage = $("span[data-valmsg-for='UserName']");
        var passwordVMessage = $("span[data-valmsg-for='Password']");
        var nameVMessage = $("span[data-valmsg-for='Name']");
        var emailVMessage = $("span[data-valmsg-for='Email']");

        var number = 6;
        if (!validatorObj.validate($('#UserName').val(), "length", number)) {
            userNameVMessage.show();
            userNameVMessage.text("Username has to be longer than " + number + " characters");
            $('#Form_Username').attr("class", "control-group error");
            canSubmit = false;
        }
        else {
            $('#Form_Username').attr("class", "control-group success");
            userNameVMessage.hide();
        }
        if (!validatorObj.validate($('#Password').val(), "length", number)) {
            passwordVMessage.show();
            passwordVMessage.text('Password is less than ' + number + ' characters');
            $('#Form_Password').attr("class", "control-group error");
            canSubmit =false;
        }
        else {
            $('#Form_Password').attr("class", "control-group success");
            passwordVMessage.hide();
        }
        if (!validatorObj.validate($('#Name').val(), "length", number)) {
            nameVMessage.show();
            nameVMessage.text('Name is less than ' + number + ' characters');
            $('#Form_Name').attr("class", "control-group error");
            canSubmit= false;
        }
        else {
            $('#Form_Name').attr("class", "control-group success");
            nameVMessage.hide();
        }
        var emailSize = 6;
        if (!validatorObj.validate($('#Email').val(), "email", emailSize)) {
            emailVMessage.show();
            emailVMessage.text('Email is less than ' + emailSize + ' characters or is invalid.');
            canSubmit = false;
            $('#Form_Email').attr("class", "control-group error");
        }
        else {
            $('#Form_Email').attr("class", "control-group success");
            emailVMessage.hide();
        }

        if (!canSubmit)
            return false;
    });
})