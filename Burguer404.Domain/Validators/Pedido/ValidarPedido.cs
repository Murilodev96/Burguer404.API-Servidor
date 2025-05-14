using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Enums;

namespace Burguer404.Domain.Validators.Pedido
{
    public static class ValidarPedido
    {
        public static ResponseBaseValidacoes Validar_CadastrarPedido_Request(PedidoRequest request) 
        {
            var ok = new ResponseBaseValidacoes();

            if (request.ProdutosSelecionados == null || request.ProdutosSelecionados.Count() <= 0)
            {
                ok.Mensagem = "É necessário adicionar ao menos 1 produto para realizar um pedido!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_PedidoIdValido(int pedidoId)
        {
            var ok = new ResponseBaseValidacoes();

            if (pedidoId <= 0)
            {
                ok.Mensagem = "Não foi possível cadastrar o pedido!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_CodigoPedidoValido(string codigoPedido)
        {
            var ok = new ResponseBaseValidacoes();

            if (string.IsNullOrWhiteSpace(codigoPedido))
            {
                ok.Mensagem = "Informar um pedido válido!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_ExistenciaPedido(PedidoEntity? pedido)
        {
            var ok = new ResponseBaseValidacoes();

            if (pedido == null)
            {
                ok.Mensagem = "Pedido não encontrado!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static int ValidacoesDeStatusDePedido(int statusPedidoId)
        {
            if (statusPedidoId == (int)EnumStatusPedido.Finalizado ||
                statusPedidoId == (int)EnumStatusPedido.Cancelado)
            {
                return statusPedidoId;
            }

            return statusPedidoId + 1;
        }

        public static ResponseBaseValidacoes Validar_GerarQrCode_Request(List<PagamentoRequest> itens)
        {
            var ok = new ResponseBaseValidacoes();

            if (itens == null | itens.Count <=0)
            {
                ok.Mensagem = "Selecione ao menos 1 item ao carrinho!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }
    }
}
