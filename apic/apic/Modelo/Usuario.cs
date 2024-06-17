using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json.Serialization;

namespace apic.Modelo
{
    public class Usuario
    {

        [JsonPropertyName("nombre_usuario")]
        public string Nombreusuario { get; set; }
        [JsonPropertyName("suma_puntos")]
        public double SumaPuntos  { get; set; }


    }
}
