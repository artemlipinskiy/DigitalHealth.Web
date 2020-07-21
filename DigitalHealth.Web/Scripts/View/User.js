$(document).ready(function () {
    $('#loading').addClass('hide');
});
function StartLoading() {
    $('#loading').removeClass('hide');
}

function EndLoading() {
    $('#loading').addClass('hide');
}
function Cancel() {
    $('#EditDialog').modal('hide');

}
async function GetRoles() {
   var result = await  $.ajax({
       type: 'GET',
       url:'/user/AllRoles'
   });
   return result;
}
async function ShowEdit(id) {
    StartLoading();
    $('#edit-id').val(id);
    var roles = await GetRoles();
    $('#edit-role').empty();
    for (var i = 0; i < roles.length; i++) {
        $('#edit-role').append('<option value="'+roles[i].Id+'">'+roles[i].Name+'</option>');
    }
    $('#EditDialog').modal('show');
    EndLoading();
}
async function Update() {
    StartLoading();
    try {
        await $.ajax({
            type: 'POST',
            url: '/user/setrole',
            traditional: true,
            data: {
                UserId: $('#edit-id').val(),
                RoleId: $('#edit-role').val()
            }
        });
        Cancel();
        location.reload();
    } catch (e) {
        console.log(e);
    }
    EndLoading();
}

function PreviousPage(CurrentPage) {
    location.href = '/user/index?page=' + (CurrentPage - 2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/user/index?page=' + (CurrentPage).toString();
}