using System.Collections.Generic;
using Newtonsoft.Json;

namespace SignNow.Net.Model
{
    public class SignNowFolder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("shared")]
        public bool Shared { get; set; }

        [JsonProperty("document_count")]
        public string DocumentCount { get; set; }

        [JsonProperty("template_count")]
        public string TemplateCount { get; set; }

        [JsonProperty("folder_count")]
        public string FolderCount { get; set; }

        [JsonProperty("sub_folders")]
        public IReadOnlyCollection<SignNowFolder> SubFolders { get; set; }
    }
}
