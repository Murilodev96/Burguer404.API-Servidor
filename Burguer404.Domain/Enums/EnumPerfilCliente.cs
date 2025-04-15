using System.Runtime.Serialization;

namespace Burguer404.Domain.Enums
{
    public enum EnumPerfilCliente
    {
        [EnumMember]
        Admin = 1,

        [EnumMember]
        Usuario = 2,
    }
}
