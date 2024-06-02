$(document).ready(function () {
    // Function to delete cart item
    function deleteCartItem(productId) {
        if (confirm("Are you sure you want to delete this item?")) {
            $.ajax({
                type: "POST",
                url: "../Home/DeleteCartItem", // Update the URL with your controller action
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
    }

    // Attach the delete function to buttons with the class btndeleteitem
    $('.btndeleteitem').on('click', function () {
        var productId = $(this).data('product-id');
        deleteCartItem(productId);
    });
});
