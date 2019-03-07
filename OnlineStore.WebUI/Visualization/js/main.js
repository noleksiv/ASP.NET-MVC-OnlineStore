
(function ($) {
    "use strict";

    /*[ Load page ]
    ===========================================================*/
    $(".animsition").animsition({
        inClass: 'fade-in',
        outClass: 'fade-out',
        inDuration: 1500,
        outDuration: 800,
        linkElement: '.animsition-link',
        loading: true,
        loadingParentElement: 'html',
        loadingClass: 'animsition-loading-1',
        loadingInner: '<div class="loader05"></div>',
        timeout: false,
        timeoutCountdown: 5000,
        onLoadEvent: true,
        browser: ['animation-duration', '-webkit-animation-duration'],
        overlay: false,
        overlayClass: 'animsition-overlay-slide',
        overlayParentElement: 'html',
        transition: function (url) { window.location.href = url; }
    });

    /*[ Back to top ]
    ===========================================================*/
    var windowH = $(window).height() / 2;

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display', 'flex');
        } else {
            $("#myBtn").css('display', 'none');
        }
    });

    $('#myBtn').on("click", function () {
        $('html, body').animate({ scrollTop: 0 }, 300);
    });


    /*==================================================================
    [ Fixed Header ]*/
    var headerDesktop = $('.container-menu-desktop');
    var wrapMenu = $('.wrap-menu-desktop');

    if ($('.top-bar').length > 0) {
        var posWrapHeader = $('.top-bar').height();
    }
    else {
        var posWrapHeader = 0;
    }


    if ($(window).scrollTop() > posWrapHeader) {
        $(headerDesktop).addClass('fix-menu-desktop');
        $(wrapMenu).css('top', 0);
    }
    else {
        $(headerDesktop).removeClass('fix-menu-desktop');
        $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
    }

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > posWrapHeader) {
            $(headerDesktop).addClass('fix-menu-desktop');
            $(wrapMenu).css('top', 0);
        }
        else {
            $(headerDesktop).removeClass('fix-menu-desktop');
            $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
        }
    });


    /*==================================================================
    [ Menu mobile ]*/
    $('.btn-show-menu-mobile').on('click', function () {
        $(this).toggleClass('is-active');
        $('.menu-mobile').slideToggle();
    });

    var arrowMainMenu = $('.arrow-main-menu-m');

    for (var i = 0; i < arrowMainMenu.length; i++) {
        $(arrowMainMenu[i]).on('click', function () {
            $(this).parent().find('.sub-menu-m').slideToggle();
            $(this).toggleClass('turn-arrow-main-menu-m');
        })
    }

    $(window).resize(function () {
        if ($(window).width() >= 992) {
            if ($('.menu-mobile').css('display') == 'block') {
                $('.menu-mobile').css('display', 'none');
                $('.btn-show-menu-mobile').toggleClass('is-active');
            }

            $('.sub-menu-m').each(function () {
                if ($(this).css('display') == 'block') {
                    console.log('hello');
                    $(this).css('display', 'none');
                    $(arrowMainMenu).removeClass('turn-arrow-main-menu-m');
                }
            });

        }
    });


    /*==================================================================
    [ Show / hide modal search ]*/
    $('.js-show-modal-search').on('click', function () {
        $('.modal-search-header').addClass('show-modal-search');
        $(this).css('opacity', '0');
    });

    $('.js-hide-modal-search').on('click', function () {
        $('.modal-search-header').removeClass('show-modal-search');
        $('.js-show-modal-search').css('opacity', '1');
    });

    $('.container-search-header').on('click', function (e) {
        e.stopPropagation();
    });


    /*==================================================================
    !!!!!!!!!!!!!!!!!!!!!!!
    [ Isotope ]*/
    var $topeContainer = $('.isotope-grid');
    var $filter = $('.filter-tope-group');
    
    // bind sort button click
    $('#sorts').on('click', 'button', function () {
        var sortByValue = $(this).attr('data-sort-by');
        if (sortByValue.indexOf("desc") >= 0) {
            $topeContainer.isotope({ sortBy: sortByValue.replace(" desc", ''), sortAscending: false });
        }
        else {
            $topeContainer.isotope({ sortBy: sortByValue, sortAscending: true });
        }
    });

    // bind any filter button click

    var filters = {};
    $('.filter-tope-group').each(function () {
        $('.filter-tope-group').on('click', 'button', function (event) {
            
            var $button = $(event.currentTarget);
            // get group key
            var $buttonGroup = $button.parents('.filter-tope-group');
            var filterGroup = $buttonGroup.attr('data-filter-group');
            // set filter for group
            filters[filterGroup] = $button.attr('data-filter');
            // combine filters
            var filterValue = concatValues(filters);

            // set filter for Isotope
            $topeContainer.isotope({ filter: filterValue });

            $('#search').val("");
           
        });

    });

    function concatValues(obj) {
        var value = '';
        for (var prop in obj) {
            value += obj[prop];
        }
        return value;
    }

    $('.filter-tope-group').each(function (i, buttonGroup) {
        var $buttonGroup = $(buttonGroup);
        $buttonGroup.on('click', 'button', function (event) {
            if ($(this).hasClass("filter-link")) {
                $buttonGroup.find('.filter-link-active').removeClass('filter-link-active');
                var $button = $(event.currentTarget);
                $button.addClass('filter-link-active');
               
            }
            else {
                $buttonGroup.find('.how-active1').removeClass('how-active1');
                var $button = $(event.currentTarget);
                $button.addClass('how-active1');
                
            }
            $('#search').val("");
        });
    });

    var qsRegex;

    //bind search button
    var $quicksearch = $('#search').keyup('input', function () {
        qsRegex = new RegExp($quicksearch.val(), 'gi');
        // set default values -> global search
        //... 
        // run filter function
        runSearch();
    }, 200);

    var runSearch = function () {
        $topeContainer.isotope({
            filter: function () {
                return qsRegex ? $(this).find('.js-name-b2').text().match(qsRegex) : true;
            }
        });
    }
  
    // init Isotope
    $(window).on('load', function () {
        var $grid = $topeContainer.each(function () {
            $(this).isotope({
                itemSelector: '.isotope-item',
                layoutMode: 'fitRows',
                percentPosition: true,
                animationEngine: 'best-available',
                masonry: {
                    columnWidth: '.isotope-item'
                },
                getSortData: {
                    name: '.js-name-b2',
                    category: '[data-category]',
                    price: function (itemElem) {
                        var weight = $(itemElem).find('.stext-105').text();
                        return parseFloat(weight.replace('$', ''));
                    }
                }
            });
        });
    });
    
    /*==================================================================

    [ low-to-high]*/

    // change is-checked class on buttons
    $('.p-r-15').each(function (i, buttonGroup) {
        var $buttonGroup = $(buttonGroup);
        $buttonGroup.on('click', 'button', function () {
            $buttonGroup.find('.filter-link-active').removeClass('filter-link-active');
            $(this).addClass('filter-link-active');
        });
    });

    /*==================================================================
    [ Filter / Search product ]*/
    $('.js-show-filter').on('click', function () {
        $(this).toggleClass('show-filter');
        $('.panel-filter').slideToggle(400);

        if ($('.js-show-search').hasClass('show-search')) {
            $('.js-show-search').removeClass('show-search');
            $('.panel-search').slideUp(400);
        }
    });

    $('.js-show-search').on('click', function () {
        $(this).toggleClass('show-search');
        $('.panel-search').slideToggle(400);

        if ($('.js-show-filter').hasClass('show-filter')) {
            $('.js-show-filter').removeClass('show-filter');
            $('.panel-filter').slideUp(400);
        }
    });

    // debounce so filtering doesn't happen every millisecond
    function debounce(fn, threshold) {
        var timeout;
        threshold = threshold || 100;
        return function debounced() {
            clearTimeout(timeout);
            var args = arguments;
            var _this = this;
            function delayed() {
                fn.apply(_this, args);
            }
            timeout = setTimeout(delayed, threshold);
        };
    }


    /*==================================================================
    [ Cart ]*/   
    // show cart with possible purchases
    $('.js-show-cart').on('click', function () {
        $('.js-panel-cart').addClass('show-header-cart');
    });

    $('.js-hide-cart').on('click', function () {
        $('.js-panel-cart').removeClass('show-header-cart');
    });

    // add cloth to the cart
    $('.js-addcart-detail').click(function () {

        var position = this.className.indexOf("item-");
        var keyClass = this.className.substring(position, this.className.length);

        var name = $('.' + keyClass).find(".js-name-b2").text();
        var clothId = $('.' + keyClass).find(".js-name-b2").attr('id');
        var image = $('.' + keyClass).find(".block2-pic img").attr("src");
        var price = $('.' + keyClass).find(".stext-105").text();
        
        // get specify counter +/-
        var count = $('input[class*="' + keyClass + '"]').val();
        var size = $("#size." + keyClass + " option:selected").text();
        var color = $("#color." + keyClass + " option:selected").text();

        addElementToCart(name, count, size, color, clothId, price, image, keyClass);
    });



    function addItemToCart(class_, name_, icon_, price_, count_,size_,color_,id_) {
        $('.header-cart-content ul').append(
            $('<li>').addClass("header-cart-item flex-w flex-t m-b-12 " + class_).append (
                $('<div>').addClass("header-cart-item-img " + class_).append(
                    $('<img>').attr({ id: "cart-img", src :icon_})
                )).append (
                $('<div>').addClass("header-cart-item-txt p-t-8").append(
                    $('<a>').addClass("header-cart-item-name m-b-18 hov-cl1 trans-04").attr({ href: '#', name: 'Name' }).append(name_)
                    )
                    .append(
                            $('<span>').addClass("header-cart-item-info").append(count_ + " x " + price_)
                )
            ).append(
                // addition data
                $('<div>').hide().append (
                        $('<p>').addClass('header-cart-item-id').append(id_)
                ).append (
                        $('<p>').addClass('header-cart-item-color').append(color_)
                ).append (               
                        $('<p>').addClass('header-cart-item-size').append(size_)
                )
            ));
    }

    function addElementToCart(name_, count_, size_, color_, id_, price_, icon_, class_) {       
        var clothes = {
            Name: name_,
            Number: count_,
            Size: size_,
            Color: color_,
            ClothId: id_
        };
        $.ajax({
            url: '/Home/AddToCart',
            type: "POST",
            data: {
                "json": JSON.stringify(clothes)
            },
            success: function (result) {
                if (result.success == true) {
                    swal(name, "Is added to cart !", "success");
                    addItemToCart(class_, name_, icon_, price_, count_, size_, color_, id_);
                    totalSum();
                }
                else {
                    swal(name, result.msg , "error");
                }
            },
            error: function () {
                swal(name, "Smth went wrong!", "error");
            },
            dataType: "json",
            async: false
        });
    }

    function totalSum() {
        var quantity = $('.header-cart-item-info').length;

        if (quantity > 0) {

            var sum = 0.0;

            $('.header-cart-item-info').each(function () {
                var countPrice = divideByCountPrice($(this).text());
                sum += parseInt(countPrice[0]) * parseFloat(countPrice[1]);
            });
            $('div[class="header-cart-total w-full p-tb-40"]').text("Total: $" + sum.toFixed(2));
        }
        else {
            $('div[class="header-cart-total w-full p-tb-40"]').text("");
        }
        $('.quantity-in-cart').attr("data-notify", quantity.toString());

    }

    function divideByCountPrice(str) {
        
        var position = str.indexOf(" x ");

        return [str.substring(0, position), str.substring(position + 2, str.length).replace('$', '').replace(",", '.')]
    }
    /*==================================================================
    [ Cart ]*/
    $('.js-show-sidebar').on('click', function () {
        $('.js-sidebar').addClass('show-sidebar');
    });

    $('.js-hide-sidebar').on('click', function () {
        $('.js-sidebar').removeClass('show-sidebar');
    });

    
    /*==================================================================
    [ -/+ num product ]*/   

    $('.btn-num-product-down').on('click', function () {
        var min = parseInt($(this).parent().find(".num-product").attr("min"));

        var numProduct = Number($(this).next().val());
        if (numProduct > min) $(this).next().val(numProduct - 1);
    });

    $('.btn-num-product-up').on('click', function () {
        var max = parseInt($(this).parent().find(".num-product").attr("max"));

        var numProduct = Number($(this).prev().val());
        if (numProduct < max) $(this).prev().val(numProduct + 1);
    });

    /*==================================================================
    [ Rating ]*/
    $('.wrap-rating').each(function () {
        var item = $(this).find('.item-rating');
        var rated = -1;
        var input = $(this).find('input');
        $(input).val(0);

        $(item).on('mouseenter', function () {
            var index = item.index(this);
            var i = 0;
            for (i = 0; i <= index; i++) {
                $(item[i]).removeClass('zmdi-star-outline');
                $(item[i]).addClass('zmdi-star');
            }

            for (var j = i; j < item.length; j++) {
                $(item[j]).addClass('zmdi-star-outline');
                $(item[j]).removeClass('zmdi-star');
            }
        });

        $(item).on('click', function () {
            var index = item.index(this);
            rated = index;
            $(input).val(index + 1);
        });

        $(this).on('mouseleave', function () {
            var i = 0;
            for (i = 0; i <= rated; i++) {
                $(item[i]).removeClass('zmdi-star-outline');
                $(item[i]).addClass('zmdi-star');
            }

            for (var j = i; j < item.length; j++) {
                $(item[j]).addClass('zmdi-star-outline');
                $(item[j]).removeClass('zmdi-star');
            }
        });
    });

    /*==================================================================
    [ Show modal1 ]*/
    // open owerlay modal
    $('[class*="js-show-modal-"]').on('click', function (e) {
        e.preventDefault();
        var beginOfLastClass = this.className.indexOf("js-show-modal-");
        var _class = this.className.substring(beginOfLastClass, this.className.length);
        $('.' + _class.replace("-show", "")).addClass(_class.replace("js-", ""));
    });

    // close owerlay modal
    $('[class*="js-hide-modal-"]').on('click', function () {
        var beginOfLastClass = this.className.indexOf("js-hide-modal-");
        var _class = this.className.substring(beginOfLastClass, this.className.length);
        $('.' + _class.replace("hide-", "")).removeClass(_class.replace("js-hide", "show"));
    });

  

})(jQuery);