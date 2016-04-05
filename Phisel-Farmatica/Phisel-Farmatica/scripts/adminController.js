'use strict';



myApp.controller('adminController', function ($scope, $http) {

    $scope.provincias = PROVINCIA;
    $scope.provinciaSeleccionada = $scope.provincias[0].Nombre;
    $scope.empleadoSeleccionado = 0;

    $scope.empleados = [
        { IdEmpleado: 123, Cedula: 123, NombreEmpleado: "Thomas Alba", ApellidoEmpleado: "Edison", NumeroTelefonico: 1234, Salario: 7895 },
        { IdEmpleado: 1203, Cedula: 1234, NombreEmpleado: "Nicholas", ApellidoEmpleado: "Tesla", NumeroTelefonico: 12345, Salario: 6000 }
    ];


    $scope.obtenerSucursales = function () {
        console.log("Se llama a obtener lista de sucursales");
        $http.get('/Product/obtenerListaSucursal')
            .success(function (result) {
                console.log(result);
                $scope.sucursales = result;
            }).error(function (data) {
                console.log("Fail");
                return [];
            });
    };

    $scope.actualizarSucursales = function () {
        console.log("Se llama a actualizar sucursales");
        $scope.obtenerSucursales();
        console.log("La lista es: ", $scope.sucursales);
    };



    $scope.seleccionarProvincia = function (provincia) {
        console.log("Se llama a seleccionar provincia");
        $scope.provinciaSeleccionada = provincia.Nombre;
        $http.post('/Product/modificarProvincia',
            { pProvincia: provincia.Nombre})
            .success(function (result) {
                console.log(result);
                //Se actualizan las sucursales segun las provincias
                $scope.actualizarSucursales();
            }).error(function (data) {
                console.log(data);
            });
    };


    $scope.obtenerClaseEmpleado = function (index) {
        if (index == $scope.empleadoSeleccionado) {
            return "success";
        }
        return "";
    };

    $scope.asignarEmpleadoSeleccionado = function (index) {
        $scope.empleadoSeleccionado = index;
    };








    /*Graficos*/


    $scope.options = {
        chart: {
            type: 'discreteBarChart',
            height: 350,
            margin: {
                top: 20,
                right: 20,
                bottom: 60,
                left: 55
            },
            x: function (d) { return d.label; },
            y: function (d) { return d.value; },
            showValues: true,
            valueFormat: function (d) {
                return d3.format(',.4f')(d);
            },
            transitionDuration: 500,
            xAxis: {
                axisLabel: 'Productos'
            },
            yAxis: {
                axisLabel: 'Cantidad vendido',
                axisLabelDistance: 30
            }
        }
    };


    $scope.data = [{
        key: "Cumulative Return",
        values: [
            { label: "B", value: 0 },
            { label: "C", value: 32.807804682612 },
            { label: "D", value: 196.45946739256 },
            { label: "E", value: 0.19434030906893 },
            { label: "F", value: -98.079782601442 },
            { label: "G", value: -13.925743130903 },
            { label: "H", value: -5.1387322875705 }
        ]
    }];

    $scope.hacerAlgo = function () {
        $scope.data = [{
            key: "Cumulative Return",
            values: [
                { label: "B", value: 100 },
                { label: "C", value: 100 },
                { label: "D", value: 100 },
                { label: "E", value: 100 },
                { label: "F", value: 100 },
                { label: "G", value: 100 },
                { label: "H", value: 100 }
            ]
        }];
    };




    




});
