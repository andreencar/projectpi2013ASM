$(document).ready(function() {
    $('#NewBoardForm').submit(function () {
        var newboardName = $('#BoardNameID').val;

        $.getJSON("/Ajax/BoardExists/", { newboardName: newboardName }, function(data) {
            return data;
        });
    });
});