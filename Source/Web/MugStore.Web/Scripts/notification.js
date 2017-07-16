var Notification = (function($) {

    var _trigger, _animation;

    function init(options) {
        if (options === null || typeof options !== 'object') {
            options = {};
        }

       _trigger = options.trigger || 'hover';
       _animation = options.animation || 'pop';
    }

    function bind(selector) {
        $(selector).webuiPopover({ trigger: _trigger });
    }

    function show(options) {
        if (options === null || typeof options !== 'object') {
            options = {};
        }

        var selector = options.selector || 'body';
        var title = options.title || '';
        var content = options.content || '';
        var timeout = options.timeout || 0;

        $(selector).webuiPopover('destroy');
        $(selector).webuiPopover({ trigger: 'manual', title: title, content: content, animation: _animation, placement: 'top' });
        $(selector).webuiPopover('show');
        if (timeout > 0) {
            setTimeout(function () { $(selector).webuiPopover('hide'); }, timeout);
        }
    }

    return {
        init: init,
        bind: bind,
        show: show
    }

})(jQuery);