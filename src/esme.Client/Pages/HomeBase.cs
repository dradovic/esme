using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class HomeBase : ComponentBase
    {
        protected CircleViewModel[] Circles { get; private set; }

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitAsync()
        {
            Circles = await Http.GetJsonAsync<CircleViewModel[]>("api/my/circles");
        }
    }
}
