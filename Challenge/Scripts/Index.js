$(document).ready(function () {
    //Legajo
    $('#Tipo_de_Usuario>label').on('click', function () {
        $('#Tipo_de_Usuario>label[class~= "active"]').removeClass("active");
        $(this).addClass("active");
        switch ($('#Tipo_de_Usuario>label[class~= active]>input').val()) {
            case "Alumno":
                $('#DivLegajo').fadeIn();
                $('#Legajo').attr("required", "true");
                break;
            case "Admin":
                $('#DivLegajo').fadeOut();
                $('#Legajo').removeAttr("required");
                $('#Legajo').val("");
                break;
        }
    });

    //Info escribir los dígitos sin puntos
    var idSetTimeout;

    let mostrarOcultar = function () {
        $('#info').removeClass("oculto");
        idSetTimeout = setTimeout(function () {
            $('#info').addClass("oculto");
        }, 6000);
    }

    $('#Dni').on("focus", function () {
        $('#info>span').html("8");
        mostrarOcultar();
    });
    $('#Legajo').on("focus", function () {
        $('#info>span').html("4");
        mostrarOcultar();
    });
    $('#Contraseña').on("focus", function () {
        switch ($('#info').css("display")) {
            case "none": break;
            default: 
                clearTimeout(idSetTimeout);
                $('#info').addClass("oculto");
                break;
        }
    });
});