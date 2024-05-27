$().ready(function () {
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
            },
            error: function () {
                alert('Error!');
            }
        });
    });
});
