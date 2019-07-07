﻿using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class BalloonBase : ComponentBase
    {
        [Parameter]
        protected MessageViewModel Message { get; set; }
    }
}
