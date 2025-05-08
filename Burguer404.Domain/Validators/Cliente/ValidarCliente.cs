using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Domain.Validators.Cliente
{
    public static class ValidarCliente
    {
        public static ResponseBaseValidacoes Validar_CadastroCliente_Request(ClienteRequest request)
        {
            var ok = new ResponseBaseValidacoes();

            if (string.IsNullOrEmpty(request.Cpf) && string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Nome))
            {
                ok.Mensagem = "Todos os dados são obrigatórios para cadastro!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_LoginCliente_Request(string cpf)
        {
            var ok = new ResponseBaseValidacoes();

            if (string.IsNullOrEmpty(cpf) || cpf.Length != 14)
            {
                ok.Mensagem = "Obrigatório informar o CPF completo (informar caracteres especiais)";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_ExistenciaCliente(ClienteEntity? cliente, string cpf)
        {
            var ok = new ResponseBaseValidacoes();

            if (cliente == null)
            {
                ok.Mensagem = $"Nenhum cadastro encontrado para o CPF {cpf}";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_AlterarStatusCliente_Request(int clienteId)
        {
            var ok = new ResponseBaseValidacoes();

            if (clienteId <= 0)
            {
                ok.Mensagem = "É necessário informar um cliente!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }
    }
}
