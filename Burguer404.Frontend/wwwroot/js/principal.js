function Login() {
    var cpf = $('#cpf').val();
    $('#msgErroAutenticacao').text('');

    $.ajax({
        url: 'http://localhost:5000/api/Cliente/autenticar/cliente',
        type: 'GET',
        data: { cpf: cpf },
        success: function (response) {

            if (response.sucesso) {
                window.location.href = "/Login/Login"
            }
            else
            {
                $('#msgErroAutenticacao').text(response.mensagem)
                                         .css({
                                             'color': 'red',
                                             'font-size': '12px' 
                                         });
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao tentar realizar login:", error);
            alert("Erro ao tentar realizar login.");
        }
    });
}

function Cadastrar() {
    window.location.href = "/Cadastrar/Cadastrar"
}

function mascaraCPF(input) {
    let value = input.value.replace(/\D/g, '');

    if (value.length > 11) value = value.slice(0, 11);

    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d)/, '$1.$2');
    value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    input.value = value;
}