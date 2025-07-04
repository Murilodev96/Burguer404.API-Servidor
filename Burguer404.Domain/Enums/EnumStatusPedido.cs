using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Burguer404.Domain.Enums
{
    public enum EnumStatusPedido
    {
        [EnumMember]
        [Display(Name = "Recebido")]
        Recebido = 1,

        [EnumMember]
        [Display(Name = "Em preparação")]
        EmPreparacao = 2,

        [EnumMember]
        [Display(Name = "Pronto")]
        Pronto = 3,

        [EnumMember]
        [Display(Name = "Finalizado")]
        Finalizado = 4,

        [EnumMember]
        [Display(Name = "Cancelado")]
        Cancelado = 5,

        [EnumMember]
        [Display(Name = "Aguardando pagamento")]
        AguardandoPagamento = 6,
    }
}
