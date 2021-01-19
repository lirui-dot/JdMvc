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
