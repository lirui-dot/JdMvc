$(document).ready(function () {
    $('#Image').on('change', function () {
        var imgBox = document.getElementById('Image');
        var img = document.getElementById('img');
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();
            //获取文件内容以url形式表示
            reader.readAsDataURL(imgBox.files[0]);
            reader.onload = function () {
                //赋值给img
                img.src = this.result
            }
            

        } else {
            alert("This browser does not support FileReader.");
        }
    })
    $('.imgUrl').children().click(function () {
        if ($(this).attr("class") == "" || $(this).attr("class") == undefined) {
            $(".imgUrl").children().each(function () {
                $(this).removeClass("selected");
            });
            $(this).addClass('selected');
        } else {
            $(this).removeClass("selected");
        }
    })
    $('#Pictures').click( function () {
        var img=$('.selected #selected-img').attr('src');
        $('#FileUrl').val(img);
        var tp=$("#img").attr('src');
        $('#ImageUrl').val(tp);
    })
})