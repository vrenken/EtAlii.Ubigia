app.controller('defaultController', ['$scope', '$location', 'authenticationService', '$rootScope', 'Event', function ($scope, $location, authenticationService, $rootScope, Event) {

    $rootScope.$on(Event.RouteChangeSuccess, function (event, currentRoute, previousRoute) {
        $rootScope.title = currentRoute.title;
        $rootScope.Controller = currentRoute.controller;
    });

    $scope.currentUser = {};
    $scope.currentUser.update = function () {
        $scope.currentUser.isAuthenticated = authenticationService.isAuthenticated();

        if ($scope.currentUser.isAuthenticated) {
            $scope.username = $rootScope.repository.currentUser.username;
        }
    };
    $scope.logout = function () {
        authenticationService.removeCredentials();
        $location.path('/');
        $scope.currentUser.update();
    };

    $scope.login = function () {
        $location.path('/login');
    };

    $scope.currentUser.update();
}]);