$(document).ready(function () {
    $('#loading').addClass('hide');
    $('#erorr-message').hide();
    $('#success-message').hide();
});
function StartLoading() {
    $('#loading').removeClass('hide');
}

function EndLoading() {
    $('#loading').addClass('hide');
}
async function CreateAccount() {
    StartLoading();
    $('#erorr-message').hide();
    $('#success-message').hide();
    if (await CheckPassword() == false) {
        $('#erorr-message').text("Password mismatch");
        $('#erorr-message').show();
        EndLoading();
        return;
    }
    if (!(await CheckLogin())) {
        $('#erorr-message').text("User with this login already exists.");
        $('#erorr-message').show();
        EndLoading();
        return;
    }
   var result = await $.ajax({
        type: 'POST',
        url: '/Account/Register',
        data: {
            Login: $('#registration-login').val(),
            Password: $('#registration-password').val(),
            RepeatPassword: $('#registration-repeat-password').val(),
        }
   });
    if (result) {
        $('#success-message').show();
        $('#registration-login').val('');
        $('#registration-password').val('');
        $('#registration-repeat-password').val('');
    }
    EndLoading();
}

async function CheckPassword() {
    if ($('#registration-password').val() == $('#registration-repeat-password').val()) {
        return true;
    } else return false;
}

async function CheckLogin() {
    var result = await  $.ajax({
        type: 'GET',
        url: '/account/LoginExist',
        data: {
             login: $('#registration-login').val()
        }
    });
    return !result;
}

async function Login() {
    $("#erorr-message").hide();
    var result = await  $.ajax({
        type: 'POST',
        url: '/account/Login',
        data: {
            Login: $('#login-login').val(),
            Password: $('#login-password').val()
        }
    });
    if (result) {
        window.location.replace('/home/index');
    } else {
        $("#erorr-message").text("Login failed");
        $("#erorr-message").show();
    }
}