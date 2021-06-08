using Microsoft.AspNetCore.Mvc;
using SignNow.Client.ItLabs.Model;
using SignNow.Client.ItLabs.Models;
using SignNow.Client.ItLabs.Services;
using SignNow.Net;
using SignNow.Net.Model;
using SignNow.Net.Model.Requests;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Controllers
{
    [Route("eg002")]
    public class Eg002AuthorizationCodeController : EgController
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


        public Eg002AuthorizationCodeController(DSConfiguration config)
            : base(config)
        {
            dsPingUrl = config.AppUrl + "/";
            dsReturnUrl = config.AppUrl + "/dsReturn";
            ViewBag.title = "Embedded Signing Ceremony";
        }

        public override string EgName => "eg002";

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var authorizationCode = "c6bfe670761546b29b1f50719912c600d26fe9d477c1d1eec9c300700bd95f43";
            var clientId = "d52b25d3523516690722cbe97a31526f";
            var authorizationCodeLink = $"https://app-eval.signnow.com/authorize?client_id={clientId}&response_type=code&redirect_uri=http://localhost:2337/eg002";
            
            // Redirect the user to the Signing Ceremony
            return Redirect(authorizationCodeLink);
        }
    }
}
