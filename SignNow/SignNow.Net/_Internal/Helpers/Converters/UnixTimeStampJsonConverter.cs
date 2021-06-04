using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SignNow.Net.Exceptions;

namespace SignNow.Net.Internal.Helpers.Converters
{
    /// <summary>
    /// Converts Unix timestamps to <see cref="DateTime"/> from JSON.
    /// </summary>
    internal class UnixTimeStampJsonConverter : UnixDateTimeConverter
    {
        /// <inheritdoc cref="JsonConverter.WriteJson" />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(UnixTimeStampConverter.ToUnixTimestamp((DateTime)value).ToString(CultureInfo.InvariantCulture));
        }

        /// <inheritdoc cref="JsonConverter.ReadJson" />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var hasAllowedType = reader.TokenType == JsonToken.Integer
                              || reader.TokenType == JsonToken.String;

            if (reader.Value == null)
            {
                return UnixTimeStampConverter.UnixEpoch;
            }

            if (hasAllowedType)
            {
                return UnixTimeStampConverter.FromUnixTimestamp(
                        long.Parse(reader.Value.ToString(), NumberStyles.None, NumberFormatInfo.InvariantInfo));
            }

            throw new JsonSerializationException(
                string.Format(
                    CultureInfo.CurrentCulture, ExceptionMessages.UnexpectedValueWhenConverting,
                    objectType.Name, "`String`, `Integer`", reader.Value?.GetType().Name));
        }

        /// <inheritdoc cref="JsonConverter.CanConvert" />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }

    /// <summary>
    /// Converts <see cref="DateTime"/> to and from Unix timestamp.
    /// </summary>
    internal static class UnixTimeStampConverter
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts Unix timestamp to <see cref="DateTime"/>
        /// </summary>
        /// <param name="unixTime">Timestamp in Unix format.</param>
        public static DateTime FromUnixTimestamp(long unixTime)
        {
            return UnixEpoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts <see cref="DateTime"/> to Unix timestamp.
        /// </summary>
        /// <param name="current">Date which should be converted in Unix timestamp.</param>
        public static long ToUnixTimestamp(DateTime current)
        {
            return (long)(current - UnixEpoch).TotalSeconds;
        }
    }
}
