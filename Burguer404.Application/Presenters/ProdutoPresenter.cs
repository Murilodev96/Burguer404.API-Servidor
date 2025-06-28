using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Enums;

namespace Burguer404.Application.Presenters
{
    public static class ProdutoPresenter
    {
        public static ResponseBase<ProdutoResponse> ObterProdutoResponse(ProdutoEntity produto) 
        {
            var response = new ResponseBase<ProdutoResponse>()
            {
                Sucesso = true,
                Mensagem = "Sucesso!",
                Resultado = [new ProdutoResponse {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ImagemByte = produto.ImagemByte ?? [],
                    ImagemBase64 = produto.ImagemBase64,
                    Status = produto.Status
                }]
            };

            return response;
        }

        public static ResponseBase<ProdutoResponse> ObterListaProdutoResponse(List<ProdutoEntity> produtos)
        {
            var listaProdutoMap = new List<ProdutoResponse>();

            foreach (var produto in produtos)
            {
                var produtoMap = new ProdutoResponse()
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ImagemByte = produto.ImagemByte ?? [],
                    ImagemBase64 = produto.ImagemBase64,
                    Status = produto.Status,
                    CategoriaProdutoId = produto.CategoriaProdutoId
                };

                listaProdutoMap.Add(produtoMap);
            }

            var response = new ResponseBase<ProdutoResponse>()
            {
                Sucesso = true,
                Mensagem = "Sucesso!",
                Resultado = listaProdutoMap
            };

            return response;
        }

        public static ResponseBase<CardapioResponse> ObterCardapioResponse(List<ProdutoResponse> itensCardapio) 
        {
            var cardapio = new CardapioResponse()
            {
                Lanches = [.. itensCardapio.Where(x => x.CategoriaProdutoId == (int)EnumCategoriaPedido.Lanche)],
                Acompanhamentos = [.. itensCardapio.Where(x => x.CategoriaProdutoId == (int)EnumCategoriaPedido.Acompanhamento)],
                Bebidas = [.. itensCardapio.Where(x => x.CategoriaProdutoId == (int)EnumCategoriaPedido.Bebida)],
                Sobremesas = [.. itensCardapio.Where(x => x.CategoriaProdutoId == (int)EnumCategoriaPedido.Sobremesa)],
            };

            var response = new ResponseBase<CardapioResponse>()
            {
                Sucesso = true,
                Mensagem = "Produtos listados com sucesso",
                Resultado = [cardapio]
            };

            return response;
        }
    }
}
