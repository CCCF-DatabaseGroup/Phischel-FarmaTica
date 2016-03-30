const PROVINCIA = [{ Nombre: "San Jose" },
                   { Nombre: "Alajuela" },
                   { Nombre: "Cartago" },
                   { Nombre: "Heredia" },
                   { Nombre: "Guanacaste" },
                   { Nombre: "Puntarenas" },
                   { Nombre: "Limon" }];


myApp = angular.module('myApp', ['ngRoute']).config(['$routeProvider', '$locationProvider',
  function ($routeProvider,$locationProvider) {
      $routeProvider.
        when('/Product', {
            templateUrl: '~/Views/Product/Index.cshtml',
            controller: 'productController'
        }).when('/Utils', {
            templateUrl: '~/Views/Utils/ProductosVistaComun.cshtml',
            controller: 'productController'
        });

  }]);


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




myApp.controller('mainController', function ($scope, $http, $route, $location) {
    $scope.$route = $route;
    $scope.$location = $location;

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


myApp.controller('productController', function ($scope, $http,$route,$location) {

    $scope.testValue = 0;
    $scope.$route = $route;
    $scope.$location = $location;


    $scope.provincias = PROVINCIA;
    $scope.provinciaClass = [{ estilo: "list-group-item active" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" }
    ];
    $scope.farmaciaClass = [{ estilo: "active" }, { estilo: "" }];
    //es verdadero para Farmatica y falso para phishel
    $scope.farmaciaActiveElement = true;
    $scope.itemNoSeleccionado = "list-group-item";
    $scope.itemSeleccionado = "list-group-item active";
    $scope.indiceItemSeleccionado = 0;
    $scope.provinciaSeleccionada = $scope.provincias[0].Nombre;
    $scope.test = function (name, quantity) {
        console.log(name);
        console.log(quantity);
    };

    $scope.obtenerSucursales = function () {
        $http.get('/Product/obtenerListaSucursal')
            .success(function (result) {
                $scope.sucursales = result;
                console.log(result);

            }).error(function (data) {
                console.log(data);
            });
    };

    $scope.seleccionarCategoria = function (categoria) {
        console.log(categoria.Nombre);
        $scope.categoriaProducto = categoria.Nombre;
        $http.post('/Product/obtenerProducto', {
            pSucursal: $scope.sucursalSeleccionada, pCategoria: categoria.Nombre
        })
            .success(function (result) {
                console.log(result)
                $scope.productos = result;
                //
            }).error(function (data) {
                console.log(data);
        });
    }

    $scope.seleccionarSucursal = function (sucursal) {
        console.log(sucursal);
        $scope.sucursalSeleccionada = sucursal.Nombre;
        console.log("dummy");
        $scope.seleccionarCategoria($scope.categoriaProducto);
    }

    $scope.asignarFarmacia = function (index) {
        console.log("se asigna la farmacia " + index);
        if (index == 0) { $scope.farmaciaActiveElement = true }
        else { $scope.farmaciaActiveElement = false; }
        //obtener las sucursales
        $http.post('/Product/modificarFarmacia', { pFarmacia: $scope.farmaciaActiveElement })
            .success(function (result) {
                if ($scope.farmaciaActiveElement) {
                    $scope.farmaciaClass[0].estilo = "active";
                    $scope.farmaciaClass[1].estilo = "";
                }
                else {
                    $scope.farmaciaClass[0].estilo = "";
                    $scope.farmaciaClass[1].estilo = "active";
                }
                //
            }).error(function (data) {
                console.log(data);
            });

        $scope.obtenerSucursales();
    };

    $scope.activarProvincia = function (index) {
        $scope.provinciaClass[$scope.indiceItemSeleccionado].estilo = $scope.itemNoSeleccionado;
        $scope.provinciaClass[index].estilo = $scope.itemSeleccionado;
        $scope.indiceItemSeleccionado = index;
        console.log($scope.provincias[$scope.indiceItemSeleccionado].Nombre);
        //obtener las sucursales
        $http.post('/Product/modificarProvincia',
            { pProvincia: $scope.provincias[$scope.indiceItemSeleccionado].Nombre })
            .success(function (result) {
                //
            }).error(function (data) {
                console.log(data);
            });

        $scope.obtenerSucursales();
    };

    $scope.getProvinciaClass = function (index) {
        return $scope.provinciaClass[index].estilo;
    };
    $scope.getFarmaciaClass = function (index) {
        return $scope.farmaciaClass[index].estilo;
    };


    $scope.obtenerCategoria = function () {
        //obtener las sucursales
        $http.get('/Product/obtenerListaCategoria')
            .success(function (result) {
                $scope.tipo_producto = result;
                $scope.categoriaProducto = result[0].Nombre;
                //
            }).error(function (data) {
                console.log(data);
            });
    };

    $scope.obtenerCategoria();
    
    
});
