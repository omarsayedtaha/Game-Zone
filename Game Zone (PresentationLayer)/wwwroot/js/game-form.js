$(document).ready(function () {
    $("#Img").on('change', function () {
        $('.cover-preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});

//let imageinpute = document.getElementById("Img");
//let image = document.getElementsByClassName("cover-preview");
//    imageinpute.onchange(function () {
//    image.setAttribute("src", window.URL.createObjectURL(this.files[0]));
//});
//let card = document.getElementsByClassName('card-body')
//let style = window.getComputedStyle(card)
//let image = style.background.image.createObjectURL(this.files[0])