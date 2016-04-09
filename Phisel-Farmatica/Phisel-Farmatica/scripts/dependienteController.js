myApp.controller('dependienteController',function ($scope, $http,$location) {


    $scope.obtenerPedidos = function () {
        console.log("Se llama a obtener lista de pedidos");
        $http.get('/Home/obtenerPedidos')
            .success(function (result) {
                console.log(result);
                $scope.pedidos = result;
            }).error(function (data) {
                console.log("Fail");
                return [];
            });
    };
    $scope.obtenerPedidos();


    $scope.tipoProcesoPedido = function (pedido) {
        if (pedido.Estado_Pedido == 1) {
            return "btn btn-warning";
        }
        else if (pedido.Estado_Pedido == 2) {
            return "btn btn-success";
        }
    }

    $scope.MensajetipoProcesoPedido = function (pedido) {
        if (pedido.Estado_Pedido == 1) {
            return "Preparar";
        }
        else if (pedido.Estado_Pedido == 2) {
            return "Facturar";
        }
    }


    $scope.obtenerListadeProductosdePedido = function (pedido) {
        $scope.pedidoActivo = pedido;
        console.log("Se llama a obtener lista de pedidos");
        $http.post('/Home/obtenerListadeProductosdePedido', { PidPedido: pedido.IdPedido })
            .success(function (result) {
                console.log(result);
                $scope.ListadeProductosdePedido = result;
            }).error(function (data) {
                console.log("Fail");
                return [];
            });
            
    };


    $scope.siguienteEtapaDePedido = function (pedido) {

        console.log("Se llama a siguiente etapa ", $scope.pedidoActivo.IdPedido);
        $http.post('/Home/siguienteEtapaDePedido', { pIdPedido: $scope.pedidoActivo.IdPedido })
            .success(function (result) {
                if (result != null){
                    console.log("Esto es diferente de null");
                    $scope.pedidos = result;
                }
            }).error(function (data) {
                console.log("Fail");
                return [];
            });
            
    };



    //$scope.paginas = [{ Numero: 1 }, { Numero: 11 }];

});
