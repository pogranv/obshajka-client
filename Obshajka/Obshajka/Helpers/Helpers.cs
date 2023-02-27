using Obshajka.Models;
using Obshajka.ObshajkaWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.Helpers
{
    public static class Helpers
    {
        public static void LogInUser(string email, string password)
        {
            UserSettings.UserSettings.UserId = ObshajkaApi.AuthorizeUser(email, password);
        }

        public static List<Advertisement> GetAdvertisements(int dormitoryId)
        {
            return ObshajkaApi.GetAdvertisements();
        }
    }
}
