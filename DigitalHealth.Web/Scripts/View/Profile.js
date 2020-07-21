$(document).ready(async function () {
  
    $('#erorr-message').hide();
    $('#success-message').hide();
    await GetMyProfile();
    $('#loading').addClass('hide');
});
function StartLoading() {
    $('#loading').removeClass('hide');
}

function EndLoading() {
    $('#loading').addClass('hide');
}

async function GetMyProfile() {
    var result = await $.ajax({
        type: 'GET',
        url:'/Account/GetMyProfile'
    });
    $('#profile-firstname').val(result.FirstName);
    $('#profile-lastname').val(result.LastName);
    $('#profile-middlename').val(result.MiddleName);
    $('#profile-gender').val(result.Gender);
    $('#profile-id').val(result.Id);
}
async function UpdateProfile() {
    StartLoading();
    $('#success-message').hide();
    await $.ajax({
        type: 'POST',
        url: '/account/UpdateProfile',
        data: {
           FirstName: $('#profile-firstname').val(),
           LastName: $('#profile-lastname').val(),
           MiddleName: $('#profile-middlename').val(),
           Gender: $('#profile-gender').val(),
           Id: $('#profile-id').val()
        }
    });
    $('#success-message').show();
    EndLoading();
}