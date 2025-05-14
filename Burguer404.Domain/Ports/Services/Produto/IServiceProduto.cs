using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using System.Buffers.Text;

namespace Burguer404.Domain.Ports.Services.Produto
{
    public interface IServiceProduto
    {
        Task<ResponseBase<ProdutoResponse>> CadastrarProduto(ProdutoRequest request);
        Task<ResponseBase<ProdutoResponse>> ListarProdutos();
        Task<ResponseBase<ProdutoResponse>> AtualizarProduto(ProdutoRequest request);
        Task<ResponseBase<bool>> RemoverProduto(int produtoId);
        Task<ResponseBase<CardapioResponse>> ObterCardapio();
        Task<ResponseBase<string>> VisualizarImagem(int produtoId);
    }
}
