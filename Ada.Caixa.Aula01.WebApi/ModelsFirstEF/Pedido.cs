using System;
using System.Collections.Generic;

namespace Ada.Caixa.Aula01.WebApi.ModelsFirstEF;

public partial class Pedido
{
    public int Id { get; set; }

    public string? Descricao { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
