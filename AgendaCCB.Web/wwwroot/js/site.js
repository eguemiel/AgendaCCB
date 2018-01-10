// Write your JavaScript code.
$(document).ready(function () {
    $.fn.capitalize = function () {
        $.each(this, function () {
            var split = this.value.split(' ');
            for (var i = 0, len = split.length; i < len; i++) {
                split[i] = split[i].charAt(0).toUpperCase() + split[i].slice(1).toLowerCase();
            }

            this.value = split.join(' ');
        });
        return this;
    };
    
    $('input[type=text]').bind('blur', function (e) {
        if (e.which >= 97 && e.which <= 122) {
            var newKey = e.which - 32;
            // I have tried setting those
            e.keyCode = newKey;
            e.charCode = newKey;
        }

        $(this).capitalize();
    });

    $("#datatable").DataTable({
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar ",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

    $(".phoneNumber").focusout(function () {
        var phone, element;
        element = $(this);
        element.unmask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9");
        } else if (phone.length == 3)
        {
            element.mask("999");
        } else {
            element.mask("(99) 9999-9999?9");
        }
    }).trigger('focusout');
});