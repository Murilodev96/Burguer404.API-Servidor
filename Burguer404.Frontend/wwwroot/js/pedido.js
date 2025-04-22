$(document).ready(function () {

    $('#tabelaPedidos').DataTable({
        ajax: {
            type: "GET",
            url: 'http://localhost:5000/api/Pedido/listar',
            dataSrc: 'data'
        },
        columns: [
            { data: 'codigoPedido' },
            { data: 'statusPedidoDescricao' },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button class="btn-visualizar" onclick="VisualizarPedido('${row.codigoPedido}')">
                                🔍
                            </button>`;
                }
            }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json"
        }    
    });
});

function VisualizarPedido(codigo) {
    $.ajax({
        url: 'http://localhost:5000/api/Pedido/visualizar', 
        type: 'GET', 
        data: { codigo: codigo },
        success: function (response) {

            debugger;
            if (!response.sucesso) {
                alert(response.Mensagem);
                return;
            }

            const pedido = response.resultado[0];
            const produtos = pedido.produtosSelecionados;

            $('#infoPedido').html(`
                <p><strong>Código:</strong> ${pedido.codigoPedido}</p>
                <p><strong>Data:</strong> ${new Date(pedido.dataPedido).toLocaleString()}</p>
                <p><strong>Status do Pedido:</strong> ${pedido.Status}</p>
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

function fecharModal() {
    $('#modalPedido').fadeOut(() => $(this).removeClass('show'));
    $('#tabelaProdutosPedido tbody').empty();
    $('#infoPedido').empty();
}