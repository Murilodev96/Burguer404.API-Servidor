using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Domain.Validators.Produto
{
    public static class ValidarProduto
    {
        public static ResponseBaseValidacoes Validar_CadastrarProduto_Request(ProdutoRequest request)
        {
            var ok = new ResponseBaseValidacoes();

            if (string.IsNullOrEmpty(request.Nome) && string.IsNullOrEmpty(request.Descricao) || request.Preco <= 0 || request.CategoriaPedidoId <= 0 || request.Imagem == null)
            {
                ok.Mensagem = "Todos os campos são obrigatórios para cadastro de um produto!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_ExistenciaProduto(ProdutoEntity? produto)
        {
            var ok = new ResponseBaseValidacoes();

            if (produto == null)
            {
                ok.Mensagem = "Não foi encontrado produto ativo na base de dados!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_ProdutoIdValido(int produtoId)
        {
            var ok = new ResponseBaseValidacoes();

            if (produtoId <= 0)
            {
                ok.Mensagem = "Selecione um produto para remover!";
                return ok;
            }

            ok.Sucesso = true;
            return ok;
        }

        public static ResponseBaseValidacoes Validar_VisualizarImagem(int produtoId)
        {
            var ok = new ResponseBaseValidacoes();

            if (produtoId <= 0)
            {
                ok.Mensagem = "Id não localizado.";
                return ok;
            }
            ok.Sucesso = true;
            return ok;
        }
    }
}
