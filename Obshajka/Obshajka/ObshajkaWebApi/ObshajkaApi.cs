﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Models;
using Obshajka.Mocks;
using Obshajka.UserSettings;
using System.Net.Http.Json;
using Obshajka.Exceptions;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;

namespace Obshajka.ObshajkaWebApi
{
    public static class ObshajkaApi
    {

        static HttpClient httpClient = new HttpClient();

        // TODO: чекнуть моки

        public record EmailWithPassword(string Email, string Password);
        public static async Task<long> AuthorizeUser(string email, string password)
        {
            if (UserSettings.UserSettings.UseMocks)
            {
                return 1;
            }
            var emailWithPassword = new EmailWithPassword(email, password);
            using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.Authorization, emailWithPassword);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await response.Content.ReadFromJsonAsync<long>();
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new IncorrectLoginOrPasswordException("Неверные имя пользователя или пароль.");
                default:
                    throw new Exception("Неизвестная ошибка. Попробуйте позже.");
            }
        }

        public static async void RegisterUser(string name, string email, string password)
        {
            if (UserSettings.UserSettings.UseMocks)
            {
                return;
            } 
            else
            {
                var newUser = new NewUser { Email= email, Password = password, Name = name };
                using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.SendVerificationCode, newUser);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return;
                    case System.Net.HttpStatusCode.Conflict:
                        throw new AlreadyRegisteredException("Пользователь с такой почтой уже зарегистрирован!");
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new SendVerificationCodeException("Не удалось отправить код верификации: проверьте корректность введенных данных.");
                    default:
                        throw new SendVerificationCodeException("Не удалось отправить код верификации. Попробуйте позже."); 
                }
            }
        }

        // private record VerificationCodeWithEmail(string Email, int VerificationCode);
        public record VerificationCodeWithEmail(string Email, string VerificationCode);

        // возвращает Id польхователя
        public static async Task<long> ConfirmVerificationCode(string email, string code)
        {
            // Mock - userID
            if (UserSettings.UserSettings.UseMocks)
            {
                return 1;
            }
            var verificationCodeWithEmail = new VerificationCodeWithEmail(email, code);
            using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.ConfirmVerificationCode, verificationCodeWithEmail);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await response.Content.ReadFromJsonAsync<long>(); 
                case System.Net.HttpStatusCode.BadRequest:
                    throw new SendVerificationCodeException("Введенный код не совпадает с тем, который был отправлен на почту.");
                default:
                    throw new Exception("Неизвестная ошибка. Попробуйте позже.");
            }
        }

        public static async Task<List<Advertisement>> GetAdvertisementsFromOtherUsers(long dormitoryId, long currentUserId)
        {
            if (UserSettings.UserSettings.UseMocks)
            {
                return MocksClass.GetAdvertisementsFromOtherUsers_Mock(dormitoryId, currentUserId);
            }
            var connectionString = $"{ConnectionSettings.GetOutsideAdvertisements}/{dormitoryId}/{currentUserId}";
            using var response = await httpClient.GetAsync(connectionString);
            // using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.SendVerificationCode, newUser);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await response.Content.ReadFromJsonAsync<List<Advertisement>>();
                case System.Net.HttpStatusCode.NotFound:
                    throw new AlreadyRegisteredException("Объявлений не найдено :(");
                default:
                    throw new Exception("Неизвестная ошибка. Попробуйте позже.");
            }
        }

        public static async Task<List<Advertisement>> GetUserAdvertisements(long userId)
        {
            if (UserSettings.UserSettings.UseMocks)
            {
                return MocksClass.GetAdvertisementsFromCurrentUser_Mock(userId);
            }
            var connectionString = $"{ConnectionSettings.GetUserAdvertisements}/{userId}";
            using var response = await httpClient.GetAsync(connectionString);
            // using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.SendVerificationCode, newUser);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await response.Content.ReadFromJsonAsync<List<Advertisement>>();
                case System.Net.HttpStatusCode.NotFound:
                    throw new AlreadyRegisteredException("Объявлений не найдено :(");
                default:
                    throw new Exception("Неизвестная ошибка. Попробуйте позже.");
            }
        }

        //public static List<Advertisement> GetAdvertisementsFromCurrentUser(long userId)
        //{
        //    return MocksClass.GetAdvertisementsFromCurrentUser_Mock(userId);
        //}

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
