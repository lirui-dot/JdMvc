$(function () {
    $('#Edit').click(function () {
        var hobby = "";
        $('.selected-li').each(function () {
            var li = $(this).val();
            hobby = hobby + li + ",";
        })
        var hobbies = $('#HobbyClassification').val(hobby);
    })

})