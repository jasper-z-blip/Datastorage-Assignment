using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        // Förväntat datum tex. "2015-02.19".
        private readonly string _format = "yyyy-MM-dd";

        // Metoden som konverterar en JSON-sträng till ett DateOnly-objekt.
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();

            // Kontroll om strängen är null eller tom.
            if (string.IsNullOrWhiteSpace(dateString))
            {
                // Felmeddelande om datum inte är ifyllt.
                throw new JsonException($"Ogiltigt datum. Fältet är obligatoriskt och måste följa formatet {_format}.");
            }

            try
            {
                return DateOnly.ParseExact(dateString, _format, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // Felemddelande om inte formatet stämmer, vilket det borde göra för man kan inte skriva datumet utan det väljer man i som en kalender.
                throw new JsonException($"Ogiltigt datumformat. Värdet måste följa formatet {_format}.");
            }
        }

        // Metoden gör om DateOnly till en JSON sträng.
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format, CultureInfo.InvariantCulture));
        }
    }
}
// Hela koden är tagen från chatGPT, jag skrev egen kod med hjälp av chatGPT, men fick den inte att fungera och såg inte vad jag gjort fel.
// Klistrade in hela min kod i chatGPT och den gav mig denna kod. Kunde inte se vad chatGPT hade annorlunda jämfört med min kod.
// Så därför skriver jag att koden är från chatGPT fast jag skrev en hel del själv först, så det inte blir plagiat.
