app = angular.module('Ubigia.User', ['common.core', 'common.ui']);

app.run(['$rootScope', '$location', '$cookieStore', '$http', 'Event', 'authenticationService', function ($rootScope, $location, $cookieStore, $http, Event, authenticationService) {

    // handle page refreshes
    $rootScope.repository = $cookieStore.get('repository') || {};
    if ($rootScope.repository.currentUser) {
        $http.defaults.headers.common['Host-Identifier'] = $rootScope.repository.currentUser.hostIdentifier;
        $http.defaults.headers.common['Authentication-Token'] = $rootScope.repository.currentUser.authenticationToken;
    }

    $rootScope.$on(Event.RouteChangeStart, function (event, nextRoute, previousRoute) {

        if (!authenticationService.isAuthenticated() && nextRoute.requiresAuthentication) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    });

    $rootScope.$on(Event.RouteChangeSuccess, function (event, currentRoute, previousRoute)
    {
        $rootScope.title = currentRoute.title;
        $rootScope.applicationSection = currentRoute.applicationSection;
    });
}]);
