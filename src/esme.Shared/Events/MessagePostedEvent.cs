using System;
using System.Text.Json.Serialization;

namespace esme.Shared.Events
{
    public class MessagePostedEvent
    {
        [JsonPropertyName("circleid")] // should become obsolete again (see: https://github.com/BlazorExtensions/SignalR/issues/39)
        public Guid CircleId { get; set; }

        [JsonPropertyName("messageid")] // should become obsolete again (see: https://github.com/BlazorExtensions/SignalR/issues/39)
        public Guid MessageId { get; set; }
    }
}
