$(document).ready(function () {

    $('.gallery-img').colorbox({
        rel: 'gallery-img', transition: "fade", photo: true, close: 'Затвори', onLoad: function () {
            $('#cboxPrevious').remove();
            $('#cboxNext').remove();
            $('#cboxSlideshow').remove();
            $('#cboxCurrent').remove();
        }
    });

    var scene = Scene;
    scene.init({ canvasId: 'canvas' });

    var mug1 = Mug;
    mug1.init('mugPreview', { x: 0, height: 2, z: 0 }, scene.getScene());
    mug1.create();

    var cart = Cart;
    cart.init({ mug: mug1 });

    var previewImages = $('#product-details').data('preview-images').split(',');

    for (var i in previewImages) {
        mug1.addImage({ name: previewImages[i], url: '/DownloadProductImage/' + previewImages[i], width: 1111, height: 1111, dpi: 72 });
    }

    $('#show-form-btn').on('click', orderCreateBtnClick);
    function orderCreateBtnClick(e) {
        e.preventDefault();

        var $orderDataContainer = $('#product-details-order').find('.hidden').first();
        $orderDataContainer.removeClass('hidden');
        $(this).remove();
        $('#create-order-btn').removeClass('hidden');
    }

});