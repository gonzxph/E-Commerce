$().ready(function () {
    $(".btn-del").click(function () {
        var row = $(this).closest('tr')
        var id = row.find('.prod-id').text()

        $('.btnDelYes').click(function () {
          
            $.ajax({
                url: '../Home/DeleteProduct',
                type: 'POST',
                data: { prod_id: id },
                success: function (response) {
                    row.remove();
                },
                error: function (error) {
                    // On error, display an alert
                    alert('Error deleting product: ' + error.responseText);
                }
            })
        })
    })
    $('#btnNo').click(function () {
        $('#deleteModal').hide();
    });
})