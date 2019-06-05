using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class HomeBase : ComponentBase
    {
        protected CircleViewModel[] _circles;

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitAsync()
        {
            _circles = await Http.GetJsonAsync<CircleViewModel[]>("api/my/circles");
        }
    }
}
