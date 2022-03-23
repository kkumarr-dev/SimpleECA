$(document).ready(function () {
    $('.bannerCarousel').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        dots: true,
        autoplay: true,
        autoplaySpeed: 1500,
        pauseOnHover: true,
        responsive: [{
            breakpoint: 768,
            settings: {
                slidesToShow: 1,
                speed: 500,
                fade: true,
                cssEase: 'linear'
            }
        }, {
            breakpoint: 520,
            settings: {
                slidesToShow: 1,
                speed: 500,
                fade: true,
                cssEase: 'linear'
            }
        }]
    });
})

function showLoader() {
    $('#seca-loader').addClass('is-active')
}
function hideLoader() {
    $('#seca-loader').removeClass('is-active')
}
function showtoaster(content) {
    $('.toaster').click(function () {
        $.toast({
            content: `${content}`,
            position: "top-right"
        })
    })
}
function openadminpartial(url, width) {
    debugger;
    showLoader()
    $.ajax({
        url: url,
        type: 'GET',
        /*dataType: '',*/
        success: function (data) {
            debugger;
            hideLoader()
            $(`#partialContent`).html(data);
            $(`#adminPatial`).modal('show');
            $('.modal-dialog').css('max-width', `${width}px`)
            $('table').DataTable();
            $('select').selectpicker();
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            showtoaster('Error');
        }
    });
}
function bindpartial(url, selector) {
    debugger;
    showLoader()
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            debugger;
            hideLoader()
            $(selector).html(data);
            $('table').DataTable();
            $('select').selectpicker();
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            showtoaster('Error');
        }
    });
}