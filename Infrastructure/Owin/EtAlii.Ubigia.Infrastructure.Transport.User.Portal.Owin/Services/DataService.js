app.factory('dataService', ['$http', '$location', 'notificationService', '$rootScope', function($http, $location, notificationService, $rootScope) {

        var notAuthenticatedErrorCode = '401';
        var authenticationErrorMessage = 'Authentication required.';

        var service = {
            get: get,
            post: post
        };
        return service;

        function get(url, config, success, failure) {

            // We are going to play safe and try to continue, even when config is null.
            if (config == null) {
                config = {};
            }
            config.headers = {
                'Host-Identifier': $rootScope.repository.currentUser.hostIdentifier,
                'Authentication-Token': $rootScope.repository.currentUser.authenticationToken
            };

            return $http.get(url, config).then(
                function(result) {
                    success(result);
                },
                function(error) {
                    handleError(error, failure);
                });
        }

        function post(url, config, success, failure) {

            // We are going to play safe and try to continue, even when config is null.
            if (config == null) {
                config = {};
            }
            config.headers = {
                'Host-Identifier': $rootScope.repository.currentUser.hostIdentifier,
                'Authentication-Token': $rootScope.repository.currentUser.authenticationToken
            };

            return $http.post(url, config).then(
                function(result) {
                    success(result);
                },
                function(error) {
                    handleError(error, failure);
                });
        }

        function handleError(error, failure) {
            if (error.status == notAuthenticatedErrorCode) {
                notificationService.displayError(authenticationErrorMessage);
                $rootScope.previousState = $location.path();
                $location.path(relativeUrl.login);
            } else if (failure != null) {
                failure(error);
            }
        }
    }
]);