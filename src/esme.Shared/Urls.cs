namespace esme.Shared
{
    public static class Urls
    {
        public const string PostReadMessages = "api/my/messages/actions/read";
        public const string PostTextMessage = "api/my/messages/text";
        public const string PostVoiceMessage = "api/my/messages/voice";

        public static string GetPostReadMessages(int circleId) => $"{PostReadMessages}?circleId={circleId}";
        public static string GetPostTextMessageUrl(int circleId) => $"{PostTextMessage}?circleId={circleId}";
        public static string GetPostVoiceMessageUrl(int circleId) => $"{PostVoiceMessage}?circleId={circleId}";
    }
}
