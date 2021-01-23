$(function () {
    //点击保存地址把省份的值赋给test
    $('#Address-btn').click(function () {
        var test = $('#test').text();
        $('#Area').val(test);
    })
    //点击close刷新页面
    $('#close-btn').click(function () {
        window.location.href = "https://localhost:5001/Personal/Address";
    })
});
//把省份div移到class=neirong
$('#test').click(function () {
    $(".neirong").append($(".adp-wraper"));
    if ($('#nei').attr("class") == "label-xuan") {
        $('.label-xuan').removeClass("label-xuan");
        $('#nei').addClass('neirong');
    } else {
        $('.neirong').removeClass("neirong");
        $('#nei').addClass('label-xuan');
    }
})
$(function () {
    //点击设为默认有图标
    $('.extra-moren').click(function () {
        var iii = $(this).data("qqqqqqq");
        var cs = "#" + iii;
        if ($('.zhong-body-1-address').attr("class") == "zhong-body-1-address") {
            $('.zhong-body-1-address-moren').removeClass('zhong-body-1-address-moren').addClass('zhong-body-1-address');

            $(cs).removeClass("zhong-body-1-address");
            $(cs).addClass("zhong-body-1-address-moren");

        } else {
            $(cs).removeClass("zhong-body-1-address-moren");
            $(cs).addClass("zhong-body-1-address");
        }

    })
})
$(function () {
    //异步新增一个地址
    $('#add-btn').click(function () {
        $('#Address-btn').click(function () {
            $('#form-Address').submit();
        })
    })
})
$(function () {
    //异步获取数据
    $('.modal-edit-btn').click(function () {
        var id = $(this).data("aaaaaa");
        $.ajax({
            url: 'https://localhost:5001/Personal/AddressEdit' + '?id=' + id,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json',
            success: function (item) {
                console.log(item);
                $('#Consignee').val(item.consignee);
                $('#test').text(item.area);
                $('#DetailedAddress').val(item.detailedAddress);
                $('#Phone').val(item.phone);
                $('#FixedPhone').val(item.fixedPhone);
                $('#EmailAddress').val(item.emailAddress);
                $('#AddressAlias').val(item.addressAlias);
            }
        })
    })
})
//异步修改数据
$(function () {
    $('#address-edit').click(function () {
        var id = $(this).data("aaaaaa");
        $('#Address-btn').click(function () {
            var consignee = $('#Consignee').val();
            var test = $('#test').text();
            var detailedAddress = $('#DetailedAddress').val();
            var phone = $('#Phone').val();
            if (phone == "") {
                alert("手机号码不能为空");
                return;
            }
            if (phone.match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
                alert("手机号码格式不正确");
                return
            }
            var fixedPhone = $('#FixedPhone').val();
            if (fixePhone.match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
                alert("手机号码格式不正确");
                return
            }
            var emailAddress = $('#EmailAddress').val();
            if (!emailAddress.match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                alert("邮箱格式不正确");
            }
            var addressAlias = $('#AddressAlias').val();
            var page = {
                Consignee: consignee,
                Area: test,
                DetailedAddress: detailedAddress,
                Phone: phone,
                FixedPhone: fixedPhone,
                EmailAddress: emailAddress,
                AddressAlias: addressAlias,
                Id: id
            };

            $.ajax({
                url: 'https://localhost:5001/Personal/AddressEdit',
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(page),
                success: function () {
                    alert("修改成功");
                    window.location.href = "https://localhost:5001/Personal/Address";
                }
            })
        })
    })
})



