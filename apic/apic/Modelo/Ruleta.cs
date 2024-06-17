using System.Text.Json.Serialization;

namespace apic.Modelo
{
    public class Ruleta
    {
        [JsonPropertyName("numero")]
        public int Numero { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
        [JsonPropertyName("par")]
        public string Par { get; set; }
        [JsonPropertyName("apuesta")]
        public double Apuesta { get; set; }
        [JsonPropertyName("puntos")]
        public double Puntos { get; set; }
    }
}
