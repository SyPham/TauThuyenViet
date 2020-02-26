$(function () {
    
    //Khởi tạo easyzoom
    var $easyzoom = $('.easyzoom').easyZoom({
        preventClicks: false,
        loadingNotice: "Đang load hình...",
        errorNotice: "Không có hình..."
    });

    // Ẩn hiện tab content
    $("#product-tab-home .expand").click(function (e) {
        //Ngăn chặn liên kết
        e.preventDefault();

        //Mở rộng nội dung
        $("#product-tab-home .tab-content").css("max-height", "none");

        //Ẩn chính nó
        $(this).removeClass("d-block").addClass("d-none");

        //Hiện nút còn lại lên
        $("#product-tab-home .collap").removeClass("d-none").addClass("d-block");
    });

    $("#product-tab-home .collap").click(function (e) {
        //Ngăn chặn liên kết
        e.preventDefault();

        //Mở rộng nội dung
        $("#product-tab-home .tab-content").css("max-height", "170px");

        //Ẩn chính nó
        $(this).removeClass("d-block").addClass("d-none");

        //Hiện nút còn lại lên
        $("#product-tab-home .expand").removeClass("d-none").addClass("d-block");
    });

    //Khởi tạo swiper carousel cho danh sách sản phẩm trang chủ
    $("#product-home .wrapper").each(function (index) {
        var currentIndex = index + 1;

        $(this).addClass("number-" + currentIndex);

        // Khởi tạo swiper
        var swiperFull = new Swiper('#product-home .wrapper.number-' + currentIndex + ' .full .swiper-container', {
            loop: true,
            autoplay: {
                delay: 3000
            },
            pagination: {
                el: '.swiper-pagination',
                clickable: true
            },
            slidesPerView: 1
        });

        var swiperThumb = new Swiper('#product-home .wrapper.number-' + currentIndex + ' .thumb .swiper-container', {
            loop: false,
            autoplay: {
                delay: 3000
            },
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            slidesPerView: 4
        });

        // Đồng bộ swiper full-thumb
        $("#product-home .wrapper.number-" + currentIndex + " .thumb .swiper-slide").click(function (e) {
            e.preventDefault();

            var index = $(this).index() + 1;
            swiperFull.slideTo(index);
        });
    });

    // Khởi tạo swiper carousel cho các sản phẩm khác
    var productOtherSwiper = new Swiper('.product-other-swiper .swiper-container', {
        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',
        coverflowEffect: {
            rotate: 50,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: true,
        },
        loop: true,
        autoplay: {
            delay: 3000,
        },
        slidesPerView: 3
    });

    // Khởi tạo swiper carousel cho danh sách hình của sản phẩm
    var productImageListSwiper = new Swiper('.product-imagelist-swiper .swiper-container', {
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',
        loop: true,
        autoplay: {
            delay: 3000,
        },
        slidesPerView: 1,
        pagination: {
            el: '.swiper-pagination',
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        }
    });

    //Khởi tại mansory
    $(".mansory").mpmansory({
        childrenClass: 'item',
        breakpoints: {
            lg: 4,
            md: 4,
            sm: 6,
            xs: 12
        },
        distributeBy: { order: false, height: false, attr: 'data-order', attrOrder: 'asc' },
        onload: function (items) { }
    });
});