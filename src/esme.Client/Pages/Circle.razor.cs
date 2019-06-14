﻿using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase
    {
        protected List<MessageViewModel> Messages { get; private set; }
        protected MessageEditModel NewMessage { get; private set; } = new MessageEditModel();

        [Parameter]
        protected int Id { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitAsync()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            await ReadMessages();
        }

        //private async Task OnMessageAdded(int m)
        //{
        //    await ReadMessages();
        //    StateHasChanged();
        //}

        private async Task ReadMessages()
        {
            var messages = await Http.PostJsonAsync<IEnumerable<MessageViewModel>>($"api/my/messages/actions/read?circleId={Id}", null);
            Messages = messages.ToList();
        }

        protected async Task OnSubmit()
        {
            await Http.PostJsonAsync($"api/my/messages?circleId={Id}", NewMessage); // FIXME: da, handle failure
        }
    }
}
