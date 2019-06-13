using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class IndexBase : ComponentBase, IDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync()
        {
            await JSRuntime.InvokeAsync<string>("startAnimation");
        }

        public void Dispose()
        {
            JSRuntime.InvokeAsync<string>("endAnimation");
        }
    }
}
