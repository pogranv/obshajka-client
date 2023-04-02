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
            // TODO: что это?
            UserSettings.UserSettings.SelectedDormitoryIdFilter = 1;
        }

        public static void RegisterUser(string name, string email, string password)
        { 
            UserSettings.UserSettings.UserId = ObshajkaApi.RegisterUser(name,email, password);
            UserSettings.UserSettings.SelectedDormitoryIdFilter = 1;
        }

        public static void ConfirmVerificationCode(string code)
        {
            UserSettings.UserSettings.UserId = ObshajkaApi.ConfirmVerificationCode(code);
        }

        public static IList<Advertisement> GetAdvertisementsFromOthers(int dormitoryId)
        {
            return ObshajkaApi.GetAdvertisementsFromOtherUsers(dormitoryId, UserSettings.UserSettings.UserId);
        }

        public static IList<Advertisement> GetUserAdvertisements()
        {
            return ObshajkaApi.GetAdvertisementsFromCurrentUser(UserSettings.UserSettings.UserId);
        }

        public static void RemoveOwnAdvert(long advertId)
        {
            ObshajkaApi.RemoveAdvertisement(advertId);
        }

        public static void PublishAdvert(Advertisement advert)
        {
            ObshajkaApi.PublishAdvertisement(advert);
        }
    }
}
