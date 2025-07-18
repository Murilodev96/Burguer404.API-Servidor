using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Controllers;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoHandler : Controller
    {
        private readonly PedidosController _pedidoController;

        public PedidoHandler(IPedidosGateway gateway, IConfiguration config, IProdutoGateway prodGateway)
        {
            _pedidoController = new PedidosController(gateway, config, prodGateway);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarPedido(PedidoRequest request)
        {
            var response = new ResponseBase<string>();
            try
            {
                response = await _pedidoController.CadastrarPedido(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("pagamento")]
        public async Task<ActionResult> ContinuarPagamento(List<PagamentoRequest> request)
        {
            var response = new ResponseBase<string>();
            try
            {
                response = await _pedidoController.GerarQrCode(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("listar")]
        public async Task<JsonResult> ListarPedidos(int clienteLogadoId)
        {
            var response = new ResponseBase<PedidoResponse>();
            try
            {
                response = await _pedidoController.ListarPedidos(clienteLogadoId);
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
                response = await _pedidoController.CancelarPedido(pedidoId);
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
                response = await _pedidoController.VisualizarPedido(codigo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }
        
        [HttpGet("avancarStatusPedido")]
        public async Task<ActionResult> AvancarStatusPedido(string codigo)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _pedidoController.AvancarStatusPedido(codigo);
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
