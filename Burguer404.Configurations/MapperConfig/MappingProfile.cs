using AutoMapper;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Configurations.MapperConfig
{
    public class MappingProfile : Profile
    {
        public class AutoMapperConfig
        {
            public MapperConfiguration Configure()
            {
                return new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); });
            }
        }

        public MappingProfile()
        {
            CreateMap<ClienteRequest, ClienteEntity>();
            CreateMap<ClienteEntity, ClienteResponse>();

            CreateMap<ProdutoRequest, ProdutoEntity>();
            CreateMap<ProdutoEntity, ProdutoResponse>();

            CreateMap<PedidoRequest, PedidoEntity>();
            CreateMap<PedidoEntity, PedidoResponse>();
        }
    }
}
