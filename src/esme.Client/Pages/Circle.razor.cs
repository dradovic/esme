using Blazor.Fluxor;
using esme.Client.Store.Messages;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

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

        protected void OnSubmit()
        {
            Dispatcher.Dispatch(new SubmitMessageAction(Id, NewMessage));
            NewMessage.Text = string.Empty;
        }
    }
}
