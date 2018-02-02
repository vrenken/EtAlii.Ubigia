app.controller('accountsController', ['$scope', 'notificationService', 'dataService', 'relativeUrl', function ($scope, notificationService, dataService, relativeUrl) {

    $scope.pageClass = 'page-accounts';
    $scope.isLoading = true;
    $scope.page = 0;
    $scope.pagesCount = 0;
    $scope.accounts = [];
    $scope.search = search;
    $scope.clearSearch = clearSearch;

    function search(page) {
        page = page || 0;

        $scope.isLoading = true;

        var config = {
            params: {
                page: page,
                pageSize: 6,
                filter: $scope.filterText
            }
        };

        dataService.get(relativeUrl.accountsAdministration, config, loadCompleted, loadFailed);
    } 

    function loadCompleted(result) {
        $scope.accounts = result.data.Items;
        $scope.page = result.data.Page;
        $scope.pagesCount = result.data.TotalPages;
        $scope.totalCount = result.data.TotalCount;
        $scope.isLoading = false;

        if ($scope.filterText && $scope.filterText.length) {
            notificationService.displayInfo(result.data.Items.length + ' accounts found');
        }

    }

    function loadFailed(response) {
        notificationService.displayError('Unable to load accounts');// + response.data);
    }

    function clearSearch() {
        $scope.filterText = '';
        search();
    }

    $scope.search();
}]);
