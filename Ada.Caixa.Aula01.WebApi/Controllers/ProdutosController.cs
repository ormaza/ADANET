using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private static List<Produto> produtos = new List<Produto>
    {
        new Produto { Id = 1, Nome = "Produto 1", Quantidade = 10 },
        new Produto { Id = 2, Nome = "Produto 2", Quantidade = 20 },
        new Produto { Id = 3, Nome = "Produto 3", Quantidade = 30 }
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(produtos);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Produto produto)
    {
        produtos.Add(produto);
        return Ok(produtos);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id.Equals(id));

        if(produto != null)
            produtos.Remove(produto);
        else
            return BadRequest($"Produto ID: {id} não encontrado");

        return Ok($"Produto ID: {id} removido com sucesso");
    }
}