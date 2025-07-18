using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Application.Presenters;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public class ObterProdutosPorCategoriaUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public ObterProdutosPorCategoriaUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static ObterProdutosPorCategoriaUseCase Create(IProdutoGateway produtoGateway)
        {
            return new ObterProdutosPorCategoriaUseCase(produtoGateway);
        }

        public async Task<ResponseBase<ProdutoResponse>> ExecuteAsync(int categoriaId)
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                if (categoriaId <= 0)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Categoria inválida!";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                var produtos = await _produtoGateway.ObterProdutosPorCategoriaIdAsync(categoriaId);
                if (produtos == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Nenhum produto encontrado para a categoria informada.";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                response = ProdutoPresenter.ObterListaProdutoResponse(produtos);
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
                response.Resultado = new List<ProdutoResponse>();
                return response;
            }
        }
    }
}