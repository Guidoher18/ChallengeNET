$(document).ready(function () {
    //Comprimir - Expandir Tablas
    $('.TituloTabla span>svg').on('click', function () {
        $(this).toggleClass("rotado");
        let tableId = $($($($($($(this).parent()).parent()).parent()).parent()).parent()).attr("id");
        $('#' + tableId + ' .toggleTable').fadeToggle();
    });

    function Confirmar(tipoBoton, html_ancestros, pregunta) {
        let r;

        r = confirm('¿Desea ' + pregunta + ' de la materia: ' + 'Cod. ' + html_ancestros[0].children[0].innerText + ' - ' + html_ancestros[0].children[1].innerText + '?');

        if (r) {
            let href;

            switch (tipoBoton) {
                case "Eliminar":
                    href = "/Home/Desanotar";
                    break;
                case "Agregar":
                    href = "/Home/Anotar";
                    break;
            }

            location.href = href;
        }
    }

    //x
    $('span.Eliminar').on('click', function () {
        let html_ancestros = ($($(this).parent()).parent());
        Confirmar("Eliminar", html_ancestros, "desanotarse");
    });

    //+
    $('span.Agregar').on('click', function () {
        let html_ancestros = ($($(this).parent()).parent());
        Confirmar("Agregar", html_ancestros, "anotarse");        
    });
});