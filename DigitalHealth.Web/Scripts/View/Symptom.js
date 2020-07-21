
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
    await  $.ajax({
        type: "POST",
        url: "/Symptom/Delete",
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

    $('#DetailsDialog').modal('hide');
}

function ShowCreate() {
    $('#CreateDialog').modal('show');
}
async function CreateItem() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/Symptom/Create",
        data: {
            Name: $('#create-name').val(),
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
    $('#edit-description').val(data.Description);


    $('#EditDialog').modal('show');
}
async function Get(Id) {
    var item = null;
    try {
        var result = await $.ajax({
            type: "GET",
            url: "/Symptom/Get",
            data: { Id: Id }
        });
        return result;
    }
    catch (e) {
        console.error(e);
    }
}
async function GetDetails(Id) {
    var item = null;
    try {
        var result = await $.ajax({
            type: "GET",
            url: "/Symptom/GetDetails",
            data: { Id: Id }
        });
        return result;
    }
    catch (e) {
        console.error(e);
    }
}

async function Details(Id) {
    StartLoading();
    var data = await GetDetails(Id);
    EndLoading();
    console.log(data);

    $('#details-name').val(data.Name);
    $('#details-id').val(data.Id);
    $('#details-description').val(data.Description);
    for (var i = 0; i < data.Diseases.length; i++) {
        var appenditem = '<li>' + data.Diseases[i] + '</li>';
        $('#DiseaseList').append(appenditem);
    }

    $('#DetailsDialog').modal('show');
}
async function Update() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/Symptom/Update",
        data: {
            Name: $('#edit-name').val(),
            Id: $('#edit-id').val(),
            Description: $('#edit-description').val(),
        }
    });
    EndLoading();
    location.reload();
}

function PreviousPage(CurrentPage) {
    location.href = '/Symptom/index?page=' + (CurrentPage - 2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/Symptom/index?page=' + (CurrentPage).toString();
}