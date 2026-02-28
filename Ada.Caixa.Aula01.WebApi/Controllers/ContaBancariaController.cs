using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ContaBancariaController : ControllerBase
{
    private readonly ContaRepository _contaRepository;

    public ContaBancariaController(ContaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var contas = _contaRepository.ListarTodasContasBancarias();
        return Ok(contas);
    }

    [HttpGet("obterconta")]
    public IActionResult ObterConta(string agencia, string numeroConta)
    {
        var conta = _contaRepository.ObterContaPorAgenciaEConta(agencia, numeroConta);
        return Ok(conta);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ContaBancaria contaBancaria)
    {
        int idItemCriado = _contaRepository.CriarNovaContaBancaria(contaBancaria.TitularConta, contaBancaria.Saldo, contaBancaria.Conta, contaBancaria.Agencia);
        return Ok(idItemCriado);
    }

    [HttpPut]
    public IActionResult Put(int id, [FromBody] decimal valorSaque)
    {
        var contaAtualizada = _contaRepository.Saque(id, valorSaque);
        return Ok(contaAtualizada);
    }
}