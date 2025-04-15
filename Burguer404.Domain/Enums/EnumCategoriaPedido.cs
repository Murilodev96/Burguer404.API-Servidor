using System.Runtime.Serialization;

namespace Burguer404.Domain.Enums
{
    public enum EnumCategoriaPedido
    {
        [EnumMember]
        Lanche = 1,

        [EnumMember]
        Acompanhamento = 2,

        [EnumMember]
        Bebida = 3,

        [EnumMember]
        Sobremesa = 4
    }
}
