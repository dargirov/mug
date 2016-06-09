var Mug = (function(BABYLON) {
	'use strict';

	var _name, _x, _y, _z, _scene;
	var MAX_IMAGE_WIDTH_CM = 20, MAX_IMAGE_HEIGHT_CM = 8.5;
	var _images = [];

	function init(name, position, scene) {
		_name = name;
		_x = position.x || 0;
		_y = position.height || 2;
		_z = position.z || 0;
		_scene = scene;
	}

	function createMug() {
		var mugShape = [];
		mugShape.push(new BABYLON.Vector3(_x, -_y, _z));
		mugShape.push(new BABYLON.Vector3(_x, _y, _z));
	    
		var mugMat = new BABYLON.StandardMaterial(_name + 'MugMat', _scene);
		mugMat.alpha = 1.0;
		mugMat.backFaceCulling = false;
		mugMat.sideOrientation = Math.PI / 4;
		mugMat.wireframe = false;

		var mugInnerMat = new BABYLON.StandardMaterial(_name + 'MugInnerMat', _scene);
		mugInnerMat.backFaceCulling = false;

		var mug = BABYLON.Mesh.CreateTube(_name + 'Mug', mugShape, 1.8, 30, null, 0, _scene, true, 900);
		var mugInner = BABYLON.Mesh.CreateTube(_name + 'MugInner', mugShape, 1.70, 30, null, 0, _scene, true, 900);
		mug.material = mugMat;
		mugInner.material = mugInnerMat;
	}

	function createTop() {
		var top = BABYLON.Mesh.CreateTorus(_name + 'Top', 3.5, 0.1, 60, _scene);
		top.position.x = _x;
		top.position.y = _y + 0.03;
		top.position.z = _z;
	}

	function createBottom() {
		var bottomMat = new BABYLON.StandardMaterial(_name + 'BottomMat', _scene);
		var bottomDisc = BABYLON.Mesh.CreateDisc(_name + 'BottomDisc', 1.8, 40, _scene);
		bottomDisc.position.x = _x;
		bottomDisc.position.y = -_y;
		bottomDisc.position.z = _z;
		bottomMat.backFaceCulling = false;
		bottomDisc.rotation = new BABYLON.Vector3(Math.PI / 2, 2, 2);
		bottomDisc.material = bottomMat;
	}

	function createHandle() {
		var shape = [
			new BABYLON.Vector3(0.2, 0.4, 0),
			new BABYLON.Vector3(0.15, 0.1, 0),
			new BABYLON.Vector3(0, 0.2, 0),
			new BABYLON.Vector3(-0.15, 0.1, 0),
			new BABYLON.Vector3(-0.2, 0.4, 0),
		];

		var path = [];
		var step = -Math.PI * 1 / 100;
		for(var i = 0; i < 100; i++) {
			var point = new BABYLON.Vector3(-1.7 + 0.9 * Math.sin(step * i), 0.2 + 1.12 * Math.cos(step * i), 0);
			path.push(point); 
		}

		var mat = new BABYLON.StandardMaterial(_name + 'HandleMat', _scene);
		mat.alpha = 1.0;
		mat.backFaceCulling = false;

		var myScale = function(i, distance) {
			var scale = 1;
			return scale;
		};

		var myRotation = function(i, distance) {
			return 0;	
		};

		var handle = BABYLON.Mesh.ExtrudeShapeCustom(_name + 'Handle', shape, path, myScale, myRotation, true, true, 0, _scene);
		handle.position.x = _x;
		handle.position.y = 0;
		handle.position.z = _z + 0.005;
		handle.rotation.y = Math.PI / 1.95;
		handle.material = mat;
	}

	function addImage(imageData) {
	    var name = imageData.name;
		var url = imageData.url;
		var width = imageData.width;
		var height = imageData.height;
		var dpi = imageData.dpi;

		var widthInCm = width / dpi * 2.54;
		var heightInCm = height / dpi * 2.54;

        // check if the image is too big and needs to be resized
		if (widthInCm > MAX_IMAGE_WIDTH_CM || heightInCm > MAX_IMAGE_HEIGHT_CM) {
		    var percent = MAX_IMAGE_WIDTH_CM / widthInCm * 100;
		    var newHeightInCm = percent / 100 * heightInCm;	    

		    if (newHeightInCm > MAX_IMAGE_HEIGHT_CM) {
		        percent = MAX_IMAGE_HEIGHT_CM / heightInCm * 100;
		        var newWidthInCm = percent / 100 * widthInCm;
		        widthInCm = newWidthInCm;
		        heightInCm = MAX_IMAGE_HEIGHT_CM;
		    } else {
		        widthInCm = MAX_IMAGE_WIDTH_CM;
		    }
		}

		// this coefficient sets the width of the image
		var mul = 0.82 + (2 * widthInCm / MAX_IMAGE_WIDTH_CM);

		// height of the image
		var h = 0.1 + (3.6 * heightInCm / MAX_IMAGE_HEIGHT_CM);

		var path1 = [];
	 	var path2 = [];
		var pi2 = mul * Math.PI; // mul = 0.8 -> 2.8
		var tess = 60;
		var step = pi2 / tess;
		var imageCountCoef = _images.length * 0.005;

		for (var i = 2; i < pi2 / 1.2; i += step) {
			path1.push(new BABYLON.Vector3(-Math.cos(i) * (1.81 + imageCountCoef), h / 2, -Math.sin(i) * (1.81 + imageCountCoef)));
			path2.push(new BABYLON.Vector3(-Math.cos(i) * (1.81 + imageCountCoef), -h / 2, -Math.sin(i) * (1.81 + imageCountCoef)));
		}

		var ribbon = BABYLON.Mesh.CreateRibbon(url + 'Image', [path1, path2], false, false, 0, _scene, false, BABYLON.Mesh.FRONTSIDE);
		var material = new BABYLON.StandardMaterial(url + 'ImageMat', _scene);
		material.alpha = 1.0;
		material.backFaceCulling = false;
		var picture = new BABYLON.Texture(url, _scene, true, false);
		material.diffuseTexture = picture;
		ribbon.material = material;
		ribbon.position.x = _x;
		ribbon.position.z = _z;
		ribbon.rotation.y = Math.PI;
		var rotationOffset = 0;

		if (imageData.hasOwnProperty('rotation')) {
		    ribbon.rotation.y = parseFloat(imageData.rotation.replace(',', '.'));
		}

		if (imageData.hasOwnProperty('y')) {
		    ribbon.position.y = parseFloat(imageData.y.replace(',', '.'));
		}

		var result = {
			moveUp: function() {
				if (ribbon.position.y >= (_y - 0.15 - h / 2)) {
					return;
				}

				ribbon.position.y += 0.1;
			},
			moveDown: function() {
				if (ribbon.position.y <= -(_y - 0.15 - h / 2)) {
					return;
				}

				ribbon.position.y -= 0.1;	
			},
			moveLeft: function() {
				if (ribbon.rotation.y >= Math.PI) {
					return;
				}

				rotationOffset -= 0.39;
				ribbon.rotation.y += 0.1;
			},
			moveRight: function() {
				if ((MAX_IMAGE_WIDTH_CM - widthInCm - rotationOffset) <= 0) {
					return;
				}

				rotationOffset += 0.39;
				ribbon.rotation.y -= 0.1;
			},
			getInfo: function() {
			    return { name: name, y: ribbon.position.y, rotation: ribbon.rotation.y };
			}
		};

		_images.push(result);
		return result;
	}

	function getImage(index) {
		return _images[index];
	}

	function getImagesCount() {
		return _images.length;
	}

	function create() {
		createMug();
		createTop();
		createBottom();
		createHandle();
	}

	return {
		init: init,
		create: create,
		addImage: addImage,
		getImage: getImage,
		getImagesCount: getImagesCount
	};

})(BABYLON);