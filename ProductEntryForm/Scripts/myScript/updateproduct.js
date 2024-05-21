$().ready(function () {
    $('#btnUp').click(function () {
        var row = $(this).closest('tr')
        var id = row.find('.prod-id').text()

        $.ajax({
            url: '../Home/GetProduct',
            type: 'GET',
            data: { id: id },
            success: function (response) {
                // On success, remove the row from the table
                row.remove();
            },
            error: function (error) {
                // On error, display an alert
                alert('Error deleting product: ' + error.responseText);
            }
        })


    })
    $('#button').click(function () {
        var data = new FormData();
        data.append('insert_file', $('#insert_file')[0].files[0]);
        data.append('firstname', $('#firstname').val());
        data.append('search_id', $('#search_id').val());

        $.ajax({
            url: '../Home/StudentUpdate',
            type: 'POST',
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log(data);
                alert('Changes saved successfully.');
            },
            error: function () {
                alert('Error saving changes.');
            }
        });
    });
    $('#btnClose').click(function () {
        $('#updateModal').hide();
    });
})