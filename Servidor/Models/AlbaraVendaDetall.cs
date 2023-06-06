using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class AlbaraVendaDetall
{
    [JsonPropertyName("IdAlbaraVenda")]
    public int IdAlbaraVenda { get; set; }
    [JsonPropertyName("IdArticle")]
    public int IdArticle { get; set; }
    [JsonPropertyName("Quantitat")]
    public double Quantitat { get; set; }

    public virtual AlbaraVendum? IdAlbaraVendaNavigation { get; set; } = null!;

    public virtual Article? IdArticleNavigation { get; set; } = null!;
}
