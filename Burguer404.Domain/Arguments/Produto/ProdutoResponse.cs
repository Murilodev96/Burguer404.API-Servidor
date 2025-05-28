using Burguer404.Application.Arguments.Base;

namespace Burguer404.Application.Arguments.Produto
{
    public class ProdutoResponse : ArgumentBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] ImagemByte { get; set; }
        public int CategoriaProdutoId { get; set; }
        public bool Status { get; set; }

        public string ImagemBase64{ get; set; }
    }
}
