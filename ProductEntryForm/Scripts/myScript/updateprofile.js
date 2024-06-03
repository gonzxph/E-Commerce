$(document).ready(function () {
    var userIdString = $('#userId').val();
    var userId = parseInt(userIdString, 10);

    if (!isNaN(userId)) {
        $.ajax({
            url: '../Home/getUser',
            type: 'GET',
            data: { user_id: userId },
            success: function (data) {
                if (data) {
                    $('#fname').val(data.fname);
                    $('#lname').val(data.lname);
                    $('#phoneno').val(data.phoneno);
                    $('#email').val(data.email);
                }
            },
            error: function (error) {
                console.error('Error fetching user data:', error);
            }
        });
    } else {
        console.error('Invalid User ID');
    }

    $("#btnUpdate").click(function () {
        var data = new FormData();
        data.append('fname', $('#fname').val());
        data.append('lname', $('#lname').val());
        data.append('phoneno', $('#phoneno').val());

        $.ajax({
            url: '/Home/updateUser',
            type: 'POST',
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                alert('Product updated successfully.');
                location.reload()
            },
            error: function () {
                alert('Error saving changes.');
            }
        });
    });

});
