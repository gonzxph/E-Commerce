$(document).ready(function () {
    $('#btnCreate').click(function (event) {
        event.preventDefault();
        window.location.href = '../Home/Customer';
    })
    $('#btnlogin').click(function (event) {
        event.preventDefault();

        var username = $('#email').val()
        var password = $('#pwd').val()

        var formData = new FormData();
        formData.append('email', username);
        formData.append('pwd', password);

        if (username == "admin@email.com" && password == "123") {
            window.location.href = '../Home/Admin_Dashboard';
        } else {
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
        }
    });
});
