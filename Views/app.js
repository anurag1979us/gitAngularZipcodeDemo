var app = angular.module("myApp", ['ngRoute', 'ui.bootstrap'])
.config(function ($routeProvider) {
    $routeProvider.when('/about',
        {
            templateUrl: 'Templates/Zipcode.html',
            controller: ''
        }).when('/home',
        {
            templateUrl: 'Templates/Zipcode.html',
            controller: 'homeController'
        }).otherwise('/home',
        {
            templateUrl: 'Templates/about.html',
            controller: 'homeController'
        });
});
