$(document).ready(async function () {
  
    $('#erorr-message').hide();
    $('#success-message').hide();
    await GetSymptoms();
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
    $('#MarksDialog').modal('hide');
}

async function GetSymptoms() {
    var result = await $.ajax({
        type: 'GET',
        url: '/diagnostic/symptoms'

    });
    console.log(result);
    for (var i = 0; i < result.length; i++) {
        $('#diagnostic-add-symptom').append('<option value="' + result[i].Id + '">' + result[i].Name + '</option>');
    }
}


function AddSymptom() {
    var id = $('#diagnostic-add-symptom').val();
    var text = $('#diagnostic-add-symptom option:selected').text()+' ';
    var span = '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>';
    var button = '<button type="button" class="btn btn-default btn-sm" id="' + id + '" onclick="RemoveSymptom(id)">' + span + '</button>';
    $('#diagnostic-selected-symptom').append('<li value="' + id + '" id="li-' + id + '">' + text + button + '</li>');
    $("#diagnostic-add-symptom option[value=" + id + "]").remove();
    if ($('#diagnostic-add-symptom').has('option').length < 1) {
        $('#button-add-symptom').prop('disabled', true);
    }
}

function RemoveSymptom(Id) {
    var text = $("#li-" + Id + "").text();
    $('#diagnostic-add-symptom').append('<option value="' + Id + '">' + text + '</option>');
    $("#li-" + Id + "").remove();
    if ($('#diagnostic-add-symptom').has('option').length > 0) {
        $('#button-add-symptom').prop('disabled', false);
    }
}

async function Diagnostic() {
    $('#diagnostic-result').empty();
    $('#table-method-result').empty();
    $('#erorr-message').hide();
    StartLoading();
    var symptoms = [];
    $('#diagnostic-selected-symptom li').each(function (i) {

        var index = $(this).index();
        var text = $(this).text();
        var value = $(this).attr('value');
        symptoms.push($(this).attr('value'));
    });
    try {
        var result = await $.ajax({
            type: 'GET',
            url: '/diagnostic/Diagnostic',
            traditional: true,
            data: {
                SymptomIds: symptoms,
            }
        });      
    } catch (e) {
        $('#erorr-message').show();
        $('#erorr-message').text('Not found');
        EndLoading();
        return;
    } 
    if (result.length == 0) {
        $('#erorr-message').show();
        $('#erorr-message').text('Not found');
        EndLoading();
        return;
    }
    $('#diagnostic-result').append('<caption>Результат диагностики</caption><thead><tr><th>Заболевание</th><th>Симптомы</th><th  width="15px">Кол-во совпадений</th><th>Действия</th></tr></thead>');
    $('#diagnostic-result').append('<tbody id="diagnostic-result-body"></tbody>');
    
    for (var j = 0; j < result.length; j++) {
        var ListSymptom='';
        for (var k = 0; k < result[j].Disease.SymptomNames.length; k++) {
            ListSymptom = ListSymptom + '<li>' + result[j].Disease.SymptomNames[k] + '</li>';
        }
        var button = ' <button type="button" class="btn btn-default btn-sm item" id="' +
            result[j].Disease.Id +
            '" onclick="FindMethod(id)"><span class="glyphicon glyphicon-info-sign" aria-hidden="true" ></span> Методы лечения</button>';
        $('#diagnostic-result-body').append('<tr><td>' + result[j].Disease.Name + '</td><td><ul>' + ListSymptom +'</ul></td><td>' + result[j].NumberOfCoincidences+'</td><td>'+button+'</td></tr></hr>');
    }
    EndLoading();
}

async function FindMethod(id) {
    StartLoading();
    $('#table-method-result').empty();
    var result = await  $.ajax({
        type: 'GET',
        url: '/diagnostic/FindMethods',
        traditional: true,
        data: {
            DiseaseId: id
}
    });
    if (result.length == 0) {
        $('#erorr-message').show();
        $('#erorr-message').text('Not found');
        EndLoading();
        return;
    }
    $('#table-method-result').append('<caption>Методы лечения</caption><thead><tr><th>Название</th><th>Источник</th><th>Описания</th><th>Действия</th></tr></thead>');
    $('#table-method-result').append('<tbody id="method-result-body"></tbody>');
    for (var i = 0; i < result.length; i++) {
        var button = ' <button type="button" class="btn btn-default btn-sm item" id="' +
            result[i].Id +
            '" onclick="AddMark(id)"><span class="glyphicon glyphicon-comment" aria-hidden="true" ></span> Оценить</button>';
        var buttonShow = ' <button type="button" class="btn btn-default btn-sm item" id="' +
            result[i].Id +
            '" onclick="ShowMarks(id)"><span class="glyphicon glyphicon-eye-open" aria-hidden="true" ></span> Показать оценки</button>';

        $('#method-result-body').append('<tr><td>' + result[i].Title + '</td><td>' + result[i].Source + '</td><td>' + result[i].Description + '</td><td>' + button+'</br>'+buttonShow + '</td>' +'</tr>');
    }
    console.log(result);
    EndLoading();
}

function AddMark(id) {
    $('#create-methodid').val(id);
    $('#CreateDialog').modal('show');
}

async function CreateItem() {
    StartLoading();
    try {
        await $.ajax({
            type: 'POST',
            url: '/diagnostic/CreateMark',
            traditional: true,
            data: {
                MethodOfTreatmentId: $('#create-methodid').val(),
                Value: $('#create-value').val(),
                Comment: $('#create-comment').val()
            }
        });    
    } catch (e) {
       
    } 
    EndLoading();
    $('#CreateDialog').modal('hide');
}

async function GetMarks(id) {
    var result = await  $.ajax({
        type: 'GET',
        url: '/diagnostic/GetMarks',
        traditional: true,
        data: {
            MethodId: id
        }
    });
    return result;
}

async function ShowMarks(id) {
    StartLoading();
    var marks = await  GetMarks(id);
    $('#marks-container').empty();
    for (var i = 0; i < marks.length; i++) {
        $('#marks-container').append('<b>' + marks[i].Login + ' </b>');
        $('#marks-container').append('<h4>' + marks[i].Comment + ' </h4>');
        $('#marks-container').append('<h5> Оценка:' + marks[i].Value + ' </h5>');
        $('#marks-container').append('<hr/>');
    }
    $('#MarksDialog').modal('show');
    EndLoading();
}