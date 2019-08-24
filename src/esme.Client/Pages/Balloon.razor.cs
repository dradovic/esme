using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class BalloonBase : ComponentBase
    {
        [Parameter]
        public MessageViewModel Message { get; set; }
    }
}
