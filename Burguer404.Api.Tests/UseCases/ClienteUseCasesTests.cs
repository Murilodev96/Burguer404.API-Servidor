using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Gateways;
using Burguer404.Application.UseCases.Cliente;
using Burguer404.Domain.Entities.Cliente;
using Moq;
using Xunit;
using System.Collections.Generic;
using Burguer404.Application.Ports.Gateways;

namespace Burguer404.Api.Tests.UseCases
{
    public class ClienteUseCasesTests
    {
        [Fact]
        public async Task CadastrarCliente_DeveRetornarCliente()
        {
            var request = new ClienteRequest { Nome = "Teste", Email = "teste@teste.com", Cpf = "12345678900" };
            var clienteEntity = ClienteEntity.MapCliente(request);
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.CadastrarClienteAsync(It.IsAny<ClienteEntity>())).ReturnsAsync(clienteEntity);

            var useCase = new CadastrarClienteUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.Nome, result.Nome);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Cpf, result.Cpf);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarNull_EmCasoDeMapeamentoInvalido()
        {
            var request = new ClienteRequest { Nome = "", Email = "", Cpf = "" };
            var gatewayMock = new Mock<IClienteGateway>();
            var useCase = new CadastrarClienteUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);
            Assert.Null(result);
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarLista()
        {
            var clientes = new List<ClienteEntity> { new ClienteEntity { Nome = "Teste" } };
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.ListarClientesAsync()).ReturnsAsync(clientes);
            var useCase = new ListarClientesUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync();
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarCliente_QuandoCpfValidoEClienteExiste()
        {
            var cpf = "12345678900000";
            var cliente = new ClienteEntity { Nome = "Teste", Cpf = cpf };
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(cpf)).ReturnsAsync(cliente);
            var useCase = new LoginClienteUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(cpf);
            Assert.NotNull(result);
            Assert.Equal(cpf, result.Cpf);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarNull_QuandoCpfInvalido()
        {
            var cpf = "";
            var gatewayMock = new Mock<IClienteGateway>();
            var useCase = new LoginClienteUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(cpf);
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarNull_QuandoClienteNaoExiste()
        {
            var cpf = "12345678900000";
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(cpf)).ReturnsAsync((ClienteEntity)null!);
            var useCase = new LoginClienteUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(cpf);
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginClienteAnonimo_DeveRetornarCliente()
        {
            var cliente = new ClienteEntity { Nome = "Anonimo" };
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.CadastrarClienteAnonimoAsync()).ReturnsAsync(cliente);
            var useCase = new LoginClienteAnonimoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync();
            Assert.NotNull(result);
            Assert.Equal("Anonimo", result.Nome);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarSucesso()
        {
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.AlterarStatusClienteAsync(It.IsAny<int>())).ReturnsAsync(true);
            var useCase = new AlterarStatusClienteUseCase(gatewayMock.Object);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(1);
            Assert.True(sucesso);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarFalha_QuandoClienteNaoEncontrado()
        {
            var gatewayMock = new Mock<IClienteGateway>();
            gatewayMock.Setup(g => g.AlterarStatusClienteAsync(It.IsAny<int>())).ReturnsAsync(false);
            var useCase = new AlterarStatusClienteUseCase(gatewayMock.Object);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(1);
            Assert.False(sucesso);
        }
    }
}