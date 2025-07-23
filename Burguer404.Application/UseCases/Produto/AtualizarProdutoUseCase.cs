using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Interfaces.Gateways;

namespace Burguer404.Application.UseCases.Produto
{
    public class AtualizarProdutoUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public AtualizarProdutoUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static AtualizarProdutoUseCase Create(IProdutoGateway produtoGateway)
        {
            return new AtualizarProdutoUseCase(produtoGateway);
        }

        public async Task<ResponseBase<ProdutoResponse>> ExecuteAsync(ProdutoRequest request)
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                var produto = ProdutoEntity.MapProduto(request);
                if (!(produto is ProdutoEntity))
                {
                    response.Sucesso = false;
                    response.Mensagem = "Produto inválido para atualização!";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                var atualizado = await _produtoGateway.AtualizarProdutoAsync(produto);
                if (atualizado == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Ocorreu um erro durante a tentativa de atualização do produto!";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                response = Burguer404.Application.Presenters.ProdutoPresenter.ObterProdutoResponse(atualizado);
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