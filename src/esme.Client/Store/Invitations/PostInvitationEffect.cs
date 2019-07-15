using Blazor.Fluxor;
using esme.Shared;
using esme.Shared.Invitations;
using Microsoft.AspNetCore.Components;
using System;
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
            try
            {
                var postedInvitation = await _httpClient.PostJsonAsync<InvitationViewModel>(Urls.PostInvitation, action.Invitation);
                dispatcher.Dispatch(new PostInvitationSucceededAction(postedInvitation));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new PostInvitationFailedAction(errorMessage: x.Message));
            }
        }
    }
}
