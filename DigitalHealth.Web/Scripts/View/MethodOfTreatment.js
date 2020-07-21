
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
        url: "/MethodOfTreatment/Delete",
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

async function GetDiseases() {
    var result = await $.ajax({
        type: "GET",
        url: "/disease/GetAll",

    });
    return result;
}

async function ShowCreate() {
    StartLoading();
    $('#CreateDialog').modal('show');
    $('#create-disease').empty();
    var diseases = await GetDiseases();
    for (var i = 0; i < diseases.length; i++) {
        $('#create-disease').append('<option value="' + diseases[i].Id + '">' + diseases[i].Name +  '</option>');
    }
    EndLoading();
}
async function CreateItem() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/MethodOfTreatment/Create",
        data: {
            Title: $("#create-title").val(),
            Source: $('#create-source').val(),
            Description: $('#create-description').val(),
            DiseaseId: $("#create-disease").val(),
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
    $('#edit-title').val(data.Title);
    $('#edit-id').val(data.Id);
    $('#edit-description').val(data.Description);
    $('#edit-disease').empty();
    $('#edit-source').val(data.Source);
    var diseases = await GetDiseases();
    for (var i = 0; i < diseases.length; i++) {
        $('#edit-disease').append('<option value="' + diseases[i].Id + '">' + diseases[i].Name + '</option>');
    }
    $('#edit-disease').val(data.DiseaseId)
    $('#EditDialog').modal('show');
}
async function Get(Id) {
    var item = null;
    try {
        var result = await $.ajax({
            type: "GET",
            url: "/MethodOfTreatment/Get",
            data: { Id: Id }
        });
        return result;
    }
    catch (e) {
        console.error(e);
    }
}
async function GetDetails(Id) {
    //var item = null;
    //try {
    //    var result = await $.ajax({
    //        type: "GET",
    //        url: "/Symptom/GetDetails",
    //        data: { Id: Id }
    //    });
    //    return result;
    //}
    //catch (e) {
    //    console.error(e);
    //}
}

async function Details(Id) {
    StartLoading();
    var data = await Get(Id);
    $('#details-id').val(data.Id);
    $('#details-description').val(data.Description);
    $('#details-title').val(data.Tite);
    $('#details-source').val(data.Source);
    $('#details-disease').val(data.DiseaseName);

    $('#DetailsDialog').modal('show');
    EndLoading();
}
async function Update() {
    StartLoading();
    await $.ajax({
        type: "POST",
        url: "/MethodOfTreatment/Update",
        data: {
            Title: $('#edit-title').val(),
            Id: $('#edit-id').val(),
            Description: $('#edit-description').val(),
            Source: $('#edit-source').val(),
            DiseaseId: $('#edit-disease').val(),
        }
    });
    EndLoading();
    location.reload();
}

function PreviousPage(CurrentPage) {
    location.href = '/MethodOfTreatment/index?page=' + (CurrentPage - 2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/MethodOfTreatment/index?page=' + (CurrentPage).toString();
}