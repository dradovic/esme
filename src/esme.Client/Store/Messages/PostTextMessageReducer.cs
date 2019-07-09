using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageReducer : PostMessageReducer<PostTextMessageAction>, IReducer<MessagesState>
    {
        protected override MessageViewModel CreateViewModel(PostTextMessageAction action)
        {
            return new MessageViewModel
            {
                Id = action.Message.Id,
                ContentType = ContentType.Text,
                Content = action.Message.Text,
                SentByMe = true,
            };
        }
    }
}
