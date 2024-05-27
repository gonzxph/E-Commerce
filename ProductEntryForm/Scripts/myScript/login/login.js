$(document).ready(function () {
    $('#btnlogin').click(function (event) {
        event.preventDefault();

        var formData = new FormData();
        formData.append('email', $('#email').val());
        formData.append('pwd', $('#pwd').val());

        $.ajax({
            url: '../Home/checkLogin',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    window.location.href = '../Home/CustomerDashboard';
                } else {
                    alert("Login failed: " + response.message);
                }
            },
            error: function () {
                alert("Error during login attempt!");
            }
        });
    });
});
