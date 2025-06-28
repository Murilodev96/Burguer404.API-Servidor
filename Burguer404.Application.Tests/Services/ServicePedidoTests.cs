using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.Arguments.Pedido;

namespace Burguer404.Application.Tests.Services
{
    public class ServicePedidoTests
    {
        //private readonly Mock<IMapper> _mapperMock;
        //private readonly Mock<IRepositoryPedido> _pedidoRepositoryMock;
        //private readonly Mock<IRepositoryProduto> _produtoRepositoryMock;
        //private readonly Mock<IRepositoryMercadoPago> _mercadoPagoRepositoryMock;
        //private readonly Mock<IConfiguration> _configurationMock;
        //private readonly ServicePedido _service;

        //public ServicePedidoTests()
        //{
        //    _mapperMock = new Mock<IMapper>();
        //    _pedidoRepositoryMock = new Mock<IRepositoryPedido>();
        //    _produtoRepositoryMock = new Mock<IRepositoryProduto>();
        //    _mercadoPagoRepositoryMock = new Mock<IRepositoryMercadoPago>();
        //    _configurationMock = new Mock<IConfiguration>();

        //    _service = new ServicePedido(
        //        _mapperMock.Object,
        //        _pedidoRepositoryMock.Object,
        //        _produtoRepositoryMock.Object,
        //        _configurationMock.Object,
        //        _mercadoPagoRepositoryMock.Object
        //    );
        //}

        //[Fact]
        //public async Task CadastrarPedido_DeveRetornarErro_SeProdutosVazios()
        //{
        //    var request = new PedidoRequest { ClienteId = 1, ProdutosSelecionados = new List<int>() };

        //    var response = await _service.CadastrarPedido(request);

        //    Assert.False(response.Sucesso);
        //    Assert.Contains("produto", response.Mensagem, StringComparison.OrdinalIgnoreCase);
        //}


        //[Fact]
        //public async Task CancelarPedido_DeveRetornarSucesso()
        //{
        //    _pedidoRepositoryMock.Setup(r => r.CancelarPedido(It.IsAny<int>()))
        //        .ReturnsAsync(true);

        //    var response = await _service.CancelarPedido(1);

        //    Assert.True(response.Sucesso);
        //}


        //[Fact]
        //public async Task AvancarStatusPedido_DeveRetornarSucesso()
        //{
        //    var pedido = new PedidoEntity { CodigoPedido = "XYZ", ClienteId = 1, PedidoProduto = new List<PedidoProdutoEntity>() };
        //    _pedidoRepositoryMock.Setup(r => r.ObterPedidoPorCodigoPedido("XYZ"))
        //        .ReturnsAsync(pedido);
        //    _pedidoRepositoryMock.Setup(r => r.AlterarStatusPedido(It.IsAny<PedidoEntity>()))
        //        .ReturnsAsync(true);

        //    var response = await _service.AvancarStatusPedido("XYZ");

        //    Assert.True(response.Sucesso);
        //}

        //[Fact]
        //public async Task GerarQrCode_DeveRetornarErro_SeListaVazia()
        //{
        //    var response = await _service.GerarQrCode(new List<PagamentoRequest>());

        //    Assert.False(response.Sucesso);
        //    Assert.Contains("item", response.Mensagem, StringComparison.OrdinalIgnoreCase);
        //}

    }
}
