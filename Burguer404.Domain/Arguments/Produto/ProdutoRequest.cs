using Burguer404.Application.Arguments.Base;

namespace Burguer404.Application.Arguments.Produto
{
    public class ProdutoRequest : ArgumentBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Imagem { get; set; }
        public int CategoriaPedidoId { get; set; }
        public bool? Status { get; set; }
    }
}
