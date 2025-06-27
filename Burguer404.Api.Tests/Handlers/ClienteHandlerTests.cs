using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Services.Cliente;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Burguer404.Api.Tests.Controllers
{
    public class ClienteHandlerTests
    {
        private readonly Mock<IServiceCliente> _serviceMock;
        private readonly ClienteController _controller;

        public ClienteHandlerTests()
        {
            _serviceMock = new Mock<IServiceCliente>();
            _controller = new ClienteController(_serviceMock.Object);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarOk()
        {
            var request = new ClienteRequest();
            var response = new ResponseBase<ClienteResponse>();
            _serviceMock.Setup(s => s.CadastrarCliente(request)).ReturnsAsync(response);

            var result = await _controller.CadastrarCliente(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ClienteRequest();
            _serviceMock.Setup(s => s.CadastrarCliente(request)).ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _controller.CadastrarCliente(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarOk()
        {
            var response = new ResponseBase<ClienteResponse>();
            _serviceMock.Setup(s => s.ListarClientes()).ReturnsAsync(response);

            var result = await _controller.ListarClientes();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _serviceMock.Setup(s => s.ListarClientes()).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _controller.ListarClientes();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarOk()
        {
            var cpf = "12345678900";
            var response = new ResponseBase<ClienteResponse>();
            _serviceMock.Setup(s => s.LoginCliente(cpf)).ReturnsAsync(response);

            var result = await _controller.LoginCliente(cpf);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var cpf = "12345678900";
            _serviceMock.Setup(s => s.LoginCliente(cpf)).ThrowsAsync(new Exception("Erro ao autenticar"));

            var result = await _controller.LoginCliente(cpf);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarOk()
        {
            var clienteId = 1;
            var response = new ResponseBase<bool>();
            _serviceMock.Setup(s => s.AlterarStatusCliente(clienteId)).ReturnsAsync(response);

            var result = await _controller.AlterarStatusCliente(clienteId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var clienteId = 1;
            _serviceMock.Setup(s => s.AlterarStatusCliente(clienteId)).ThrowsAsync(new Exception("Erro ao alterar status"));

            var result = await _controller.AlterarStatusCliente(clienteId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
