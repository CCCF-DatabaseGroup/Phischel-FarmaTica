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

    $scope.obtenerSucursales = function () {
        console.log("=================================================================");
        console.log("Obtener Sucursales");
        console.log("=================================================================");
        $http.get('/Product/obtenerListaSucursal')
            .success(function (result) {
                $scope.sucursales = result;
                if (result.lenght != 0) {
                    $scope.seleccionarSucursal($scope.sucursales[0], 0);
                }
                else {
                    $scope.seleccionarSucursal("", 0);
                }
                console.log("Resultados de sucursales: ");
                console.log(result);

            }).error(function (data) {
                console.log(data);
            });
    };

   

    $scope.obtenerproductos = function () {
        $http.post('/Product/obtenerProducto', {
            pSucursal: $scope.sucursalSeleccionada, pCategoria: $scope.categoriaProducto
        })
            .success(function (result) {
                console.log(result)
                $scope.productos = result;
                //
            }).error(function (data) {
                console.log(data);
            });
    };

    $scope.seleccionarCategoria = function (categoria,index) {
        console.log(categoria.Nombre);
        $scope.categoriaProducto = categoria.Nombre;
        $scope.categoriaSeleccionada = index;
        $scope.obtenerproductos();
    };

    $scope.getCategoriaClass = function (index) {
        if (index == $scope.categoriaSeleccionada) {
            return $scope.itemSeleccionado;
        }
        return $scope.itemNoSeleccionado;
    }

    $scope.seleccionarSucursal = function (sucursal, index) {
        console.log("Sucursal seleccionada: ");
        console.log(sucursal);
        $scope.sucursalSeleccionada = sucursal.Nombre;
        $scope.sucursalSeleccionadaIndex = index;
        $scope.obtenerproductos();
    };

    $scope.getSucursalClass = function (index) {
        if (index == $scope.sucursalSeleccionadaIndex) {
            return $scope.itemSeleccionado;
        }
        return $scope.itemNoSeleccionado;
    }

    $scope.asignarFarmacia = function (index) {
        console.log("se asigna la farmacia " + index);
        if (index == 0) { $scope.farmaciaActiveElement = true }
        else { $scope.farmaciaActiveElement = false; }
        //obtener las sucursales
        $http.post('/Product/modificarFarmacia', { pFarmacia: $scope.farmaciaActiveElement })
            .success(function (result) {
                console.log("Farmacia seleccionada",result);
                if ($scope.farmaciaActiveElement) {
                    $scope.farmaciaClass[0].estilo = "active";
                    $scope.farmaciaClass[1].estilo = "";
                }
                else {
                    $scope.farmaciaClass[0].estilo = "";
                    $scope.farmaciaClass[1].estilo = "active";
                }
                $scope.obtenerSucursales();
                //
            }).error(function (data) {
                console.log(data);
            });
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
                $scope.obtenerSucursales();
            }).error(function (data) {
                console.log(data);
            });
    };

    $scope.getProvinciaClass = function (index) {
        return $scope.provinciaClass[index].estilo;
    };
    $scope.getFarmaciaClass = function (index) {
        return $scope.farmaciaClass[index].estilo;
    };


    $scope.obtenerCategoria = function () {
        console.log("obtener categoria called");
        //obtener las sucursales
        $http.get('/Product/obtenerListaCategoria')
            .success(function (result) {
                $scope.tipo_producto = result;
                $scope.categoriaProducto = result[0].Nombre;
                $scope.seleccionarCategoria($scope.tipo_producto[0],0);
                //
            }).error(function (data) {
                console.log(data);
            });
    };


    $scope.modificarProductoSeleccionado = function (producto) {
        $scope.productoSeleccionado = producto;
    };

    $scope.obtenerCategoria();
    $scope.activarProvincia(0);
    
    
});


myApp.controller('carritoController', function ($scope, $http, $route, $location) {
    $scope.sucursalesProductos = [
        {
            Sucursal: "TAL", ListaSucursal: [
            { Nombre: "A", Precio: 12, Descripcion: "AAA", Prescripcion: "SI",Cantidad:1 },
            { Nombre: "B", Precio: 32, Descripcion: "BBB", Prescripcion: "SI", Cantidad: 1 },
            { Nombre: "C", Precio: 85, Descripcion: "CCC", Prescripcion: "NO", Cantidad: 1 }
            ]
        },
        {
            Sucursal: "TOL", ListaSucursal: [
            { Nombre: "D", Precio: 12, Descripcion: "AAA", Prescripcion: "SI", Cantidad: 1 },
            { Nombre: "E", Precio: 32, Descripcion: "BBB", Prescripcion: "SI", Cantidad: 1 },
            { Nombre: "F", Precio: 85, Descripcion: "CCC", Prescripcion: "NO", Cantidad: 1 }
            ]
        }
    ];
    $scope.aumentarCantidadProducto = function (product) {
        product.Cantidad += 1;
    };
    $scope.decrementarCantidadProducto = function (product) {
        if (product.Cantidad > 1) {
            product.Cantidad -= 1;
        }
        
    };

    $scope.eliminarProducto = function (sucursalProducto, index) {
        if (index > -1) {
            sucursalProducto.ListaSucursal.splice(index, 1);
            console.log("Se ha eliminado un producto comprado", sucursalProducto.ListaSucursal.length);
            if (sucursalProducto.ListaSucursal.length == 0) {
                console.log("Se ha eliminado una sucursal");
                $scope.sucursalesProductos.splice($scope.sucursalesProductos.indexOf(sucursalProducto), 1);
            }
        }
    };
    $scope.eliminarSucursal = function (index) {
        if (index > -1) {
            console.log("Se ha eliminado una sucursal");
            $scope.sucursalesProductos.splice(index, 1);
        }
    };

});