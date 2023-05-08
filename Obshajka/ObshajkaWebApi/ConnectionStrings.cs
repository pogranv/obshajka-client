namespace ObshajkaWebApi
{
    internal static class ConnectionStrings
    {
        public static string BaseAddress { get; } = "http://84.252.138.220";
        public static string SendVerificationCode { get; } = "/api/v1/reg/verification";
        public static string ConfirmVerificationCode { get; } = "/api/v1/reg/confirmation";
        public static string Authorization { get; } = "/api/v1/auth/authorize";
        public static string GetOutsideAdvertisements { get; } = "/api/v1/adverts/outsides";
        public static string GetUserAdvertisements { get; } = "/api/v1/adverts/my";
        public static string DeleteAdvertisement { get; } = "/api/v1/adverts/remove";
        public static string PublishAdvertisement { get; } = "/api/v1/adverts/add";
    }
}
