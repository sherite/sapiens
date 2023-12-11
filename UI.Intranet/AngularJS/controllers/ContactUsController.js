app.factory("ContactUsFactory", function () {

    var contactUsFactory = {};

    return contactUsFactory;
});

app.controller("ContactUSController", function ($scope, $translate, $http, ContactUsFactory) {

    $scope.Initialize = function () {

        $scope.Name = '';

        $scope.Email = '';

        $scope.Subject = '';

        $scope.Message = '';
}
});