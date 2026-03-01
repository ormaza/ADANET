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
        return _meuContextoExistente.Clientes.ToList();
    }
}