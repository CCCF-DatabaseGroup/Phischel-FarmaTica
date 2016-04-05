

myApp.controller('carritoController', function ($scope, $http) {
    $scope.sucursalesProductosFn = function () {

        console.log("llamada a -> obtener categoria");
        //obtener las sucursales
        $http.get('/Carrito/ObtenerCarrito')
            .success(function (result) {
                console.log(result);
                $scope.sucursalesProductos = result;
                $scope.SucursalProductoActivo = $scope.sucursalesProductos[0];
            }).error(function (data) {
                console.log(data);
                $scope.sucursalesProductos = [];
            });
    };

    $scope.sucursalesProductosFn();



    $scope.iniciarCantidadAComprar = function (producto) {
        if (producto.CantidadAComprar == null) producto.CantidadAComprar = 1;
    }
    
    $scope.aumentarCantidadProducto = function (product) {
        
        if (product.CantidadAComprar < product.Cantidad) {
            product.CantidadAComprar += 1;
        }
    };
    $scope.decrementarCantidadProducto = function (product) {
        if (product.CantidadAComprar > 1) {
            product.CantidadAComprar -= 1;
        }

    };

    $scope.eliminarProducto = function (sucursalProducto, producto) {
        $http.post('/Carrito/eliminarDelCarrito', { pProductoId: producto.IdProducto, pSucursal: sucursalProducto.Sucursal })
            .success(function (result) {
                console.log(result);
                $scope.sucursalesProductos = result;
            }).error(function (data) {
                console.log(data);
                $scope.sucursalesProductos = [];
            });
    };


    $scope.asiginarSucursalProductoActivo = function (sucursalProducto) {
        $scope.SucursalProductoActivo = sucursalProducto;
    };

    $scope.obtenerTotalCompra = function (SucursalProductoActivo) {
        console.log("Compra...");
        console.log(SucursalProductoActivo.ListaSucursal);
        $scope.total = 0;
        for (var x = 0; x < SucursalProductoActivo.ListaSucursal.length; x++) {
            producto = SucursalProductoActivo.ListaSucursal[x];
            $scope.total += producto.CantidadAComprar * producto.Precio;
            console.log("Total: " + $scope.total);
            console.log("producto cantidad " + producto.CantidadAComprar);
        };
    };


});