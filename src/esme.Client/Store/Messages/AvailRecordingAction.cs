using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class AvailRecordingAction : IAction
    {
        public AvailRecordingAction(string recordingUrl)
        {
            RecordingUrl = recordingUrl;
        }

        public string RecordingUrl { get; }
    }
}
