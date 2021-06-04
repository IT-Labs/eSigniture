using System.Collections.Generic;
using SignNow.Net.Model;

namespace SignNow.Client.ItLabs.Models
{
    public class FolderModel
    {
        public IReadOnlyCollection<SignNowFolder> Folders { get; set; }
        public IReadOnlyCollection<SignNowDocument> Documents { get; set; }
    }
}
