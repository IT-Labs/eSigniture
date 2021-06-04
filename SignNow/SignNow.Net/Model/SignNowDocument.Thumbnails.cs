using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SignNow.Net.Interfaces;
using SignNow.Net.Model.FieldContents;

namespace SignNow.Net.Model
{
    /// <remarks>
    /// This part contains related to Fields and Fields value retrieval methods only.
    /// </remarks>
    public partial class SignNowDocument
    {
        /// <summary>
        /// All the document <see cref="TextContent"/> fields.
        /// </summary>
        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

     
    }

    public class Thumbnail
    {
        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}
