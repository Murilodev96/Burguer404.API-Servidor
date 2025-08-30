function Login() {
    var cpf = $('#cpf').val();
    $('#msgErroAutenticacao').text('');
    const apiUrl = window.API_URL;


    $.ajax({
        url: `${apiUrl}/api/ClienteHandler/autenticar/cliente`,
        type: 'GET',
        data: { cpf: cpf },
        success: function (response) {

            if (response.sucesso) {
                sessionStorage.setItem("clienteLogadoId", response.resultado[0].id);
                sessionStorage.setItem("perfilClienteId", response.resultado[0].perfilClienteId);
                sessionStorage.setItem("nomeClienteLogado", response.resultado[0].nome);
                sessionStorage.setItem("emailClienteLogado", response.resultado[0].email);
                sessionStorage.setItem("access_token", response.token);
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

function LoginAnonimo() {
    const apiUrl = window.API_URL;

    $.ajax({
        url: `${apiUrl}/api/ClienteHandler/autenticar/anonimo`,
        type: 'GET',
        success: function (response) {

            if (response.sucesso) {
                sessionStorage.setItem("clienteLogadoId", response.resultado[0].id);
                sessionStorage.setItem("perfilClienteId", response.resultado[0].perfilClienteId);
                sessionStorage.setItem("nomeClienteLogado", response.resultado[0].nome);
                sessionStorage.setItem("emailClienteLogado", response.resultado[0].email);
                window.location.href = "/Login/Login"
            }
            else {
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