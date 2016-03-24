myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {
    $scope.userName = "";
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