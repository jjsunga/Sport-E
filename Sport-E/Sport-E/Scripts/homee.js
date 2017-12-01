/*$(document).ready(function () {
    setBindings();
});

/*
function setBindings() {
    $("nav a").click(function(e){
        e.preventDefault();
        var sectionId = e.currentTarget.id + "Section";

    $("html, body").animate({
        scrollTop: $("#" + sectionId).offset().top
            },1000)
        })
}
*/
$(document).ready(function () {
$("nav a").on('click', function (event) {
    event.preventDefault();
    var hash = this.hash; 
    
    $('html, body').animate({ 
        scrollTop: $(hash).offset().top 
    }, 1000) 
}); 
})