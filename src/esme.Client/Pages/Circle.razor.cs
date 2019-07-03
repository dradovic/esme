using Blazor.Fluxor;
using esme.Client.Store.Messages;
using esme.Shared.Circles;
using esme.Shared.Events;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        protected TextMessageEditModel NewMessage { get; private set; } = new TextMessageEditModel();

        [Parameter]
        protected Guid CircleId { get; set; }

        private Stopwatch _recordingWatch;
        private Timer _timer;
        protected string RecordingTime { get; private set; }

        private bool _postVoiceMessageWhenAvailable;

        protected override void OnInit()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            EventAggregator.Subscribe(this);
            MessagesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchInitialMessagesAction(CircleId));
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (_postVoiceMessageWhenAvailable && MessagesState.Value.State == State.RecordingAvailable)
            {
                _postVoiceMessageWhenAvailable = false;
                PostVoiceMessage();
                return;
            }
            await JSRuntime.InvokeAsync<object>("esme_scroll_messages");
        }

        protected void StartRecording()
        {
            Dispatcher.Dispatch(new StartRecordingAction());
            _recordingWatch = Stopwatch.StartNew();
            _timer = new Timer(new TimerCallback(_ =>
            {
                RecordingTime = _recordingWatch.Elapsed.ToString("mm':'ss");
                StateHasChanged();
            }), null, 0, 1000);
        }

        protected void StopRecording()
        {
            Dispatcher.Dispatch(new StopRecordingAction());
            _recordingWatch.Stop();
            _timer.Dispose();
        }

        private void PostVoiceMessage()
        {
            Dispatcher.Dispatch(new PostVoiceMessageAction(CircleId, MessagesState.Value.RecordingUrl, new VoiceMessageEditModel()));
        }

        protected void OnSubmit()
        {
            if (MessagesState.Value.State == State.Default)
            {
                Dispatcher.Dispatch(new PostTextMessageAction(CircleId, NewMessage));
                NewMessage = new TextMessageEditModel(); // start over
            }
            else if (MessagesState.Value.State == State.IsRecording)
            {
                _postVoiceMessageWhenAvailable = true;
                StopRecording();
            }
            else if (MessagesState.Value.State == State.RecordingAvailable)
            {
                PostVoiceMessage();
            }
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

        protected IEnumerable<object> MessagesWithDateLabels()
        {
            return MessagesState.Value.Messages.WithDateLabels();
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
