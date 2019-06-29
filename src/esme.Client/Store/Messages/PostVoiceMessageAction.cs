using System;
using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageAction : IAction
    {
        public PostVoiceMessageAction(int circleId, string recordingUrl, VoiceMessageEditModel message)
        {
            CircleId = circleId;
            RecordingUrl = recordingUrl;
            Message = message;
        }

        public int CircleId { get; }
        public string RecordingUrl { get; }
        public VoiceMessageEditModel Message {get;}
    }
}
