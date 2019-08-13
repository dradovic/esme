using System;
using System.Text.Json.Serialization;

namespace esme.Shared.Events
{
    public class MessagePostedEvent
    {
        [JsonPropertyName("circleid")]
        public Guid CircleId { get; set; }

        [JsonPropertyName("messageid")]
        public Guid MessageId { get; set; }
    }
}
