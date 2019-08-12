using Blazor.Fluxor;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class StopRecordingEffect : Effect<StopRecordingAction>
    {
        private readonly IJSRuntime _jsRuntime;

        public StopRecordingEffect(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task HandleAsync(StopRecordingAction action, IDispatcher dispatcher)
        {
            var blobUrl = await _jsRuntime.InvokeAsync<string>("esme_stopRecording");
            dispatcher.Dispatch(new AvailRecordingAction(blobUrl));
        }
    }
}
