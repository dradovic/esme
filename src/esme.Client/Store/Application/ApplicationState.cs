namespace esme.Client.Store.Application
{
    public enum State
    {
        Loaded,
        Entered
    }

    public class ApplicationState
    {
        public ApplicationState(State state)
        {
            State = state;
        }

        public State State { get; set; }
    }
}
