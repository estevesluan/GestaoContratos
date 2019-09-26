var ContratoListaModulo = (function () {
    var _numeroPagina;
    var _paginacao;

    var _novaPesquisa = function(){

        _numeroPagina = 1;
        _carregarContratos();

    }

    var _editarContrato = function (e) {
        var id = $(e).attr("data-id-contrato");
        window.location.href = caminhoBase + "Contrato/Cadastro/" + id;
    }

    var _carregarContratos = function () {

        var lista = $("#div-lista-contratos");
        var pesquisa = $("#input-pesquisa-contrato").val();
        var contratosPorPagina = $("#select-contratos-por-pagina option:selected").val();

        //Exibe o loading no botão
        var botao = $("#btn-pesquisar-contrato");
        botao.prop("disabled", true);
        botao.find(".spinner-border").show();

        $.ajax({
            url: caminhoBase + "Contrato/ListaContratosPagina/",
            data: { pesquisa: pesquisa, numeroPagina: _numeroPagina, numeroItensPorPagina: contratosPorPagina },
            type: "GET"
        })
            .done(function (data) {

                lista.empty();

                if (data.Contratos !== undefined && data.Contratos !== null && data.Contratos.length > 0) {
  
                    _paginacao.twbsPagination('destroy');
                    _paginacao.twbsPagination({
                        startPage: _numeroPagina,
                        totalPages: data.Total,
                        first: 'Início',
                        prev: '<',
                        next: '>',
                        last: 'Fim'
                    }).on('page', function (evt, page) {
                        _numeroPagina = page;
                        _carregarContratos();
                    });
                    $.each(data.Contratos, (index, valor) => {


                        //Cria elementos da pagina para adicionar ao html
                        var img = $(document.createElement("img"));
                        var divContrato = $(document.createElement("div"));
                        var divCard = $(document.createElement("div"));
                        var divCardBody = $(document.createElement("div"));
                        var pCard = $(document.createElement("p"));
                        var divCardBtn = $(document.createElement("div"));
                        var divCardBtnGroup = $(document.createElement("div"));

                        var btnEditar = $(document.createElement("button"));

                        img.attr("src", "/Content/imagens/contrato.png");

                        img.addClass("card-img-top");
                        divContrato.addClass("col-md-4");
                        divCard.addClass("card mb-4 shadow-sm");
                        divCardBody.addClass("card-body");
                        pCard.html(valor.NomeCliente).addClass("card-text");
                        divCardBtn.addClass("d-flex justify-content-between align-items-center");
                        divCardBtnGroup.addClass("btn-group");

                        btnEditar.html("Editar").attr("type", "button").attr("data-id-contrato", valor.Id).addClass("btn btn-sm btn-outline-secondary btn-editar-contrato");

                        divCardBtnGroup.append(btnEditar);
                        divCardBtn.append(divCardBtnGroup);
                        divCardBody.append(pCard);
                        divCardBody.append(divCardBtnGroup);
                        divCard.append(img);
                        divCard.append(divCardBody);
                        divContrato.append(divCard);

                        lista.append(divContrato);
                    });
                }
                //Esconde o loading do botão
                botao.prop("disabled", false);
                botao.find(".spinner-border").hide();
            })
            .fail(function () {
                //Esconde o loading do botão
                botao.prop("disabled", false);
                botao.find(".spinner-border").hide();
            });

    }

    var inicializar = function () {

        //iniciar contagem
        _numerooPagina = 1
        _paginacao = $('#ul-paginacao-contratos');

        //Eventos
        $("body").on("click", "#btn-pesquisar-contrato", _novaPesquisa);
        $("body").on("click", ".btn-editar-contrato", function (e) { _editarContrato(e.currentTarget) });
        $('#select-contratos-por-pagina').on("change", _novaPesquisa );

        _novaPesquisa();

    }

    return {
        inicializar: inicializar
    }

});

$(function () {

    var modulo = new ContratoListaModulo();
    modulo.inicializar();

});