app.factory("ForgotPasswordFactory", function () {

    var login;

    var forgotPasswordFactory = {};

    forgotPasswordFactory.login = login;

    return forgotPasswordFactory;

});

app.controller('ForgotPasswordController', function ($scope, $location, $translate, $http, ForgotPasswordFactory) {

    $scope.login = $translate.instant("index.login");
    $scope.register = $translate.instant("index.register");
    $scope.requestNewPassword = $translate.instant("index.requestNewPassword");

    $scope.Initialize = function () {

        ForgotPasswordFactory.login = $scope.login;
    };

    $scope.go = function (url, actions, data) {

        $location.path("/" + url);
    }
});