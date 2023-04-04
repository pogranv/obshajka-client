using Obshajka.Models;
using Obshajka.ObshajkaWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Obshajka.Exceptions;

namespace Obshajka.Helpers
{
    public static class Helpers
    {
        public static async void TryAutorizeUser(string email, string password)
        {
            try
            {
                UserSettings.UserSettings.UserId = await ObshajkaApi.AuthorizeUser(email, password);
            } catch (Exception ex)
            {
                throw;
            }
            
        }

        public static void TryRegisterUser(string name, string email, string password)
        {
            try
            {
                ObshajkaApi.RegisterUser(name, email, password);
            } catch (Exception ex)
            {
                throw;
            }
        }

        public static async void TryConfirmVerificationCode(string email, string code)
        {
            try
            {
                UserSettings.UserSettings.UserId = await ObshajkaApi.ConfirmVerificationCode(email, code);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<IList<Advertisement>> TryGetOutsideAdvertisements(int dormitoryId)
        {
            try
            {
                IList<Advertisement> adverts = await ObshajkaApi.GetAdvertisementsFromOtherUsers(dormitoryId, UserSettings.UserSettings.UserId);
                return adverts;
            } catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<IList<Advertisement>> TryGetUserAdvertisements()
        {
            try
            {
                return await ObshajkaApi.GetUserAdvertisements(UserSettings.UserSettings.UserId);
            } catch (Exception ex)
            {
                throw;
            }
            
        }

        public static void RemoveOwnAdvert(long advertId)
        {
            ObshajkaApi.RemoveAdvertisement(advertId);
        }

        public static Advertisement PublishAndGetNewAdvert(Advertisement advert)
        {
            return ObshajkaApi.PublishAndGetNewAdvert(advert);
        }
    }
}
