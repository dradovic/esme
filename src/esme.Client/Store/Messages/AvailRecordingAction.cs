namespace esme.Client.Store.Messages
{
    public class AvailRecordingAction
    {
        public AvailRecordingAction(string recordingUrl)
        {
            RecordingUrl = recordingUrl;
        }

        public string RecordingUrl { get; }
    }
}
