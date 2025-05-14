using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Domain.Ports.Repositories.Produto
{
    public interface IRepositoryProduto
    {
        Task<ProdutoEntity> CadastrarProduto(ProdutoEntity produto);
        Task<List<ProdutoEntity>> ListarProdutos();
        Task<ProdutoEntity?> AtualizarCadastro(ProdutoEntity produto);
        Task<ProdutoEntity?> ObterProdutoPorId(int produtoId);
        Task RemoverProduto(ProdutoEntity produto);
        Task<ProdutoEntity> VisualizarImagem(int produtoId);
    }
}
