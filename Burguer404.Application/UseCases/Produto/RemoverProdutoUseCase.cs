using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Application.UseCases.Produto
{
    public class RemoverProdutoUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public RemoverProdutoUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static RemoverProdutoUseCase Create(IProdutoGateway produtoGateway)
        {
            return new RemoverProdutoUseCase(produtoGateway);
        }

        public async Task<ResponseBase<bool>> ExecuteAsync(int produtoId)
        {
            var response = new ResponseBase<bool>();
            try
            {
                var produto = await _produtoGateway.ObterProdutoPorIdAsync(produtoId);

                if (!(produto is ProdutoEntity))
                {
                    response.Sucesso = false;
                    response.Mensagem = "Produto não encontrado!";
                    response.Resultado = new List<bool> { false };
                    return response;
                }

                await _produtoGateway.RemoverProdutoAsync(produto!);
                response.Sucesso = true;
                response.Mensagem = "Produto removido com sucesso!";
                response.Resultado = new List<bool> { true };
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
                response.Resultado = new List<bool> { false };
                return response;
            }
        }
    }
}