using System;
using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageReducer : PostMessageReducer<PostVoiceMessageAction>, IReducer<MessagesState>
    {
        protected override MessageViewModel CreateViewModel(PostVoiceMessageAction action)
        {
            return new MessageViewModel
            {
                Id = Guid.NewGuid(),
                ContentType = ContentType.Voice,
                Content = action.RecordingUrl,
            };
        }
    }
}
