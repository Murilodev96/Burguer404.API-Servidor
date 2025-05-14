$(document).ready(function () {

    const EnumCategoriaPedido = {
        1: "Lanche",
        2: "Acompanhamento",
        3: "Bebida",
        4: "Sobremesa"
    };

    $('#tabelaProdutos').DataTable({
        ajax: {
            url: 'http://localhost:5000/api/Produto/listar',
            dataSrc: 'data'
        },
        columns: [
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <button class="btn-arquivo" onclick="buscarImagem(${row.id})">
                            <i class="bi bi-search"></i>
                        </button>
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
                data: 'categoriaPedidoId',
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
    $.ajax({
        url: `http://localhost:5000/api/Produto/visualizarImagem?id=${id}`,
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
        // Remove tudo que não for número
        let digits = this.value.replace(/\D/g, "");

        if (digits.length === 0) {
            this.value = "";
            return;
        }

        // Converte os últimos dois dígitos em centavos
        let intPart = digits.slice(0, -2) || "0";
        let decimalPart = digits.slice(-2);
        let formatted = `${parseInt(intPart)}.${decimalPart}`;

        // Formata com vírgula para o usuário
        this.value = parseFloat(formatted).toFixed(2).replace(".", ",");
    });

    // Converte para número com ponto ao enviar (opcional)
    document.querySelector("form")?.addEventListener("submit", function () {
        let raw = precoInput.value.replace(",", ".");
        precoInput.value = parseFloat(raw).toFixed(2); // backend-friendly
    });
});