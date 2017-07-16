var Scene = (function ($, BABYLON) {
	'use strict';

	var _canvasId, _scene;

	function initBabylon() {

	    var canvas = document.getElementById(_canvasId);
		var engine = new BABYLON.Engine(canvas, true);

		var createScene = function() {
			var scene = new BABYLON.Scene(engine);
			var camera = new BABYLON.ArcRotateCamera('camera', 3.3, 1.1, 8.0, new BABYLON.Vector3(0, 0, 0), scene);

			camera.setTarget(BABYLON.Vector3.Zero());
			camera.attachControl(canvas, false);

			var lightFront = new BABYLON.HemisphericLight('lightFront', new BABYLON.Vector3(0, 10, 10), scene);
			var lightBack = new BABYLON.PointLight('lightBack', new BABYLON.Vector3(0, 10, -10), scene);
			var lightHandle = new BABYLON.PointLight('lightHandle', new BABYLON.Vector3(0, -10, -2.5), scene);

			scene.clearColor = new BABYLON.Color4(228 / 255, 228 / 255, 228 / 255, 1); 
			return scene;
		}

		_scene = createScene();	

		engine.runRenderLoop(function() {
			_scene.render();
		});

		window.addEventListener('resize', function() {
			engine.resize();
		});
    }

	var showAxis = function(size) {
	    var makeTextPlane = function(text, color, size) {
	    var dynamicTexture = new BABYLON.DynamicTexture("DynamicTexture", 50, _scene, true);
	    dynamicTexture.hasAlpha = true;
	    dynamicTexture.drawText(text, 5, 40, "bold 36px Arial", color , "transparent", true);
	    var plane = new BABYLON.Mesh.CreatePlane("TextPlane", size, _scene, true);
	    plane.material = new BABYLON.StandardMaterial("TextPlaneMaterial", _scene);
	    plane.material.backFaceCulling = false;
	    plane.material.specularColor = new BABYLON.Color3(0, 0, 0);
	    plane.material.diffuseTexture = dynamicTexture;
	    return plane;
	    };

	    var axisX = BABYLON.Mesh.CreateLines("axisX", [ 
	      new BABYLON.Vector3.Zero(), new BABYLON.Vector3(size, 0, 0), new BABYLON.Vector3(size * 0.95, 0.05 * size, 0), 
	      new BABYLON.Vector3(size, 0, 0), new BABYLON.Vector3(size * 0.95, -0.05 * size, 0)
	      ], _scene);
	    axisX.color = new BABYLON.Color3(1, 0, 0);
	    var xChar = makeTextPlane("X", "red", size / 10);
	    xChar.position = new BABYLON.Vector3(0.9 * size, -0.05 * size, 0);
	    var axisY = BABYLON.Mesh.CreateLines("axisY", [
	        new BABYLON.Vector3.Zero(), new BABYLON.Vector3(0, size, 0), new BABYLON.Vector3( -0.05 * size, size * 0.95, 0), 
	        new BABYLON.Vector3(0, size, 0), new BABYLON.Vector3( 0.05 * size, size * 0.95, 0)
	        ], _scene);
	    axisY.color = new BABYLON.Color3(0, 1, 0);
	    var yChar = makeTextPlane("Y", "green", size / 10);
	    yChar.position = new BABYLON.Vector3(0, 0.9 * size, -0.05 * size);
	    var axisZ = BABYLON.Mesh.CreateLines("axisZ", [
	        new BABYLON.Vector3.Zero(), new BABYLON.Vector3(0, 0, size), new BABYLON.Vector3( 0 , -0.05 * size, size * 0.95),
	        new BABYLON.Vector3(0, 0, size), new BABYLON.Vector3( 0, 0.05 * size, size * 0.95)
	        ], _scene);
	    axisZ.color = new BABYLON.Color3(0, 0, 1);
	    var zChar = makeTextPlane("Z", "blue", size / 10);
	    zChar.position = new BABYLON.Vector3(0, 0.05 * size, 0.9 * size);
	};

	function init(options) {
	    _canvasId = options.canvasId;
		initBabylon();
	}

	function getScene() {
		return _scene;
	}

	function isSupported() {
	    return BABYLON.Engine.isSupported();
	}

	return {
        isSupported: isSupported,
		init: init,
		getScene: getScene,
        showAxis: showAxis
	}

})(jQuery, BABYLON);