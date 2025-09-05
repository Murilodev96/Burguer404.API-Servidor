using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Interfaces.Gateways;
using Microsoft.AspNetCore.Mvc;


namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteHandler : Controller
    {
        private ClienteController _clienteController;
        private AutenticacaoController _autenticacao;

        public ClienteHandler(IClienteGateway clienteGateway, IConfiguration _config)
        {
            _clienteController = new ClienteController(clienteGateway, _config);
            _autenticacao = new AutenticacaoController(_config);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarCliente(ClienteRequest request)
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.CadastrarCliente(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("listar")]
        public async Task<ActionResult> ListarClientes()
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.ListarClientes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("autenticar/cliente")]
        public async Task<ActionResult> LoginCliente(string cpf)
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.LoginCliente(cpf);

                if (!response.Sucesso)
                    return Unauthorized(response);

                var token = _autenticacao.GerarJwt(response.Resultado!.FirstOrDefault()!);

                return Ok(new { response.Sucesso, response.Mensagem, response.Resultado, Token = token });
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("autenticar/anonimo")]
        public async Task<ActionResult> LoginClienteAnonimo()
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.LoginClienteAnonimo();
                var token = _autenticacao.GerarJwt(response.Resultado!.FirstOrDefault()!);

                return Ok(new { response.Sucesso, response.Mensagem, response.Resultado, Token = token });
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("alterar/status")]
        public async Task<ActionResult> AlterarStatusCliente(int clienteId)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _clienteController.AlterarStatusCliente(clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

    }
}
