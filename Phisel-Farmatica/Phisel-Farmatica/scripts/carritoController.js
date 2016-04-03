

myApp.controller('carritoController', function ($scope, $http) {
    $scope.sucursalesProductos = [
        {
            Sucursal: "TAL", ListaSucursal: [
            { Nombre: "A", Precio: 12, Descripcion: "AAA", Prescripcion: "SI", Cantidad: 1 },
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