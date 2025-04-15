function Login() {
    window.location.href = "/Login/Login"
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