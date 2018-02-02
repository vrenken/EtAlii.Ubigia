var authority = 'https://login.windows.net/[AZUREADINSTANCE]';
//var accessTokenUrl = 'https://login.microsoftonline.com/steamworksadfs.onmicrosoft.com/oauth2/token';
var resourceUrl = 'd475989a-2eed-46b0-ac3e-dfff74e874e5'; // EtAlii.Ubigia
var appId = '87e12d4a-e323-4ad4-b33f-00e68f4808a5'; // Ubigia
var redirectUrl = 'http://NeptuneApp';

angular.module("ngapp").service("msAdal", function (shared, $log, $q) {

    var authContext;

    var getAuthTokenSilent = function () {
        authContext = new Microsoft.ADAL.AuthenticationContext(authority);

        return $q(function (resolve, reject) {
            authContext.tokenCache.readItems().then(function (items) {
                if (items.length > 0) {
                    authority = items[0].authority;
                    authContext = new Microsoft.ADAL.AuthenticationContext(authority);
                }
                // Attempt to authorize user silently
                authContext.acquireTokenSilentAsync(resourceUrl, appId)
                .then(function (result) {
                    resolve(result);
                }, function () {
                    reject();
                });
            });
        });
    };

    var getBearerToken = function () {
        var deferred = $q.defer();

        getAuthToken().then(function (authResult) {
            deferred.resolve(authResult.accessToken);
        }, function () {
            $log.error("Cannot get token because authentication failed.");
            deferred.reject();
        });

        return deferred.promise;
    };

    // Shows user authentication dialog if required.
    var getAuthToken = function () {
        return $q(function (resolve, reject) {
            getAuthTokenSilent().then(function (result) {
                // we already have an auth token so just resolve the result
                resolve(result);
            }, function () {
                // We require user cridentials so triggers authentication dialog
                authContext.acquireTokenAsync(resourceUrl, appId, redirectUrl)
                .then(function (result) {
                    resolve(result);
                }, function (err) {
                    $log.error("Failed to authenticate: " + err);
                    reject();
                });
            });
        });
    };

    var request = function (url) {
        return $q(function (resolve, reject) {
            getAuthToken().then(function (authResult) {
                resolve(sendRequest(authResult.accessToken, url));
            }, function () {
                $log.error("Cannot send request because authentication failed.");
            })
        });
    };

    var isLoggedIn = function () {
        return $q(function (resolve, reject) {
            // try to silently get a token
            getAuthTokenSilent().then(function (authResult) {
                var url = shared.apiInfo.baseUrl + "/command";
                sendRequest(authResult.accessToken, url).then(function () {
                    resolve(true);
                }, function () {
                    resolve(false);
                });
            }, function () {
                resolve(false);
            });
        });
    }

    var login = function () {
        getAuthToken();
    }


    function sendRequest(token, url) {
        if (token == null) {
            $log.error("Cannot send a request when the token is null.");
            return;
        }

        var req = new XMLHttpRequest();

        req.open("POST", url, true);
        req.setRequestHeader('Authorization', 'Bearer ' + token);

        console.log("Sending API POST request to: " + url);

        return $q(function (resolve, reject) {
            req.onload = function (e) {
                if (e.target.status >= 200 && e.target.status < 300) {
                    resolve(JSON.parse(e.target.response));
                    return;
                }
                reject('Data request failed: ' + e.target.response);
            };
            req.onerror = function (e) {
                reject('Data request failed: ' + e.error);
            }

            req.send();
        });
    }

    return {
        request: request,
        login: login,
        isLoggedIn: isLoggedIn,
        getBearerToken: getBearerToken
    };

});
