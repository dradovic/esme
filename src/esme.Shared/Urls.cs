using System;

namespace esme.Shared
{
    public static class Urls
    {
        public const string PostLogin = "api/authorization/login";
        public const string PostSignup = "api/authorization/signup";
        public const string PostLogout = "api/authorization/logout";
        public const string GetMe = "api/authorization/me";

        public const string PostReadMessages = "api/my/messages/actions/read";
        public const string PostTextMessage = "api/my/messages/text";
        public const string PostVoiceMessage = "api/my/messages/voice";

        public const string GetInvitations = "api/my/invitations";
        public const string PostInvitation = "api/my/invitations";

        public static string GetPostReadMessages(Guid circleId) => $"{PostReadMessages}?circleId={circleId}";
        public static string GetPostTextMessageUrl(Guid circleId) => $"{PostTextMessage}?circleId={circleId}";
        public static string GetPostVoiceMessageUrl(Guid circleId) => $"{PostVoiceMessage}?circleId={circleId}";
    }
}
