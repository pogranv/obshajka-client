using Xamarin.Essentials;

using ObshajkaWebApi.Exceptions;

namespace ObshajkaWebApi.Utils
{
    internal static class CheckNetworkUtils
    {
        /// <summary>
        /// Проверяет подключение к интернету.
        /// </summary>
        /// <exception cref="NetworkUnavailableException"></exception>
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
