myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {
    $scope.userName = "";

    $http.get('/home/getUserName/')
            .success(function (result) {
                console.log(result.UserId);
                $scope.userName = result["UserId"];
            }).error(function (data) {
                console.log(data);
    });

    $scope.userPassword = "";
    $scope.loginUser = function () {
        if ($scope.userName != "" && $scope.userPassword != "") {
            $scope.loginMessage = "";
            $http.post('/home/getUserData/', { userId: $scope.userName, userPassword: $scope.userPassword })
            .success(function (result) {
                console.log($scope.userName);
                $scope.value = result;
                console.log(result);
                console.log(result.Algo);
                alert(result.Algo);
                alert(encodeURIComponent("a"));
                window.location = '/Home/Index' + '?a=' + encodeURIComponent("a");
                //location.reload();
            }).error(function (data) {
                console.log(data);
            });
        }
        else {
            if ($scope.userName == "" && $scope.userPassword == "") {
                $scope.loginMessage = "campos de usuario y contraseña vacios";
            }
            else if ($scope.userPassword == "") {
                $scope.loginMessage = "constraseña vacia";
            }
            else {
                $scope.loginMessage = "usuario vacio";
            }

        }

    }
})