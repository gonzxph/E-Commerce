$().ready(function () {
    $('.btnaddcart').click(function () {
        var productId = $(this).data('product-id'); 
        var quantity = $(this).closest('.card-footer').find('.quantity').val(); 

        if (quantity != 0) {
            $.ajax({
                url: '../Home/AddToCart',
                type: 'POST',
                data: { productId: productId, quantity: quantity },
                success: function (data) {
                    alert(data.message);
                    location.reload()
                },
                error: function () {
                    alert('Error adding to cart');
                }
            });
        } else {
            alert("You must add a product")
        }

        
    });



});

function updateQuantity(button, change, price, productId) {
    var quantityInput = $(button).closest('.d-flex').find('input[type=number]');
    var currentQuantity = parseInt(quantityInput.val());
    var newQuantity = currentQuantity + change;

    if (newQuantity >= 0) {
        quantityInput.val(newQuantity);
        var totalPriceElement = $(button).closest('tr').find('.total-price');
        var newTotalPrice = newQuantity * price;
        totalPriceElement.text(newTotalPrice.toFixed(2));

        // Send AJAX request to update quantity in the database
        $.ajax({
            url: '/Home/UpdateQuantity',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                productId: productId,
                newQuantity: newQuantity
            }),
            success: function (response) {
                if (response.success) {
                    console.log("Quantity updated successfully.");
                } else {
                    console.error("Error updating quantity:", response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX error:", error);
            }
        });
    }
}
