using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using SignNow.Net.Internal.Helpers.Converters;

namespace SignNow.Net.Model.FieldContents
{
    /// <summary>
    /// Represents SignNow field types: `Signature`, `Initials fields`.
    /// </summary>
    public class SignatureContent : BaseContent
    {
        /// <summary>
        /// Identity of the signature request.
        /// </summary>
        [JsonProperty("signature_request_id")]
        public string SignatureRequestId { get; set; }

        /// <summary>
        /// <see cref="SignNow.Net.Model.User"/> email who was signed the document.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Timestamp document was signed.
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        public DateTime Created { get; set; }

        /// <summary>
        /// Raw text value of the field.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(StringBase64ToByteArrayJsonConverter))]
        [SuppressMessage("Properties should not return arrays", "CA1819")]
        public byte[] Data { get; set; }

        /// <summary>
        /// Returns SignatureContent content as base64 string.
        /// </summary>
        public override string ToString()
        {
            return Convert.ToBase64String(Data);
        }

        /// <inheritdoc />
        public override object GetValue() => Data;
    }
}
