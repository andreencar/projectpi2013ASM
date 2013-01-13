$(document).ready(function () {
    $('#RegisterForm').submit(function () {
        var canSubmit = true;
        var userNameVMessage = $("span[data-valmsg-for='UserName']");
        var passwordVMessage = $("span[data-valmsg-for='Password']");
        var nameVMessage = $("span[data-valmsg-for='Name']");
        var EmailVMessage = $("span[data-valmsg-for='Email']");

        if ($('#UserName').val().length <= 3) {
            userNameVMessage.show();
            userNameVMessage.text("Username has to be longer than 3 characters");
            canSubmit = false;
        }
        else {
            userNameVMessage.hide();
        }
        if ($('#Password').val().length <= 3) {
            passwordVMessage.show();
            passwordVMessage.text('Password is less than 3 characters');
            canSubmit =false;
        }
        else {
            passwordVMessage.hide();
        }
        if ($('#Name').val().length <= 3) {
            nameVMessage.show();
            nameVMessage.text('Name is less than 3 characters');
            canSubmit= false;
        }
        else { 
            nameVMessage.hide();
        }
        if ($('#Email').val().length <= 3) {
            EmailVMessage.show();
            EmailVMessage.text('Email is less than 3 characters');
            canSubmit = false;
        }
        else {
            Email.hide();
        }

        if (!canSubmit)
            return false;
    })
})