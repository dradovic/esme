using Microsoft.AspNetCore.Components;

namespace esme.Client.Shared
{
    public class FlowerOfLifeBase : ComponentBase
    {
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
    }
}
