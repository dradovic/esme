using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase
    {
        [Parameter]
        protected int Id { get; set; }
    }
}
