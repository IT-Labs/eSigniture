using Microsoft.AspNetCore.Mvc;
using SignNow.Client.ItLabs.Model;
using SignNow.Client.ItLabs.Models;
using SignNow.Client.ItLabs.Services;
using SignNow.Net;
using SignNow.Net.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Controllers
{
    [Route("eg003")]
    public class Eg003EmbeddedSigningController : EgController
    {
        private string dsPingUrl;
        private string signerClientId = "1000";
        private string dsReturnUrl;

        private Token token;
        /// SignNow service container used for ExampleRunner
        private SignNowContext context;

        /// <summary>
        /// Document Id which should be deleted after each test
        /// </summary>
        private string disposableDocumentId;

        /// <summary>
        /// SignNow API base Url (sandbox)
        /// </summary>
        private static Uri ApiBaseUrl => new Uri("https://api-eval.signnow.com/");


        public Eg003EmbeddedSigningController(DSConfiguration config)
            : base(config)
        {
            dsPingUrl = config.AppUrl + "/";
            dsReturnUrl = config.AppUrl + "/dsReturn";
            ViewBag.title = "Embedded Signing Ceremony";
        }

        public override string EgName => "eg003";

        [HttpPost]
        public async Task<IActionResult> Create(string signerEmail, string signerName)
        {

            // If you want to use your own credentials just for simple and fast test
            // uncomment next lines bellow and replace placeholders with your credentials:

            var credentials = new CredentialModel
            {
                Login = "zoran.petrushev@it-labs.com",
                Password = "P@ssw0rd123#",
                ClientId = "d52b25d3523516690722cbe97a31526f",
                ClientSecret = "8eae7c60c7b030e2873b56dbc4b75550"
            };

            // Token for user
            token = Authentication.RequestAccessToken(ApiBaseUrl, credentials).Result;
            context = new SignNowContext(ApiBaseUrl, token);

            var documentId = string.Empty;

            //var template = await context.Documents.GetDocumentAsync("2d6b1654b85d4c25bf1f975c0a9b2a980b423bd1");
            var filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\_demo_resources\\";

            using (var fileStream = System.IO.File.OpenRead($"{filePath}\\Invoice - tags.pdf"))
            {
                var rd = new Random();
                var rand_num = rd.Next(100, 999);

                var uploadResponse = await context.Documents.UploadDocumentWithFieldExtractAsync(fileStream, $"Invoice-{rand_num}.pdf");

                documentId = uploadResponse.Id;
            };

            var document = await context.Documents.GetDocumentAsync(documentId);

            //var email = "zoran.petrusev@gmail.com";
            //var email2 = "zoran.petrushev@it-labs.com";

            //////############################ email invites #####################################
            var invite = new RoleBasedInvite(document)
            {
                Message = $"IT Labs invited you to sign the document {document.Name}",
                Subject = "Document signature required"
            };

            // Creates options for signers
            var signer = new SignerOptions(signerEmail, invite.DocumentRoles().ElementAt(0))
            {
                ExpirationDays = 15,
                RemindAfterDays = 7,
            }.SetAuthenticationByPassword("pass123");

            // Attach signer to existing roles in the document
            invite.AddRoleBasedInvite(signer);

            var inviteResponse = await context.Invites.CreateInviteAsync(documentId, invite);

            ////################################################################################

            // Redirect the user to the Signing Ceremony
            return Redirect($"?s={inviteResponse.Status}");
        }
    }
}
