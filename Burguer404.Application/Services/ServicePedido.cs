using AutoMapper;
using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Services.Pedido;

namespace Burguer404.Application.Services
{
    public class ServicePedido : IServicePedido
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryPedido _pedidoRepository;

        public ServicePedido(IMapper mapper, IRepositoryPedido pedidoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ResponseBase<bool>> CadastrarPedido(PedidoRequest request)
        {
            var response = new ResponseBase<bool>();

            if (request.ProdutosSelecionados == null || request.ProdutosSelecionados.Count() <= 0)
            {
                response.Mensagem = "É necessário adicionar ao menos 1 produto para realizar um pedido!";
                return response;
            }

            var entidade = _mapper.Map<PedidoRequest, PedidoEntity>(request);
            entidade = await _pedidoRepository.CriarPedido(entidade);

            if (entidade.Id > 0)
            {
                entidade.PedidoProduto = [.. entidade.ProdutosSelecionados
                                                .GroupBy(produtoId => produtoId)
                                                .Select(grupo => new PedidoProdutoEntity
                                                {
                                                    PedidoId = entidade.Id,
                                                    ProdutoId = grupo.Key,
                                                    Quantidade = grupo.Count()
                                                })];

                await _pedidoRepository.InserirProdutosNoPedido([.. entidade.PedidoProduto]);
            }
            else
            {
                response.Mensagem = "Não foi possível cadastrar o pedido!";
                return response;
            }

            response.Sucesso = true;
            response.Mensagem = "Pedido realizado com sucesso!";
            response.Resultado = [true];

            return response;
        }

        public async Task<ResponseBase<PedidoResponse>> ListarPedidos()
        {
            var response = new ResponseBase<PedidoResponse>();

            var pedidos = await _pedidoRepository.ListarPedidos();

            var pedidosResponse = new List<PedidoResponse>();
            Parallel.ForEach(pedidos, pedido =>
            {
                pedido.StatusPedidoDescricao = pedido.StatusPedido.Descricao;
                pedidosResponse.Add(_mapper.Map<PedidoEntity, PedidoResponse>(pedido));
            });

            response.Sucesso = true;
            response.Mensagem = "Pedidos listados com sucesso!";
            response.Resultado = pedidosResponse;

            return response;
        }

        public async Task<ResponseBase<bool>> CancelarPedido(int pedidoId)
        {
            var response = new ResponseBase<bool>();

            if (pedidoId <= 0)
            {
                response.Mensagem = "Informar um pedido válido!";
                return response;
            }

            var pedidoCancelado = await _pedidoRepository.CancelarPedido(pedidoId);

            if (!pedidoCancelado)
            {
                response.Mensagem = "Pedido não encontrado!";
                return response;
            }

            response.Sucesso = true;
            response.Mensagem = "Pedido cancelado com sucesso!";
            response.Resultado = [true];

            return response;
        }

        public async Task<ResponseBase<PedidoResponse>> VisualizarPedido(int pedidoId)
        {
            var response = new ResponseBase<PedidoResponse>();

            if (pedidoId <= 0)
            {
                response.Mensagem = "Informar um pedido válido!";
                return response;
            }

            var pedido = await _pedidoRepository.ObterPedidoPorId(pedidoId);

            if (pedido == null)
            {
                response.Mensagem = "Pedido não encontrado!";
                return response;
            }

            response.Sucesso = true;
            response.Mensagem = "Pedido cancelado com sucesso!";
            response.Resultado = [_mapper.Map<PedidoEntity, PedidoResponse>(pedido)];

            return response;
        }
    }
}
