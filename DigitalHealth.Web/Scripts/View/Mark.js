
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
    $('#CreateDialog').modal('hide');

    $('#DeleteDialog').modal('hide');

    $('#EditDialog').modal('hide');

    $('#DetailsDialog').modal('hide');
}
function Delete(Id) {
    $('#DeleteDialog').modal('show');
    $('#DeleteConfirm').prop('itemid', Id);
}
async function DeleteConfirm(Id) {

    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/Mark/Delete",
        data: {
            Id: Id
        },
        success: function (msg) {

        }
    });
    EndLoading();
    Cancel();
    location.reload(true);
}

function PreviousPage(CurrentPage) {
    location.href = '/Mark/index?page=' + (CurrentPage - 2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/Mark/index?page=' + (CurrentPage).toString();
}