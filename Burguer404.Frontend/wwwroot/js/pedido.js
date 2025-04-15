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
                    return `<button class="btn-visualizar" onclick="VisualizarPedido(${row.codigoPedido})">
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

}