using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class TextMessageBase : ComponentBase
    {
        [Parameter]
        protected string Text { get; set; }
    }
}
