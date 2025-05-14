using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Ports.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController(IServiceProduto _service) : Controller
    {
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarProduto([FromForm] ProdutoRequest request)
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                response = await _service.CadastrarProduto(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("listar")]
        public async Task<ActionResult> ListarProdutos()
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                response = await _service.ListarProdutos();
                return new JsonResult(new { data = response.Resultado });
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPatch("atualizar")]
        public async Task<ActionResult> AtualizarProduto(ProdutoRequest request)
        {
            var response = new ResponseBase<ProdutoResponse>();
            try
            {
                response = await _service.AtualizarProduto(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("remover")]
        public async Task<ActionResult> RemoverProduto(int id)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _service.RemoverProduto(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("obterCardapio")]
        public async Task<ActionResult> ObterCardapio()
        {
            var response = new ResponseBase<CardapioResponse>();
            try
            {
                response = await _service.ObterCardapio();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("visualizarImagem")]
        public async Task<ActionResult> VisualizarImagem(int id)
        {
            var response = new ResponseBase<string>();
            try
            {
                response = await _service.VisualizarImagem(id);
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
