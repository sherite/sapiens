app.factory('GeneralFactory', function () {

    var currentUser = '';
    var currentUserImage = 'images/CurrentUser.png';

    var generalFactory = {

        currentUser: currentUser,
        currentUserImage: currentUserImage
    }

    return generalFactory;
})

app.controller("IndexController", function ($scope, $http, $translate, $location,GeneralFactory) {

    $scope.currentUser = GeneralFactory.currentUser;
    $scope.currentUserImage = 'images/CurrentUser.png';

    $scope.dynamicMenu = [];

    $scope.changeLanguage = function (lang) {

        switch (lang) {
            case "es":
                language = "es-es";
                break;
            case "br":
                language = "br-br";
                break;
            case "fr":
                language = "fr-fr";
                break;
            case "de":
                language = "de-de";
                break;
            case "it":
                language = "it-it";
                break;
            case "en":
                language = "en-us";
                break;
        }

        $translate.use(language);

        $scope.$broadcast('languageChanged', language);
    };

    $scope.go = function (url, action, data) {

        $location.path("/" + url);

    };

    $scope.ButtonPushed = function () {

        IsOpenPushButton = !IsOpenPushButton;
    };

    $scope.Initialize = function () {

        //$('#cover-spin').show(0);

        //$('[data-toggle="push-menu"]').pushMenu('toggle');

        IsOpenPushButton = false;

        $scope.go('login');
    };

    $scope.CreateDynamicMenu = function () {

        $http({
            url: urlBase + "dynamicMenu",
            method: 'GET'
        }).then(
            function successCallback(response) {

                var lista = document.getElementById("tvwSetup");

                var result = "";

                for (var i = 0; i < response.data.length; i++) {

                    result += response.data[i];
                }

                lista.insertAdjacentHTML("beforebegin", result);

            });
    };

    $scope.IsLogin = function () {

        return IsLogin;
    };
});