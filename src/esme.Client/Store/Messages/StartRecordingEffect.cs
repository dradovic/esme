using Blazor.Fluxor;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class StartRecordingEffect : Effect<StartRecordingAction>
    {
        private readonly IJSRuntime _jsRuntime;

        public StartRecordingEffect(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task HandleAsync(StartRecordingAction action, IDispatcher dispatcher)
        {
            await _jsRuntime.InvokeAsync<object>("esme_startRecording");
        }
    }
}
