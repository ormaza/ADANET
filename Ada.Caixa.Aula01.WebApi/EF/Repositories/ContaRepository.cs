public class ContaRepository
{
    private readonly CaixaDBContext _caixaDBContext;

    public ContaRepository(CaixaDBContext caixaDBContext)
    {
        _caixaDBContext = caixaDBContext;
    }

    public int CriarNovaContaBancaria(string NomeTitular, decimal depositoInicial, string numeroConta, string agencia = "1234-5")
    {
        var novaConta = new ContaBancaria
        {
            TitularConta = NomeTitular,
            Agencia = agencia,
            Conta = numeroConta,
            Saldo = depositoInicial
        };

        _caixaDBContext.Contas.Add(novaConta);
        _caixaDBContext.SaveChanges();
        return novaConta.Id;
    }

    public List<ContaBancaria> ListarTodasContasBancarias()
    {
        return _caixaDBContext.Contas.ToList();
    }

    public ContaBancaria ObterContaPorAgenciaEConta(string agencia, string numeroConta)
    {
        return _caixaDBContext.Contas.FirstOrDefault(c => c.Agencia.Equals(agencia) 
                                                    && c.Conta.Equals(numeroConta));
    }

    public ContaBancaria Saque(int id, decimal valorSaque)
    {
        var conta = _caixaDBContext.Contas.FirstOrDefault(c => c.Id.Equals(id));
        var saldoAposSaque = conta.Saldo - valorSaque;

        if(saldoAposSaque >= 0 && valorSaque > 0)
        {
            conta.Saldo = saldoAposSaque;
            _caixaDBContext.SaveChanges();
            return conta;
        }
        else
            throw new Exception("Saldo insuficiente para o saque.");
    }
}