app.controller('googlePeopleApiController', ['$scope', 'notificationService', 'dataService', 'relativeUrl', '$routeParams', function ($scope, notificationService, dataService, relativeUrl, $routeParams) {

    $scope.pageClass = 'page-settings';
    $scope.settings = {};
    $scope.save = saveSettings;

    loadSettings();

    function loadSettings() {

        var config = {
        }
        dataService.get(relativeUrl.GooglePeopleApiSettingsAdmin, config,
        loadCompleted,
        loadFailed);
    }

    function saveSettings() {

        dataService.post(relativeUrl.GooglePeopleApiSettingsAdmin, $scope.settings,
        saveSucceeded,
        saveFailed);
    }

    function loadCompleted(result) {
        $scope.settings = result.data;
    }

    function loadFailed(response) {
        notificationService.displayError(response.data);
    }

    function saveSucceeded(response) {
        var savedSettings = response.data;
        $scope.settings = savedSettings;
    }

    function saveFailed(response) {
    }
}]);
