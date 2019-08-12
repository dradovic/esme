using Blazor.Fluxor;
using esme.Client.Services;
using esme.Shared;
using esme.Shared.Invitations;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationEffect : Effect<PostInvitationAction>
    {
        private readonly HttpClient _httpClient;

        public PostInvitationEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(PostInvitationAction action, IDispatcher dispatcher)
        {
            var postedInvitation = await _httpClient.PostAsync<InvitationViewModel>(Urls.PostInvitation, action.Invitation, errorMessage =>
            {
                dispatcher.Dispatch(new PostInvitationFailedAction(action.Invitation, errorMessage));
            });
            dispatcher.Dispatch(new PostInvitationSucceededAction(postedInvitation));
        }
    }
}
