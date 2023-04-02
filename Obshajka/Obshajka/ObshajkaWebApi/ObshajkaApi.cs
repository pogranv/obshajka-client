using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Models;
using Obshajka.Mocks;

namespace Obshajka.ObshajkaWebApi
{
    public static class ObshajkaApi
    {
        // TODO: чекнуть моки
        public static long AuthorizeUser(string email, string password)
        {
            // Mock
            return 1;
        }

        public static long RegisterUser(string name, string email, string password)
        {
            // Mock
            return 1;
        }

        public static long ConfirmVerificationCode(string code)
        {
            // Mock - userID
            return 1;
        }

        public static List<Advertisement> GetAdvertisementsFromOtherUsers(long dormitoryId, long currentUserId)
        {
            return MocksClass.GetAdvertisementsFromOtherUsers_Mock(dormitoryId, currentUserId);
        }

        public static List<Advertisement> GetAdvertisementsFromCurrentUser(long userId)
        {
            return MocksClass.GetAdvertisementsFromCurrentUser_Mock(userId);
        }

        public static void RemoveAdvertisement(long advertisementId)
        {
            MocksClass.RemoveAdvert(advertisementId);
        }

        public static Advertisement PublishAndGetNewAdvert(Advertisement advertisement)
        {
            return MocksClass.BuplishAndGetNewAdvertisement(advertisement);
        }
    }
}
