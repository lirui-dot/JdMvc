$(function(){
    $('#Address-btn').click(function(){
        var test=$('#test').text();
        $('#Area').val(test);
    })
    $('#test').click(function(){
        $(".neirong").append($(".adp-wraper")); 
        if ($('#nei').attr("class")=="label-xuan") {
            $('.label-xuan').removeClass("label-xuan");
            $('#nei').addClass('neirong');
        }else{
            $('.neirong').removeClass("neirong");
            $('#nei').addClass('label-xuan');
        }
    })
})
$(function(){
    $('.extra-moren').click(function(){
        //var ii= $('.zhong-body-2-item-extra span').attr('id');
        var iii=$(this).data("qqqqqqq");
        // var len=ii.length;
        // var links=[];
        // for(var i=0;i<len;i++){
        //     links.push($(ii[i]).attr('id'));
        // }
        // alert(links.join(";"));
        // alert(ii);
        
            // $(this).remove();
            var cs="#"+iii;
            if ($('.zhong-body-1-address').attr("class")=="zhong-body-1-address") {
                $('.zhong-body-1-address-moren').removeClass('zhong-body-1-address-moren').addClass('zhong-body-1-address');
                
                $(cs).removeClass("zhong-body-1-address");
                $(cs).addClass("zhong-body-1-address-moren");

            }else{
                $(cs).removeClass("zhong-body-1-address-moren");
                $(cs).addClass("zhong-body-1-address");
            }
       
    })
})
$(function(){
    $('.modal-edit-btn').click(function(){
        var id=$(this).data("aaaaaa");
        $.ajax({
            url:'https://localhost:5001/Personal/AddressEdit'+'?id='+id,
            type:"GET",
            dataType:'json',
            contentType:'application/json',
            success:function(item){
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



