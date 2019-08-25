using Microsoft.AspNetCore.Components;

namespace esme.Client.Shared
{
    public class FlowerOfLifeBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
