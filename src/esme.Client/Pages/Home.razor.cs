using Blazor.Fluxor;
using esme.Client.Store.Circles;
using esme.Shared.Circles;
using esme.Shared.Events;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    [Authorize]
    public abstract class HomeBase : ComponentBase, IHandle<MessagePostedEvent>, IDisposable
    {
        [Inject]
        private IEventAggregator EventAggregator { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<CirclesState> CirclesState { get; private set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override void OnInit()
        {
            EventAggregator.Subscribe(this); // FIXME: da, need to unsubscribe?
            CirclesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchCirclesAction());
        }

        protected void CircleClick(CircleViewModel circle)
        {
            UriHelper.NavigateTo($"/circle/{circle.Id}");
        }

        public Task HandleAsync(MessagePostedEvent message)
        {
            Dispatcher.Dispatch(new IncrementUnreadMessagesAction(message.CircleId));
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
