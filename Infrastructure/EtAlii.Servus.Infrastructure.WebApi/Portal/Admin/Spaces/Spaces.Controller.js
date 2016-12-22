app.controller('spacesController', ['$scope', function ($scope) {

    $scope.pageClass = 'page-spaces';
    $scope.isLoading = true;
    $scope.page = 0;
    $scope.pagesCount = 0;
    $scope.spaces = [];
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

        $scope.spaces = [
            { User: 'John Doe', Name: 'Default', Size: '2938 MB', Complexity: '0.85', Age: '3 years 5 months 1 day', Status: 'Operational' },
            { User: 'John Doe', Name: 'Backup', Size: '2834 MB', Complexity: '0.52', Age: '1 year 4 months 16 days', Status: 'Corrupt' },
            { User: 'Peter Vrenken', Name: 'Default', Size: '1.4 TB', Complexity: '0.87', Age: '4 years 2 months 18 days', Status: 'Operational' },
            { User: 'Peter Vrenken', Name: 'Backup', Size: '1.2 TB', Complexity: '0.23', Age: '2 years 9 months 8 days', Status: 'Old version' }
        ];

        //apiService.get('/api/spaces/', config,
        //loadCompleted,
        //loadFailed);
    }

    function loadCompleted(result) {
        $scope.spaces = result.data.Items;
        $scope.page = result.data.Page;
        $scope.pagesCount = result.data.TotalPages;
        $scope.totalCount = result.data.TotalCount;
        $scope.isLoading = false;

        if ($scope.filterText && $scope.filterText.length) {
            notificationService.displayInfo(result.data.Items.length + ' spaces found');
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
