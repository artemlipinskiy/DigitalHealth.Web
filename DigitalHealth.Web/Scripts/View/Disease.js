
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
        url: "/Disease/Delete",
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

function AddSymptom() {
    var id = $('#create-add-symptom').val();
    var text = $('#create-add-symptom option:selected').text();
    var span = '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>';
    var button = '<button type="button" class="btn btn-default btn-sm" id="'+id+'" onclick="RemoveSymptom(id)">'+span+'</button>';
    $('#create-selected-symptom').append('<li value="' + id + '" id="li-'+id+'">' + text + button+ '</li>');
    $("#create-add-symptom option[value=" + id + "]").remove();
    if ($('#create-add-symptom').has('option').length < 1) {
        $('#button-create-add-symptom').prop('disabled', true);
    }

}

function RemoveSymptom(Id) {
    var text = $("#li-" + Id + "").text();
    $('#create-add-symptom').append('<option value="' + Id + '">' + text + '</option>');
    $("#li-" + Id + "").remove();
    if ($('#create-add-symptom').has('option').length > 0) {
        $('#button-create-add-symptom').prop('disabled', false);
    }
}

async function GetICDs() {
    var result = await $.ajax({
        type: "GET",
        url: "/icd/GetAll",

    });
    return result;
}
async function GetSymptoms() {
    var symptoms = await $.ajax({
        type: "GET",
        url: "/symptom/GetAll",

    });
    return symptoms;
}
async function ShowCreate() {
    StartLoading();
    $('#CreateDialog').modal('show');
    $('#create-icd').empty();
    $('#create-add-symptom').empty();
    var ICDs = await GetICDs();
    var symptoms = await GetSymptoms();
    for (var i = 0; i < ICDs.length; i++) {
        $('#create-icd').append('<option value="' + ICDs[i].Id + '">' + ICDs[i].Code + ' (' + ICDs[i].Name+' )' +'</option>');
    }
    for (var i = 0; i < symptoms.length; i++) {
        $('#create-add-symptom').append('<option value="' + symptoms[i].Id + '">' + symptoms[i].Name + '</option>');
    }
    EndLoading();
}
async function CreateItem() {
    StartLoading();
    var symptoms = [];
     $('#create-selected-symptom li').each(function (i) {

        var index = $(this).index();
        var text = $(this).text();
        var value = $(this).attr('value');
        symptoms.push($(this).attr('value'));
     });
    await $.ajax({
        type: "POST",
        url: "/Disease/Create",
        data: {
            Name: $('#create-name').val(),
            Description: $('#create-description').val(),
            ICDID: $('#create-icd').val(),
            SymptomIds : symptoms,
}
    });
    EndLoading();
    Cancel();
    location.reload();
}

function UpdateAddSymptom() {
    var id = $('#edit-add-symptom').val();
    var text = $('#edit-add-symptom option:selected').text();
    var span = '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>';
    var button = '<button type="button" class="btn btn-default btn-sm" id="' + id + '" onclick="RemoveSymptomUpdate(id)">' + span + '</button>';
    $('#edit-selected-symptom').append('<li value="' + id + '" id="li-' + id + '">' + text + button + '</li>');
    $("#edit-add-symptom option[value=" + id + "]").remove();
    if ($('#edit-add-symptom').has('option').length < 1) {
        $('#button-edit-add-symptom').prop('disabled', true);
    }
}

function RemoveSymptomUpdate(Id) {
    var text = $("#li-" + Id + "").text();
    $('#edit-add-symptom').append('<option value="' + Id + '">' + text + '</option>');
    $("#li-" + Id + "").remove();
    if ($('#edit-add-symptom').has('option').length > 0) {
        $('#button-edit-add-symptom').prop('disabled', false);
    }
}

async function ShowEdit(Id) {
    StartLoading();
    var data = await Get(Id);
    EndLoading();
    console.log(data);

    $('#edit-name').val(data.Name);
    $('#edit-id').val(data.Id);
    $('#edit-description').val(data.Description);
    $('#edit-icd').empty();
    $('#edit-add-symptom').empty();
    var ICDs = await GetICDs();
    var symptoms = await GetSymptoms();
    for (var i = 0; i < ICDs.length; i++) {
        $('#edit-icd').append('<option value="' + ICDs[i].Id + '">' + ICDs[i].Code + ' (' + ICDs[i].Name + ' )' + '</option>');
    }
    for (var i = 0; i < symptoms.length; i++) {
        $('#edit-add-symptom').append('<option value="' + symptoms[i].Id + '">' + symptoms[i].Name + '</option>');
    }
    $('#edit-icd').val(data.ICDID);
    for (var j = 0; j < data.SymptomIds.length; j++) {
        
        var id = data.SymptomIds[j];
        var text = data.SymptomNames[j]
        var span = '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>';
        var button = '<button type="button" class="btn btn-default btn-sm" id="' + id + '" onclick="RemoveSymptomUpdate(id)">' + span + '</button>';
        $('#edit-selected-symptom').append('<li value="' + id + '" id="li-' + id + '">' + text + button + '</li>');

        $("#edit-add-symptom option[value=" + data.SymptomIds[j] + "]").remove();
    }
    

    $('#EditDialog').modal('show');
}
async function Get(Id) {
    var item = null;
    try {
        var result = await $.ajax({
            type: "GET",
            url: "/Disease/Get",
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
    var symptoms = [];
    $('#edit-selected-symptom li').each(function (i) {

        var index = $(this).index();
        var text = $(this).text();
        var value = $(this).attr('value');
        symptoms.push($(this).attr('value'));
    });
    await $.ajax({
        type: "POST",
        url: "/Disease/Update",
        data: {
            Id: $('#edit-id').val(),
            Name: $('#edit-name').val(),
            Description: $('#edit-description').val(),
            ICDID: $('#edit-icd').val(),
            SymptomIds: symptoms,
        }
    });
    EndLoading();
    Cancel();
    location.reload();
}

function PreviousPage(CurrentPage) {
    location.href = '/Disease/index?page=' + (CurrentPage - 2).toString();
}
function NextPage(CurrentPage) {
    location.href = '/Disease/index?page=' + (CurrentPage).toString();
}