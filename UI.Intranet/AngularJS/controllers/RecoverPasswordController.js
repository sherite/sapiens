app.factory("RecoverPasswordFactory", function () {

    var password = '';

    var recoverPasswordFactory = {};

    recoverPasswordFactory.password = password;

    return recoverPasswordFactory;
});

app.controller('RecoverPasswordController', function ($scope, $location, $translate, $http, RecoverPasswordFactory) {

    $scope.password = '';

    RecoverPasswordFactory.password = $scope.password;

    $scope.placeHolderTFAPassword = $translate.instant('index.placeHolderTFAPassword');
    $scope.txtRecoverPassword = $translate.instant('index.txtRecoverPassword');
    $scope.login = $translate.instant('index.login');

    $scope.Initialize = function () {

      
    }
});