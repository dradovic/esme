using Blazor.Fluxor;
using esme.Client.Store.Messages;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<MessagesState> MessagesState { get; private set; }

        protected MessageEditModel NewMessage { get; private set; } = new MessageEditModel();

        [Parameter]
        protected int Id { get; set; }

        protected override void OnInit()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            MessagesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchMessagesAction(Id));
        }

        //private async Task OnMessageAdded(int m)
        //{
        //    await ReadMessages();
        //    StateHasChanged();
        //}

        protected async Task OnSubmit()
        {
            //await Http.PostJsonAsync($"api/my/messages?circleId={Id}", NewMessage); // FIXME: da, handle failure
        }
    }
}
