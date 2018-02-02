app.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalAuthenticationServiceProvider) {

        var authority = 'https://login.windows.net/[AZUREADINSTANCE]';
        var tenantId = '1e1d80f4-377c-4462-9c9d-75a0b5a9bf1c'; // EtAlii.Ubigia
        var accessTokenUri = 'https://login.microsoftonline.com/' + tenantId + '/oauth2/token';
        //var accessTokenUri = 'https://login.microsoftonline.com/ubigia.onmicrosoft.com/oauth2/token';
        var appId = 'fc078b8b-9883-43bb-8ebe-0125c05d8f8f'; // Ubigia
        var redirectUri = 'http://localhost:64000/#/settings/microsoft/graph/token/';


        var endpoints = {
            //'/api/Todo/': 'resource1',
            //'/anotherApi/Item/': 'resource2',
            //'https://testapi.com/': 'resource1'
        };


        // Initialize the ADAL provider with your clientID (found in the Azure Management Portal) and 
        // the API URL (to enable CORS requests).
        adalAuthenticationServiceProvider.init(
            {
                // Use this value for the public instance of Azure AD
                //instance: 'https://login.microsoftonline.com/',

                tenant: tenantId,
                // Your application id from the registration portal
                clientId: appId,
                loginResource: 'https://graph.windows.net',//accessTokenUri,
                redirectUri: redirectUri

                // If you're using IE, uncommment this line - the default HTML5 sessionStorage does not work for localhost.
                //cacheLocation: 'localStorage',

                //endpoints: endpoints  // optional
            }//,
            // Not needed here. $httpProvider   // pass http provider to inject request interceptor to attach tokens
        );

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

                // Settings - Microsoft Graph
            .when("settings/microsoft/graph/token/:token*", {
                templateUrl: "Settings/Microsoft/Graph/MicrosoftGraph.html",
                controller: "microsoftGraphController",
                applicationSection: 'Settings',
                requiresAuthentication: true
            })


            // Settings - Google
            .when("/settings/google/peopleapi/", {
                templateUrl: "Settings/Google/PeopleApi/GooglePeopleApi.html",
                controller: "googlePeopleApiController",
                applicationSection: 'Settings',
                requiresAuthentication: true
            })

            // Settings - Google
            .when("/settings/google/peopleapi/authorization_code/:authorization_code*", {
                templateUrl: "Settings/Google/PeopleApi/GooglePeopleApi.html",
                controller: "googlePeopleApiController",
                applicationSection: 'Settings',
                requiresAuthentication: true
            })



            // Accounts
            .when("/account/", {
                templateUrl: "Accounts/Account.html",
                controller: "accountController",
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
            .when("/spaces/edit/:spaceId", {
                templateUrl: "Spaces/Space.html",
                controller: "spaceController",
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
