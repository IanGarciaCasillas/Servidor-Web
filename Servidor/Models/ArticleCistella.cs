using System.Text.Json.Serialization;

namespace Servidor.Models
{
    public class ArticleCistella
    {
        [JsonPropertyName("IdArticle")]
        public Article Article{ get;set; }
        [JsonPropertyName("QuantitatDemanada")]
        public int Quantitat { get; set; }

    }
}
