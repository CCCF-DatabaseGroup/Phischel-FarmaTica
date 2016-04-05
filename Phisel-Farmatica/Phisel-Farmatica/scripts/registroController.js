myApp.controller('registroController', function ($scope, $http,$window) {
    /**
    $scope.usuario_Cedula;
    $scope.usuario_Padecimiento;


    */

    $scope.registrarUsuario = function () {
        $http.post("/RegistrarUsuario/RegistrarUsuario", {
            pNickname: $scope.usuario_Alias,
            pContrasena: $scope.usuario_Contrasenia, pCorreoElectronico: $scope.usuario_Email
        })
    .success(function (result) {

        if (result.Status == true) {
            $window.location.href = '/';
        }
        else {
            $scope.advertenciaUsuarioRegistrado = "El usuario ya existe";
        }

    })
    .error(function (data) {
        $scope.advertenciaUsuarioRegistrado = "El usuario ya existe";
    });

    }


});

