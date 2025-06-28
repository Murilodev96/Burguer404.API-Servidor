using AutoMapper;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Moq;

namespace Burguer404.Application.Tests.Services
{
    public class ServiceClienteTests
    {
        //private readonly Mock<IMapper> _mapperMock;
        //private readonly Mock<IRepositoryCliente> _clienteRepositoryMock;
        //private readonly ServiceCliente _service;

        //public ServiceClienteTests()
        //{
        //    _mapperMock = new Mock<IMapper>();
        //    _clienteRepositoryMock = new Mock<IRepositoryCliente>();
        //    _service = new ServiceCliente(_mapperMock.Object, _clienteRepositoryMock.Object);
        //}

        //[Fact]
        //public async Task CadastrarCliente_DeveRetornarErro_SeValidacaoFalhar()
        //{
        //    var request = new ClienteRequest(); 
        //    var response = await _service.CadastrarCliente(request);

        //    Assert.False(response.Sucesso);
        //    Assert.NotNull(response.Mensagem);
        //}

        //[Fact]
        //public async Task CadastrarCliente_DeveRetornarErro_SeCpfJaCadastrado()
        //{
        //    var request = new ClienteRequest { Nome = "Teste", Email = "teste@teste.com", Cpf = "123" };
        //    var entity = new ClienteEntity { Cpf = "123", Email = "teste@teste.com" };

        //    _mapperMock.Setup(m => m.Map<ClienteRequest, ClienteEntity>(It.IsAny<ClienteRequest>())).Returns(entity);
        //    _clienteRepositoryMock.Setup(r => r.ValidarCadastroCliente(entity.Cpf, entity.Email)).ReturnsAsync(true);

        //    var response = await _service.CadastrarCliente(request);

        //    Assert.False(response.Sucesso);
        //    Assert.Equal("CPF já cadastrado como cliente!", response.Mensagem);
        //}

        //[Fact]
        //public async Task CadastrarCliente_DeveRetornarSucesso()
        //{
        //    var request = new ClienteRequest { Nome = "Teste", Email = "teste@teste.com", Cpf = "123" };
        //    var entity = new ClienteEntity { Cpf = "123", Email = "teste@teste.com" };
        //    var responseEntity = new ClienteEntity { Cpf = "123", Email = "teste@teste.com", Status = true };
        //    var responseDto = new ClienteResponse { Cpf = "123", Email = "teste@teste.com" };

        //    _mapperMock.Setup(m => m.Map<ClienteRequest, ClienteEntity>(It.IsAny<ClienteRequest>())).Returns(entity);
        //    _clienteRepositoryMock.Setup(r => r.ValidarCadastroCliente(entity.Cpf, entity.Email)).ReturnsAsync(false);
        //    _clienteRepositoryMock.Setup(r => r.CadastrarCliente(It.IsAny<ClienteEntity>())).ReturnsAsync(responseEntity);
        //    _mapperMock.Setup(m => m.Map<ClienteEntity, ClienteResponse>(It.IsAny<ClienteEntity>())).Returns(responseDto);

        //    var response = await _service.CadastrarCliente(request);

        //    Assert.True(response.Sucesso);
        //    Assert.Equal("Cliente cadastrado com sucesso!", response.Mensagem);
        //    Assert.NotNull(response.Resultado);
        //}

        //[Fact]
        //public async Task ListarClientes_DeveRetornarClientes()
        //{
        //    var clientes = new List<ClienteEntity> { new ClienteEntity() };
        //    var clienteResponse = new ClienteResponse();

        //    _clienteRepositoryMock.Setup(r => r.ListarClientes()).ReturnsAsync(clientes);
        //    _mapperMock.Setup(m => m.Map<ClienteEntity, ClienteResponse>(It.IsAny<ClienteEntity>())).Returns(clienteResponse);

        //    var response = await _service.ListarClientes();

        //    Assert.True(response.Sucesso);
        //    Assert.NotNull(response.Resultado);
        //    Assert.Single(response.Resultado);
        //}

        //[Fact]
        //public async Task LoginCliente_DeveRetornarErro_SeValidacaoFalhar()
        //{
        //    var response = await _service.LoginCliente(null);

        //    Assert.False(response.Sucesso);
        //    Assert.NotNull(response.Mensagem);
        //}

        //[Fact]
        //public async Task LoginCliente_DeveRetornarErro_SeClienteNaoEncontrado()
        //{
        //    _clienteRepositoryMock.Setup(r => r.ObterClientePorCpf(It.IsAny<string>())).ReturnsAsync((ClienteEntity)null);

        //    var response = await _service.LoginCliente("123");

        //    Assert.False(response.Sucesso);
        //    Assert.NotNull(response.Mensagem);
        //}

        //[Fact]
        //public async Task LoginCliente_DeveRetornarSucesso()
        //{
        //    // Arrange
        //    var cliente = new ClienteEntity
        //    {
        //        Cpf = "855.369.630-82",
        //        Nome = "Teste",
        //        Email = "teste@teste.com",
        //        Status = true,
        //        PerfilClienteId = 1,
        //        PerfilCliente = new PerfilClienteEntity { Id = 1}, 
        //        Pedidos = new List<PedidoEntity>() 
        //    };
        //    var clienteResponse = new ClienteResponse
        //    {
        //        Cpf = "855.369.630-82",
        //        Nome = "Teste",
        //        Email = "teste@teste.com",
        //        Status = true,
        //        PerfilClienteId = 1
        //    };

        //    _clienteRepositoryMock.Setup(r => r.ObterClientePorCpf("855.369.630-82")).ReturnsAsync(cliente);
        //    _mapperMock.Setup(m => m.Map<ClienteEntity, ClienteResponse>(It.IsAny<ClienteEntity>())).Returns(clienteResponse);

        //    // Act
        //    var response = await _service.LoginCliente("855.369.630-82");

        //    // Assert
        //    Assert.True(response.Sucesso);
        //    Assert.Equal("Login realizado com sucesso!", response.Mensagem);
        //    Assert.NotNull(response.Resultado);
        //    Assert.Equal("855.369.630-82", response.Resultado.First().Cpf);
        //}



        //[Fact]
        //public async Task AlterarStatusCliente_DeveRetornarErro_SeValidacaoFalhar()
        //{
        //    var response = await _service.AlterarStatusCliente(0);

        //    Assert.False(response.Sucesso);
        //    Assert.NotNull(response.Mensagem);
        //}

        //[Fact]
        //public async Task AlterarStatusCliente_DeveRetornarErro_SeClienteNaoEncontrado()
        //{
        //    _clienteRepositoryMock.Setup(r => r.AlterarStatusCliente(It.IsAny<int>())).ReturnsAsync(false);

        //    var response = await _service.AlterarStatusCliente(1);

        //    Assert.False(response.Sucesso);
        //    Assert.Equal("Cliente informado não encontrado!", response.Mensagem);
        //}

        //[Fact]
        //public async Task AlterarStatusCliente_DeveRetornarSucesso()
        //{
        //    _clienteRepositoryMock.Setup(r => r.AlterarStatusCliente(It.IsAny<int>())).ReturnsAsync(true);

        //    var response = await _service.AlterarStatusCliente(1);

        //    Assert.True(response.Sucesso);
        //    Assert.Equal("Status do cliente alterado com sucesso!", response.Mensagem);
        //}
    }
}
