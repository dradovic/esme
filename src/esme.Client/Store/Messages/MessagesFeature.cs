using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class MessagesFeature : Feature<MessagesState>
    {
        public override string GetName() => "Messages";

        protected override MessagesState GetInitialState() => new MessagesState(
            isLoading: false,
            errorMessage: null,
            messages: null);
    }
}
