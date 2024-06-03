$().ready(function () {
    $('.btnaddcart').click(function () {
        var stock = $(this).data('stock');
        var productId = $(this).data('product-id');
        var quantity = $(this).closest('.card-footer').find('.quantity').val();
        if (quantity > 0 && quantity <= stock) {
            $.ajax({
                url: '../Home/AddToCart',
                type: 'POST',
                data: { productId: productId, quantity: quantity },
                success: function (data) {
                    alert(data.message);
                    location.reload();
                },
                error: function () {
                    alert('Error adding to cart');
                }
            });
        }else {
            alert("Error: Negative quantity or Quantity greater than stocks");
        }
    });

    // Add input event listener to quantity inputs
    $('.quantity').on('input', function () {
        var quantityInput = $(this);
        var stock = $(this).data('stock');
        var newQuantity = parseInt(quantityInput.val());
        var productId = quantityInput.closest('.card-footer').find('.btnaddcart').data('product-id');
        var price = parseFloat(quantityInput.closest('tr').find('.btnaddcart').data('product-price'));

        if (newQuantity > 0 && newQuantity <= stock) {
            updateQuantityDirect(quantityInput, newQuantity, price, productId);
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

function updateQuantityDirect(inputField, price, productId) {
    var newQuantity = parseInt(inputField.value);
    var totalPriceElement = $(inputField).closest('tr').find('.total-price');
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

document.addEventListener('DOMContentLoaded', function () {
    var productModal = document.getElementById('productModal');
    productModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var prodName = button.getAttribute('data-prod-name');
        var prodDescription = button.getAttribute('data-prod-description');
        var prodImage = button.getAttribute('data-prod-image');
        var prodISBN = button.getAttribute('data-prod-isbn');
        var prodPublisher = button.getAttribute('data-prod-publisher');
        var prodPage = button.getAttribute('data-prod-page');
        var prodWeight = button.getAttribute('data-prod-weight');
        var prodDimension = button.getAttribute('data-prod-dimension');
        var prodStock = button.getAttribute('data-prod-stock');
        var prodPrice = button.getAttribute('data-prod-price');

        var modalTitle = productModal.querySelector('.modal-title');
        var modalName = productModal.querySelector('#modalName');
        var modalDescription = productModal.querySelector('#modalDescription');
        var modalImage = productModal.querySelector('#modalImage');
        var modalISBN = productModal.querySelector('#modalISBN');
        var modalPublisher = productModal.querySelector('#modalPublisher');
        var modalPage = productModal.querySelector('#modalPage');
        var modalWeight = productModal.querySelector('#modalWeight');
        var modalDimension = productModal.querySelector('#modalDimension');
        var modalStock = productModal.querySelector('#modalStock');
        var modalPrice = productModal.querySelector('#modalPrice');

        modalTitle.textContent = prodName;
        modalName.textContent = prodName;
        modalDescription.textContent = prodDescription;
        modalImage.src = "../Home/Image?filename=" + encodeURIComponent(prodImage);
        modalISBN.textContent = prodISBN;
        modalPublisher.textContent = prodPublisher;
        modalPage.textContent = prodPage;
        modalWeight.textContent = prodWeight;
        modalDimension.textContent = prodDimension;
        modalStock.textContent = prodStock;
        modalPrice.textContent = prodPrice;
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var checkoutButton = document.querySelector('.btncheckout');

    checkoutButton.addEventListener('click', function () {
        var cartItems = document.querySelectorAll('.items');
        var allValid = true;

        cartItems.forEach(function (item) {
            var stock = parseInt(item.getAttribute('data-stock'));
            var quantity = parseInt(item.getAttribute('data-quantity'));

            if (quantity <= 0 || quantity > stock) {
                allValid = false;
                //alert('Quantity for ' + item.querySelector('.prod-id').textContent + ' is invalid. Please ensure it is greater than 0 and less than or equal to stock.');
                return;
            }
        });

        if (allValid) {
            console.log('All quantities are valid. Proceeding to checkout...');
        } else {
            console.log('Invalid quantities found. Checkout aborted.');
        }
    });
});
