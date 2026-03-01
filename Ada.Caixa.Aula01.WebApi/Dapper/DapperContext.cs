using Ada.Caixa.Aula01.WebApi.ModelsFirstEF;
using Dapper;
using Microsoft.Data.Sqlite;

public class DapperContext
{
    public List<Cliente> ListAllClientesDapper()
    {
        string connectionString = "Data Source=MeuBancoFirst.db";

        using(var connection = new SqliteConnection(connectionString))
        {
            var sql = "SELECT Id, Nome, Email, DataCriacao FROM Clientes";
            var clientes = new List<Cliente>();
            foreach(var cliente in connection.Query<Cliente>(sql).ToList())
            {
                cliente.Pedidos = ListarPedidosPorClienteDapper(cliente.Id);
                clientes.Add(cliente);
            }
            return clientes;
        }
    }

    public List<Pedido> ListarPedidosPorClienteDapper(int clienteId)
    {
        string connectionString = "Data Source=MeuBancoFirst.db";

        using(var connection = new SqliteConnection(connectionString))
        {
            var sql = "SELECT Id, Descricao FROM Pedidos WHERE ClienteId = @ClienteId";
            var pedidos = connection.Query<Pedido>(sql, new { ClienteId = clienteId }).ToList();
            return pedidos;
        }
    }
}