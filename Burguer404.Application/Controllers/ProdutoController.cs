using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Application.Controllers
{
    public class ProdutoController
    {
        private IRepositoryProduto _repository;

        public ProdutoController(IRepositoryProduto repository)
        {
            _repository = repository;
        }

        public async Task<ResponseBase<ProdutoResponse>> CadastrarProduto(ProdutoRequest request) 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = CadastrarProdutoUseCase.Create(produtoGateway);

            var produto = await useCase.ExecuteAsync(request);

            if (produto == null)
            {
                return new ResponseBase<ProdutoResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante a tentativa de cadastro do produto!", Resultado = [] };
            }

            return ProdutoPresenter.ObterProdutoResponse(produto);
        }

        public async Task<ResponseBase<ProdutoResponse>> ListarProdutos() 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = ListarProdutosUseCase.Create(produtoGateway);

            var produtos = await useCase.ExecuteAsync();

            if (produtos == null)
            {
                return new ResponseBase<ProdutoResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante a listagem dos produtos!", Resultado = [] };
            }

            return ProdutoPresenter.ObterListaProdutoResponse(produtos);
        }

        public async Task<ResponseBase<ProdutoResponse>> AtualizarProduto(ProdutoRequest request) 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = AtualizarProdutoUseCase.Create(produtoGateway);

            var produto = await useCase.ExecuteAsync(request);

            if (produto == null)
            {
                return new ResponseBase<ProdutoResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante a tentativa de atualização do produto!", Resultado = [] };
            }

            return ProdutoPresenter.ObterProdutoResponse(produto);
        }

        public async Task<ResponseBase<bool>> RemoverProduto(int produtoId) 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = RemoverProdutoUseCase.Create(produtoGateway);

            var (sucesso, mensagem) = await useCase.ExecuteAsync(produtoId);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<CardapioResponse>> ObterCardapio() 
        {
            var produtos = await ListarProdutos();

            if (!produtos.Sucesso)
                return new ResponseBase<CardapioResponse>();

            var itensCardapio = new List<ProdutoResponse>();
            itensCardapio = [.. produtos.Resultado!];

            return ProdutoPresenter.ObterCardapioResponse(itensCardapio);
        }

        public async Task<ResponseBase<string>> VisualizarImagem(int produtoId) 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = VisualizarImagemProdutoUseCase.Create(produtoGateway);

            var (sucesso, mensagem, imagem) = await useCase.ExecuteAsync(produtoId);

            return new ResponseBase<string>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [imagem] };
        }

        public async Task<ResponseBase<ProdutoResponse>> ObterProdutosPorCategoria(int categoriaId) 
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = ObterProdutosPorCategoriaUseCase.Create(produtoGateway);

            var itens = await useCase.ExecuteAsync(categoriaId);

            if (itens == null)
            {
                return new ResponseBase<ProdutoResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro na listagem dos produtos por categoria!", Resultado = [] };
            }

            return ProdutoPresenter.ObterListaProdutoResponse(itens);
        }
    }
}
