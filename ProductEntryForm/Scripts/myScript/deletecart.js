﻿$(document).ready(function () {
    $('.btndeleteitem').on('click', function () {
        var productId = $(this).data('product-id');
        
        if (confirm("Are you sure you want to delete this item?")) {
                $.ajax({
                    type: "POST",
                    url: "../Home/DeleteCartItem",
                    data: { productId: productId },
                    success: function (response) {
                        if (response.success) {
                            // Reload or update the cart view after deletion
                            window.location.reload();
                        } else {
                            alert(response.error);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                        alert("Error deleting item.");
                    }
                });
            } 
  
    });

    

});

$(document).ready(function () {
    $('.btncheckout').on('click', function () {
        var allValid = true;
        var cartItems = $('.items');

        cartItems.each(function () {
            var stock = parseInt($(this).data('stock'));
            var quantity = parseInt($(this).find('.quantity').val());

            if (quantity <= 0 || quantity > stock) {
                allValid = false;
                alert('Quantity for ' + $(this).find('.prod-id').text() + ' is invalid. Please ensure it is greater than 0 and less than or equal to stock.');
                return false; // Break out of the loop
            }
        });

        if (allValid) {
            var userId = $(this).data('user-id');

            if (confirm("Are you sure you want to checkout?")) {
                $.ajax({
                    type: "POST",
                    url: "/Home/Checkout",
                    data: { userId: userId },
                    success: function (response) {
                        if (response.success) {
                            alert("Checkout successful!");
                            window.location.reload(); // Reload the page after successful checkout
                        } else {
                            alert(response.error);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                        alert("Error during checkout.");
                    }
                });
            }
        } else {
            console.log('Invalid quantities found. Checkout aborted.');
        }
    });

    // Other code...
});


////