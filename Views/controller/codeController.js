'use strict'
app.controller('codeController',
    function codeController($scope, $http, $log, limitToFilter) {
        $log.warn('Initializing code controller');
        //$scope.results = [{ code: 1, place: "aa" }];
        $scope.loadData = function () {
            $log.warn("Query-" + $scope.querytext);
            var url = 'http://localhost:41953/api/Zipcode?queryParameters=' + $scope.querytext;
            $log.warn("URL-" + url);
            
            $http.get(url).then(function (response) {
                console.log("Success");
                $scope.results= eval(response.data) ;          
                console.log("Success-End");
            });
        };
    }
);