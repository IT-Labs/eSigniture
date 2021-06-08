using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SignNow.Client.ItLabs.Model;
using SignNow.Client.ItLabs.Models;
using SignNow.Client.ItLabs.Services;
using SignNow.Net;
using SignNow.Net.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger { get; }
        private IConfiguration _configuration { get; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        private static Uri ApiBaseUrl => new Uri("https://api-eval.signnow.com/");
        public async Task<IActionResult> Index(string folderId)
        {
            var credentials = new CredentialModel
            {
                Login = "zoran.petrushev@it-labs.com",
                Password = "P@ssw0rd123#",
                ClientId = "d52b25d3523516690722cbe97a31526f",
                ClientSecret = "8eae7c60c7b030e2873b56dbc4b75550",
                Code = "c6bfe670761546b29b1f50719912c600d26fe9d477c1d1eec9c300700bd95f43"
            };

            //var t = Authentication.RequestAccessTokenWithCode(ApiBaseUrl, credentials);

            // token for user
            var token = Authentication.RequestAccessToken(ApiBaseUrl, credentials).Result;
            var context = new SignNowContext(ApiBaseUrl, token);

            var foldersResponse = await context.Documents.GetFoldersAsync();

            if (string.IsNullOrEmpty(folderId))
            {
                folderId = foldersResponse.Folders.FirstOrDefault().Id;
            }
            var documentsResponse = await context.Documents.GetFolderAsync(folderId);

            var model = new FolderModel();
            model.Folders = foldersResponse.Folders;
            model.Documents = documentsResponse.Documents;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<FileResult> DownloadFile(string documentId)
        {
            var credentials = new CredentialModel
            {
                Login = "zoran.petrushev@it-labs.com",
                Password = "P@ssw0rd123#",
                ClientId = "d52b25d3523516690722cbe97a31526f",
                ClientSecret = "8eae7c60c7b030e2873b56dbc4b75550"
            };

            // token for user
            var token = Authentication.RequestAccessToken(ApiBaseUrl, credentials).Result;
            var context = new SignNowContext(ApiBaseUrl, token);

            var downloadDocument = await context.Documents
                .DownloadDocumentAsync(documentId, DownloadType.PdfWithHistory);

            //Send the File to Download.
            return File(downloadDocument.Document, "application/octet-stream", downloadDocument.Filename);
        }
    }
}
