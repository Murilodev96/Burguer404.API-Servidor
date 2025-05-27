using AutoMapper;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Services;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Enums;
using Burguer404.Domain.Ports.Repositories.Produto;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Burguer404.Application.Tests.Services
{
    public class ServiceProdutoTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryProduto> _produtoRepositoryMock;
        private readonly ServiceProduto _service;

        public ServiceProdutoTests()
        {
            _mapperMock = new Mock<IMapper>();
            _produtoRepositoryMock = new Mock<IRepositoryProduto>();
            _service = new ServiceProduto(_mapperMock.Object, _produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarErro_SeValidacaoFalhar()
        {
            // Arrange
            var request = new ProdutoRequest();
            var service = new ServiceProduto(_mapperMock.Object, _produtoRepositoryMock.Object);
            request.Nome = null;

            var response = await service.CadastrarProduto(request);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarSucesso()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("fake image content");
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns((Stream s, System.Threading.CancellationToken t) => ms.CopyToAsync(s));
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var request = new ProdutoRequest
            {
                Nome = "Produto Teste",
                Imagem = fileMock.Object,
                CategoriaProdutoId = 1,
                Preco = 10.0
            };

            var produtoEntity = new ProdutoEntity();
            var produtoResponse = new ProdutoResponse();

            _mapperMock.Setup(m => m.Map<ProdutoRequest, ProdutoEntity>(It.IsAny<ProdutoRequest>())).Returns(produtoEntity);
            _produtoRepositoryMock.Setup(r => r.CadastrarProduto(It.IsAny<ProdutoEntity>())).ReturnsAsync(produtoEntity);
            _mapperMock.Setup(m => m.Map<ProdutoEntity, ProdutoResponse>(It.IsAny<ProdutoEntity>())).Returns(produtoResponse);

            var response = await _service.CadastrarProduto(request);

            Assert.True(response.Sucesso);
            Assert.Equal("Produto cadastrado com sucesso!", response.Mensagem);
            Assert.NotNull(response.Resultado);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarProdutos()
        {
            var produtos = new List<ProdutoEntity> { new ProdutoEntity() };
            var produtoResponse = new ProdutoResponse();

            _produtoRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<ProdutoEntity, ProdutoResponse>(It.IsAny<ProdutoEntity>())).Returns(produtoResponse);

            var response = await _service.ListarProdutos();

            Assert.True(response.Sucesso);
            Assert.NotNull(response.Resultado);
            Assert.Single(response.Resultado);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarErro_SeValidacaoFalhar()
        {
            var request = new ProdutoRequest(); 
            var response = await _service.AtualizarProduto(request);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarErro_SeProdutoNaoExistir()
        {
            var request = new ProdutoRequest
            {
                Nome = "Produto Teste",
                CategoriaProdutoId = 1,
                Preco = 10.0
            };

            var produtoEntity = (ProdutoEntity)null;

            _mapperMock.Setup(m => m.Map<ProdutoRequest, ProdutoEntity>(It.IsAny<ProdutoRequest>())).Returns(produtoEntity);
            _produtoRepositoryMock.Setup(r => r.AtualizarCadastro(It.IsAny<ProdutoEntity>())).ReturnsAsync(produtoEntity);

            var response = await _service.AtualizarProduto(request);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarErro_SeIdInvalido()
        {
            var response = await _service.RemoverProduto(0);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarErro_SeProdutoNaoExistir()
        {
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorId(It.IsAny<int>())).ReturnsAsync((ProdutoEntity)null);

            var response = await _service.RemoverProduto(1);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarSucesso()
        {
            var produto = new ProdutoEntity();
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorId(It.IsAny<int>())).ReturnsAsync(produto);

            var response = await _service.RemoverProduto(1);

            Assert.True(response.Sucesso);
            Assert.Equal("Produto removido com sucesso!", response.Mensagem);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarCardapio()
        {
            var produtos = new List<ProdutoEntity>
            {
                new ProdutoEntity { CategoriaProdutoId = (int)EnumCategoriaPedido.Lanche },
                new ProdutoEntity { CategoriaProdutoId = (int)EnumCategoriaPedido.Bebida }
            };
            var produtoResponse = new ProdutoResponse { CategoriaProdutoId = (int)EnumCategoriaPedido.Lanche };
            var produtoResponse2 = new ProdutoResponse { CategoriaProdutoId = (int)EnumCategoriaPedido.Bebida };

            _produtoRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);
            _mapperMock.SetupSequence(m => m.Map<ProdutoEntity, ProdutoResponse>(It.IsAny<ProdutoEntity>()))
                .Returns(produtoResponse)
                .Returns(produtoResponse2);

            var response = await _service.ObterCardapio();

            Assert.True(response.Sucesso);
            Assert.NotNull(response.Resultado);
            Assert.Single(response.Resultado);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarErro_SeIdInvalido()
        {
            var response = await _service.VisualizarImagem(0);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarImagem()
        {
            var produto = new ProdutoEntity { ImagemByte = new byte[] { 1, 2, 3 } };
            _produtoRepositoryMock.Setup(r => r.VisualizarImagem(It.IsAny<int>())).ReturnsAsync(produto);

            var response = await _service.VisualizarImagem(1);

            Assert.True(response.Sucesso);
            Assert.NotNull(response.Resultado);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarErro_SeIdInvalido()
        {
            var response = await _service.ObterProdutosPorCategoria(0);

            Assert.False(response.Sucesso);
            Assert.NotNull(response.Mensagem);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarProdutos()
        {
            var produtos = new List<ProdutoEntity> { new ProdutoEntity() };
            var produtoResponse = new ProdutoResponse();

            _produtoRepositoryMock.Setup(r => r.ObterProdutosPorCategoriaId(It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<ProdutoEntity, ProdutoResponse>(It.IsAny<ProdutoEntity>())).Returns(produtoResponse);

            var response = await _service.ObterProdutosPorCategoria(1);

            Assert.True(response.Sucesso);
            Assert.NotNull(response.Resultado);
        }
    }
}
