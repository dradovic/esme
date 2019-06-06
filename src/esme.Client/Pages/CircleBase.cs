using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase
    {
        protected MessageViewModel[] Messages { get; private set; }
        protected MessageEditModel NewMessage { get; private set; } = new MessageEditModel();

        [Parameter]
        protected int Id { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitAsync()
        {
            Messages = await Http.GetJsonAsync<MessageViewModel[]>($"api/my/messages?circleId={Id}");
        }

        protected async Task OnSubmit()
        {
            await Http.PostJsonAsync($"api/my/messages?circleId={Id}", NewMessage); // FIXME: da, handle failure
        }
    }
}
