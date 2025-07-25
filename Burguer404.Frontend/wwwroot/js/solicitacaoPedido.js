$(document).ready(function () {
    const apiUrl = window.API_URL;

    $('select').select2({
        templateResult: formatOption,
        templateSelection: formatSelection,
        escapeMarkup: function (m) { return m; }
    });

    $('select').html('<option>Carregando...</option>');

    $.ajax({
        url: `${apiUrl}/api/ProdutoHandler/obterCardapio`,
        type: 'GET',
        success: function (response) {            
            if (response.sucesso) {
                $('select').select2('destroy');

                preencherSelect('#lanche', response.resultado[0].lanches);
                preencherSelect('#acompanhamento', response.resultado[0].acompanhamentos);
                preencherSelect('#bebida', response.resultado[0].bebidas);
                preencherSelect('#sobremesa', response.resultado[0].sobremesas);

                inicializarSelects();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao obter o cardápio:", error);
            alert("Erro ao obter o cardápio.");
        }
    });

    // Inicializa a DataTable
    $('#tabelaPedidos').DataTable({
        paging: false,
        searching: false,
        info: false,
        ordering: false,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json"
        }
    }); 
});

function adicionarItem() {
    const lancheId = $('#lanche option:selected').data('id') || 0;
    const lancheNome = $('#lanche option:selected').data('nome') || '-';
    const lancheValor = $('#lanche option:selected').data('preco') || 0;

    const acompanhamentoId = $('#acompanhamento option:selected').data('id') || 0;
    const acompanhamentoNome = $('#acompanhamento option:selected').data('nome') || '-';
    const acompanhamentoValor = $('#acompanhamento option:selected').data('preco') || 0;

    const bebidaId = $('#bebida option:selected').data('id') || 0;
    const bebidaNome = $('#bebida option:selected').data('nome') || '-';
    const bebidaValor = $('#bebida option:selected').data('preco') || 0;

    const sobremesaId = $('#sobremesa option:selected').data('id') || 0;
    const sobremesaNome = $('#sobremesa option:selected').data('nome') || '-';
    const sobremesaValor = $('#sobremesa option:selected').data('preco') || 0;

    const valorCombo = (lancheValor + acompanhamentoValor + bebidaValor + sobremesaValor).toFixed(2);

    const botaoExcluir = `<div style="display: flex; align-items: center; justify-content: center; gap: 5px;">
                            <button onclick="removerLinha(this)" style="background:none;border:none;color:red;font-size:18px;">                            
                                <i class='bi bi-trash'></i>
                            </button>
                                <button onclick="alterarQuantidade(this, -1)" style="background:none; border:none; color:#e74c3c; font-size:18px;">
                                    <i class="bi bi-dash-circle"></i>
                                </button>
                                <input type="text" value="1" readonly style="width: 30px; text-align: center; border: none; background: transparent; font-size: 16px;" />
                                <button onclick="alterarQuantidade(this, 1)" style="background:none; border:none; color:#27ae60; font-size:18px;">
                                    <i class="bi bi-plus-circle"></i>
                                </button>
                            </div>`;

    const table = $('#tabelaPedidos').DataTable();
    table.row.add([botaoExcluir,
        `<span data-lanche-id=${lancheId} style="display:none"></span>${lancheNome}`,
        `<span data-acompanhamento-id=${acompanhamentoId} style="display:none"></span>${acompanhamentoNome}`,
        `<span data-bebida-id=${bebidaId} style="display:none"></span>${bebidaNome}`,
        `<span data-sobremesa-id=${sobremesaId} style="display:none"></span>${sobremesaNome}`,
        valorCombo]).draw();

    $('select').val('').trigger('change');
}

function removerLinha(botao) {
    const table = $('#tabelaPedidos').DataTable();
    table.row($(botao).parents('tr')).remove().draw();
}
function continuarPagamento() {
    const table = $('#tabelaPedidos').DataTable();
    const dadosTabela = table.rows().data();
    const pedidos = [];
    const apiUrl = window.API_URL;


    for (let i = 0; i < dadosTabela.length; i++) {

        const row = dadosTabela[i];

        const lanche = $(row[1]).filter('span').data('lanche-id');
        const acompanhamento = $(row[2]).filter('span').data('acompanhamento-id');
        const bebida = $(row[3]).filter('span').data('bebida-id');
        const sobremesa = $(row[4]).filter('span').data('sobremesa-id');

        // Recupera a quantidade diretamente do input na célula de ações
        const quantidadeInput = $(table.row(i).node()).find("input");
        const quantidade = parseInt(quantidadeInput.val()) || 1;

        // Valor total (já multiplicado pela quantidade), vamos calcular unitário
        const valorTotal = parseFloat(row[5].replace('R$', '').replace(',', '.'));
        const valorUnitario = valorTotal / quantidade;

        var clienteLogadoId = sessionStorage.getItem("clienteLogadoId");

        pedidos.push({
            LancheId: lanche,
            AcompanhamentoId: acompanhamento,
            BebidaId: bebida,
            SobremesaId: sobremesa,
            Valor: valorUnitario,
            Quantidade: quantidade,
            ClienteId: clienteLogadoId
        });
    }

    if (pedidos.length === 0) {
        alert("Adicione pelo menos um item ao pedido.");
        return;
    }

    $.ajax({
            type: "POST",
        url: `${apiUrl}/api/PedidoHandler/pagamento`,
            data: JSON.stringify(pedidos),
            contentType: "application/json",

        success: function (response) {
                console.log("Resposta do servidor:", response);                
            table.clear().draw();
            window.location.href = `/Pedidos/QrCode?qrcode=${encodeURIComponent(response.resultado)}`;

            },
            error: function (xhr, status, error) {
                console.error("Erro ao enviar pedido:", error);
                alert("Ocorreu um erro ao processar o pedido.");
            }
        });
}

function preencherSelect(idSelect, lista) {
    const select = $(idSelect);
    select.empty();
    select.append(new Option("Selecione", "", true, true));

    lista.forEach(item => {
        const option = new Option(item.nome, item.nome, false, false);
        $(option).data('id', item.id);
        $(option).data('nome', item.nome);
        $(option).data('descricao', item.descricao);
        $(option).data('preco', item.preco);
        $(option).data('imagem', item.imagemBase64);
        select.append(option);
    });

    select.trigger('change');
}

function formatOption(option) {
    if (!option.id) {
        return option.text;
    }

    const $element = $(option.element);

    const nome = $element.data('nome') || '';
    const descricao = $element.data('descricao') || '';
    const preco = $element.data('preco') || '';
    const imagem = $element.data('imagem') || 'https://via.placeholder.com/50';

    return `
        <div style="display: flex; align-items: center;">
            <img src="${imagem}" style="width: 50px; height: 50px; object-fit: cover; border-radius: 5px; margin-right: 10px;">
            <div style="flex-grow: 1;">
                <div style="font-weight: bold;">${nome}</div>
                <div style="font-size: 12px; color: #777;">${descricao}</div>
            </div>
            <div style="margin-left: auto; font-weight: bold; color: green;">R$ ${parseFloat(preco).toFixed(2)}</div>
        </div>
    `;
}

function formatSelection(option) {
    if (!option.id) {
        return option.text;
    }
    const $element = $(option.element);
    const nome = $element.data('nome') || option.text;
    return nome;
}

function inicializarSelects() {
    $('select').select2({
        templateResult: formatOption,
        templateSelection: formatSelection,
        escapeMarkup: function (m) { return m; }
    });
}

function alterarQuantidade(botao, delta) {
    const container = botao.parentElement;
    const input = container.querySelector("input");
    let quantidadeAtual = parseInt(input.value);
    debugger;
    let novaQuantidade = quantidadeAtual + delta;
    if (novaQuantidade < 1) novaQuantidade = 1;

    input.value = novaQuantidade;
    const rowElement = botao.closest('tr');

    let valorTotalText = $(rowElement).find('td').eq(5).text().replace('R$', '').replace(',', '.').trim();
    let valorTotalAtual = parseFloat(valorTotalText);
    let valorUnitario = valorTotalAtual / quantidadeAtual;
    let novoValorTotal = (valorUnitario * novaQuantidade).toFixed(2).replace('.', ',');

    $(rowElement).find('td').eq(5).text(`R$ ${novoValorTotal}`);
}

function VoltarParaPedidos() {
    window.location.href = "/Login/Login";
}