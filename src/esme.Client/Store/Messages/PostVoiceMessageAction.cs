using System;
using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageAction : IAction
    {
        public PostVoiceMessageAction(int circleId, string recordingUrl)
        {
            CircleId = circleId;
            RecordingUrl = recordingUrl;
        }

        public int CircleId { get; }
        public string RecordingUrl { get; }
    }
}
