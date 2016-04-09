'use strict';



myApp.controller('adminController', function ($scope, $http) {

    $scope.provincias = PROVINCIA;
    $scope.provinciaSeleccionada = $scope.provincias[0].Nombre;
    $scope.empleadoSeleccionado = 0;



    $scope.obtenerDependienteDeSucursal = function () {
        console.log("Se llama a obtener la lista de dependientes de la sucursal ", $scope.sucursalSeleccionadaId);
        $http.post('/Home/obtenerDependientes', { pIdSucursal: $scope.sucursalSeleccionadaId })
            .success(function (result) {
                console.log(result);
                $scope.empleados = result;
                if ($scope.empleados.length != 0)
                    $scope.empleadoSeleccionado = empleados[0].Nombre_persona;
            }).error(function (data) {
                console.log("Fail");
                return [];
            });
    };



    $scope.obtenerSucursales = function () {
        console.log("Se llama a obtener lista de sucursales");
        $http.get('/Product/obtenerListaSucursal')
            .success(function (result) {
                console.log(result);
                $scope.sucursales = result;
                if ($scope.sucursales.length > 0) {
                    $scope.sucursalSeleccionada = $scope.sucursales[0].Nombre;
                    $scope.sucursalSeleccionadaId = $scope.sucursales[0].IdSucursal;
                    $scope.obtenerDependienteDeSucursal();
                }
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

    $scope.seleccionarProvincia($scope.provincias[0]);


    $scope.seleccionarSucursal = function (sucursal) {
        
        $scope.sucursalSeleccionada = sucursal.Nombre;
        $scope.sucursalSeleccionadaId = sucursal.IdSucursal;
        $scope.obtenerDependienteDeSucursal();

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
