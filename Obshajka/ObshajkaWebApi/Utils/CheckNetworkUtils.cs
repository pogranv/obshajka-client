using ObshajkaWebApi.Exceptions;
using Xamarin.Essentials;

namespace ObshajkaWebApi.Utils
{
    internal static class CheckNetworkUtils
    {
        public static void CheckNetworkAccess()
        {
            NetworkAccess accessType = Connectivity.NetworkAccess;
            if (accessType != NetworkAccess.Internet)
            {
                throw new NetworkUnavailableException("Проверьте подключение к интернету");
            }
        }
    }
}
