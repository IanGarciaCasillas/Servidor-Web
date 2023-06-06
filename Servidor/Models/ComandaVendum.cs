using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class ComandaVendum
{
    [JsonPropertyName("IdComanda")]
    public int IdComanda { get; set; }
    [JsonPropertyName("DataComanda")]
    public DateTime DataComanda { get; set; }
    [JsonPropertyName("EstatComandaVenda")]
    public string EstatComandaVenda { get; set; } = null!;
    [JsonPropertyName("IdClient")]
    public int IdClient { get; set; }

    public virtual ICollection<ComandaVendaDetall> ComandaVendaDetalls { get; set; } = new List<ComandaVendaDetall>();

    public virtual Client? IdClientNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
