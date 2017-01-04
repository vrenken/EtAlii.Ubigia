app.controller('accountController', ['$scope', 'notificationService', 'dataService', 'relativeUrl', '$routeParams', function ($scope, notificationService, dataService, relativeUrl, $routeParams) {

    $scope.pageClass = 'page-account';
    $scope.account = {};
    $scope.save = saveAccount;
    var accountId = $routeParams.accountId;

    loadAccount();

    function loadAccount() {

        var config = {
            //parameters: {
            //    accountId: accountId
            //}
        }
        dataService.get(relativeUrl.accountAdministration + '/' + accountId, config,
        loadCompleted,
        loadFailed);
    }

    function saveAccount() {

        dataService.post(relativeUrl.accountAdministration, $scope.account,
       saveSucceeded,
       saveFailed);
    }

    function loadCompleted(result) {
        $scope.account = result.data;
    }

    function loadFailed(response) {
        notificationService.displayError(response.data);
    }

    function saveSucceeded(response) {
    //    $scope.submission.errorMessages = ['Submition errors will appear here.'];
    //    console.log(response);
        var savedAccount = response.data;
    //    $scope.submission.successMessages = [];
    //    $scope.submission.successMessages.push($scope.newCustomer.LastName + ' has been successfully registed');
    //    $scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
        $scope.account = savedAccount;
    }

    function saveFailed(response) {
    //    console.log(response);
    //    if (response.status == '400')
    //        $scope.submission.errorMessages = response.data;
    //    else
    //        $scope.submission.errorMessages = response.statusText;
    }

    //function openDatePicker($event) {
    //    $event.preventDefault();
    //    $event.stopPropagation();

    //    $scope.datepicker.opened = true;
    //};

}]);
