﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace esme.Client
{
    public abstract class RedirectToHomeBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // check again since we might be here because of a missing user role
            var authenticationState = await AuthenticationState;
            if (authenticationState.User.Identity.IsAuthenticated)
            {
                UriHelper.NavigateTo("/home");
            }
        }
    }
}
