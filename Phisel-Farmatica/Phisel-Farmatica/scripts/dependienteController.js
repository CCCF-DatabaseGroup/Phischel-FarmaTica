myApp.controller('dependienteController',function ($scope, $http,$location) {

    $scope.pedidos = [
        { Codigo: "A3", NombreCliente: "Cristian", ApellidoCliente: "Rivera", AlCobro: 3000, HoraDeCompra: "10:30 a.m" },
        { Codigo: "A3", NombreCliente: "Cristal", ApellidoCliente: "Rivera", AlCobro: 2000, HoraDeCompra: "11:30 a.m" },
        { Codigo: "A3", NombreCliente: "Eddie", ApellidoCliente: "Jimenez", AlCobro: 5000, HoraDeCompra: "11:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" },
        { Codigo: "A3", NombreCliente: "Carlos", ApellidoCliente: "Jimenez", AlCobro: 8000, HoraDeCompra: "13:30 a.m" }
    ];

    $scope.paginas = [{ Numero: 1 }, { Numero: 11 }];

});
