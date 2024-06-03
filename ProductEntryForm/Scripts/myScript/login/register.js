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
                alert(data[0].mess);
                window.location.href = '../Home/UserLogin';
            },
            error: function () {
                alert('Error!');
            }
        });
    });
});
