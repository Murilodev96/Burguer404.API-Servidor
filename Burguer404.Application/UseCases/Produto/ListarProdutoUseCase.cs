using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Application.UseCases.Produto
{
    public class ListarProdutosUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public ListarProdutosUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static ListarProdutosUseCase Create(IProdutoGateway produtoGateway)
        {
            return new ListarProdutosUseCase(produtoGateway);
        }

        public void ConverterBase64(List<ProdutoResponse> produtos)
        {
            Parallel.ForEach(produtos, produto =>
            {
                if (produto.ImagemByte != null)
                    produto.ImagemBase64 = "data:image/png;base64," + Convert.ToBase64String(produto.ImagemByte);
            });
        }

        public async Task<ResponseBase<ProdutoResponse>> ExecuteAsync()
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                var produtos = await _produtoGateway.ListarProdutosAsync();
                var produtosResponse = produtos.Select(p => new ProdutoResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    CategoriaProdutoId = p.CategoriaProdutoId,
                    ImagemBase64 = p.ImagemBase64
                }).ToList();
                response.Sucesso = true;
                response.Resultado = produtosResponse;
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }
    }
}