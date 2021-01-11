$(document).ready(function () {
    $('#Image').on('change', function () {
        var imgBox=document.getElementById('Image');
        var img=document.getElementById('img');
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();
            //获取文件内容以url形式表示
            reader.readAsDataURL(imgBox.files[0]);
            reader.onload = function () {
                //赋值给img
                img.src = this.result
            }
        }else{
            alert("This browser does not support FileReader.");
        }
    })
})