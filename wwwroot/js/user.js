$(function () {
    $('#user-btn').click(function () {
        var username = $('#UserName').val();
        var pwd = $('#PassWord').val();
        if (username == "") {
            alert("用户名不能为空");
            return;
        }
        if (pwd == "") {
            alert("密码不能为空");
            return;
        }
        var item = {
            UserName: username,
            PassWord: pwd
        }
        $.ajax({
            url: 'https://localhost:5001/User',
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(item),
            contentType: 'application/json',
            success: function (data) {
                if (data == null) {
                    alert("密码或者用户名错误，请重新输入！");
                    window.location.href = "https://localhost:5001/User";
                } else {
                    alert("登录成功！欢迎您！！");
                    window.location.href="https://localhost:5001/Personal/Edit";
                }

            }
        })
    })
})