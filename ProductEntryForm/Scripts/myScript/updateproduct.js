$().ready(function () {
    $('.btn-up').click(function () {
        var row = $(this).closest('tr')
        var id = row.find('.prod-id').text()

        $.ajax({
            url: '../Home/GetProduct',
            type: 'GET',
            data: { id: id },
            success: function (data) {
                if (data) {
                    $('#upid').val(id);
                    $('#upname').val(data.name);
                    $('#updescript').val(data.desc);
                    $('#upisbn').val(data.isbn);
                    $('#upweight').val(data.weight);
                    $('#upprice').val(data.price);
                    $('#uppub').val(data.pub);
                    $('#updimen').val(data.dimension);
                    $('#upstock').val(data.stock);
                    $('#uppage').val(data.page);
                    $('#upshowimg').attr('src',data.image);
                }

            },
            error: function (error) {
                alert('Error deleting product: ' + error.responseText);
            }
        })
    })


    $("#btnUpChange").click(function () {
        var data = new FormData();
        var fileInput = $('#upimg')[0].files[0];
        if (fileInput) {
            data.append('insert_image', fileInput);
        }
        data.append('prod_id', $('#upid').val());
        data.append('prod_name', $('#upname').val());
        data.append('prod_desc', $('#updescript').val());
        data.append('prod_price', $('#upprice').val());
        data.append('prod_isbn', $('#upisbn').val());
        data.append('prod_weight', $('#upweight').val());
        data.append('prod_pub', $('#uppub').val());
        data.append('prod_dimen', $('#updimen').val());
        data.append('prod_stock', $('#upstock').val());
        data.append('prod_page', $('#uppage').val());

        $.ajax({
            url: '/Home/PostProductUpdate',
            type: 'POST',
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data[0].mess == 1) {
                    alert(data[0].message);
                    location.reload()
                } else if (data[0].mess == 2) {
                    alert(data[0].message);
                }
                $('#updateModal').modal('hide');
                
            },
            error: function () {
                alert('Error saving changes.');
            }
        });
    });
});