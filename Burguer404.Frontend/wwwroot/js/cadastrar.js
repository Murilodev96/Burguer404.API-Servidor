function cadastrar(event) {
    event.preventDefault();

    const nome = $('#nome').val();
    const email = $('#email').val();
    const cpf = $('#cpf').val();

    const request = {
        Nome: nome,
        Email: email,
        Cpf: cpf
    };

    $.ajax({
        type: "POST",
        url: "http://localhost:5000/api/Cliente/cadastrar",
        data: JSON.stringify(request),
        contentType: "application/json",

        sucess: function (response) {
            if (reponse.Sucesso) {
                console.log(response.Mensagem);
            }
        },
        error: function (response) {
            console.log(response.Mensagem);
        }
    })
}


function cadastrarProduto(event) {
    event.preventDefault();

    const form = document.querySelector("form");
    const formData = new FormData(form);

    // Corrigir o valor do preço (de vírgula para ponto)
    const precoCorrigido = formData.get("preco").replace(",", ".");
    formData.set("preco", precoCorrigido);

    $.ajax({
        type: "POST",
        url: "http://localhost:5000/api/Produto/cadastrar",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.sucesso) {
                console.log(response.mensagem);
                alert("Produto cadastrado!");
                form.reset();
                $('#nome-arquivo').text("Nenhum arquivo escolhido");
            }
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Erro ao cadastrar produto.");
        }
    });
}
function mascaraCPF(input) {
    let value = input.value.replace(/\D/g, '');

    if (value.length > 11) value = value.slice(0, 11);

    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    input.value = value;
}