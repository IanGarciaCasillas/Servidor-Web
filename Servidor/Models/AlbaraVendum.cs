using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class AlbaraVendum
{
    [JsonPropertyName("IdAlbara")]
    public int IdAlbara { get; set; }
    [JsonPropertyName("Data")]
    public DateTime Data { get; set; }

    public virtual ICollection<AlbaraVendaDetall> AlbaraVendaDetalls { get; set; } = new List<AlbaraVendaDetall>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
