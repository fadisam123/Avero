import { slider_func } from "./Partial/_slider.js";  // js code for slider

$(function () {
    // ------------------ nav on scroll behavior start ------------------
    {
        let w = $(window);
        function scrollFunction() {
            if (document.body.scrollTop > 40 || document.documentElement.scrollTop > 40) {
                if (w.width() > 576)
                    $('nav').addClass(' bg-dark  bg-opacity-50 to-top');
                else
                    $('nav').removeClass('bg-dark  bg-opacity-50 to-top');
            } else {
                $('nav').removeClass('bg-dark  bg-opacity-50 to-top');
            }
        }
        window.onscroll = function () { scrollFunction() };
        window.onresize = function () { scrollFunction() };
    }
    // ------------------ nav on scroll behavior end ------------------



    // ------------ dim the screen when nav dropdown click start ------------
    {
        let el_overlay;
        // in small size (Dim the screen on navbar icon clicked (navbar-toggler) )
        let clicked = true; // check if the dim screen if already exist (prevent from double dim)
        $('.navbar .navbar-toggler').click(function () {
            if (clicked) {
                el_overlay = document.createElement('span');
                el_overlay.className = 'screen-darken';
                document.body.appendChild(el_overlay);
                clicked = false;
            }
            else {
                document.body.removeChild(document.querySelector('.screen-darken'));
                clicked = true;
            }
        });


        // in navbar Dim the screen on menu item active and focus on dropdown
        document.querySelectorAll('.navbar .dropdown').forEach(function (everydropdown) {
            everydropdown.addEventListener('shown.bs.dropdown', function () {
                if (!clicked)
                    return;
                el_overlay = document.createElement('span');
                el_overlay.className = 'screen-darken';
                document.body.appendChild(el_overlay);
            });

            everydropdown.addEventListener('hide.bs.dropdown', function () {
                if (clicked)
                    document.body.removeChild(document.querySelector('.screen-darken'));
            });
        });
    }
    // ------------ dim the screen when nav dropdown click end ------------

    slider_func();

});