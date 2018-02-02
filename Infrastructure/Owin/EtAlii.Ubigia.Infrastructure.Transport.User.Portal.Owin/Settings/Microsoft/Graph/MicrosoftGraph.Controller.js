app.controller('microsoftGraphController', ['$scope', 'notificationService', 'dataService', 'relativeUrl', '$routeParams', 'adalAuthenticationService', function ($scope, notificationService, dataService, relativeUrl, $routeParams, adalAuthenticationService) {

    $scope.pageClass = 'page-settings';
    $scope.data = {};
    $scope.addAccount = addAccount;
    load();

    function load() {

        var config = {
        }
        dataService.get(relativeUrl.microsoftGraphSettingsUser, config,
        loadCompleted,
        loadFailed);
    }

    function loadCompleted(result) {
        $scope.data = result.data;
    }

    function loadFailed(response) {
        notificationService.displayError(response.data);
    }

    function addAccount() {

        var tenantId = '1e1d80f4-377c-4462-9c9d-75a0b5a9bf1c'; // EtAlii.Ubigia
        var accessTokenUri = 'https://login.microsoftonline.com/' + tenantId + '/oauth2/token';

        var token = adalAuthenticationService.acquireToken(accessTokenUri);//.login();
    }
}]);
