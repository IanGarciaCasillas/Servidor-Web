using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Client
{
    [JsonPropertyName("IdClient")]
    public int IdClient { get; set; }
    [JsonPropertyName("Dni")]
    public string Dni { get; set; } = null!;
    [JsonPropertyName("NomClient")]
    public string NomClient { get; set; } = null!;
    [JsonPropertyName("Cognom1Client")]
    public string Cognom1Client { get; set; } = null!;
    [JsonPropertyName("Cognom2Client")]
    public string Cognom2Client { get; set; } = null!;
    [JsonPropertyName("CorreuClient")]
    public string CorreuClient { get; set; } = null!;
    [JsonPropertyName("ContrasenyaClient")]
    public string ContrasenyaClient { get; set; } = null!;
    [JsonPropertyName("TelefonClient")]
    public string TelefonClient { get; set; } = null!;
    [JsonPropertyName("DireccioClient")]
    public string DireccioClient { get; set; } = null!;
    [JsonPropertyName("CodicPostal")]
    public string CodicPostal { get; set; } = null!;
    [JsonPropertyName("Token")]
    public string Token { get; set; } = null!;

    public virtual ICollection<ComandaVendum>? ComandaVenda { get; set; } = new List<ComandaVendum>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
