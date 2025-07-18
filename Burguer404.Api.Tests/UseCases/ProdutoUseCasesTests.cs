using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Application.UseCases.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Burguer404.Api.Tests.UseCases
{
    [Trait("Category", "ProdutoUseCasesTests")]
    public class ProdutoUseCasesTests
    {
        private readonly Mock<IProdutoGateway> _produtoGatewayMock;
        private readonly CadastrarProdutoUseCase _cadastrarProdutoUseCase;
        public ProdutoUseCasesTests()
        {
            _produtoGatewayMock = new Mock<IProdutoGateway>();
            _cadastrarProdutoUseCase = new CadastrarProdutoUseCase(_produtoGatewayMock.Object);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest { 
            CategoriaProdutoId = 2,
            Descricao = "Descrição Teste",
            Nome = "Teste descrição",
            Status = true,
            Preco = 11};
                        _produtoGatewayMock.Setup(g => g.CadastrarProdutoAsync(It.IsAny<ProdutoEntity>())).ReturnsAsync(new ProdutoEntity());

            var useCase = new CadastrarProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var imagemBytes = new byte[10];
            new Random().NextBytes(imagemBytes);
            var fileMock = new Moq.Mock<Microsoft.AspNetCore.Http.IFormFile>();
            fileMock.Setup(f => f.FileName).Returns($"imagem_{Guid.NewGuid().ToString().Substring(0, 8)}.jpg");
            fileMock.Setup(f => f.Length).Returns(imagemBytes.Length);
            fileMock.Setup(f => f.OpenReadStream()).Returns(new System.IO.MemoryStream(imagemBytes));
            fileMock.Setup(f => f.ContentType).Returns("image/jpeg");
            
            var request = new ProdutoRequest { Imagem = fileMock.Object };
            _produtoGatewayMock.Setup(g => g.CadastrarProdutoAsync(It.IsAny<ProdutoEntity>())).ThrowsAsync(new Exception("Erro ao cadastrar"));

            var useCase = new CadastrarProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarJsonResult()
        {
                        _produtoGatewayMock.Setup(g => g.ListarProdutosAsync()).ReturnsAsync(new List<ProdutoEntity>());

            var useCase = new ListarProdutosUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync();

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarBadRequest_EmCasoDeErro()
        {
                        _produtoGatewayMock.Setup(g => g.ListarProdutosAsync()).ThrowsAsync(new Exception("Erro ao listar"));

            var useCase = new ListarProdutosUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync();

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest { Nome = "Produto Teste", Preco = 10.0, CategoriaProdutoId = 1 };
            _produtoGatewayMock.Setup(g => g.AtualizarProdutoAsync(It.IsAny<ProdutoEntity>())).ReturnsAsync(new ProdutoEntity());

            var useCase = new AtualizarProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
                        _produtoGatewayMock.Setup(g => g.AtualizarProdutoAsync(It.IsAny<ProdutoEntity>())).ThrowsAsync(new Exception("Erro ao atualizar"));

            var useCase = new AtualizarProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarOk()
        {
            int id = 1;
                        _produtoGatewayMock.Setup(g => g.ObterProdutoPorIdAsync(id)).ReturnsAsync(new ProdutoEntity());
            _produtoGatewayMock.Setup(g => g.RemoverProdutoAsync(It.IsAny<ProdutoEntity>()));

            var useCase = new RemoverProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(id);

            Assert.IsType<ResponseBase<bool>>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
                        _produtoGatewayMock.Setup(g => g.ObterProdutoPorIdAsync(id)).ThrowsAsync(new Exception("Erro ao remover"));

            var useCase = new RemoverProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(id);

            Assert.IsType<ResponseBase<bool>>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarOk()
        {
                        _produtoGatewayMock.Setup(g => g.ListarProdutosAsync()).ReturnsAsync(new List<ProdutoEntity>());

            var useCase = new ListarProdutosUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync();

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarBadRequest_EmCasoDeErro()
        {
                        _produtoGatewayMock.Setup(g => g.ListarProdutosAsync()).ThrowsAsync(new Exception("Erro ao obter cardápio"));

            var useCase = new ListarProdutosUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync();

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarOk()
        {
            int id = 1;
            _produtoGatewayMock.Setup(g => g.ObterProdutoPorIdAsync(id)).ReturnsAsync(new ProdutoEntity { ImagemByte = new byte[1] });

            var useCase = new VisualizarImagemProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(id);

            Assert.IsType<ResponseBase<string>>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            _produtoGatewayMock.Setup(g => g.ObterProdutoPorIdAsync(id)).ThrowsAsync(new Exception("Erro ao visualizar imagem"));

            var useCase = new VisualizarImagemProdutoUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(id);

            Assert.IsType<ResponseBase<string>>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarOk()
        {
            int categoriaId = 1;
            _produtoGatewayMock.Setup(g => g.ObterProdutosPorCategoriaIdAsync(categoriaId)).ReturnsAsync(new List<ProdutoEntity>());

            var useCase = new ObterProdutosPorCategoriaUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(categoriaId);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int categoriaId = 1;
            _produtoGatewayMock.Setup(g => g.ObterProdutosPorCategoriaIdAsync(categoriaId)).ThrowsAsync(new Exception("Erro ao obter produtos por categoria"));

            var useCase = new ObterProdutosPorCategoriaUseCase(_produtoGatewayMock.Object);
            var result = await useCase.ExecuteAsync(categoriaId);

            Assert.IsType<ResponseBase<ProdutoResponse>>(result);
        }
    }
}
