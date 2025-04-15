using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Infrastructure.Data.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace Burguer404.Infrastructure.Data.Repositories.Produto
{
    public class RepositoryProduto : IRepositoryProduto
    {
        private readonly Context _context;

        public RepositoryProduto(Context context)
        {
            _context = context;
        }

        public async Task<ProdutoEntity> CadastrarProduto(ProdutoEntity produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<List<ProdutoEntity>> ListarProdutos()
            => await _context.Produtos.ToListAsync();

        public async Task<ProdutoEntity?> AtualizarCadastro(ProdutoEntity produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<ProdutoEntity?> ObterProdutoPorId(int produtoId)
            => await _context.Produtos.Where(x => x.Id == produtoId).FirstOrDefaultAsync();

        public async Task RemoverProduto(ProdutoEntity produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
