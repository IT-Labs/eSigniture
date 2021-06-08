using SignNow.Net.Interfaces;
using SignNow.Net.Model;
using SignNow.Net.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Services
{
    public class ExtendedUserService : UserService
    {
        protected internal ExtendedUserService(Uri baseApiUrl, Token token, ISignNowClient client) : base(baseApiUrl, token, client)
        {
        }
    }
}
