const PROVINCIA = [{ Nombre: "San Jose" },
                   { Nombre: "Alajuela" },
                   { Nombre: "Cartago" },
                   { Nombre: "Heredia" },
                   { Nombre: "Guanacaste" },
                   { Nombre: "Puntarenas" },
                   { Nombre: "Limon" }];


myApp = angular.module('myApp', []);



myApp.directive("specialView", function ($http) {
    return {
        link: function (scope, element) {
            $http.get("/View/Product/ProductosVistaComun") // immediately call to retrieve partial
              .success(function (data) {
                  element.html(data);  // replace insides of this element with response
              });
        }
    };
});




myApp.controller('mainController', function ($scope, $http) {

    $scope.userName = "";
    $scope.userPassword = "";
    $http.get('/Home/getUserName/')
            .success(function (result) {
                if (result["UserId"] != null) {
                    document.getElementById("Login_button").style.display = "none";
                    document.getElementById("Logout_button").style.display = "block";
                }
                else {
                    document.getElementById("Login_button").style.display = "block";
                    document.getElementById("Logout_button").style.display = "none";
                }
                //window.location = '/Home/Index';
            }).error(function (data) {
                console.log(data);
            });
    $scope.loginUser = function () {
        if ($scope.userName != "" && $scope.userPassword != "") {
            $scope.loginMessage = "";
            $http.post('/Home/loginUser/', { userId: $scope.userName, userPassword: $scope.userPassword })
            .success(function (result) {
                console.log($scope.userName);
                if (result["Response"] == "Conectado") {
                    document.getElementById("Login_button").style.display = "none";
                    document.getElementById("Logout_button").style.display = "block";
                    location.reload();

                }
                else {
                    $scope.loginMessage = "Constraseña o Usuario incorrectos";
                    console.log("nope");
                }
                //window.location = '/Home/Index';
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

    $scope.logoutUser = function () {
        $http.get('/Home/logoutUser/')
        .success(function (result) {
            console.log($scope.userName);
            if (result["UserId"] == "") {
                document.getElementById("Login_button").style.display = "block";
                document.getElementById("Logout_button").style.display = "none";
                location.reload();

            }
            else {

                console.log("nope");
            }
            //window.location = '/Home/Index';
        }).error(function (data) {
            console.log(data);
        });
    }
});
