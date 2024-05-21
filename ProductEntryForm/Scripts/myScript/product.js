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
        formData.append('stock', $('#stock').val())


        $.ajax({
            url: '../Home/Product_Data',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert('Product Successfully Created')
            },
            error: function () {
                alert('Error!')
            }
        })



        /*$.post('../Home/Product_Data', {

            prodName: $('#name').val(),
            prodPrice: $('#price').val(),
            prodDescript: $('#descript').val(),
            prodIsbn: $('#isbn').val(),
            prodPub: $('#pub').val(),
            prodPage: $('#page').val(),
            prodWeight: $('#weight').val(),
            prodDimen: $('#dimen').val(),
            prodQty: $('#qty').val()


        }, function (data) {
            if (data[0].mess == 1) {
                alert("Successfully Added!")
            } else {
                alert("Please input correct information. Try again!")
            }
        })*/
    }) // btn end
    $('#btnClose').click(function () {
        $('#exampleModal').hide();
    });
    ('#btnClose').click(function () {
        $('#exampleModal').hide();
    });

}) // ready end