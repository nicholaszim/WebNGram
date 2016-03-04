'use strict';

/* Application AngularJS Controllers*/
var FSAControllers = angular.module('FSAControllers', []);

function mainController($scope, $http) {
	$scope.models = {
		categories: {},
		examples: {},
		distance: 0
	};
	$scope.functions = {
		initCategory: function () {

		}
	}
}

/*Injection*/
mainController.$inject = ['$scope', '$http'];
/*Creation of controller*/
FSAControllers.controller('mainController', mainController);