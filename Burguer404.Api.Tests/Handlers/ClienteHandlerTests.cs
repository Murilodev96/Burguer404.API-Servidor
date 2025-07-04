using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Api.Tests.Controllers
{
    public class ClienteHandlerTests
    {
        private readonly Mock<IRepositoryCliente> _repositoryMock;
        private readonly ClienteHandler _handler;

        public ClienteHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryCliente>();
            _handler = new ClienteHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarOk()
        {
            var request = new ClienteRequest { Nome = "Teste", Email = "teste@teste.com", Cpf = "12345678900" };
            _repositoryMock.Setup(r => r.ValidarCadastroCliente(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.CadastrarCliente(It.IsAny<ClienteEntity>())).ReturnsAsync(new ClienteEntity());

            var result = await _handler.CadastrarCliente(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ClienteRequest { Nome = "", Email = "", Cpf = "" };
            _repositoryMock.Setup(r => r.ValidarCadastroCliente(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _handler.CadastrarCliente(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseBase<ClienteResponse>>(okResult.Value);
            Assert.False(response.Sucesso);

        }


        [Fact]
        public async Task ListarClientes_DeveRetornarOk()
        {
            _repositoryMock.Setup(r => r.ListarClientes()).ReturnsAsync(new List<ClienteEntity>());

            var result = await _handler.ListarClientes();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ListarClientes_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _repositoryMock.Setup(r => r.ListarClientes()).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _handler.ListarClientes();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarOk()
        {
            var cpf = "12345678900";
            _repositoryMock.Setup(r => r.ObterClientePorCpf(cpf)).ReturnsAsync(new ClienteEntity());

            var result = await _handler.LoginCliente(cpf);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var cpf = "12345678900";
            _repositoryMock.Setup(r => r.ObterClientePorCpf(cpf))
                .ThrowsAsync(new Exception("Erro ao autenticar"));

            var result = await _handler.LoginCliente(cpf);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseBase<ClienteResponse>>(okResult.Value);
            Assert.False(response.Sucesso);
        }




        [Fact]
        public async Task LoginClienteAnonimo_DeveRetornarOk()
        {
            _repositoryMock.Setup(r => r.CadastrarClienteAnonimo()).ReturnsAsync(new ClienteEntity());

            var result = await _handler.LoginClienteAnonimo();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginClienteAnonimo_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _repositoryMock.Setup(r => r.CadastrarClienteAnonimo()).ThrowsAsync(new Exception("Erro ao autenticar"));

            var result = await _handler.LoginClienteAnonimo();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarOk()
        {
            var clienteId = 1;
            _repositoryMock.Setup(r => r.AlterarStatusCliente(clienteId)).ReturnsAsync(true);

            var result = await _handler.AlterarStatusCliente(clienteId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AlterarStatusCliente_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var clienteId = 1;
            _repositoryMock.Setup(r => r.AlterarStatusCliente(clienteId)).ThrowsAsync(new Exception("Erro ao alterar status"));

            var result = await _handler.AlterarStatusCliente(clienteId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
