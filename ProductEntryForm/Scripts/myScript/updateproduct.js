$().ready(function () {
    $('.btnUp').click(function () {
        var row = $(this).closest('tr')
        var id = row.find('.prod-id').text()

        $.ajax({
            url: '../Home/UpdateProduct',
            type: 'GET',
            data: { id: id },
            success: function (prod_data) {
                console.log(prod_data)
                $('#upname').val(prod_data[0].name);

            },
            error: function (error) {
                // On error, display an alert
                alert('Error deleting product: ' + error.responseText);
            }
        })
    })


    $('#btnUpChange').click(function () {
        var data = new FormData();
        data.append('insert_file', $('#insert_file')[0].files[0]);
        data.append('firstname', $('#firstname').val());

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