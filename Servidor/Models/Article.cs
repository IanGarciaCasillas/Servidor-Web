using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Article
{
    [JsonPropertyName("IdArticle")]
    public int IdArticle { get; set; }
    [JsonPropertyName("NomArticle")]
    public string NomArticle { get; set; } = null!;
    [JsonPropertyName("DescripcioArticle")]
    public string DescripcioArticle { get; set; } = null!;
    [JsonPropertyName("PreuVenta")]
    public decimal PreuVenta { get; set; }
    [JsonPropertyName("TipusUnitat")]
    public string TipusUnitat { get; set; } = null!;
    [JsonPropertyName("Stock")]
    public decimal Stock { get; set; }
    [JsonPropertyName("MinimStock")]
    public decimal MinimStock { get; set; }
    [JsonPropertyName("AutoStock")]
    public decimal AutoStock { get; set; }
    [JsonPropertyName("IdCategoria")]
    public int? IdCategoria { get; set; }
    [JsonPropertyName("IdProveidorHabitual")]
    public int? IdProveidorHabitual { get; set; }
    [JsonPropertyName("FotoArticle")]
    public byte[] FotoArticle { get; set; } = null!;
    [JsonPropertyName("IvaAplicar")]
    public double IvaAplicar { get; set; }
    [JsonPropertyName("NumVenda")]
    public int NumVenda { get; set; }

    public virtual ICollection<AlbaraCompraDetall> AlbaraCompraDetalls { get; set; } = new List<AlbaraCompraDetall>();

    public virtual ICollection<AlbaraVendaDetall> AlbaraVendaDetalls { get; set; } = new List<AlbaraVendaDetall>();

    public virtual ICollection<ComandaCompraDetall> ComandaCompraDetalls { get; set; } = new List<ComandaCompraDetall>();

    public virtual ICollection<ComandaVendaDetall> ComandaVendaDetalls { get; set; } = new List<ComandaVendaDetall>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Proveidor? IdProveidorHabitualNavigation { get; set; }

    public virtual ICollection<PreuArticleProveidor> PreuArticleProveidors { get; set; } = new List<PreuArticleProveidor>();

    public virtual ICollection<TicketDetall> TicketDetalls { get; set; } = new List<TicketDetall>();
}
