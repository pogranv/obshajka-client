﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Models;
using ObshajkaWebApi.Mocks;
using ObshajkaWebApi.Interfaces;

namespace ObshajkaWebApi
{
    public class ObshajkaClient
    {
        // todo: удалить
        private bool useMocks = true;

        // TODO: исправить типы экспешинов
        static ObshajkaClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new(ConnectionStrings.BaseAddress)
            };
        }

        // TODO: чекнуть моки
        public async Task<long> AuthorizeUser(string email, string password)
        {
            if (useMocks)
            {
                return 1;
            }
            var emailWithPassword = new EmailWithPassword(email, password);
            using var response = await _httpClient.PostAsJsonAsync(ConnectionStrings.Authorization, emailWithPassword);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<long>();
            }

            string message;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    message = "Неверные имя пользователя или пароль.";
                    break;
                default:
                    message = "Неизвестная ошибка. Попробуйте позже.";
                    break;
            }
            throw new FailAuthorizeException(message);
        }
        public async void RegisterUser(string name, string email, string password)
        {
            if (useMocks)
            {
                return;
            }
            else
            {
                var newUser = new User { Email = email, Password = password, Name = name };
                using var response = await _httpClient.PostAsJsonAsync(ConnectionStrings.SendVerificationCode, newUser);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return;
                }

                string message;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Conflict:
                        message = "Пользователь с такой почтой уже зарегистрирован!";
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        message = "Не удалось отправить код верификации: проверьте корректность введенных данных.";
                        break;
                    default:
                        message = "Не удалось отправить код верификации. Попробуйте позже.";
                        break;
                }
                throw new FailRegisterUserException(message);
            }
        }

        public async Task<long> ConfirmVerificationCode(string email, string code)
        {
            // Mock - userID
            if (useMocks)
            {
                return 1;
            }
            var verificationCodeWithEmail = new VerificationCodeWithEmail(email, code);
            using var response = await _httpClient.PostAsJsonAsync(ConnectionStrings.ConfirmVerificationCode, verificationCodeWithEmail);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<long>();
            }

            string message;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    message = "Введенный код не совпадает с тем, который был отправлен на почту.";
                    break;
                default:
                    message = "Неизвестная ошибка. Попробуйте позже.";
                    break;
            }
            throw new FailConfirmVerificationCodeException(message);
        }

        public async Task<IEnumerable<IAdvertisement>> GetAdvertisementsFromOtherUsers(long dormitoryId, long currentUserId)
        {
            if (useMocks)
            {
                return MocksClass.GetAdvertisementsFromOtherUsers_Mock(dormitoryId, currentUserId);
            }
            var connectionString = $"{ConnectionStrings.GetOutsideAdvertisements}/{dormitoryId}/{currentUserId}";
            using var response = await _httpClient.GetAsync(connectionString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<Advertisement>>();
            }

            string message;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    message = "Объявлений по запросу не найдено :(";
                    break;
                default:
                    message = "Неизвестная ошибка. Попробуйте позже.";
                    break;
            }

            throw new FailGetAdvertisementsException(message);
        }

        public async Task<IEnumerable<IAdvertisement>> GetUserAdvertisements(long userId)
        {
            if (useMocks)
            {
                return MocksClass.GetAdvertisementsFromCurrentUser_Mock(userId);
            }
            var connectionString = $"{ConnectionStrings.GetUserAdvertisements}/{userId}";
            using var response = await _httpClient.GetAsync(connectionString);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<Advertisement>>();
            }

            string message;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    message = "Объявлений не найдено :(";
                    break;
                default:
                    message = "Неизвестная ошибка. Попробуйте позже.";
                    break;
            }

            throw new FailGetAdvertisementsException(message);
        }

        public async void RemoveAdvertisement(long advertisementId)
        {
            if (useMocks)
            {
                MocksClass.RemoveAdvert(advertisementId);
                return;
            }

            var connectionString = $"{ConnectionStrings.DeleteAdvertisement}/{advertisementId}";
            using var response = await _httpClient.DeleteAsync(connectionString);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }
            throw new ("Не удалось удалить объявление :(");
        }

        public async void PubslishNewAdvert(IPublishingAdvertisement advertisement, string imagePath)
        {
            // TODOOOOOOOOO тут траблики могут быть
            // using Stream stream = !string.IsNullOrEmpty(imagePath) ? System.IO.File.OpenRead(imagePath) : await FileSystem.Current.OpenAppPackageFileAsync("default_advert_image.png");
            using Stream stream = System.IO.File.OpenRead(imagePath);
            var payload = advertisement;

            // using var request = new HttpRequestMessage(HttpMethod.Post, "/api/adverts/add"); // "api/GetAdvertisements/MakeAdvertisement PublishAdvertisement
            using var request = new HttpRequestMessage(HttpMethod.Post, ConnectionStrings.PublishAdvertisement);

            using var content = new MultipartFormDataContent
            {
                // TODO: тут чекнуть
                { new StreamContent(stream), "FileToUpload1", "Test.txt" },

                // payload
                {
                    new StringContent(
                        JsonSerializer.Serialize(payload),
                        Encoding.UTF8,
                        "application/json"),
                    "Data"
                },

            };

            request.Content = content;
            var response = await _httpClient.SendAsync(request);
        }

        private static HttpClient _httpClient;
        private record EmailWithPassword(string Email, string Password);
        private record VerificationCodeWithEmail(string Email, string VerificationCode);

        private static class ConnectionStrings
        {
            public static string BaseAddress { get; } = "http://localhost:80";
            public static string SendVerificationCode { get; } = "/api/reg/verification";
            public static string ConfirmVerificationCode { get; } = "/api/reg/confirmation";
            public static string Authorization { get; } = "/api/auth/authorize";
            public static string GetOutsideAdvertisements { get; } = "/api/adverts/outsides";
            public static string GetUserAdvertisements { get; } = "/api/adverts/my";
            public static string DeleteAdvertisement { get; } = "/api/adverts/remove";
            public static string PublishAdvertisement { get; } = "/api/adverts/add";
        }
    }
}