using AutoMapper;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Enums;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.Ports.Services.Produto;
using Burguer404.Domain.Validators;
using Burguer404.Domain.Validators.Pedido;
using Burguer404.Domain.Validators.Produto;

namespace Burguer404.Application.Services
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryProduto _produtoRepository;

        public ServiceProduto(IMapper mapper, IRepositoryProduto produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task<ResponseBase<ProdutoResponse>> CadastrarProduto(ProdutoRequest request)
        {
            var response = new ResponseBase<ProdutoResponse>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarProduto.Validar_CadastrarProduto_Request(request);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var entidade = _mapper.Map<ProdutoRequest, ProdutoEntity>(request);
            entidade = await _produtoRepository.CadastrarProduto(entidade);

            response.Resultado = [_mapper.Map<ProdutoEntity, ProdutoResponse>(entidade)];
            response.Sucesso = true;
            response.Mensagem = "Produto cadastrado com sucesso!";

            return response;
        }

        public async Task<ResponseBase<ProdutoResponse>> ListarProdutos()
        {
            var response = new ResponseBase<ProdutoResponse>();
            var produtos = await _produtoRepository.ListarProdutos();

            var produtosResponse = new List<ProdutoResponse>();
            Parallel.ForEach(produtos, cliente => { produtosResponse.Add(_mapper.Map<ProdutoEntity, ProdutoResponse>(cliente)); });

            response.Resultado = produtosResponse;
            response.Sucesso = true;
            response.Mensagem = "Listagem de produtos realizadas com sucesso!";

            return response;
        }

        public async Task<ResponseBase<ProdutoResponse>> AtualizarProduto(ProdutoRequest request)
        {
            var response = new ResponseBase<ProdutoResponse>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarProduto.Validar_CadastrarProduto_Request(request);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var entidade = _mapper.Map<ProdutoRequest, ProdutoEntity>(request);
            entidade = await _produtoRepository.AtualizarCadastro(entidade);

            validacoes = ValidarProduto.Validar_ExistenciaProduto(entidade);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            response.Resultado = [_mapper.Map<ProdutoEntity, ProdutoResponse>(entidade)];
            response.Sucesso = true;
            response.Mensagem = "Produto cadastrado com sucesso!";

            return response;
        }

        public async Task<ResponseBase<bool>> RemoverProduto(int produtoId)
        {
            var response = new ResponseBase<bool>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarProduto.Validar_ProdutoIdValido(produtoId);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);

            validacoes = ValidarProduto.Validar_ExistenciaProduto(produto);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            await _produtoRepository.RemoverProduto(produto);

            response.Sucesso = true;
            response.Mensagem = "Produto removido com sucesso!";

            return response;
        }

        public async Task<ResponseBase<CardapioResponse>> ObterCardapio()
        {
            var response = new ResponseBase<CardapioResponse>();
            var itensCardapio = new List<ProdutoResponse>();

            var produtos = await ListarProdutos();

            if (produtos.Sucesso && produtos.Resultado != null && produtos.Resultado.Any())
                itensCardapio = [.. produtos.Resultado!];
            else 
            {
                response.Mensagem = "Nenhum produto encontrado!";
                return response;
            }

            var cardapio = new CardapioResponse() {
                Lanches = [.. itensCardapio.Where(x => x.CategoriaPedidoId == (int)EnumCategoriaPedido.Lanche)],
                Acompanhamentos = [.. itensCardapio.Where(x => x.CategoriaPedidoId == (int)EnumCategoriaPedido.Acompanhamento)],
                Bebidas = [.. itensCardapio.Where(x => x.CategoriaPedidoId == (int)EnumCategoriaPedido.Bebida)],
                Sobremesas = [.. itensCardapio.Where(x => x.CategoriaPedidoId == (int)EnumCategoriaPedido.Sobremesa)],
            };

            response.Sucesso = true;
            response.Mensagem = "Produtos listados com sucesso!";
            response.Resultado = [cardapio];

            return response;
        }
    }
}
