using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Domain.Ports.Services.Produto
{
    public interface IServiceProduto
    {
        Task<ResponseBase<ProdutoResponse>> CadastrarProduto(ProdutoRequest request);
        Task<ResponseBase<ProdutoResponse>> ListarProdutos();
        Task<ResponseBase<ProdutoResponse>> AtualizarProduto(ProdutoRequest request);
        Task<ResponseBase<bool>> RemoverProduto(int produtoId);
    }
}
