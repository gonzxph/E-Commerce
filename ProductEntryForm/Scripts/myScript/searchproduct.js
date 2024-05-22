$(document).ready(function () {
    $("#btnsearch").click(function () {
        var searchId = $("#search").val().trim();

        $.ajax({
            url: '../Home/GetProductsByPartialId',
            type: 'GET',
            data: { id: searchId },
            success: function (products) {
                // Clear existing products
                $("#datatable tbody").empty();

                // Append filtered products to the table
                $.each(products, function (index, product) {
                    var row = $("<tr>").addClass("items");
                    row.append($("<td>").text(product.id));
                    row.append($("<td>").text(product.name));
                    row.append($("<td>").text(product.desc));
                    row.append($("<td>").html(`<img src="../Home/Image?filename=${encodeURIComponent(product.image)}" width="50%" />`));
                    row.append($("<td>").text(product.isbn));
                    row.append($("<td>").text(product.page));
                    row.append($("<td>").text(product.pub));
                    row.append($("<td>").text(product.weight));
                    row.append($("<td>").text(product.dimension));
                    row.append($("<td>").text(product.stock));
                    row.append($("<td>").text(product.price));

                    $("#datatable tbody").append(row);
                });
            },
            error: function (error) {
                // On error, display an alert
                alert('Error searching products: ' + error.responseText);
            }
        });
    });
});
