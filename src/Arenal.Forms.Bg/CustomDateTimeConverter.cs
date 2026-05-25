using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Skyware.Arenal.Forms.Bg;

/// <summary>
/// <inheritdoc/>
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string Format;

    /// <summary>
    /// <inheritdoc/>
    public CustomDateTimeConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
    {
        writer.WriteStringValue(date.ToString(Format));
    }

    /// <summary>
    /// <inheritdoc/>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), Format, null);
    }
}
