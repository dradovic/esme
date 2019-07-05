using Blazor.Fluxor;

namespace esme.Client.Store.Application
{
    public class EnterReducer : Reducer<ApplicationState, EnterAction>
    {
        public override ApplicationState Reduce(ApplicationState state, EnterAction action)
        {
            return new ApplicationState(
                state: State.Entered
            );
        }
    }
}
