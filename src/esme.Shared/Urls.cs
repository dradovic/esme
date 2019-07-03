using System;

namespace esme.Shared
{
    public static class Urls
    {
        public const string PostReadMessages = "api/my/messages/actions/read";
        public const string PostTextMessage = "api/my/messages/text";
        public const string PostVoiceMessage = "api/my/messages/voice";

        public static string GetPostReadMessages(Guid circleId) => $"{PostReadMessages}?circleId={circleId}";
        public static string GetPostTextMessageUrl(Guid circleId) => $"{PostTextMessage}?circleId={circleId}";
        public static string GetPostVoiceMessageUrl(Guid circleId) => $"{PostVoiceMessage}?circleId={circleId}";
    }
}
