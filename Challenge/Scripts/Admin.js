$(document).ready(function () {
    if (sessionStorage.getItem("PestañaActiva") != null)
    {
        switch (sessionStorage.getItem("PestañaActiva"))
        {
            case "Materias":
                $('#myTab li:first-child').click();
                break;
            case "Docentes":
                $('#myTab li:nth-child(2)').click();
                break;
        }
    }

    function mostrarModal(id, color, titulo, claseBoton) {
        $(id).removeClass("hide");
        $(id).addClass("show");

        $(id + ' h5.modal-title').html(titulo);

        switch (color) {
            case "azul":
                $(id + ' h5.modal-title').removeClass("verde");
                $(id + ' h5.modal-title').addClass("azul");
                break;
            case "verde":
                $(id + ' h5.modal-title').removeClass("azul");
                $(id + ' h5.modal-title').addClass("verde");
                break;
        }

        switch (claseBoton) {
            case "primary":
                $(id + ' .modal-footer>button').removeClass("btn-success");
                $(id + ' .modal-footer>button').addClass("btn-primary");
                break;
            case "success":
                $(id + ' .modal-footer>button').removeClass("btn-primary");
                $(id + ' .modal-footer>button').addClass("btn-success");
                break;
        }
    }

    //EDITAR 
    $('span.Editar').on('click', function () {
        let tabId = '#Modal_' + $($($($($($(this).parent()).parent()).parent()).parent()).parent()).attr("id");

        let datosFila = ($($(this).parent()).parent());
        let htmlForm = $(tabId + ' form').html();

        $(tabId + ' form').html('<p >Cod. <span>' + datosFila[0].children[0].innerText + '</span></p>' + htmlForm);

        function editarMaterias() {
            $('#IdMaterias').attr("value", datosFila[0].children[0].innerText);
            $('#Modal_Materias form input[name="Nombre"]').val(datosFila[0].children[1].innerText);
            $('#Modal_Materias form input[name="Descripcion"]').val(datosFila[0].children[2].innerText);
            $('#Modal_Materias form input[name="Horario"]').val(datosFila[0].children[3].innerText);

            let valor = datosFila[0].children[4].id;
            $('#Modal_Materias form select option').attr("selected", false);
            $('#Modal_Materias form select[name="Docente"] option[value="' + valor + '"]').attr("selected", true);

            $('#Modal_Materias form input[name="Cupo"]').val(datosFila[0].children[5].innerText);
        }

        function editarDocentes() {
            $('#IdMaterias').attr("value", datosFila[0].children[0].innerText);
            $('#Modal_Docentes form input[name="ApellidoDocente"]').val(datosFila[0].children[1].innerText);
            $('#Modal_Docentes form input[name="NombreDocente"]').val(datosFila[0].children[2].innerText);
            $('#Modal_Docentes form input[name="DNIDocente"]').val(datosFila[0].children[3].innerText);

            let valor = datosFila[0].children[4].innerText;
            $('#Modal_Docentes form select option').attr("selected", false);
            $('#Modal_Docentes form select[name="EstadoDocente"] option[value="' + valor + '"]').attr("selected", true);
        }

        let titulo;
        switch (tabId) {
            case '#Modal_Materias':
                editarMaterias();
                titulo = "Editar Materia";
                $(tabId + ' form').attr("action", "/Home/MMateria");
                break;
            case '#Modal_Docentes':
                editarDocentes();
                titulo = "Editar Docente";
                $(tabId + ' form').attr("action", "/Home/MDocente");
                break;
        }

        mostrarModal(tabId, "azul", titulo, "primary");
    });

    //AGREGAR 
    $('span.Agregar').on('click', function () {
        let tabId = '#Modal_' + $($($($($($(this).parent()).parent()).parent()).parent()).parent()).attr("id");
        let titulo;

        switch (tabId) {
            case '#Modal_Materias':
                titulo = "Agregar Materia";
                $(tabId + ' form').attr("action", "/Home/AMateria");
                break;
            case '#Modal_Docentes':
                titulo = "Agregar Docente"
                $(tabId + ' form').attr("action", "/Home/ADocente");
                break;
        }

        mostrarModal(tabId, "verde", titulo, "success");
    });

    //ELIMINAR 
    $('span.Eliminar').on('click', function () {
        let tabId = $($($($($($(this).parent()).parent()).parent()).parent()).parent()).attr("id");
        let html_ancestros = ($($(this).parent()).parent());
        let Titulo;
        let Body;
        let action;

        switch (tabId) {
            case "Materias":
                Titulo = "Materia";
                Body = '¿Desea elminar la materia: <strong>' + 'Cod ' + html_ancestros[0].children[0].innerText + ' - ' + html_ancestros[0].children[1].innerText + '</strong>?';
                action = "/Home/BMateria";
                break;
            case "Docentes":
                Titulo = "Docente";
                Body = '¿Desea elminar a: <strong>' + 'Cod. ' + html_ancestros[0].children[0].innerText + ' - ' + html_ancestros[0].children[1].innerText + ', ' + html_ancestros[0].children[2].innerText + '</strong>?';
                action = "/Home/BDocente";
                break;
        }

        $('#Modal_Eliminar .modal-title').html("Eliminar " + Titulo);
        $('#Modal_Eliminar .modal-body>p').html(Body);
        $('#Modal_Eliminar input[type="text"]').val(html_ancestros[0].children[0].innerText);
        $('#Modal_Eliminar form').attr("action", action);

        $('#Modal_Eliminar').removeClass("hide");
        $('#Modal_Eliminar').addClass("show");
    });

    //MODAL
    //Botón X 
    $('div.modal-header>button').on('click', function () {
        var idModal;
        switch ($($($($(this).parent()).parent()).parent()).parent().attr("id")) {
            case "Modal_Materias":
                idModal = "#Modal_Materias";
                break;
            case "Modal_Docentes":
                idModal = "#Modal_Docentes";
                break;
            case "Modal_Eliminar":
                idModal = "#Modal_Eliminar";
                break;
        }

        $(idModal).removeClass("show");
        $(idModal).addClass("hide");

        switch (idModal) {
            case "#Modal_Eliminar":
                break;
            default:
                $(idModal + ' input').val("");
                $(idModal + ' form p').empty();
                break;
        }
    });

    //Botón Guardar 
    $('div.modal-footer>button').on('click', function () {
        let idModal = $($($($(this).parent()).parent()).parent()).parent().attr("id");

        sessionStorage.setItem("PestañaActiva", $('div.active').attr("id"));

        switch (idModal) {
            case "Modal_Materias":
                $('#ModalSubmitMaterias').click();
                break;
            case "Modal_Docentes":
                $('#ModalSubmitDocentes').click();
                break;
            case "Modal_Eliminar":
                $('#ModalSubmitEliminar').click();
                break;
        }
    });




});


