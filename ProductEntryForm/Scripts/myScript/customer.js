$().ready(function () {

    $('#btn').click(function () {

        $.post('../Home/postCustomer', {

            firstname: $('#fname').val(),
            lastname: $('#lname').val(),
            phonenumber: $('#phoneno').val(),
            email: $('#email').val(),
            pwd: $('#pwd').val(),
            pwd2: $('#pwd2').val()

        }, function (data) {

            alert(data[0].mess)

        })

    })//btn end

})//ready end