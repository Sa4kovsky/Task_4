$('#fullCheckBox').click(function () {
    if ($(this).is(':checked')) {
        $('input:checkbox').prop('checked', true);
    } else {
        $('input:checkbox').prop('checked', false);
    }
});

$('input:checkbox').click(function () {
    if ($(this).is(':checked')) { }
    else { $('#fullCheckBox').prop('checked', false); }
});

var id = [];
function CheckedID()
{
    var object = $('td input[type ="checkbox"]:checked');

    $.each(object, function (index, value) {
        console.log('Индекс:' + index + '; Значение' + value.id);
        id[index] = value.id;
    });

}

$('#buttonDelete').click(function () {
    CheckedID();
    $.ajax({
        url: '/Home/Delete',
        type: 'POST',
        data: { id: id },
        success: function (result) {
            if (result == 1) {
                window.location = '/';
            }
            else {
                alert("Error delete");
            }
        }
    });
});

$('#buttonBlock').click(function () {
    CheckedID();
    $.ajax({
        url: '/Home/EditStatus',
        type: 'POST',
        data: { id: id },
        success: function (result) {
            if (result == 1) {
                window.location = '/';
            }
            else {
                alert("Error status");
            }
        }
    });
});

$('#buttonUnblock').click(function () {
    CheckedID();
    $.ajax({
        url: '/Home/EditStatus',
        type: 'POST',
        data: { id: id },
        success: function (result) {
            if (result == 1) {
                window.location = '/';
            }
            else
            {
                alert("Error status");
            }
        }
    });
});