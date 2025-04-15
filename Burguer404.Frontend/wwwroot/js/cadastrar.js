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

function mascaraCPF(input) {
    let value = input.value.replace(/\D/g, '');

    if (value.length > 11) value = value.slice(0, 11);

    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    input.value = value;
}