using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Services.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController(IServicePedido _service) : Controller
    {

        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarPedido(PedidoRequest request)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _service.CadastrarPedido(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("listar")]
        public async Task<JsonResult> ListarPedidos()
        {
            var response = new ResponseBase<PedidoResponse>();
            try
            {
                response = await _service.ListarPedidos();
                return new JsonResult(new { data = response.Resultado });
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return new JsonResult(new { });
            }
        }

        [HttpGet("cancelar")]
        public async Task<ActionResult> CancelarPedido(int pedidoId)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _service.CancelarPedido(pedidoId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("visualizar")]
        public async Task<ActionResult> VisualizarPedido(string codigo)
        {
            var response = new ResponseBase<PedidoResponse>();
            try
            {
                response = await _service.VisualizarPedido(codigo);
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
