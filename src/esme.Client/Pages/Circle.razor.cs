using Blazor.Fluxor;
using esme.Client.Store.Messages;
using esme.Shared.Circles;
using esme.Shared.Events;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase, IHandle<MessagePostedEvent>, IDisposable
    {
        [Inject]
        private IEventAggregator EventAggregator { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<MessagesState> MessagesState { get; private set; }

        protected MessageEditModel NewMessage { get; private set; } = new MessageEditModel();

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Parameter]
        protected int Id { get; set; }

        protected override void OnInit()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            EventAggregator.Subscribe(this);
            MessagesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchInitialMessagesAction(Id));
        }

        protected async Task Record()
        {
            await JSRuntime.InvokeAsync<object>("esme_record");
        }

        protected void OnSubmit()
        {
            Dispatcher.Dispatch(new PostMessageAction(Id, NewMessage));
            NewMessage = new MessageEditModel();
        }

        public Task HandleAsync(MessagePostedEvent messagePostedEvent)
        {
            if (MessagesState.Value.Messages != null && MessagesState.Value.Messages.Any(m => m.Id == messagePostedEvent.MessageId))
            {
                // this is the message that this user has posted, so no need to fetch it
                return Task.CompletedTask;
            }

            Dispatcher.Dispatch(new FetchUnreadMessagesAction(messagePostedEvent.CircleId));
            return Task.CompletedTask;
        }

        #region IDisposable Support

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    EventAggregator.Unsubscribe(this);
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
