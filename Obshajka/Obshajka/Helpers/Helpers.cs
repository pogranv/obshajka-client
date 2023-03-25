using Obshajka.Models;
using Obshajka.ObshajkaWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Obshajka.Helpers
{
    public static class Helpers
    {
        public static void LogInUser(string email, string password)
        {
            UserSettings.UserSettings.UserId = ObshajkaApi.AuthorizeUser(email, password);
            UserSettings.UserSettings.SelectedDormitoryIdFilter = 1;
        }

        public static IList<Advertisement> GetAdvertisementsFromOthers()
        {
            return ObshajkaApi.GetAdvertisementsFromOtherUsers(UserSettings.UserSettings.SelectedDormitoryIdFilter, UserSettings.UserSettings.UserId);
        }

        public static IList<Advertisement> GetUserAdvertisements()
        {
            return ObshajkaApi.GetAdvertisementsFromCurrentUser(UserSettings.UserSettings.UserId);
        }
    }
}
