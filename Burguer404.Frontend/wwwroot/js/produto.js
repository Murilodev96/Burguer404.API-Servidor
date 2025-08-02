$(document).ready(function () {
    const apiUrl = window.API_URL;

    const EnumCategoriaPedido = {
        1: "Lanche",
        2: "Acompanhamento",
        3: "Bebida",
        4: "Sobremesa"
    };

    $('#tabelaProdutos').DataTable({
        ajax: {
            url: `${apiUrl}/api/ProdutoHandler/listar`,
            dataSrc: 'data'
        },
        columns: [
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <div style="display: flex; gap: 5px;">
                            <button class="btn-arquivo" onclick="buscarImagem(${row.id})">
                                <i class="bi bi-search"></i>
                            </button>
                            <button class="btn-excluir" onclick="excluirProduto(${row.id})">
                                <i class="bi bi-trash"></i>
                            </button>
                        </div>
                        `;
                }
            },
            { data: 'nome' },
            { data: 'descricao' },
            {
                data: 'preco',
                render: function (data) {
                    return 'R$' + data.toFixed(2).replace('.', ',');
                }
            },
            {
                data: 'categoriaProdutoId',
                render: function (data) {
                    return EnumCategoriaPedido[data] || "Desconhecido";
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json"
        }
    });   
});

function buscarImagem(id) {
    const apiUrl = window.API_URL;
    $.ajax({
        url: `${apiUrl}/api/ProdutoHandler/visualizarImagem?id=${id}`,
        method: 'GET',
        success: function (response) {
            if (response && response.resultado && response.resultado.length > 0) {
                const imagemBase64 = response.resultado[0];
                mostrarImagem(imagemBase64);
            } else {
                alert("Este produto não possui imagem.");
            }
        },
        error: function () {
            alert("Erro ao buscar imagem do produto.");
        }
    });
}

function excluirProduto(id) {
    if (confirm("Tem certeza que deseja excluir este produto?")) {
        $.ajax({
            url: `${apiUrl}/api/ProdutoHandler/remover/?id=${id}`,
            method: 'GET',
            success: function (response) {
                alert("Produto excluído com sucesso!");
                $('#tabelaProdutos').DataTable().ajax.reload();
            },
            error: function () {
                alert("Erro ao excluir o produto.");
            }
        });
    }
}

function mostrarImagem(base64) {
    const imagemHtml = `
        <div class="modal-imagem">
            <img src="data:image/jpeg;base64,${base64}" alt="Imagem do Produto" style="max-width: 100%; max-height: 500px;" />
        </div>
    `;

    $('body').append(`
        <div id="modalImagem" class="modal-overlay">
            <div class="modal-content">
                <button onclick="$('#modalImagem').remove()" class="btn-fechar">X</button>
                ${imagemHtml}
            </div>
        </div>
    `);
}



document.addEventListener("DOMContentLoaded", function () {
    const precoInput = document.getElementById("preco");

    precoInput.addEventListener("input", function () {
        let valor = this.value;

        // Remove tudo exceto números e vírgula
        valor = valor.replace(/[^\d,]/g, "");

        // Permitir apenas uma vírgula
        let partes = valor.split(",");
        if (partes.length > 2) {
            valor = partes[0] + "," + partes[1];
        }

        // Se houver mais de 2 dígitos após a vírgula, corta
        if (partes[1]?.length > 2) {
            valor = partes[0] + "," + partes[1].slice(0, 2);
        }

        this.value = valor;
    });

    precoInput.addEventListener("blur", function () {
        let valor = this.value;

        // Completa com zeros se necessário
        if (valor.includes(",")) {
            let partes = valor.split(",");
            partes[1] = (partes[1] + "00").slice(0, 2);
            this.value = partes[0] + "," + partes[1];
        } else if (valor !== "") {
            this.value = valor + ",00";
        }
    });
});

function VoltarParaPedidos() {
    window.location.href = "/Login/Login";
}