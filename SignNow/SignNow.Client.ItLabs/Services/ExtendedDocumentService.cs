using SignNow.Net.Interfaces;
using SignNow.Net.Model;
using SignNow.Net.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignNow.Client.ItLabs.Services
{
    public class ExtendedDocumentService : DocumentService, IExtendedDocumentService
    {
        //public ExtendedDocumentService(Token token) : this(ApiUrl.ApiBaseUrl, token)
        //{
        //}

        /// <inheritdoc cref="DocumentService(Uri, Token, ISignNowClient)"/>
        public ExtendedDocumentService(Uri baseApiUrl, Token token) : base(baseApiUrl, token)
        {
        }

        protected internal ExtendedDocumentService(Uri baseApiUrl, Token token, ISignNowClient signNowClient) : base(baseApiUrl, token, signNowClient)
        {
        }

        public async Task<FoldersResponse> GetFoldersAsync(CancellationToken cancellationToken = default)
        {
            var requestedDocuments = "user/folder";

            var requestOptions = new GetHttpRequestOptions
            {
                RequestUrl = new Uri(ApiBaseUrl, requestedDocuments),
                Token = Token
            };

            return await SignNowClient
                .RequestAsync<FoldersResponse>(requestOptions, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<FolderResponse> GetFolderAsync(string folderId, CancellationToken cancellationToken = default)
        {
            var requestedDocuments = $"folder/{folderId}";

            var requestOptions = new GetHttpRequestOptions
            {
                RequestUrl = new Uri(ApiBaseUrl, requestedDocuments),
                Token = Token
            };

            return await SignNowClient
                .RequestAsync<FolderResponse>(requestOptions, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
