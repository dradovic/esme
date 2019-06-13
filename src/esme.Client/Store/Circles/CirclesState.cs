using esme.Shared.Circles;

namespace esme.Client.Store.Circles
{
    public class CirclesState
    {
        public bool IsLoading { get; }
        public string ErrorMessage { get; }
        public CircleViewModel[] Circles { get; }

        public CirclesState(bool isLoading, string errorMessage, CircleViewModel[] circles)
        {
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            Circles = circles;
        }
    }
}
