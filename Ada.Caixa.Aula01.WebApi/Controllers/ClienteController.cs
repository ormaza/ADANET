using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly ClienteRepository _clienteRepository;

    public ClienteController(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // var clientes = _clienteRepository.ListarTodosClientes();
        // return Ok(clientes);

        DapperContext dapperContext = new DapperContext();
        var clientesDapper = dapperContext.ListAllClientesDapper();
        return Ok(clientesDapper);
    }
}