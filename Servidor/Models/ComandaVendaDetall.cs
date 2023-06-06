using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class ComandaVendaDetall
{
    [JsonPropertyName("IdComandaVenda")]
    public int IdComandaVenda { get; set; }
    [JsonPropertyName("IdArticle")]
    public int IdArticle { get; set; }
    [JsonPropertyName("QuantitatDemanada")]
    public double QuantitatDemanada { get; set; }
    [JsonPropertyName("QuantitatServida")]
    public double QuantitatServida { get; set; }

    public virtual Article? IdArticleNavigation { get; set; } = null!;

    public virtual ComandaVendum? IdComandaVendaNavigation { get; set; } = null!;
}
