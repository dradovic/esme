using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class VoiceMessageBase : ComponentBase
    {
        [Parameter]
        protected string RecordingUrl { get; set; }
    }
}
