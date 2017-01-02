app.factory('authenticationService', ['dataService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope', 'relativeUrl', function (dataService, notificationService, $http, $base64, $cookieStore, $rootScope, relativeUrl) {

    var service = {
        login: login,
        //register: register,
        //saveCredentials: saveCredentials,
        removeCredentials: removeCredentials,
        isAuthenticated: isAuthenticated
    }
    return service;

    function login(username, password, onLoginSucceeded) {
        var hostidentifier = createHostIdentifier();
        var authentication = $base64.encode(username + ':' + password);
  
        var request = {
            headers: {
                'RespondWithChallenge': 'false',
                'Authorization': 'Basic ' + authentication,
                'Host-Identifier': hostidentifier,
                'Content-Type': undefined
            },
            hostIdentifier: hostidentifier,
            usernName: username
        };

        $http.get(relativeUrl.authenticate, request)
            .then(function successCallback(response) {
                storeLogin(response.data, response.config.hostIdentifier, response.config.username);
                    onLoginSucceeded();
                },
                function errorCallback(response) {
                    notificationService.displayError('Login failed.');
                    //notificationService.displayError(response);
                });
    }

    // 0. Get host identifier:
    //var bytes = new byte[64];
    //var rnd = new Random();
    //rnd.NextBytes(bytes);
    //_hostIdentifier = Convert.ToBase64String(bytes);
    // Use host identifier in all requests as header "Host-Identifier"

    // 1. Get from relativeUrl + credentials (use host identifier).
    // Result is a token.
    // Store token and use in further requests as header "Authentication-Token"


    //function register(user, completed) {
    //    dataService.post('/api/account/register', user,
    //    completed,
    //    registrationFailed);
    //}

    function storeLogin(authenticationToken, hostIdentifier, username) {

        $rootScope.repository = {
            currentUser: {
                username: username,
                authenticationToken: authenticationToken,
                hostIdentifier: hostIdentifier
            }
        };

        $http.defaults.headers.common['Host-Identifier'] = hostIdentifier;
        $http.defaults.headers.common['Authentication-Token'] = authenticationToken;
        $cookieStore.put('repository', $rootScope.repository);
    }

    function removeCredentials() {
        $rootScope.repository = {};
        $cookieStore.remove('repository');
        $http.defaults.headers.common['Host-Identifier'] = '';
        $http.defaults.headers.common['Authentication-Token'] = '';
    };

    function isAuthenticated() {
        return $rootScope.repository.currentUser != null;
    }

    // Internal plumbing.

    function createHostIdentifier() {
        var bytes = new Uint8Array(64);
        for (var i = 0; i < bytes.length; i++) {
            bytes[i] = randomInt(0, 256);
        }
        var hostIdentifier = $base64.encode(bytes);
        return hostIdentifier;
    }

    function randomInt(low, high) {
        return Math.floor(Math.random() * (high - low) + low);
    }

}]);