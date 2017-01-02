app.controller('googlePeopleApiController', ['$scope', '$location', 'notificationService', 'dataService', 'relativeUrl', '$routeParams', function ($scope, $location, notificationService, dataService, relativeUrl, $routeParams) {

    $scope.pageClass = 'page-settings';
    $scope.data = {};
    $scope.addAccount = addAccount;

    var authorizationCode = getAuthorizationCode();
    if (authorizationCode != null) {
        saveAuthorizationCode(authorizationCode);
    } else {
        load();
    }

    function saveAuthorizationCode(authorizationCode) {
        dataService.post(relativeUrl.googlePeopleApiSettingsUser, authorizationCode, saveAuthorizationCodeSucceeded, saveAuthorizationCodeFailed);
    }

    function saveAuthorizationCodeSucceeded(result) {
        load();
    }

    function saveAuthorizationCodeFailed(response) {
        notificationService.displayError('Authorization code save failed: ' + response.data);
        load();
    }


    function getAuthorizationCode() {
        var hasAuthorizationCode = $routeParams.authorization_code != null;
        if (!hasAuthorizationCode) {
            return null;
        }
        return {
            authorization_code: $routeParams.authorization_code,
        }
        
    }
    function load() {
        dataService.get(relativeUrl.googlePeopleApiSettingsUser, null, loadCompleted, loadFailed);
    }

    function loadCompleted(result) {
        $scope.data = result.data;
    }

    function loadFailed(response) {
        notificationService.displayError('Load failed: ' + response.data);
    }

    function addAccount() {
        dataService.get(relativeUrl.googlePeopleApiSettingsUser + '/SystemSettings', null, loadSystemSettingsSucceeded, loadFailed);
    }

    function loadSystemSettingsSucceeded(result) {

        var client_id = result.data.ClientId;
        var redirect_uri = result.data.RedirectUrl;

        var scopes =
            // Calendar
            //'https://www.googleapis.com/auth/gmail.readonly ' +
            ' ' +
            // Persons
            'https://www.googleapis.com/auth/plus.me ' +
            'https://www.googleapis.com/auth/contacts.readonly ' +
            'https://www.googleapis.com/auth/plus.login ' +
            'https://www.googleapis.com/auth/user.addresses.read ' +
            'https://www.googleapis.com/auth/user.birthday.read ' + 
            'https://www.googleapis.com/auth/user.emails.read ' + 
            'https://www.googleapis.com/auth/user.phonenumbers.read ' +
            'https://www.googleapis.com/auth/userinfo.email ' +
            'https://www.googleapis.com/auth/userinfo.profile ' +

            // Calendar
            ' ' +
            ' ';

        var url = "https://accounts.google.com/o/oauth2/v2/auth?scope="
            + scopes
            + "&client_id=" + client_id
            + "&redirect_uri=" + redirect_uri
            + "&login_hint="
            + "&response_type=code"
            + "&access_type=offline"
            + "&prompt=select_account consent";
        window.location.replace(url);
    }

}]);
