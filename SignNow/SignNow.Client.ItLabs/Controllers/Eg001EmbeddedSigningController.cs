using Microsoft.AspNetCore.Mvc;
using SignNow.Client.ItLabs.Model;
using SignNow.Client.ItLabs.Models;
using SignNow.Client.ItLabs.Services;
using SignNow.Net;
using SignNow.Net.Model;
using SignNow.Net.Model.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Controllers
{
    [Route("eg001")]
    public class Eg001EmbeddedSigningController : EgController
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


        public Eg001EmbeddedSigningController(DSConfiguration config)
            : base(config)
        {
            dsPingUrl = config.AppUrl + "/";
            dsReturnUrl = config.AppUrl + "/dsReturn";
            ViewBag.title = "Embedded Signing Ceremony";
        }

        public override string EgName => "eg001";

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

            using (var fileStream = System.IO.File.OpenRead(@"D:\_it-labs projects\Signers\Invoice - tags.pdf"))
            {
                var rd = new Random();
                var rand_num = rd.Next(100, 999);

                var uploadResponse = await context.Documents.UploadDocumentWithFieldExtractAsync(fileStream, $"Invoice-{rand_num}.pdf");

                documentId = uploadResponse.Id;
            };

            //var fields = new List<UpdateDocumentFields>
            //{
            //    new UpdateDocumentFields
            //    {
            //        X = 50,
            //        Y = 710,
            //        Width = 129,
            //        Height = 25,
            //        PageNumber = 0,
            //        Name = "Signature_1",
            //        Required = true,
            //        RoleName = "Signer 1",
            //        Type = FieldType.Signature
            //    },
            //    //new UpdateDocumentFields
            //    //{
            //    //    X = 450,
            //    //    Y = 710,
            //    //    Width = 129,
            //    //    Height = 25,
            //    //    PageNumber = 0,
            //    //    Name = "Signature_2",
            //    //    Required = true,
            //    //    RoleName = "Signer 2",
            //    //    Type = FieldType.Signature
            //    //}
            //};

            //var updateDocumentResponse = await context.Documents.UpdateDocumentFieldsAsync(documentId, fields);

            var document = await context.Documents.GetDocumentAsync(documentId);

            var email = "zoran.petrusev@gmail.com";
            //var email2 = "zoran.petrushev@it-labs.com";

            ////////############################ email invites #####################################
            //var invite = new RoleBasedInvite(document)
            //{
            //    Message = $"IT Labs invited you to sign the document {document.Name}",
            //    Subject = "Document signature required"
            //};

            //// Creates options for signers
            //var signer = new SignerOptions(email, invite.DocumentRoles().ElementAt(0))
            //{
            //    ExpirationDays = 15,
            //    RemindAfterDays = 7,
            //}.SetAuthenticationByPassword("pass123");

            //var signer2 = new SignerOptions(email2, invite.DocumentRoles().ElementAt(1))
            //{
            //    ExpirationDays = 15,
            //    RemindAfterDays = 7,
            //}.SetAuthenticationByPassword("pass123");

            //// Attach signer to existing roles in the document
            //invite.AddRoleBasedInvite(signer);
            //invite.AddRoleBasedInvite(signer2);

            //var inviteResponse = await context.Invites.CreateInviteAsync(documentId, invite);

            //Console.WriteLine(inviteResponse.Status);
            //Console.WriteLine("Waiting signers");
            //Console.ReadKey();
            ////################################################################################

            ////############################ embedded invites #####################################
            var embeddedInvite = new EmbeddedSigningInvite(document);

            embeddedInvite.AddEmbeddedSigningInvite(
                new EmbeddedInvite
                {
                    Email = email,
                    RoleId = document.Roles[0].Id,
                    SigningOrder = 1,
                    AuthMethod = EmbeddedAuthType.None
                });

            //embeddedInvite.AddEmbeddedSigningInvite(
            //    new EmbeddedInvite
            //    {
            //        Email = email2,
            //        RoleId = document.Roles[1].Id,
            //        SigningOrder = 2,
            //        AuthMethod = EmbeddedAuthType.None
            //    });

            var embeddedInviteResponse = await context.Invites.CreateInviteAsync(documentId, embeddedInvite);

            document = await context.Documents.GetDocumentAsync(documentId);

            var expires = 30;
            var options = new CreateEmbedLinkOptions
            {
                FieldInvite = document.FieldInvites.ElementAt(0),
                LinkExpiration = (uint)expires,
                AuthMethod = EmbeddedAuthType.None
            };

            var embeddedInviteLinkResponse = await context.Invites.GenerateEmbeddedInviteLinkAsync(documentId, options);
            ////################################################################################


            //var downloadDocument = await context.Documents
            //    .DownloadDocumentAsync(documentId, DownloadType.PdfWithHistory);

            //var fileName = @$"D:\_it-labs projects\Signers\{downloadDocument.Filename}";

            //using (var outputFileStream = new FileStream(fileName, FileMode.Create))
            //{
            //    downloadDocument.Document.CopyTo(outputFileStream);
            //}


        
            // Redirect the user to the Signing Ceremony
            return Redirect(embeddedInviteLinkResponse.Link.ToString());
        }
    }
}
