$(document).ready(function () {
    $('.search_popup').hide();
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

$(function () {
    var minlength = 3;

    $("#seca-search").keyup(function () {
        debugger;
        var that = this,
            value = $(this).val();

        if (value.length >= minlength) {
            $.ajax({
                type: "GET",
                url: "/Product/SearchProducts",
                data: {
                    'searchText': value
                },
                dataType: "text",
                success: function (res) {
                    debugger;
                    if (res) {
                        $('.search_popup').show();
                        $('.search_popup').html(res);
                    }
                }
            });
        } else {
            $('.search_popup').hide();
        }
    });
});


$(document).on('click', 'body', function () {
    debugger;
    $('.search_popup').hide();
})

function ProductAddtoWishList(productId) {
    debugger;
    showLoader();
    $.ajax({
        url: 'Product/ProductAddtoWishList',
        type: 'POST',
        dataType: 'json',
        data: { productId: productId },
        success: function (data) {
            debugger;
            hideLoader()
            showtoaster('Success');
            window.location.reload();
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            showtoaster('Error');
            $(`#modalLRForm`).modal('show');
        }
    });
}
function ProductAddtoCart(productId) {
    debugger;
    showLoader();
    $.ajax({
        url: 'Product/ProductAddtoCart',
        type: 'POST',
        dataType: 'json',
        data: { productId: productId },
        success: function (data) {
            debugger;
            hideLoader()
            showtoaster('Success');
            window.location.reload();
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            showtoaster('Error');
            $(`#modalLRForm`).modal('show');
        }
    });
}

function ProductRemovetoCart(productId) {
    debugger;
    showLoader();
    $.ajax({
        url: 'Product/ProductRemovetoCart',
        type: 'POST',
        dataType: 'json',
        data: { productId: productId },
        success: function (data) {
            debugger;
            hideLoader()
            showtoaster('Success');
            window.location.reload();
        },
        error: function (request, error) {
            debugger;
            hideLoader()
            showtoaster('Error');
        }
    });
}