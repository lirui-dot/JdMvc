$(function () {
    $('.i-li').click(function () {
        if($(this).attr("class")=="" ||$(this).attr("class")=="i-li" ||$(this).attr("class")==undefined){
        $(this).removeClass("i-li");
        $(this).addClass('selected-li');
       }else{
        $(this).removeClass();
        $(this).addClass('i-li');
       }
    })
    $('.selected-li').click(function () {
        if($(this).attr("class")=="" ||$(this).attr("class")=="i-li" ||$(this).attr("class")==undefined){
        $(this).removeClass("i-li");
        $(this).addClass('selected-li');
       }else{
        $(this).removeClass();
        $(this).addClass('i-li');
       }
    })
})


