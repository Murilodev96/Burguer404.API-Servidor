using Burguer404.Application.Arguments.Base;


namespace Burguer404.Domain.Arguments.Pedido
{
    public class PagamentoRequest
    {
        public int AcompanhamentoId { get; set; }
        public int BebidaId { get; set; }
        public int LancheId { get; set; }
        public int Quantidade { get; set; }
        public int SobremesaId { get; set; }
        public double Valor { get; set; }
    }
}
