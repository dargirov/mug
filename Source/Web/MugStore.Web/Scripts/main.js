$(function () {

    $('#bulletin-add-btn').on('click', bulletinAddBtnClick);
    function bulletinAddBtnClick(e) {
        e.preventDefault();
        $('#subscribe-to-bulletin-form').submit();
    }

    $('#subscribe-to-bulletin-form').on('submit', bulletinFormSubmit);
    function bulletinFormSubmit(e) {
        e.preventDefault();
        var url = $(this).data('url');
        var email = $(this).find('input[type=email]').val();
        var token = $(this).find('input[name=__RequestVerificationToken]').val();

        if (email.length < 5) {
            return;
        }

        $.ajax({ method: 'POST', url: url, data: { email: email, __RequestVerificationToken: token } })
            .done(function (data) {
                if (data.success) {
                    $('#subscribe-to-bulletin-form').addClass('hidden');
                    $('#subscribe-success').removeClass('hidden');
                } else {
                    var $email = $('#subscribe-to-bulletin-form').find('input[type=email]');
                    $email.addClass('invalid');
                    if (data.message === 'Email exists') {
                        $email.val('Този адрес е регистриран');
                    }
                }
            });
    }

    $('#image-help').on('click', imageHelpClick);
    function imageHelpClick(e) {
        e.preventDefault();
        var url = $(this).data('url');
        $.colorbox({ href: url, opacity: 1, transition: "none", className: 'image-help-colorbox', close: 'Затвори', width: 900, height: 620 });
    }

    Notification.init();
    Notification.bind('.popover');

});