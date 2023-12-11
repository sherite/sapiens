app.factory("ChangePasswordFactory", function () {

    var password = '';

    var changePasswordFactory = {};

    changePasswordFactory.password = password;

    return changePasswordFactory;
});

app.controller('ChangePasswordController', function ($scope, $location, $translate, $http, ChangePasswordFactory) {

    $scope.showOldPassword = false;
    $scope.oldPasssword = '';
    $scope.password1 = '';
    $scope.password2 = '';

    $scope.placeHolderPassword1 = $translate.instant('index.newPassword');
    $scope.placeHolderPassword2 = $translate.instant('index.retypeNewPassword');
    $scope.txtChangePassword = $translate.instant('index.changePassword');
    $scope.login = $translate.instant('index.login');

    $scope.Initialize = function () {

        ChangePasswordFactory.password = $scope.password;


    }
});