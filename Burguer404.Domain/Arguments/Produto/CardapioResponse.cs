using Burguer404.Application.Arguments.Produto;

namespace Burguer404.Domain.Arguments.Produto
{
    public class CardapioResponse
    {
        public List<ProdutoResponse> Lanches { get; set; } = [];
        public List<ProdutoResponse> Acompanhamentos { get; set; } = [];
        public List<ProdutoResponse> Bebidas { get; set; } = [];
        public List<ProdutoResponse> Sobremesas { get; set; } = [];
    }
}
