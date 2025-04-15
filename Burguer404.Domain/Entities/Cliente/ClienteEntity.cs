using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Domain.Entities.Cliente
{
    public class ClienteEntity : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<PedidoEntity> Pedidos { get; set; }
    }
}
