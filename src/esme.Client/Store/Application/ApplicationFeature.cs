using Blazor.Fluxor;

namespace esme.Client.Store.Application
{
    public class ApplicationFeature : Feature<ApplicationState>
    {
        public override string GetName() => "Application";

        protected override ApplicationState GetInitialState() => new ApplicationState(
            state: Application.State.Loaded
        );
    }
}
