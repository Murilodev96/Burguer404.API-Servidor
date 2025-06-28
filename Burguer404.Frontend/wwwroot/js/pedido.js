$(document).ready(function () {

    var clienteLogadoId = sessionStorage.getItem("clienteLogadoId");
    var perfilClienteId = sessionStorage.getItem("perfilClienteId");
    var nomeClienteLogado = sessionStorage.getItem("nomeClienteLogado");

    $('#lblNomeUsuarioLogadoId').text(nomeClienteLogado);

    if (perfilClienteId != 1) {
        $('#btnPaginaProdutos').css({
            'display': 'none'
        });
    }

    $('#tabelaPedidos').DataTable({
        ajax: {
            type: "GET",
            url: 'http://localhost:5000/api/PedidoHandler/listar',
            data: { clienteLogadoId: clienteLogadoId },
        },
        columns: [
            { data: 'codigoPedido' },
            { data: 'nomeCliente' },
            { data: 'dataFormatada' },
            {
                data: 'dataPedido',
                visible: false
            },
            { data: 'statusPedidoDescricao' },
            {
                data: null,
                render: function (data, type, row) {
                    var botoes = `<button class="btn-visualizar" onclick="VisualizarPedido('${row.codigoPedido}')">
                                     🔍
                                  </button>`;

                    if (perfilClienteId == 1) {
                        botoes += `<button class="btn-visualizar" onclick="AvancarStatus('${row.codigoPedido}')">
                                       🍔➡️
                                   </button>`;
                    }

                    return botoes;
                }
            }
        ],
        order: [[3, 'desc']],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json"
        }    
    });    
});

function VisualizarPedido(codigo) {
    $.ajax({
        url: 'http://localhost:5000/api/PedidoHandler/visualizar', 
        type: 'GET', 
        data: { codigo: codigo },
        success: function (response) {

            if (!response.sucesso) {
                alert(response.Mensagem);
                return;
            }

            const pedido = response.resultado[0];
            const produtos = pedido.produtosSelecionados;
            const valorTotal = produtos.reduce((total, p) => {
                return total + (p.produto.preco * p.quantidade);
            }, 0);

            $('#infoPedido').html(`
                <p><strong>Código do pedido:</strong> ${pedido.codigoPedido}</p>
                <p><strong>Pedido realizado em:</strong> ${new Date(pedido.dataPedido).toLocaleString()}</p>
                <p><strong>Valor total do pedido:</strong> R$${valorTotal.toFixed(2)}</p>
            `);

            const tbody = $('#tabelaProdutosPedido tbody');
            tbody.empty();

            produtos.forEach(p => {
                const { nome, descricao, preco } = p.produto;
                const quantidade = p.quantidade;
                const total = preco * quantidade;

                tbody.append(`
                    <tr>
                        <td>${nome}</td>
                        <td>${descricao}</td>
                        <td>${quantidade}</td>
                        <td>R$ ${preco.toFixed(2)}</td>
                        <td>R$ ${total.toFixed(2)}</td>
                    </tr>
                `);
            });

            $('#modalPedido').fadeIn().addClass('show');

            // Foco no modal (acessibilidade)
            $('.modal-content').attr('tabindex', '-1').focus();
        },
        error: function (xhr, status, error) {
            console.error("Erro ao visualizar pedido:", error);
            alert("Erro ao buscar os dados do pedido.");
        }
    });
}

function AvancarStatus(codigo) {
    $.ajax({
        url: 'http://localhost:5000/api/PedidoHandler/avancarStatusPedido',
        type: 'GET',
        data: { codigo: codigo },
        success: function (response) {

            if (!response.sucesso) {
                alert(response.Mensagem);
                return;
            }

            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error("Erro ao visualizar pedido:", error);
            alert("Erro ao buscar os dados do pedido.");
        }
    });
}

function fecharModal() {
    $('#modalPedido').fadeOut(() => $(this).removeClass('show'));
    $('#tabelaProdutosPedido tbody').empty();
    $('#infoPedido').empty();
}

function solicitarPedido() {
    window.location.href = "/Pedidos/Pedidos"
}

function painelProdutos() {
    window.location.href = "/Produto/Produto"
}

function DesconectarSessao() {
    sessionStorage.clear();
    window.location.href = "http://localhost:5001"
}