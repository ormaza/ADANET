using Ada.Caixa.Aula01.WebApi.ModelsFirstEF;

public class ClienteRepository
{
    private readonly MeuContextoExistente _meuContextoExistente;

    public ClienteRepository(MeuContextoExistente meuContextoExistente)
    {
        _meuContextoExistente = meuContextoExistente;
    }
    public List<Cliente> ListarTodosClientes()
    {
        var clientes = new List<Cliente>();
        foreach (var cliente in _meuContextoExistente.Clientes)
        {
            var pedidos = _meuContextoExistente.Pedidos.Where(p => p.ClienteId == cliente.Id).ToList();
            
            var newCliente = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Pedidos = pedidos
            };

            clientes.Add(newCliente);
        }
        return clientes;
    }
}