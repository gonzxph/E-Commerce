$(document).ready(function () {
    $('#btnBack').click(function (event) {
        event.preventDefault();
        window.location.href = '../Home/UserLogin';
    });

    $('#registrationForm').submit(function (event) {
        event.preventDefault();

        var formData = new FormData(this);

        $.ajax({
            url: '../Home/UserRegister',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data[0].mess == 1) {
                    alert("User registered successfully!")
                    window.location.href = '../Home/UserLogin';
                } else {
                    alert("Email already exists!")
                }
            },
            error: function () {
                alert('Error!');
            }
        });
    });
});
