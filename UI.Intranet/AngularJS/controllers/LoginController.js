app.factory('LoginService', function ($http) {

    var userName = '';
    var password = '';
    var loginService = {};

    loginService.isAuthenticated = false;
    loginService.userName = userName;
    loginService.password = password;

    return loginService;
});

app.controller('LoginController', function ($scope, $location, $http, $translate, LoginService) {
   

    $scope.Initialize = function () {

        //$('#cover-spin').hide();

        $scope.username = "";
        $scope.password = "";
        $scope.currentUser = "";


        //$('[data-toggle="push-menu"]').pushMenu('toggle');
        //document.getElementById("pushButton").disabled = true;
        //document.getElementById("pushButton").style.visibility = "hidden";
        //document.getElementById("userMenu").style.visibility = "hidden";
        document.getElementById("leftBar").style.visibility = "hidden";
        document.getElementById("myBody").className = "skin-blue sidebar-collapse";

        if (IsOpenPushButton) {

            //$('[data-toggle="push-menu"]').pushMenu('toggle');
            //document.getElementById("pushButton").disabled = true;
            IsOpenPushButton = false;
        }

        //$('#cover-spin').hide(0);

    };


    $scope.forgotPassword = $translate.instant('index.forgotPassword')
    $scope.register = $translate.instant('index.register')

    $scope.formSubmit = function () {

        $location.path('/dashboard');

        //document.getElementById("pushButton").disabled = false;
        //document.getElementById("pushButton").style.visibility = "visible";
        //document.getElementById("userMenu").style.visibility = "visible";
        document.getElementById("myBody").className = "skin-blue sidebar-mini";
        document.getElementById("leftBar").style.visibility = "visible";

        //LoginService.isAuthenticated = false;

        //$scope.username.replaceAll('\\', '');
        //$scope.password.replaceAll('\\', '');

        //$http.get(urlBase + 'login?user=' + $scope.username + '&password=' + $scope.password).then(function (a, b, c) {

        //    LoginService.isAuthenticated = true;

        //    LoginService.username = $scope.username;
        //    LoginService.password = $scope.password;

        //    $scope.error = '';

        //    $scope.$parent.currentUser = LoginService.username;
        //    $scope.$parent.currentUserImage = "images/" + LoginService.username + ".png";

        //    IsLogin = LoginService.isAuthenticated;

        //    $location.path('/dashboard');

        //    //document.getElementById("pushButton").disabled = false;
        //    //document.getElementById("pushButton").style.visibility = "visible";
        //    //document.getElementById("userMenu").style.visibility = "visible";
        //    document.getElementById("myBody").className = "skin-blue sidebar-mini";
        //    document.getElementById("leftBar").style.visibility = "visible";
        //},
        //    function (a, b, c) {
        //        $scope.error = "Incorrect username/password !";
        //    });
    };
});

