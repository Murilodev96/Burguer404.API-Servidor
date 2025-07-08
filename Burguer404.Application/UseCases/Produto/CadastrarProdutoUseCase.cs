using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;
using System.Reflection.Metadata.Ecma335;

namespace Burguer404.Application.UseCases.Produto
{
    public class CadastrarProdutoUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public CadastrarProdutoUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static CadastrarProdutoUseCase Create(ProdutoGateway produtoGateway)
        {
            return new CadastrarProdutoUseCase(produtoGateway);
        }

        public async Task<ProdutoEntity?> ExecuteAsync(ProdutoRequest request)
        {
            var produto = ProdutoEntity.MapProduto(request);

            if (!(produto is ProdutoEntity))
                return null;

            return await _produtoGateway.CadastrarProdutoAsync(produto);
        }

        public async Task<byte[]?> ConverterMemoryStream(ProdutoRequest request)
        {
            var memoryStream = new MemoryStream();

            await request.Imagem.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

    }
}