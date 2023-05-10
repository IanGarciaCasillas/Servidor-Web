using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Empl
{
    [JsonPropertyName("IdEmpl")]
    public int IdEmpl { get; set; }
    [JsonPropertyName("NomEmpl")]
    public string NomEmpl { get; set; } = null!;
    [JsonPropertyName("Cognom1Empl")]
    public string Cognom1Empl { get; set; } = null!;
    [JsonPropertyName("Cognom2Empl")]
    public string Cognom2Empl { get; set; } = null!;
    [JsonPropertyName("DniEmpl")]
    public string DniEmpl { get; set; } = null!;
    [JsonPropertyName("DataNaixEmpl")]
    public DateTime DataNaixEmpl { get; set; }
    [JsonPropertyName("DataAltaEmpl")]
    public DateTime DataAltaEmpl { get; set; }
    [JsonPropertyName("DataBaixaEmpl")]
    public DateTime DataBaixaEmpl { get; set; }
    [JsonPropertyName("SexeEmpl")]
    public string SexeEmpl { get; set; } = null!;
    [JsonPropertyName("TelefonEmpl")]
    public int TelefonEmpl { get; set; }
    [JsonPropertyName("NssEmpl")]
    public string NssEmpl { get; set; } = null!;
    [JsonPropertyName("DireccioEmpl")]
    public string DireccioEmpl { get; set; } = null!;
    [JsonPropertyName("FotoEmpl")]
    public byte[] FotoEmpl { get; set; } = null!;
    [JsonPropertyName("IdRol")]
    public int IdRol { get; set; }
    [JsonPropertyName("UserEmpl")]
    public string UserEmpl { get; set; }
    [JsonPropertyName("PasswordEmpl")]
    public string PasswordEmpl { get; set; }
    [JsonPropertyName("JornadaEmpl")]
    public string JornadaEmpl { get; set; } = null!;
    [JsonPropertyName("IdRolNavigation")]
    public virtual Rol IdRolNavigation { get; set; } = null!;
}
