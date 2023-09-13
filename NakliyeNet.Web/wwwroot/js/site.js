function showThumbnail(input, img) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            //$(img).attr('src', e.target.result);
            $(img).css('background-image', 'url(' + e.target.result + ')');
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {
    var current = location.pathname;
    $('.nav-item .nav-link').each(function () {
        var $this = $(this);
        // if the current path is like this link, make it active
        if ($this.attr('href').startsWith(current)) {
            $this.parent().addClass('active');
        }
    })
})