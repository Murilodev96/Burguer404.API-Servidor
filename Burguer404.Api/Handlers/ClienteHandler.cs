using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Services.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(IServiceCliente _service) : Controller
    {
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarCliente(ClienteRequest request)
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _service.CadastrarCliente(request);
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
                response = await _service.ListarClientes();
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
                response = await _service.LoginCliente(cpf);
                return Ok(response);
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
                response = await _service.LoginClienteAnonimo();
                return Ok(response);
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
                response = await _service.AlterarStatusCliente(clienteId);
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
