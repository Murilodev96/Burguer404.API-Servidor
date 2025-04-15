using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Domain.Entities.Cliente
{
    public class PerfilClienteEntity : EntityBase
    {
        public string Descricao { get; set; }     
        public virtual ICollection<ClienteEntity> Cliente { get; set; }
    }
}
