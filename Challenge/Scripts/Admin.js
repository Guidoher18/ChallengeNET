$(document).ready(function () {

    function MostrarModal(color, tipoTitulo, boton) {
        $('#Modal_Uno').removeClass("hide");
        $('#Modal_Uno').addClass("show");
        let title = tipoTitulo + " Materia";
        $('#ModalTitle.modal-title').html(title);
        switch (color) {
            case "azul":
                $('#ModalTitle.modal-title').removeClass("verde");
                $('#ModalTitle.modal-title').addClass("azul");
                break;
            case "verde":
                $('#ModalTitle.modal-title').removeClass("azul");
                $('#ModalTitle.modal-title').addClass("verde");
                break;
        }
        $('#ModalTitle.modal-title').addClass(color);
        switch (boton) {
            case "primary":
                $('.modal-footer>button').removeClass("btn-success");
                $('.modal-footer>button').addClass("btn-primary");
                break;
            case "success":
                $('.modal-footer>button').removeClass("btn-primary");
                $('.modal-footer>button').addClass("btn-success");
                break;
        }
    }

    //EDITAR
    $('span.Editar').on('click', function () {
        let texto = ($($(this).parent()).parent());
        //let codigo = $('#Modal_Uno form').html();

        $('#Modal_Uno form').html('<p>Cod. <span>' + texto[0].children[0].innerText + '</span></p>' + codigo);
        $('form input[name="Nombre"]').val(texto[0].children[1].innerText);
        $('form input[name="Descripcion"]').val(texto[0].children[2].innerText);
        $('form input[name="Horario"]').val(texto[0].children[3].innerText);

        let valor = texto[0].children[4].id;
        $('form select option').attr("selected", false);
        $('form select[name="Docente"] option[value="' + valor + '"]').attr("selected",true);

        $('form input[name="Cupo"]').val(texto[0].children[5].innerText);

        MostrarModal("azul", "Editar", "primary");
    });

    //ELIMINAR
    $('span.Eliminar').on('click', function () {
        let texto = ($($(this).parent()).parent());
        r = confirm('¿Desea elminar ' + 'Cod. ' + texto[0].children[0].innerText + ' - ' + texto[0].children[1].innerText + '?');
        if (r) {
            location.href ="/Home/Eliminar";
        }
    });


    //AGREGAR
    $('#Agregar').on('click', function () {
        MostrarModal("verde", "Agregar", "success");
    });


    //MODAL
    //Botón X
    $('#Modal_Uno div.modal-header>button').on('click', function () {
        $('#Modal_Uno').removeClass("show");
        $('#Modal_Uno').addClass("hide");
        $('#Modal_Uno input').val("");
        $('#Modal_Uno form p').empty();
    });

    //Botón Guardar
    $('div.modal-footer>button').on('click', function () {
        $('#ModalSubmit').click();
    });
});


