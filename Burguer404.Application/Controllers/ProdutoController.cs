using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;
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
            request.ImagemByte = await useCase.ConverterMemoryStream(request);
            var response = await useCase.ExecuteAsync(request);
            return response;
        }

        public async Task<ResponseBase<ProdutoResponse>> ListarProdutos()
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = ListarProdutosUseCase.Create(produtoGateway);

            var response = await useCase.ExecuteAsync();
            var produtos = response.Resultado;

            useCase.ConverterBase64(produtos.ToList());

            if (produtos == null)
            {
                return new ResponseBase<ProdutoResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante a listagem dos produtos!", Resultado = new List<ProdutoResponse>() };
            }

            // Conversão explícita de ProdutoResponse para ProdutoEntity
            var produtosEntity = produtos.Select(x => new ProdutoEntity
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                Preco = x.Preco,
                ImagemByte = x.ImagemByte,
                ImagemBase64 = x.ImagemBase64,
                Status = x.Status,
                CategoriaProdutoId = x.CategoriaProdutoId
            }).ToList();

            return ProdutoPresenter.ObterListaProdutoResponse(produtosEntity);
        }

        public async Task<ResponseBase<ProdutoResponse>> AtualizarProduto(ProdutoRequest request)
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = AtualizarProdutoUseCase.Create(produtoGateway);

            var response = await useCase.ExecuteAsync(request);
            return response;
        }

        public async Task<ResponseBase<bool>> RemoverProduto(int produtoId)
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = RemoverProdutoUseCase.Create(produtoGateway);

            var response = await useCase.ExecuteAsync(produtoId);
            return response;
        }

        public async Task<ResponseBase<CardapioResponse>> ObterCardapio()
        {
            var produtos = await ListarProdutos();

            if (!produtos.Sucesso)
                return new ResponseBase<CardapioResponse>();

            var itensCardapio = new List<ProdutoResponse>(produtos.Resultado!);

            return ProdutoPresenter.ObterCardapioResponse(itensCardapio);
        }

        public async Task<ResponseBase<string>> VisualizarImagem(int produtoId)
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = VisualizarImagemProdutoUseCase.Create(produtoGateway);

            var response = await useCase.ExecuteAsync(produtoId);
            return response;
        }

        public async Task<ResponseBase<ProdutoResponse>> ObterProdutosPorCategoria(int categoriaId)
        {
            var produtoGateway = new ProdutoGateway(_repository);
            var useCase = ObterProdutosPorCategoriaUseCase.Create(produtoGateway);

            var response = await useCase.ExecuteAsync(categoriaId);
            return response;
        }
    }
}
