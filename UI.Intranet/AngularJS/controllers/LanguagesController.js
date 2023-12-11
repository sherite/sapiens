app.factory('LanguagesFactory', function () {

    var lblLanguageIU = '';
    var actions = '';
    var LanguagesFactory = {};

    LanguagesFactory.lblLanguageIU = lblLanguageIU;
    LanguagesFactory.actions = actions;
    LanguagesFactory.data = {
        ID: '',
        Alias: '',
        Name: '',
        LastName: '',
        Status: '',
        Groups: [],
        Options: []
    };

    return LanguagesFactory;
});

app.controller('LanguagesController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, $translate, LanguagesFactory, CommonDataFactory) {

    $scope.Initialize = function () {

        // $('#cover-spin').show(0);
    };

    $scope.changeLanguage = function (lang) {

        switch (lang) {
            case "es":
                language = "es-es";
                currentLang = spanish;
                break;
            case "br":
                language = "br-br";
                currentLang = brazilian;
                break;
            case "fr":
                language = "fr-fr";
                currentLang = french;
                break;
            case "de":
                language = "de-de";
                currentLang = deutsche;
                break;
            case "it":
                language = "it-it";
                currentLang = italian;
                break;
            default:
                language = "en-us";
                currentLang = english;
                break;
        }

        switch (currentLang) {

            case spanish:
            case undefined:
                NonSelectedText = 'Nada Seleccionado'; 
                AllSelectedText = 'Todo Seleccionado';
                CloseText = 'Cerrar';
                AcceptText = 'Aceptar';
                break;
            case brazilian:
                NonSelectedText = 'Nada selecionado';
                AllSelectedText = 'Todos Selecionados';
                CloseText = 'Fechar';
                AcceptText = 'Aceitar';
                break;
            case french:
                NonSelectedText = 'Rien sélectionné';
                AllSelectedText = 'Tous sélectionnés';
                CloseText = 'Fermer';
                AcceptText = 'Accepter';
                break;
            case italian:
                NonSelectedText = 'Niente selezionato';
                AllSelectedText = 'Tutto selezionato';
                CloseText = 'Uscire';
                AcceptText = 'Accettare';
                break;
            case english:
                NonSelectedText = 'Nothing selected';
                AllSelectedText = 'All selected';
                CloseText = 'Close';
                AcceptText = 'Accept';
                break;
            case deutsche:
                NonSelectedText = 'Nichts ausgewählt';
                AllSelectedText = 'Alle ausgewählt';
                CloseText = 'Schließen';
                AcceptText = 'Akzeptieren';
                break;
        }

        $translate.use(language);

        $scope.$broadcast('languageChanged', language);
    };
});

app.controller('LanguagesViewController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, LanguagesFactory) {

    $scope.myObject = [];

    $scope.go = function (url, actions, data) {
        $location.path(url);

        if (actions !== null || actions !== undefined) {

            LanguagesFactory.actions = actions;
        }
    };

    $scope.Initialize = function (action) {

        $('#cover-spin').show(0);

    };
});

app.controller('LanguagesEditController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, LanguagesFactory) {

    $scope.myObject = [];

    $scope.action = LanguagesFactory.actions;

    $scope.go = function (url, actions, data) {
        $location.path(url);
    };


    $scope.Initialize = function (action) {

        $('#cover-spin').show(0);
    };
});