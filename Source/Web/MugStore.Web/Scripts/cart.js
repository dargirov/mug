var Cart = (function($) {
	'use strict';

	var _selector, _mug, _skipCustomization = false, _currentStep = 1;

	function bindCarousel() {
		$('#recommended-products').touchCarousel({
			itemsPerPage: 4,
			scrollbar: false,
			scrollbarAutoHide: true,
			pagingNav: false,
			snapToItems: true,
			scrollToLast: false,
			useWebkit3d: true,
			loopItems: true,
			autoplay: true,
			autoplayDelay: 1500
	    });
	}

	function bindUpload() {
		$('#step1').find('input[type=file]').on('change', function(){    
	    	var $self = $(this);
		    var data = new FormData();
		    data.append('upload_file', $self.prop('files')[0]);
		    // append other variables to data if you want: data.append('field_name_x', field_value_x);

		    $.ajax({
		        type: 'POST',               
		        processData: false, // important
		        contentType: false, // important
		        data: data,
		        url: $('#step1').data('url'),
		        dataType : 'json',  
		        // in PHP you can call and process file in the same way as if it was submitted from a form:
		        // $_FILES['input_file_name']
		        success: uploadFile
		    }); 
		});
	}

	function uploadFile(data) {
		_mug.addImage({url: '/Image/' + data.filename, width: data.width, height: data.height, dpi: data.dpi});
		$('#customization-controls-container').removeClass('hidden');
		var $controls = $($('.move-controls.hidden')[0]);
		$controls.removeClass('hidden');
	}

	function bindMovingImages() {
		$('.move').on('click', moveButtonClick);
		function moveButtonClick(e) {
			e.preventDefault();
			var imageIndex = $(this).parent().parent().data('image');
			var dir = $(this).data('direction');
			var image = _mug.getImage(imageIndex);

			switch (dir)
			{
				case 'up':
					image.moveUp();
					break;
				case 'down':
					image.moveDown();
					break;
				case 'left':
					image.moveLeft();
					break;
				case 'right':
					image.moveRight();
					break;
			}
		}
	}

	function bindEvents() {

		bindCarousel();

		if (!_skipCustomization) {
			bindUpload();
			bindMovingImages();
		}
		

		// next step buttons
		$('#goto-step2').on('click', gotoStep2BtnClick);
		function gotoStep2BtnClick(e) {
			e.preventDefault();
			gotoStep2();
		}

		$('#goto-step3').on('click', gotoStep3BtnClick);
		function gotoStep3BtnClick(e) {
			e.preventDefault();
			gotoStep3();
		}

		// quantity range
		$('#quantity-range').on('change', quantityRangeChange);
		function quantityRangeChange(e) {
			var price = parseFloat($('#single-price').data('price'));
			var quantity = parseInt($(this).val());
			var deliveryFee = parseFloat($('#delivery-fee').data('fee'));

			$('#selected-quantity').html(quantity);
			$('#price-container').find('h3').html((price * quantity + deliveryFee) + ',');
		}

		// go back bottons
		$('.order-back').on('click', goBackToStep);
		function goBackToStep(e) {
			e.preventDefault();
			var toStep = parseInt($(this).data('to-step'));
			switch (toStep) {
				case 1:
					gotoStep1();
					break;
				case 2:
					gotoStep2();
					break;
				case 3:
					gotoStep3();
					break;
			}
		}

		// final button for creating order
		$('#create-order-btn').on('click', createOrderBtnClick);
		function createOrderBtnClick(e) {
			e.preventDefault();
			var count = _mug.getImagesCount();
			var data = {};
			data.images = [];
			for (var i = 0; i < count; i++) {
				data.images.push(_mug.getImage(i).getInfo());
			}

			data.quantity = $('#quantity-range').val();
			data.price = $('#single-price').data('price');
			data.deliveryType = $('#delivery-type').val();
			data.names = $('#names-field').val();
			data.phone = $('#phone-field').val();
			data.city = $('#city-dd').val();
			data.address = $('#address-field').val();
			data.comment = $('#comment-field').val();

			var invalidInput = false;
			if (data.names.length === 0) {
				$('#names-field').addClass('invalid').val('Въведи име и фамилия');
				invalidInput = true;
			}

			if (data.names.length > 0 && data.names.length < 4) {
				$('#names-field').addClass('invalid').val('Въведи валидно име и фамилия');
				invalidInput = true;
			}

			if (data.phone.length === 0) {
				$('#phone-field').addClass('invalid').val('Въведи телефон за контакт');
				invalidInput = true;
			}

			if (data.phone.length > 0 && data.phone.length < 5) {
				$('#phone-field').addClass('invalid').val('Въведи валиден телефон за контакт');
				invalidInput = true;
			}

			if (data.address.length === 0) {
				$('#address-field').addClass('invalid').val('Въведи адрес за доставка');
				invalidInput = true;
			}

			if (data.address.length > 0 && data.address.length < 8) {
				$('#address-field').addClass('invalid').val('Въведи валиден адрес за доставка');
				invalidInput = true;
			}

			if (!invalidInput) {
				console.log(JSON.stringify(data), data);				
			}
		}

		$('body').on('focus', '.invalid', removeInvalidMessageFocus);
		function removeInvalidMessageFocus(e) {
			$(this).val('');
			$(this).removeClass('invalid');
		}
	}


	
	
	var gotoStep1 = function() {
		$('#step1').removeClass('hidden');
		$('#step2').addClass('hidden');
		$('#step3').addClass('hidden');
	}

	var gotoStep2 = function() {
		$('#step1').addClass('hidden');
		$('#step3').addClass('hidden');
		$('#step2').removeClass('hidden');
	}

	var gotoStep3 = function() {
		$('#step1').addClass('hidden');
		$('#step2').addClass('hidden');
		$('#step3').removeClass('hidden');
	}

	function init(options) {
		_selector = options.selector;
		_mug = options.mug;
		if (options.mug === undefined) {
			_skipCustomization = true;
		}

		bindEvents();
	}

	return {
		init: init
	}

})(jQuery);