using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Ticket
{
    [JsonPropertyName("IdTicket")]
    public int IdTicket { get; set; }
    [JsonPropertyName("NumDocument")]
    public int NumDocument { get; set; }
    [JsonPropertyName("DataTicket")]
    public DateTime DataTicket { get; set; }
    [JsonPropertyName("IdClient")]
    public int? IdClient { get; set; }
    [JsonPropertyName("IdComanda")]
    public int? IdComanda { get; set; }
    [JsonPropertyName("IdAlbara")]
    public int? IdAlbara { get; set; }

    public virtual AlbaraVendum? IdAlbaraNavigation { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ComandaVendum? IdComandaNavigation { get; set; }

    public virtual ICollection<TicketDetall> TicketDetalls { get; set; }
}
