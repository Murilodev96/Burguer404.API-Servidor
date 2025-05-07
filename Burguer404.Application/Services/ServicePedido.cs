using AutoMapper;
using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Enums;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.Ports.Services.Pedido;
using Burguer404.Domain.Utils;

namespace Burguer404.Application.Services
{
    public class ServicePedido : IServicePedido
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryPedido _pedidoRepository;
        private readonly IRepositoryProduto _produtoRepository;

        public ServicePedido(IMapper mapper, IRepositoryPedido pedidoRepository, IRepositoryProduto produtoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request)
        {
            var response = new ResponseBase<string>();

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
            response.Resultado = [entidade.CodigoPedido];

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
                pedido.NomeCliente = pedido.Cliente.Nome;
                pedido.DataFormatada = pedido.DataPedido.ToString("dd/MM/yyyy hh:mm");
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

        public async Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo)
        {
            var response = new ResponseBase<PedidoResponse>();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                response.Mensagem = "Informar um pedido válido!";
                return response;
            }

            var pedido = await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);

            if (pedido == null)
            {
                response.Mensagem = "Pedido não encontrado!";
                return response;
            }

            var resultado = _mapper.Map<PedidoEntity, PedidoResponse>(pedido);

            foreach (var item in pedido.PedidoProduto)
            {
                var produto = await _produtoRepository.ObterProdutoPorId(item.ProdutoId);

                var pedProd = new PedidoProdutoResponse()
                {
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Produto = _mapper.Map<ProdutoEntity, ProdutoResponse>(produto)
                };

                resultado.ProdutosSelecionados.Add(pedProd);
            }

            response.Sucesso = true;
            response.Mensagem = "Pedido obtido com sucesso!";
            response.Resultado = [resultado];

            return response;
        }

        public async Task<ResponseBase<bool>> AvancarStatusPedido(string codigo)
        {
            var response = new ResponseBase<bool>();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                response.Mensagem = "Informar um pedido válido!";
                return response;
            }

            var pedido = await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);

            if (pedido == null)
            {
                response.Mensagem = "Pedido não encontrado!";
                return response;
            }

            pedido.StatusPedidoId = ValidacoesDeStatusDePedido(pedido.StatusPedidoId);

            var statusAlterado = await _pedidoRepository.AlterarStatusPedido(pedido);

            if (!statusAlterado)
            {
                response.Mensagem = "Ocorreu um erro ao tentar avançar o status d o pedido!";
                return response;
            }

            response.Sucesso = true;
            response.Resultado = [true];
            response.Mensagem = $"Status do pedido alterado para {EnumExtensions.GetDisplayNamePorValorEnum<EnumStatusPedido>(pedido.StatusPedidoId)}";

            return response;
        }

        public int ValidacoesDeStatusDePedido(int statusPedidoId)
        {
            if (statusPedidoId == (int)EnumStatusPedido.Finalizado ||
                statusPedidoId == (int)EnumStatusPedido.Cancelado)
            {
                return statusPedidoId;
            }

            return statusPedidoId + 1;
        }

        public async Task<ResponseBase<bool>> gerarQrCode(List<PagamentoRequest> itens)
        {
            if (itens.Count() > 0)
            {
                List<int> itensPedido = new List<int>();
                Parallel.ForEach(itens, item =>
                {
                    if (item.LancheId > 0)
                        itensPedido.Add(item.LancheId);
                    if (item.AcompanhamentoId > 0)
                        itensPedido.Add(item.AcompanhamentoId);
                    if (item.BebidaId > 0)
                        itensPedido.Add(item.BebidaId);
                    if (item.SobremesaId > 0)
                        itensPedido.Add(item.SobremesaId);

                });
                var pedidoRequest = new PedidoRequest()
                {
                    ClienteId = 2,
                    ProdutosSelecionados = itensPedido
                };

                var codigoPedido = await CadastrarPedido(pedidoRequest);

                if (!string.IsNullOrWhiteSpace(codigoPedido.Resultado.FirstOrDefault()))
                {

                }
                // faz chamada da api do Mercado Pago


                var qrCode = new QrCodeRequest
                {
                    Description = "Lanchonete Burguer404",
                };

                return null;

            }
            return null;
        }

    }
}
