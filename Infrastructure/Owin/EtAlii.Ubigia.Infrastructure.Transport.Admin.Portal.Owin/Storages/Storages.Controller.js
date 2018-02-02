app.controller('storagesController', ['$scope', function ($scope) {

    $scope.pageClass = 'page-storages';
    $scope.isLoading = true;
    $scope.page = 0;
    $scope.pagesCount = 0;
    $scope.storages = [];
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

        $scope.storages = [
            { Name: 'Local', Address: 'http://localhost:64000', Accounts: '3', Spaces: '3', Status: 'Operational', Throughput: '84.24 MB/s' },
            { Name: 'EMEA1', Address: 'http://emea1.ubigia.net:64000', Accounts: '2234', Spaces: '5323', Status: 'Operational', Throughput: '92.86 MB/s' },
            { Name: 'NAM1', Address: 'http://nam1.ubigia.net:64000', Accounts: '9322', Spaces: '12839', Status: 'Performance degraded', Throughput: '13.01 MB/s' },
            { Name: 'NAM2', Address: 'http://nam2.ubigia.net:64000', Accounts: '1402', Spaces: '2392', Status: 'Operational', Throughput: '92.19 MB/s' }
        ];

        //apiService.get('/api/storages/', config,
        //loadCompleted,
        //loadFailed);
    }

    function loadCompleted(result) {
        $scope.storages = result.data.Items;
        $scope.page = result.data.Page;
        $scope.pagesCount = result.data.TotalPages;
        $scope.totalCount = result.data.TotalCount;
        $scope.isLoading = false;

        if ($scope.filterText && $scope.filterText.length) {
            notificationService.displayInfo(result.data.Items.length + ' storages found');
        }

    }

    function loadFailed(response) {
        notificationService.displayError(response.data);
    }

    function clearSearch() {
        $scope.filterText = '';
        search();
    }

    $scope.search();
}]);
