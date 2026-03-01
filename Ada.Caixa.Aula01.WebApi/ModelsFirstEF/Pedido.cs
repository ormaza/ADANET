using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ada.Caixa.Aula01.WebApi.ModelsFirstEF;

public partial class Pedido
{
    public int Id { get; set; }

    public string? Descricao { get; set; }

    public int? ClienteId { get; set; }

    [JsonIgnore] //evitar referência circular ao serializar o objeto para JSON
    public virtual Cliente? Cliente { get; set; }
}
