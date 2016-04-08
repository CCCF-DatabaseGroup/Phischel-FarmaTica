
myApp.controller('productAdminController', function ($scope, $http) {
    $scope.provincias = PROVINCIA;

    //representa a la clase en boostrap de un item no seleccionado
    $scope.itemNoSeleccionado = "list-group-item";
    //representa a la clase en boostrap de un item seleccionado
    $scope.itemSeleccionado = "list-group-item active";
    //representa el indice seleccionado de la lista de provincia
    $scope.indiceItemSeleccionado = 0;
    //Representa el nombre de la provincia seleccionada inicial
    $scope.provinciaSeleccionada = $scope.provincias[0].Nombre;
    //Es para verificar si requiere prescripcion
    $scope.prescripcion = false;
    


    /**
  se obtienen las sucursales disponibles segun la provincia seleccionada, esta funcion es llamada por las funciones:
  activarProvincia y asignarFarmacia
  */
    $scope.obtenerSucursales = function () {
        console.log("=================================================================");
        console.log("Obtener Sucursales");
        console.log("=================================================================");
        $http.get('/Product/obtenerListaSucursal')
            .success(function (result) {
                //se obtiene la lista de sucursales
                $scope.sucursales = result;
                if (result.lenght != 0) {
                    //se selecciona la primera sucursal como resultado inicial
                    $scope.seleccionarSucursal($scope.sucursales[0], 0);
                }
                else {
                    //no se seleccionada nada
                    $scope.seleccionarSucursal("", 0);
                }
                //debug
                console.log("Resultados de sucursales: ");
                console.log(result);

            }).error(function (data) {
                console.log(data);
            });
    };





    /**
    activarProvincia: Activa la provincia seleccionada
    */
    $scope.activarProvincia = function (index) {
        //se setean los datos de la provincia
        $scope.provinciaClass[index].estilo = $scope.itemSeleccionado;
        $scope.indiceItemSeleccionado = index;
        console.log($scope.provincias[$scope.indiceItemSeleccionado].Nombre);
        //obtener las sucursales
        $http.post('/Product/modificarProvincia',
            { pProvincia: $scope.provincias[$scope.indiceItemSeleccionado].Nombre })
            .success(function (result) {
                //Se actualizan las sucursales segun las provincias
                $scope.obtenerSucursales();
            }).error(function (data) {
                console.log(data);
            });
    };



    /**
    Es llamada por la funcion obtener categoria o por el usuario al seleccionar una categoria de la lista,
    Selecciona una categoria y actualiza la lista de productos
    */
    $scope.seleccionarCategoria = function (categoria, index) {
        console.log(categoria.Nombre);
        //Setea el nombre de la categoria seleccionada
        $scope.categoriaProducto = categoria.Nombre;
        //setea el indice de la categoria seleccionada, con la finalidad de usarse en el metodo getCategoriaClass
        $scope.categoriaSeleccionada = index;
        $scope.obtenerproductos();
    };








    $scope.verificarInsercionProducto = function () {
        var go = true;
        if ("" == ($scope.nombreProductoARegistrar) || $scope.nombreProductoARegistrar == null) {
            $scope.nombreProductoError = "El nombre esta vacio";
            go = false;
        }
        else {
            $scope.nombreProductoError = "";
        }
        if ($scope.tipo_producto == null || $scope.tipo_producto[$scope.categoriaSeleccionada] == null ||
            $scope.tipo_producto[$scope.categoriaSeleccionada].IdCategoria == null) {
            $scope.categoriaProductoError = "Seleccione una categoria";
            go = false;
        }
        else {
            $scope.categoriaProductoError = "";
        }
        if ($scope.laboratoriosIdActivo == null) {
            $scope.laboratorioProductoError = "Seleccione un laboratorio";
            go = false;
        }
        else {
            $scope.laboratorioProductoError = "";
        }
        if ($scope.descripcionProducto == null) {
            $scope.descripcionProducto = "";
        }
        return go;
    }


    $scope.registrarProducto = function () {


        if ($scope.verificarInsercionProducto()) {
            console.log("llamada a -> registrar producto");
            //registro producto
            $http.post('Product/registrarProducto', {
                pNombreProducto: $scope.nombreProductoARegistrar,
                pIdCategoria: $scope.tipo_producto[$scope.categoriaSeleccionada].IdCategoria,
                pIdLaboratorio: $scope.laboratoriosIdActivo,
                pPreescripcion: $scope.prescripcion,
                pDescripcion: $scope.descripcionProducto
            })
                .success(function (result) {
                    console.log(result);
                }).error(function (data) {
                    console.log(data);
                });
        }



    };





    $scope.obtenerTodosProducto = function () {

        console.log("llamada a -> obtener lista laboratorio");
        //obtener la lista de laboratorios
        $http.get('Product/obtenerTodosProducto')
            .success(function (result) {
                $scope.todo_productos = result;
                if ($scope.todo_productos.length > 0)
                    $scope.producto_activo = $scope.todo_productos[0].IdProducto;
                console.log(result);
            }).error(function (data) {
                console.log(data);
            });
    };



    $scope.editarProducto = function () {


        if ($scope.verificarInsercionProducto() && $scope.producto_activo != null) {
            console.log("llamada a -> editar producto");
            //editar al producto
            $http.post('Product/editarProducto', {
                pIdProducto: $scope.producto_activo,
                pNombreProducto: $scope.nombreProductoARegistrar,
                pIdCategoria: $scope.tipo_producto[$scope.categoriaSeleccionada].IdCategoria,
                pIdLaboratorio: $scope.laboratoriosIdActivo,
                pPreescripcion: $scope.prescripcion,
                pDescripcion: $scope.descripcionProducto
            })
                .success(function (result) {
                    $scope.obtenerTodosProducto();
                    console.log(result);
                }).error(function (data) {
                    console.log(data);
                });
        }
    };


    $scope.obtenerListaLaboratorio = function () {

        console.log("llamada a -> obtener lista laboratorio");
        //obtener la lista de laboratorios
        $http.get('Product/obtenerListaLaboratorio')
            .success(function (result) {
                $scope.laboratorios = result;
                if ($scope.laboratorios.length > 0)
                    $scope.laboratoriosIdActivo = $scope.laboratorios[0].IdLaboratorio;
                console.log(result);
            }).error(function (data) {
                console.log(data);
            });
    };


    $scope.setearProductoActivo = function (producto) {
        console.log("seteando id producto...");
        $scope.producto_activo = producto.IdProducto;
        $scope.descripcionProducto = producto.Descripcion;
    };


    $scope.necesitaPrescripcion = function (prescripcion) {
        $scope.prescripcion = prescripcion;
        console.log(prescripcion);
    };

    $scope.setearIdLaboratorio = function (laboratorio) {
        console.log("seteando id laboratorio...");
        $scope.laboratoriosIdActivo = laboratorio.IdLaboratorio;
    };

    $scope.obtenerCategoria();

    function maping () {
        var Hola = 0;
    };

    var a = maping;

    $http.post('Product/test', { ptest: [1, 1 , 2] })
            .success(function (result) {
                $scope.laboratorios = result;
                if ($scope.laboratorios.length > 0)
                    $scope.laboratoriosIdActivo = $scope.laboratorios[0].IdLaboratorio;
                console.log(result);
            }).error(function (data) {
                console.log(data);
            });


});