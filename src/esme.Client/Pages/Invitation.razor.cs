using esme.Shared.Invitations;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class InvitationBase : ComponentBase
    {
        [Parameter]
        protected InvitationViewModel Invitation { get; set; }
    }
}
