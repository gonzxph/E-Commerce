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
