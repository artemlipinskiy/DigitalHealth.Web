
$(document).ready(function () {
    $('#loading').addClass('hide');
});

function StartLoading() {
    $('#loading').removeClass('hide');
}

function EndLoading() {
    $('#loading').addClass('hide');
}

function Delete(Id) {
    $('#DeleteDialog').modal('show');
    $('#DeleteConfirm').prop('itemid', Id);
}
async function DeleteConfirm(Id) {

    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/icd/Delete",
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
function Cancel() {
    $('#CreateDialog').modal('hide');

    $('#DeleteDialog').modal('hide');

    $('#EditDialog').modal('hide');
}

function ShowCreate() {
    $('#CreateDialog').modal('show');
}
async function CreateItem() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/icd/Create",
        data: {
            Name: $('#create-name').val(),
            Code: $('#create-code').val(),
            Description: $('#create-description').val(),
        }
    });
    EndLoading();
    Cancel();
    location.reload();
}

async function ShowEdit(Id) {
    StartLoading();
    var data = await Get(Id);
    EndLoading();
    console.log(data);

    $('#edit-name').val(data.Name);
    $('#edit-id').val(data.Id);
    $('#edit-code').val(data.Code);
    $('#edit-description').val(data.Description);


    $('#EditDialog').modal('show');
}
async function Get(Id) {
    var item = null;
    try {
        var result = await $.ajax({
            type: "GET",
            traditional:true,
            url: "/icd/Get",
            data: { Id: Id }
        });
        return result;
    }
    catch (e) {
        console.error(e);
    }
}

async function Update() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/icd/Update",
        data: {
            Name: $('#edit-name').val(),
            Id: $('#edit-id').val(),
            Code: $('#edit-code').val(),
            Description: $('#edit-description').val(),
        }
    });
    EndLoading();
    location.reload();
}

function PreviousPage(CurrentPage) {
    location.href = '/icd/index?page=' + (CurrentPage-2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/icd/index?page=' + (CurrentPage).toString();
}