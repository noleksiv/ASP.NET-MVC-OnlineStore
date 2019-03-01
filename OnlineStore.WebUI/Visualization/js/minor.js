(function ($) {
    "use strict";

    $("#submit-button").click(function () {
        var arr = [];

        // unction fills up the array
        getItems();

        $.ajax({
            url: '/Home/ReceiveCartItems',
            type: "POST",
            data: {
                "json": JSON.stringify(arr)
            },
            dataType: "json",
            async: false,
            success: function (data) {
                if(data.result=='redirect')
                window.location.href = data.url;              
            },
            error: function (data) {
                alert("smth went wrong....");
            }            
        });   

        function getItems() {

            var clothes = {
                Name: undefined,
                Number: undefined,
                Size: undefined,
                Color: undefined
            };

            $('.header-cart-wrapitem').find('li').each(function () {
                // deep copy
                var item = jQuery.extend(true, {}, clothes);

                item.Name = $(this).find('.header-cart-item-name').text();
                item.Size = $(this).find('.header-cart-item-size').text();
                item.Color = $(this).find('.header-cart-item-color').text();;
                var tempStr = $(this).find('.header-cart-item-info').text();
                item.Number = parseInt(tempStr.substring(0, tempStr.indexOf(" x ")));

                arr.push(item);
            });
        }; 

    });

})(jQuery);