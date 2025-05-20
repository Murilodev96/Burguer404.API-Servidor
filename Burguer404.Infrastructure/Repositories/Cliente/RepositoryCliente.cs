using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Infrastructure.Data.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace Burguer404.Infrastructure.Data.Repositories.Cliente
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private readonly Context _context;

        public RepositoryCliente(Context context)
        {
            _context = context;
        }

        public async Task<ClienteEntity> CadastrarCliente(ClienteEntity cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<List<ClienteEntity>> ListarClientes()
            => await _context.Clientes.Where(x => x.Status == true).ToListAsync();

        public async Task<ClienteEntity?> ObterClientePorCpf(string cpf)
            => await _context.Clientes.Where(x => x.Cpf == cpf).FirstOrDefaultAsync();

        public async Task<bool> ValidarCadastroCliente(string cpf, string email)
        {
            var cliente = await _context.Clientes.Where(x => x.Cpf == cpf &&
                                                        x.Status == true)
                                                 .FirstOrDefaultAsync();

            return cliente != null;
        }

        public async Task<bool> AlterarStatusCliente(int clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
                return false;

            cliente.Status = !cliente.Status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
