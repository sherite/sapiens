app.factory("RegisterFactory", function () {

    var registerFactory = {};

    return registerFactory;
})

app.controller("RegisterController", function ($scope, $location, $translate, $http, RegisterFactory) {

    $scope.register = $translate.instant("index.register");
    $scope.login = $translate.instant("index.login");

    $scope.Initialize = function () { }

});