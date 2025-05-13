$(document).ready(function () {
    $('#tabelaProdutos').DataTable();

    $('#imagem').on('change', function () {
        const nomeArquivo = $(this).val().split('\\').pop();
        $('#nome-arquivo').text(nomeArquivo || "Nenhum arquivo escolhido");
    });
});

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