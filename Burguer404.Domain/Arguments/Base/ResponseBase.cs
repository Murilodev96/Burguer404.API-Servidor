namespace Burguer404.Domain.Arguments.Base
{
    public class ResponseBase<T>
    {
        public bool Sucesso { get; set; } = false;
        public string Mensagem { get; set; } = string.Empty;
        public IEnumerable<T>? Resultado { get; set; }
    }
}
