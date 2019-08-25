using esme.Shared.Invitations;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class InvitationBase : ComponentBase
    {
        [Parameter]
        public InvitationViewModel Invitation { get; set; }
    }
}
