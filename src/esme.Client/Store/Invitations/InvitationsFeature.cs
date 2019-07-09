using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class InvitationsFeature : Feature<InvitationsState>
    {
        public override string GetName() => "Invitations";

        protected override InvitationsState GetInitialState() => new InvitationsState(
            isLoading: false,
            errorMessage: null,
            invitations: null);

    }
}
