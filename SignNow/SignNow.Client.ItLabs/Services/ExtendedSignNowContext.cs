using SignNow.Net.Interfaces;
using SignNow.Net.Model;
using SignNow.Net.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Services
{
    public class ExtendedSignNowContext : AuthorizedWebClientBase, IExtendedSignNowContext
    {
        public IUserService Users { get; protected set; }

        public ISignInvite Invites { get; protected set; }

        public IExtendedDocumentService Documents { get; protected set; }

        public ExtendedSignNowContext(Uri baseApiUrl, Token token) : this(baseApiUrl, token, null)
        {
        }

        protected ExtendedSignNowContext(Uri baseApiUrl, Token token, ISignNowClient signNowClient) : base(baseApiUrl, token, signNowClient)
        {
            Users = new ExtendedUserService(baseApiUrl, token, signNowClient);
            Invites = (ISignInvite)Users;
            Documents = new ExtendedDocumentService(baseApiUrl, token, signNowClient);
        }
    }
}
