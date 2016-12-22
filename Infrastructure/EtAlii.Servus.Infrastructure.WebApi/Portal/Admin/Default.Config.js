app.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {

        // A 401 unauthorized interceptor. We will take care of this ourselves.
        var interceptor = ['$rootScope', '$q', 'notificationService', function (scope, $q, notificationService) {

            function success(response) {
                return response;
            }

            function error(response) {
                var status = response.status;

                if (status === 401) {
                    var deferred = $q.defer();
                    notificationService.displayError('Unauthorized.');
                    return deferred.promise;
                }
                // otherwise
                return $q.reject(response);
            }

            return function (promise) {
                return promise.then(success, error);
            }
        }];
        $httpProvider.interceptors.push(interceptor);

        // The routing.
        $routeProvider
            .when("/", {
                templateUrl: "Home/Home.html",
                controller: "homeController",
                applicationSection: 'Home'
            })

            // Settings - Microsoft Graph
            .when("/settings/microsoft/graph/", {
                templateUrl: "Settings/Microsoft/Graph/MicrosoftGraph.html",
                controller: "microsoftGraphController",
                applicationSection: 'Settings',
                requiresAuthentication: true
            })

            // Settings - Google
            .when("/settings/google/peopleapi", {
                templateUrl: "Settings/Google/PeopleApi/GooglePeopleApi.html",
                controller: "googlePeopleApiController",
                applicationSection: 'Settings',
                requiresAuthentication: true
            })

            // Accounts
            .when("/accounts/", {
                templateUrl: "Accounts/Accounts.html",
                controller: "accountsController",
                applicationSection: 'Data',
                requiresAuthentication: true
            })
            .when("/account/edit/:accountId", {
                templateUrl: "Accounts/Account.html",
                controller: "accountController",
                applicationSection: 'Data',
                requiresAuthentication: true
            })

            // Storages
            .when("/storages/", {
                templateUrl: "Storages/Storages.html",
                controller: "storagesController",
                applicationSection: 'Data',
                requiresAuthentication: true
            })

            // Spaces
            .when("/spaces/", {
                templateUrl: "Spaces/Spaces.html",
                controller: "spacesController",
                applicationSection: 'Data',
                requiresAuthentication: true
            })

            // Login
            .when("/login/", {
                templateUrl: "Login/Login.html",
                controller: "loginController",
                applicationSection: 'Login'
        })
            .otherwise({
                redirectTo: "/"    
            });
    }
]);
