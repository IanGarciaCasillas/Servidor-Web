using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servidor.Model
{
    [Table("client")]
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        public string Dni { get; set; }
        public string NomClient { get; set; }
        public string Cognom1Client { get; set; }
        public string Cognom2Client { get; set; }
        public string CorreuClient { get; set; }
        public string ContrasenyaClient { get; set; }
        public string TelefonClient { get; set; }
        public string DireccioClient { get; set; }
        public string CodicPostal { get; set; }
        public string Token { get; set; }


    }
}
