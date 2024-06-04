$().ready(function () {

    $(document).ready(function () {
        $('#pwd2').on('input', function () {
            var pwd = $('#pwd').val();
            if ($(this).val() !== pwd) {
                this.setCustomValidity('Passwords do not match.');
            } else {
                this.setCustomValidity('');
            }
        });

        $('#btn').click(function () {
            var pwd2 = $('#pwd2').val();
            var pwd = $('#pwd').val();

            if (pwd2 !== pwd) {
                $('#pwd2')[0].setCustomValidity('Passwords do not match.');
                $('#pwd2')[0].reportValidity();
                return;
            }

            $('#pwd2')[0].setCustomValidity('');

            $.post('../Home/postCustomer', {
                firstname: $('#fname').val(),
                lastname: $('#lname').val(),
                phonenumber: $('#phoneno').val(),
                email: $('#email').val(),
                pwd: $('#pwd').val(),
                pwd2: $('#pwd2').val()
            }, function (data) {
                alert(data[0].mess);
            }).fail(function () {
                alert("An error occurred. Please try again.");
            });
        });
    });//btn end

})//ready end