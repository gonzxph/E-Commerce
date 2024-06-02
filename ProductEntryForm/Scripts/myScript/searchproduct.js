$(document).ready(function () {
    $('#btnsearch').click(function () {
        var searchQuery = $('#searchQuery').val();
        if (searchQuery !== '') {
            window.location.href = '?searchQuery=' + searchQuery;
        } else {
            window.location.href = '/Home/Admin_Dashboard';
        }
    });
});