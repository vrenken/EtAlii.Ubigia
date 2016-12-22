app.controller('homeController', ['$scope', function($scope) {

    $scope.storages = [
        { Name: 'Local', Address: 'http://localhost:64000', Accounts: '3', Spaces: '3', Status: 'Operational', Throughput: '84.24 MB/s' },
        { Name: 'EMEA1', Address: 'http://emea1.ubigia.net:64000', Accounts: '2234', Spaces: '5323', Status: 'Operational', Throughput: '92.86 MB/s' },
        { Name: 'NAM1', Address: 'http://nam1.ubigia.net:64000', Accounts: '9322', Spaces: '12839', Status: 'Performance degraded', Throughput: '13.01 MB/s' },
        { Name: 'NAM2', Address: 'http://nam2.ubigia.net:64000', Accounts: '1402', Spaces: '2392', Status: 'Operational', Throughput: '92.19 MB/s' }
    ];
}]);
