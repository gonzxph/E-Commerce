$().ready(function () {

    $('#btnAddProd').click(function () {

        var formData = new FormData();
        formData.append('prodName', $('#name').val())
        formData.append('prodDescript', $('#descript').val())
        formData.append('prodIsbn', $('#isbn').val())
        formData.append('prodPage', $('#page').val())
        formData.append('prodWeight', $('#weight').val())
        formData.append('img', $('#img')[0].files[0])
        formData.append('prodPrice', $('#price').val())
        formData.append('prodPub', $('#pub').val())
        formData.append('prodDimen', $('#dimen').val())
        formData.append('prodStock', $('#stock').val())


        $.ajax({
            url: '../Home/Product_Data',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert('Product Successfully Created')
                location.reload()
            },
            error: function () {
                alert('Error!')
            }
        })


    }) // btn end
    $('#btnClose').click(function () {
        $('#exampleModal').hide();
    });
    ('#btnClose').click(function () {
        $('#exampleModal').hide();
    });

}) // ready end