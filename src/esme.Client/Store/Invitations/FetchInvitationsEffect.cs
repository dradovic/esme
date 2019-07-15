using Blazor.Fluxor;
using esme.Shared.Invitations;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsEffect : Effect<FetchInvitationsAction>
    {
        private readonly HttpClient _httpClient;

        public FetchInvitationsEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(FetchInvitationsAction action, IDispatcher dispatcher)
        {
            try
            {
                var invitations = await _httpClient.GetJsonAsync<InvitationViewModel[]>("api/my/invitations");
                dispatcher.Dispatch(new FetchInvitationsSucceededAction(invitations.ToList()));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new FetchInvitationsFailedAction(errorMessage: x.Message));
            }
        }
    }
}
