$(document).ready(function () {

    function mostrarModal(html_ancestros, color, titulo, nombreBoton) {

        $('#Modal h5.modal-title').html(titulo);
        console.log(this);
        console.log(html_ancestros);

        switch (color) {
            case "rojo":
                $('#Modal h5.modal-title').removeClass("verde");
                $('#Modal h5.modal-title').addClass("rojo");

                $('#Modal .modal-body>p').html('¿Desea desanotarse de la materia: <strong> Cod. ' + html_ancestros[0].children[0].innerText + ' - ' + html_ancestros[0].children[1].innerText + '</strong>?')

                $('#Modal .modal-footer>button').removeClass("btn-success");
                $('#Modal .modal-footer>button').addClass("btn-danger");

                $('form').attr("action", "/Home/Desanotarme");
                break;

            case "verde":
                $('#Modal h5.modal-title').removeClass("rojo");
                $('#Modal h5.modal-title').addClass("verde");

                $('#Modal .modal-body > p').html('¿Desea inscribirse en la materia: <strong> Cod. ' + html_ancestros[0].children[0].innerText + ' - ' + html_ancestros[0].children[1].innerText + '</strong>?')

                $('#Modal .modal-footer>button').removeClass("btn-danger");
                $('#Modal .modal-footer>button').addClass("btn-success");

                $('form').attr("action", "/Home/Anotarme");
                break;
        }

        $('#Modal .modal-footer>button').html(nombreBoton);

        $('input').val(html_ancestros[0].children[0].innerText);

        $('#Modal').removeClass("hide");
        $('#Modal').addClass("show");
    }

    //TABLAS
    //Comprimir - Expandir
    $('.TituloTabla span>svg').on('click', function () {
        $(this).toggleClass("rotado");
        let tableId = $($($($($($(this).parent()).parent()).parent()).parent()).parent()).attr("id");
        $('#' + tableId + ' .toggleTable').fadeToggle();
    });

    //x
    $('span.Eliminar').on('click', function () {
        let html_ancestros = ($($(this).parent()).parent());
        mostrarModal(html_ancestros, "rojo", "Materias Inscriptas", "Desanotarme");
    });

    //+
    $('span.Agregar').on('click', function () {
        let html_ancestros = ($($(this).parent()).parent());
        mostrarModal(html_ancestros, "verde", "Listado de Materias", "Anotarme");
    });

    //MODAL
    //Botón x 
    $('div.modal-header>button').on('click', function () {
        $('#Modal').removeClass("show");
        $('#Modal').addClass("hide");
        $('input').val("");
    });

    //Botón Submit 
    $('div.modal-footer>button').on('click', function () {
        $('#ModalSubmit').click();
    });
});