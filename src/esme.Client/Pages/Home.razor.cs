using Blazor.Fluxor;
using esme.Client.Store.Circles;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class HomeBase : ComponentBase
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<CirclesState> CirclesState { get; private set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override void OnInit()
        {
            CirclesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchCirclesAction());
        }

        protected void CircleClick(CircleViewModel circle)
        {
            UriHelper.NavigateTo($"/circle/{circle.Id}");
        }
    }
}
