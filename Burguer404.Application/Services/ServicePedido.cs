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
using Burguer404.Domain.Validators;
using Burguer404.Domain.Validators.Pedido;
using Microsoft.Extensions.Configuration;

namespace Burguer404.Application.Services
{
    public class ServicePedido : IServicePedido
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryPedido _pedidoRepository;
        private readonly IRepositoryProduto _produtoRepository;
        private readonly IRepositoryMercadoPago _mercadoPagoRepository;
        private readonly IConfiguration _configuration;

        public ServicePedido(IMapper mapper, IRepositoryPedido pedidoRepository, IRepositoryProduto produtoRepository, IConfiguration configuration, IRepositoryMercadoPago mercadoPagoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _configuration = configuration;
            _mercadoPagoRepository = mercadoPagoRepository;
        }

        public async Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request)
        {
            var response = new ResponseBase<string>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarPedido.Validar_CadastrarPedido_Request(request);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var entidade = _mapper.Map<PedidoRequest, PedidoEntity>(request);
            entidade = await _pedidoRepository.CriarPedido(entidade);

            validacoes = ValidarPedido.Validar_PedidoIdValido(entidade.Id);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            entidade.PedidoProduto = [.. entidade.ProdutosSelecionados
                                                .GroupBy(produtoId => produtoId)
                                                .Select(grupo => new PedidoProdutoEntity
                                                {
                                                    PedidoId = entidade.Id,
                                                    ProdutoId = grupo.Key,
                                                    Quantidade = grupo.Count()
                                                })];

            await _pedidoRepository.InserirProdutosNoPedido([.. entidade.PedidoProduto]);



            response.Sucesso = true;
            response.Mensagem = "Pedido realizado com sucesso!";
            response.Resultado = [entidade.CodigoPedido];

            return response;
        }

        public async Task<ResponseBase<PedidoResponse>> ListarPedidos(int clienteLogadoId)
        {
            var response = new ResponseBase<PedidoResponse>();

            var pedidos = await _pedidoRepository.ListarPedidos(clienteLogadoId);

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
            response.Resultado = pedidosResponse.OrderByDescending(x => x.Id);

            return response;
        }

        public async Task<ResponseBase<bool>> CancelarPedido(int pedidoId)
        {
            var response = new ResponseBase<bool>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarPedido.Validar_PedidoIdValido(pedidoId);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
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
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarPedido.Validar_CodigoPedidoValido(codigo);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var pedido = await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);

            validacoes = ValidarPedido.Validar_ExistenciaPedido(pedido);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
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
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarPedido.Validar_CodigoPedidoValido(codigo);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var pedido = await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);

            validacoes = ValidarPedido.Validar_ExistenciaPedido(pedido);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            pedido!.StatusPedidoId = ValidarPedido.ValidacoesDeStatusDePedido(pedido.StatusPedidoId);

            var statusAlterado = await _pedidoRepository.AlterarStatusPedido(pedido);

            if (!statusAlterado)
            {
                response.Mensagem = "Ocorreu um erro ao tentar avançar o status do pedido!";
                return response;
            }

            response.Sucesso = true;
            response.Resultado = [true];
            response.Mensagem = $"Status do pedido alterado para {EnumExtensions.GetDisplayNamePorValorEnum<EnumStatusPedido>(pedido.StatusPedidoId)}";

            return response;
        }

        public async Task<ResponseBase<string>> GerarQrCode(List<PagamentoRequest> itens)
        {
            var response = new ResponseBase<string>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarPedido.Validar_GerarQrCode_Request(itens);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

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
                ClienteId = itens.FirstOrDefault()!.ClienteId,
                ProdutosSelecionados = itensPedido
            };

            var codigoPedido = await CadastrarPedido(pedidoRequest);

            if (!string.IsNullOrWhiteSpace(codigoPedido?.Resultado?.FirstOrDefault()))
            {
                try
                {
                    var qrCodeRequest = new QrCodeRequest
                    {
                        description = "Lanchonete Burguer404",
                        total_amount = Math.Round(itens.Sum(x => x.Valor), 2),
                        title = $"Confirmação de pagamento do pedido {codigoPedido?.Resultado?.FirstOrDefault()}",
                        external_reference = codigoPedido.Resultado.FirstOrDefault()!,
                        items = []
                    };

                    foreach (var item in itens)
                    {
                        var tarefas = new List<Task>();

                        var lanche = await _produtoRepository.ObterProdutoPorId(item.LancheId);
                        var acompanhamento = await _produtoRepository.ObterProdutoPorId(item.AcompanhamentoId);
                        var bebida = await _produtoRepository.ObterProdutoPorId(item.BebidaId);
                        var sobremesa = await _produtoRepository.ObterProdutoPorId(item.SobremesaId);

                        var itemProduto = new ItemQrCode()
                        {
                            title = $"Lanche: {lanche?.Nome} - Acompanhamento: {acompanhamento?.Nome} = Bebida: {bebida?.Nome} - Sobremesa: {sobremesa?.Nome}",
                            description = "Combo solicitado via app no Burguer404",
                            quantity = 1,
                            total_amount = Math.Round(itens.Sum(x => x.Valor), 2),
                            unit_price = Math.Round(itens.Sum(x => x.Valor), 2),
                            category = "Lanche",
                            sku_number = "001",
                            unit_measure = "unit"
                        };

                        qrCodeRequest.items.Add(itemProduto);
                    }

                    var (sucesso, qrCode) = await _mercadoPagoRepository.SolicitarQrCodeMercadoPago(qrCodeRequest);

                    if (!sucesso)
                    {
                        response.Mensagem = qrCode;
                        return response;
                    }

                    response.Sucesso = true;
                    response.Mensagem = "QR Code gerado com sucesso!";
                    response.Resultado = [qrCode];

                    return response;
                }
                catch (Exception ex)
                {
                    response.Mensagem = ex.Message;
                    return response;
                }
            }


            return response;
        }

    }
}
