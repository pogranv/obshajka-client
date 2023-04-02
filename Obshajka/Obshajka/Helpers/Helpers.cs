﻿using Obshajka.Models;
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

        public static Advertisement PublishAndGetNewAdvert(Advertisement advert)
        {
            return ObshajkaApi.PublishAndGetNewAdvert(advert);
        }
    }
}
