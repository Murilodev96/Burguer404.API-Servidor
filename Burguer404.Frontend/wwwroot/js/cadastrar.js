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
        url: "http://localhost:5000/api/ClienteHandler/cadastrar",
        data: JSON.stringify(request),
        contentType: "application/json",

        success: function (response) {
            if (response.sucesso) {
                alert(response.mensagem);
                window.location.href = "/"
            }
            else {
                alert(response.mensagem)
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


    $.ajax({
        type: "POST",
        url: "http://localhost:5000/api/ProdutoHandler/cadastrar",
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
            else
            {
                alert("Erro Ao Cadastrar Produto.");
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