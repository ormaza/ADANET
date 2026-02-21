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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id.Equals(id));
        if(produto == null)
            return NotFound($"Produto ID: {id} não encontrado");

        var response = new
        {
            produto.Id,
            produto.Nome,
            produto.Quantidade,
            _links = new
            {
                self = new { href = Url.Action("Get", new {id}), method = "GET" },
                update = new { href = Url.Action("Update", new Produto()), method = "PUT" },
                delete = new { href = Url.Action("Delete", new {id}), method = "DELETE" }
            }
        };
        
        return Ok(response);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Produto produto)
    {
        var produtoExistente = produtos.FirstOrDefault(p => p.Id.Equals(produto.Id));

        if(produtoExistente != null)
        {
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Quantidade = produto.Quantidade;
        }
        else
            return BadRequest($"Produto ID: {produto.Id} não encontrado");

        var index = produtos.IndexOf(produtoExistente);
        produtos[index] = produtoExistente;

        return Ok(produtoExistente);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Produto produto)
    {
        produtos.Add(produto);
        return Ok(produtos);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id.Equals(id));

        if(produto != null)
            produtos.Remove(produto);
        else
            return BadRequest($"Produto ID: {id} não encontrado");

        return Ok($"Produto ID: {id} removido com sucesso");
    }
}