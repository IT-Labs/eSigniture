using System.Collections.Generic;
using Newtonsoft.Json;

namespace SignNow.Net.Model
{
    /// <summary>
    /// Represents response from SignNow API for User create request.
    /// </summary>
    public class FoldersResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("system_folder")]
        public bool SystemFolder { get; set; }

        [JsonProperty("shared")]
        public bool Shared { get; set; }

        [JsonProperty("total_documents")]
        public int TotalDocuments { get; set; }

        [JsonProperty("folders")]
        public IReadOnlyCollection<SignNowFolder> Folders { get; set; }
    }
}
