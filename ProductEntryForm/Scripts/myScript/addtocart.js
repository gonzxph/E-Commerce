$().ready(function () {
    // Handle click event for Add to Cart buttons
    $('.btnaddcart').click(function () {
        var productId = $(this).data('product-id'); // Get the product ID from the button's data attribute
        var quantity = $(this).closest('.card-footer').find('.quantity').val(); // Get the quantity from the input field

        // Send AJAX request to add to cart
        $.ajax({
            url: '../Home/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (data) {
                alert(data.message); // Display success or error message
            },
            error: function () {
                alert('Error adding to cart');
            }
        });
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
            url: '/Cart/UpdateQuantity',
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
