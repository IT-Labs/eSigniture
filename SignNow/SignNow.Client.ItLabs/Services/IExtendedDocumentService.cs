using SignNow.Net.Interfaces;
using SignNow.Net.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Services
{
    public interface IExtendedDocumentService : IDocumentService
    {
        Task<FoldersResponse> GetFoldersAsync(CancellationToken cancellationToken = default);
        Task<FolderResponse> GetFolderAsync(string folderId, CancellationToken cancellationToken = default);
    }
}
