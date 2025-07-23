using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Interfaces.Gateways;

namespace Burguer404.Application.UseCases.Produto
{
    public class CadastrarProdutoUseCase
    {
        private readonly IProdutoGateway _produtoGateway;

        public CadastrarProdutoUseCase(IProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static CadastrarProdutoUseCase Create(IProdutoGateway produtoGateway)
        {
            return new CadastrarProdutoUseCase(produtoGateway);
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
                    response.Mensagem = "Produto inválido para cadastro!";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                var cadastrado = await _produtoGateway.CadastrarProdutoAsync(produto);
                if (cadastrado == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Ocorreu um erro durante a tentativa de cadastro do produto!";
                    response.Resultado = new List<ProdutoResponse>();
                    return response;
                }
                response = Burguer404.Application.Presenters.ProdutoPresenter.ObterProdutoResponse(cadastrado);
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

        public async Task<byte[]?> ConverterMemoryStream(ProdutoRequest request)
        {
            var memoryStream = new MemoryStream();

            await request.Imagem.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

    }
}