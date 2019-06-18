using Blazor.Fluxor;
using esme.Client.Store.Messages;
using esme.Shared.Circles;
using esme.Shared.Events;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using System;
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

        [Parameter]
        protected int Id { get; set; }

        protected override void OnInit()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            EventAggregator.Subscribe(this);
            MessagesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchInitialMessagesAction(Id));
        }

        protected void OnSubmit()
        {
            Dispatcher.Dispatch(new SubmitMessageAction(Id, NewMessage));
            NewMessage = new MessageEditModel();
        }

        public Task HandleAsync(MessagePostedEvent message)
        {
            Dispatcher.Dispatch(new FetchUnreadMessagesAction(message.CircleId));
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
