myApp = angular.module('myApp', []);

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


myApp.controller('registroController', function ($scope, $http) {
    /**
    $scope.usuario_Cedula;
    $scope.usuario_Padecimiento;


    */
    
    

});


myApp.controller('productController',function ($scope, $http){
    $scope.productos = [{ name: "Uno", quantity: 3 }, { name: "Dos", quantity: 6 }];
    $scope.sucursales = [{ name: "Farmatica San Jose" }, { name: "Farmatica San Carlos" }];
    $scope.tipo_producto  = [{ name: "Pastillas"}, { name: "Inyectables"}];
    $scope.test= function (name, quantity) {
        console.log(name);
        console.log(quantity);
    }
});