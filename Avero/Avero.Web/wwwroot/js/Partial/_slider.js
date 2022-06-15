// --------------------------- Slider & pagitation start ---------------------------
export function slider_func() {
    let element_index = 4;          // mine
    var btns = document.querySelectorAll('.btn');
    var paginationWrapper = document.querySelector('.pagination-wrapper');

    for (var i = 0; i < btns.length; i++) {
        btns[i].addEventListener('click', btnClick);
    }

    function btnClick() {
        if (this.classList.contains('btn--prev')) {
            paginationWrapper.classList.add('transition-prev');
            element_index = (element_index % 5) - 1;    // mine
            if (element_index == 0)                     // mine
                element_index = 4;                      // mine
            slider_show_prev(element_index);            // mine
        } else {
            paginationWrapper.classList.add('transition-next');
            element_index = (element_index % 4) + 1;    // mine
            slider_show_next(element_index);            // mine
        }
        var timeout = setTimeout(cleanClasses, 800);
    }

    function cleanClasses() {
        if (paginationWrapper.classList.contains('transition-next')) {
            paginationWrapper.classList.remove('transition-next')
        } else if (paginationWrapper.classList.contains('transition-prev')) {
            paginationWrapper.classList.remove('transition-prev')
        }
    }

    function slider_show_prev(element) {
        clearInterval(auto_slide_intervar);  // (to reset interval) stop it first and at the end start it again
        $('.slider-right').animate({
            'opacity': "0"
        }, 800, function () {
            $(this).css({
                'background': 'url(/img/test/r' + element + '.jpg) center center no-repeat',
                'background-size': 'cover',
            });
            $(this).animate({
                'opacity': "1"
            }, 800);
        });

        $('.slider-left').animate({
            'left': "-600"
        }, 800, function () {
            $(this).css({

            });
            $(this).animate({
                'left': "0"
            }, 800);
        });
        auto_slide_intervar = setInterval(function () {  // reset the interval
            element_index = (element_index % 4) + 1;
            slider_show_next(element_index);
        }, 6000);
    }


    function slider_show_next(element) {
        clearInterval(auto_slide_intervar);  // (to reset interval) stop it first and at the end start it again
        $('.slider-right').animate({
            'right': "-600",
        }, 800, function () {
            $(this).css({
                'background': 'url(/img/test/r' + element + '.jpg) center center no-repeat',
                'background-size': 'cover',
            });
            $(this).animate({
                'right': "0"
            }, 800);
        });

        $('.slider-left').animate({
            'opacity': "0"
        }, 800, function () {
            $(this).css({

            });
            $(this).animate({
                'opacity': "1"
            }, 800);
        });
        auto_slide_intervar = setInterval(function () {  // reset the interval
            element_index = (element_index % 4) + 1;
            slider_show_next(element_index);
        }, 6000);
    }

    // auto slide
    let auto_slide_intervar;

    //  detect if the user minimize browser window or change tab
    document.addEventListener("visibilitychange", event => {
        if (document.visibilityState === "visible") {
            // tab is active (so run the slider)
            auto_slide_intervar = setInterval(function () {
                element_index = (element_index % 4) + 1;
                slider_show_next(element_index);
            }, 6000);
        } else {
            // tab is inactive or minimize (so stop the slider)
            if (auto_slide_intervar)
                clearInterval(auto_slide_intervar);
        }
    });
    auto_slide_intervar = setInterval(function () {
        element_index = (element_index % 4) + 1;
        slider_show_next(element_index);
    }, 6000);
}
    // --------------------------- Slider & pagitation end ---------------------------