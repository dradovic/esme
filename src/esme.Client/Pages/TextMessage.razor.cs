using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class TextMessageBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
    }
}
