using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Interfaces.Gateways;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Burguer404.Api.Tests.Controllers
{
    public class ClienteHandlerTests
    {
        private readonly Mock<IClienteGateway> _gatewayMock;
        private readonly ClienteHandler _handler;
        private readonly IConfiguration _config;

        public ClienteHandlerTests()
        {
            _gatewayMock = new Mock<IClienteGateway>();
            var configsEmMemoria = new Dictionary<string, string> {
                {"Jwt:Key", "Burguer404SecretKeyAutenticate2025@postechFiap!!"},
                {"Jwt:Issuer", "Burguer404"},
                {"Jwt:Audience", "restaurante"}
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(configsEmMemoria!)
                .Build();

            _handler = new ClienteHandler(_gatewayMock.Object, _config);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarOk()
        {
            var request = new ClienteRequest { Nome = "Teste", Email = "teste@teste.com", Cpf = "12345678900" };
            _gatewayMock.Setup(r => r.ValidarCadastroClienteAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            _gatewayMock.Setup(r => r.CadastrarClienteAsync(It.IsAny<ClienteEntity>())).ReturnsAsync(new ClienteEntity());

            var result = await _handler.CadastrarCliente(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ClienteRequest { Nome = "", Email = "", Cpf = "" };
            _gatewayMock.Setup(r => r.ValidarCadastroClienteAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _handler.CadastrarCliente(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseBase<ClienteResponse>>(okResult.Value);
            Assert.False(response.Sucesso);

        }


        [Fact]
        public async Task ListarClientes_DeveRetornarOk()
        {
            _gatewayMock.Setup(r => r.ListarClientesAsync()).ReturnsAsync(new List<ClienteEntity>());

            var result = await _handler.ListarClientes();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _gatewayMock.Setup(r => r.ListarClientesAsync()).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _handler.ListarClientes();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarOk()
        {
            var cpf = "111.111.111-11";
            _gatewayMock.Setup(r => r.ObterClientePorCpfAsync(cpf)).ReturnsAsync(new ClienteEntity());

            var result = await _handler.LoginCliente(cpf);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarUnauthorized_EmCasoDeErro()
        {
            var cpf = "12345678900";
            _gatewayMock.Setup(r => r.ObterClientePorCpfAsync(cpf))
                .ThrowsAsync(new Exception("Erro ao autenticar"));

            var result = await _handler.LoginCliente(cpf);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var response = Assert.IsType<ResponseBase<ClienteResponse>>(unauthorizedResult.Value);

            Assert.False(response.Sucesso);
        }


        [Fact]
        public async Task LoginClienteAnonimo_DeveRetornarOk()
        {
            _gatewayMock.Setup(r => r.CadastrarClienteAnonimoAsync()).ReturnsAsync(new ClienteEntity());

            var result = await _handler.LoginClienteAnonimo();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginClienteAnonimo_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _gatewayMock.Setup(r => r.CadastrarClienteAnonimoAsync()).ThrowsAsync(new Exception("Erro ao autenticar"));

            var result = await _handler.LoginClienteAnonimo();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarOk()
        {
            var clienteId = 1;
            _gatewayMock.Setup(r => r.AlterarStatusClienteAsync(clienteId)).ReturnsAsync(true);

            var result = await _handler.AlterarStatusCliente(clienteId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var clienteId = 1;
            _gatewayMock.Setup(r => r.AlterarStatusClienteAsync(clienteId)).ThrowsAsync(new Exception("Erro ao alterar status"));

            var result = await _handler.AlterarStatusCliente(clienteId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
