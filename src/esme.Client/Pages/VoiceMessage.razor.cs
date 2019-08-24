using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class VoiceMessageBase : ComponentBase
    {
        [Parameter]
        public string RecordingUrl { get; set; }
    }
}
