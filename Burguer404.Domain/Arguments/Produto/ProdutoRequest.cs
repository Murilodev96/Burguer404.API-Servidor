using Burguer404.Application.Arguments.Base;
using Microsoft.AspNetCore.Http;

namespace Burguer404.Application.Arguments.Produto
{
    public class ProdutoRequest : ArgumentBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public IFormFile Imagem { get; set; }
        public int CategoriaProdutoId { get; set; }
        public bool? Status { get; set; }
        public byte[]? ImagemByte { get; set; }
    }
}
