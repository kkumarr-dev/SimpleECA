$(document).ready(function () {
    debugger;
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

