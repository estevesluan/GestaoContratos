var ContratoModulo = (function () {

    var _novoCadastro = function () {

        $("#form-contrato")[0].reset();
        $("#input-id").val("0");
        $("#input-arquivo").next(".custom-file-label").html("Selecione o PDF do contrato");
        $("#input-nome-cliente").val("");
        $("#select-tipo-contrato").val(0);
        $("#input-quantidade").val("");
        $("#input-valor").val("");
        $("#input-inicio").val("");
        $("#input-fim").val("");
        $("#btn-excluir").hide();

        var campos = $("input.form-control");
        $.each(campos, function (index, campo) {
            $(campo).removeClass("is-valid");
            $(campo).removeClass("is-invalid");
        });

    }

    var _salvar = function (e) {

        var data = new FormData();

        data.append("id", $("#input-id").val());
        data.append("arquivo", $('#input-arquivo')[0].files[0]);
        data.append("nomeCliente",  $("#input-nome-cliente").val());
        data.append("tipoContrato",  $("#select-tipo-contrato").val());
        data.append("quantidade", $("#input-quantidade").val());
        data.append("valor", $("#input-valor").val().replace('.', ''));
        data.append("inicio",  '01/' + $("#input-inicio").val());
        data.append("fim",  '01/' + $("#input-fim").val());

        var campos = [
            {
                obj: $("#input-nome-cliente"),
                valor: data.get("nomeCliente"),
                mensagem: "Preencha o nome do cliente"
            },
            {
                obj: $("#input-quantidade"),
                valor: data.get("quantidade"),
                mensagem: "Preencha a quantidade"
            },
            {
                obj: $("#input-valor"),
                valor: data.get("valor"),
                mensagem: "Preencha o valor"
            },
            {
                obj: $("#input-inicio"),
                valor: $("#input-inicio").val(),
                mensagem: "Preencha a data de início"
            },
            {
                obj: $("#input-fim"),
                valor: $("#input-fim").val(),
                mensagem: "Preencha a data final"
            }
        ];

        let validacao = true;

        $.each(campos, function (index, campo) {

            if (isNullOrEmpty(campo.valor) || campo.valor == 0) {

                campo.obj.focus();

                campo.obj.removeClass("is-valid");
                campo.obj.addClass("is-invalid");

                validacao = false;
                return;
            }

            campo.obj.removeClass("is-invalid");
            campo.obj.addClass("is-valid");
        });

        if (!validacao) {
            PNotify.alert({
                text: "Preencha os campos obrigatórios",
                type: 'notice'
            });
            return;
        }

        var dateDataInicio = createDate(data.get("inicio"));

        if (dateDataInicio == null || dateDataInicio == "Invalid Date") {

            var campo = $("#input-inicio");

            campo.focus();
            campo.removeClass("is-valid");
            campo.addClass("is-invalid");

            PNotify.alert({
                text: "Data de início inválida. Preencha no formato mm/aaaa",
                type: 'notice'
            });

            return;
        }

        var dateDataFim = createDate(data.get("fim"));

        if (dateDataFim == null || dateDataFim == "Invalid Date") {

            var campo = $("#input-fim");

            campo.focus();
            campo.removeClass("is-valid");
            campo.addClass("is-invalid");

            PNotify.alert({
                text: "Data fim inválida. Preencha no formato mm/aaaa",
                type: 'notice'
            });

            return;
        }

        if (dateDataInicio > dateDataFim) {

            var campo = $("#input-inicio");

            campo.focus();
            campo.removeClass("is-valid");
            campo.addClass("is-invalid");

            PNotify.alert({
                text: "A data início não pode ser maior que a data final do contrato",
                type: 'notice'
            });

            return;
        }

        $.ajax({

            url: caminhoBase + "Contrato/Cadastro",
            type: "POST",
            data: data,
            processData: false,
            contentType: false

        })
            .done(function (data) {

                if (data.hasOwnProperty("erro")) {

                    PNotify.alert({
                        text: data.erro,
                        type: 'error'
                    });

                } else {

                    PNotify.alert({
                        text: "Cadastro realizado!",
                        type: 'success'
                    });
                    _novoCadastro();

                }

            })
            .fail(function () {

                PNotify.alert({
                    text: "Erro ao realizar a comunicação com o servidor",
                    type: 'error'
                });

            });

    }

    var _excluir = function () {

        var id = $("#input-id").val();

        $.ajax({
            url: caminhoBase + "Contrato/Remover/" + id,
            type: "POST",
        })
            .done(function (data) {

                if (data.hasOwnProperty("erro")) {

                    PNotify.alert({
                        text: data.erro,
                        type: 'error'
                    });

                } else {

                    PNotify.alert({
                        text: "Removido com sucesso!",
                        type: 'success'
                    });
                    _novoCadastro();

                }

            })
            .fail(function () {

                PNotify.alert({
                    text: "Erro ao realizar a comunicação com o servidor",
                    type: 'error'
                });

            });

    }

    var _carregarContrato = function (id) {

        $.ajax({

            url: caminhoBase + "Contrato/Dados/" + id,
            type: "GET"

        })
            .done(function (data) {

                if (data.hasOwnProperty("erro")) {

                    PNotify.alert({
                        text: data.erro,
                        type: 'error'
                    });

                    $("#btn-excluir").hide();

                } else {

                    $("#input-arquivo").next(".custom-file-label").html("Selecione o PDF do contrato");
                    $("#input-nome-cliente").val(data.NomeCliente);
                    $("#select-tipo-contrato").val(data.TipoContrato);
                    $("#input-quantidade").val(data.Quantidade);
                    $("#input-valor").val(data.Valor.toFixed(2)).trigger('input');
                    $("#input-inicio").val(moment.utc(data.Inicio).format('MM/YYYY')).trigger('input');
                    $("#input-fim").val(moment.utc(data.Fim).format('MM/YYYY')).trigger('input');

                    $("#btn-excluir").show();

                }
            })
            .fail(function () {

                PNotify.alert({
                    text: "Erro ao realizar a comunicação com o servidor",
                    type: 'error'
                });
                $("#btn-excluir").hide();

            });

    }

    var _inicializarMascara = function () {

        //Inicializa máscaras
        $("#input-quantidade").mask('000000');
        $("#input-valor").mask('#.##0,00', { reverse: true });
        $("#input-inicio").mask('00/0000');
        $("#input-fim").mask('00/0000');

    };

    var inicializar = function () {

        _inicializarMascara();

        //Inicializa os eventos
        $("form").on("submit", function (e) { e.preventDefault() });
        $("body").on("click", "#btn-novo", _novoCadastro);
        $("body").on("click", "#btn-salvar", _salvar);
        $("body").on("click", "#btn-excluir", _excluir);

        //Verifica edição de usuário
        var id = $("#input-id").val();

        if (id != null && id > 0) {
            _carregarContrato(id);
        }

    }

    return {

        inicializar: inicializar

    }

});


$(function () {

    var modulo = new ContratoModulo();
    modulo.inicializar();

    $("#input-nome-cliente").val("luan");
    $("#input-quantidade").val("10").trigger('input');
    $("#input-valor").val("10000").trigger('input');
    $("#input-inicio").val("01/2019").trigger('input');
    $("#input-fim").val("02/2019").trigger('input');

});