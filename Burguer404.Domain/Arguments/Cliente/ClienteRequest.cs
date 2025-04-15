using Burguer404.Application.Arguments.Base;

namespace Burguer404.Application.Arguments.Cliente
{
    public class ClienteRequest : ArgumentBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool? Status { get; set; }
    }
}
