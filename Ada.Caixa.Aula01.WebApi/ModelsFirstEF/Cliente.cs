using System;
using System.Collections.Generic;

namespace Ada.Caixa.Aula01.WebApi.ModelsFirstEF;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public string? DataCriacao { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
