using SignNow.Net.Interfaces;

namespace SignNow.Client.ItLabs.Services
{
    public interface IExtendedSignNowContext
    {
        /// <inheritdoc cref="IDocumentService"/>
        IExtendedDocumentService Documents { get; }

        /// <inheritdoc cref="IUserService"/>
        IUserService Users { get; }

        /// <inheritdoc cref="ISignInvite"/>
        ISignInvite Invites { get; }
    }
}
