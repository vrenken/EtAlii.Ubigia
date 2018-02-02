app.controller('loginController', ['$scope', 'authenticationService', 'notificationService', '$rootScope', '$location', function ($scope, authenticationService, notificationService, $rootScope, $location) {

        $scope.pageClass = 'page-login';
        $scope.login = login;
        $scope.loginRequest = {};

        function login() {
            authenticationService.login($scope.loginRequest.username, $scope.loginRequest.password, onSuccess);
        }

        function onSuccess() {
            $scope.currentUser.update();
            notificationService.displaySuccess('Hello ' + $scope.currentUser.username);
            if ($rootScope.previousState) {
                $location.path($rootScope.previousState);
            } else {
                $location.path('/');
            }
        }
}]);