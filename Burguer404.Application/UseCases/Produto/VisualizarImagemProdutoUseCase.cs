using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public class VisualizarImagemProdutoUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public VisualizarImagemProdutoUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static VisualizarImagemProdutoUseCase Create(IProdutoGateway produtoGateway)
        {
            return new VisualizarImagemProdutoUseCase(produtoGateway);
        }


        public async Task<ResponseBase<string>> ExecuteAsync(int produtoId)
        {
            var response = new ResponseBase<string>();
            try
            {
                var produto = await _produtoGateway.VisualizarImagemAsync(produtoId);

                if (!(produto is ProdutoEntity) || produto.ImagemByte == null || produto.ImagemByte.Length == 0)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Imagem não encontrada!";
                    response.Resultado = new List<string> { string.Empty };
                    return response;
                }

                response.Sucesso = true;
                response.Mensagem = "Imagem encontrada com sucesso!";
                response.Resultado = new List<string> { Convert.ToBase64String(produto.ImagemByte) };
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
                response.Resultado = new List<string> { string.Empty };
                return response;
            }
        }
    }
}