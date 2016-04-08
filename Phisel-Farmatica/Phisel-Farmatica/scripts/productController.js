
myApp.controller('productController', function ($scope, $http) {

    $scope.testValue = 0;

    //Representa la lista de provincias del pais
    $scope.provincias = PROVINCIA;

    //Representa la clase en bootstrap de como se mostraran las provincias
    $scope.provinciaClass = [{ estilo: "list-group-item active" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" },
                             { estilo: "list-group-item" }
    ];
    //es la configuracion inicial de la farmacia seleccionada
    $scope.farmaciaClass = [{ estilo: "active" }, { estilo: "" }];
    //es verdadero para Farmatica y falso para phishel
    $scope.farmaciaActiveElement = true;
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
    //Es la lista de laboratorios;

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
    Obtiene los productos segun la sucursal y farmacia seleccionada
    Esta funcion es llamada por las funciones seleccionarCategoria y seleccionarSucursal
    */
    $scope.obtenerproductos = function () {
        $http.post('/Product/obtenerProductoEnSucursal', {
            pSucursal: $scope.sucursalSeleccionada, pCategoria: $scope.categoriaProducto
        })
            .success(function (result) {
                console.log(result)
                //se setea la lista de productos
                $scope.productos = result;
                //
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

    /**
    Obtiene la clase de Bootstrap del item de la lista de categoria, ya sea un item activo o inactivo
    (Ayuda o soporta al view)
    */
    $scope.getCategoriaClass = function (index) {
        if (index == $scope.categoriaSeleccionada) {
            //si el indice es igual a la categoria seleccionada se devolvera el string que representa un item activo
            //en clase Bootstrap
            return $scope.itemSeleccionado;
        }
        //item inactivo en clase Bootstrap
        return $scope.itemNoSeleccionado;
    }

    /**
    seleccionarSucursal: selecciona una sucursal y actualiza la lista de producto, puede ser llamada por el usuario o por el 
    metodo obtenerSucursales
    */
    $scope.seleccionarSucursal = function (sucursal, index) {
        console.log("Sucursal seleccionada: ");
        console.log(sucursal);
        //Setea el nombre de la sucursal seleccionada
        $scope.sucursalSeleccionada = sucursal.Nombre;
        //setea el indice de la sucursal seleccionada, con la finalidad de usarse en el metodo getSucursalClass
        $scope.sucursalSeleccionadaIndex = index;
        $scope.obtenerproductos();
    };

    /**
    Obtiene la clase en Bootstrap del item de la lista de sucursales, ya sea un item activo o inactivo
    (Ayuda o soporta al view)
    */
    $scope.getSucursalClass = function (index) {
        if (index == $scope.sucursalSeleccionadaIndex) {
            return $scope.itemSeleccionado;
        }
        return $scope.itemNoSeleccionado;
    }


    /**
    asignarFarmacia: Asigna una farmacia, ya sea Farmatica o Phishel
    */
    $scope.asignarFarmacia = function (index) {
        console.log("se asigna la farmacia " + index);
        if (index == 0) { $scope.farmaciaActiveElement = true }
        else { $scope.farmaciaActiveElement = false; }
        //obtener las sucursales
        $http.post('/Product/modificarFarmacia', { pFarmacia: $scope.farmaciaActiveElement })
            .success(function (result) {
                console.log("Farmacia seleccionada", result);
                //Este if/else setea los estilos en Bootstrap de los botones de la farmacia seleccionada
                if ($scope.farmaciaActiveElement) {
                    $scope.farmaciaClass[0].estilo = "active";
                    $scope.farmaciaClass[1].estilo = "";
                }
                else {
                    $scope.farmaciaClass[0].estilo = "";
                    $scope.farmaciaClass[1].estilo = "active";
                }
                $scope.obtenerSucursales();
                //fin del bloque if/else
            }).error(function (data) {
                console.log(data);
            });
    };


    /**
    activarProvincia: Activa la provincia seleccionada
    */
    $scope.activarProvincia = function (index) {
        //se setean los datos de la provincia
        $scope.provinciaClass[$scope.indiceItemSeleccionado].estilo = $scope.itemNoSeleccionado;
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


    //se obtiene el estilo visual de la provincia en la lista de provincias segun su indice
    $scope.getProvinciaClass = function (index) {
        return $scope.provinciaClass[index].estilo;
    };
    //se obtiene el estilo visual se del boton correspondiente a la farmacia
    $scope.getFarmaciaClass = function (index) {
        return $scope.farmaciaClass[index].estilo;
    };

    //Se obtiene las categorias disponibles
    $scope.obtenerCategoria = function () {
        console.log("llamada a -> obtener categoria");
        //obtener las sucursales
        $http.get('/Product/obtenerListaCategoria')
            .success(function (result) {
                //se setean los datos de la categoria
                $scope.tipo_producto = result;
                $scope.categoriaProducto = result[0].Nombre;
                //se setea como valor inicial la categoria en el indice 0
                $scope.seleccionarCategoria($scope.tipo_producto[0], 0);
            }).error(function (data) {
                console.log(data);
            });
    };

    //se asigna el producto seleccionado
    $scope.asignarProductoSeleccionado = function (producto) {
        $scope.productoSeleccionado = producto;
    };


    $scope.agregarAlCarrito = function () {

        console.log("llamada a -> obtener categoria");
        //obtener las sucursales
        $http.post('Product/agregarAlCarrito', {
            pSucursal: $scope.sucursalSeleccionada,
                pFarmacia: $scope.farmaciaActiveElement,
                pProductoId: $scope.productoSeleccionado.IdProducto
        })
            .success(function (result) {
                console.log(result);
            }).error(function (data) {
                console.log(data);
            });
    }


    //se llaman a los valores iniciales al controlador
    $scope.activarProvincia(0);
    $scope.obtenerCategoria();


    //registrarProducto(string pNombreProducto, int pIdCategoria,int pIdLaboratorio,bool pPreescripcion, string pDescripcion)


});
