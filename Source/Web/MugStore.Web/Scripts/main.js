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

});