﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Models;
using ObshajkaWebApi.Mocks;
using ObshajkaWebApi.Interfaces;
using ObshajkaWebApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;

namespace ObshajkaWebApi
{
    public class ObshajkaClient
    {
        // todo: удалить
        private bool useMocks = false;
        static ObshajkaClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new(ConnectionStrings.BaseAddress)
            };
        }
        public async Task<long> AuthorizeUser(string email, string password)
        {
            if (useMocks)
            {
                return 1;
            }
            var hashedPassword = HashUtils.GetHashString(password);
            var emailWithPassword = new EmailWithPassword(email, hashedPassword);
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
        public async Task RegisterUser(string name, string email, string password)
        {
            if (useMocks)
            {
                return;
            }
            else
            {
                var hashedPassword = HashUtils.GetHashString(password);
                var user = new User(email, hashedPassword, name);

                using var response = await _httpClient.PostAsJsonAsync(ConnectionStrings.SendVerificationCode, user);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return;
                }

                string message;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Conflict:
                        message = await response.Content.ReadAsStringAsync();
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        message = "Не удалось отправить код верификации: проверьте корректность введенных данных.";
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        message = await response.Content.ReadAsStringAsync();
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
                case System.Net.HttpStatusCode.NotFound:
                    message = "Введенный код не совпадает с тем, который был отправлен на почту или с момента отправки кода прошло больше 5 минут.";
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
                case System.Net.HttpStatusCode.BadRequest:
                    message = "Неверный запрос.";
                    break;
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
                case System.Net.HttpStatusCode.BadRequest:
                    message = "Неверный запрос.";
                    break;
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
            string message;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    message = "Не удалось удалить объявление: объявления не существует";
                    break;
                default:
                    message = "Не удалось удалить объявление: неизвестная ошибка";
                    break;
            }

            throw new FailRemoveAdvertisementException(message);
        }

        public async void PubslishNewAdvertisement(IPublishingAdvertisement advertisement, Stream imageStream/*string imagePath, Stream mystream*/)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, ConnectionStrings.PublishAdvertisement);

            using var content = new MultipartFormDataContent
            {
                // image
                { new StreamContent(imageStream), "Image", "image.jpg" },

                // payload
                { new StringContent(advertisement.CreatorId.ToString()), "Details.CreatorId" },
                { new StringContent(advertisement.Title), "Details.Title" },
                { new StringContent(advertisement.Description), "Details.Description" },
                { new StringContent(advertisement.DormitoryId.ToString()), "Details.DormitoryId" },
                { new StringContent(advertisement.Price.ToString()), "Details.Price" },
            };

            request.Content = content;

            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }
            throw new FailPublishAdvertisementException("Неизвестная ошибка, повторите попытку.");
        }

        private static HttpClient _httpClient;
        private record EmailWithPassword(string Email, string Password);
        private record VerificationCodeWithEmail(string Email, string VerificationCode);

        private static class ConnectionStrings
        {
            public static string BaseAddress { get; } = "https://localhost:7082";// "http://localhost:80";
            public static string SendVerificationCode { get; } = "/api/v1/reg/verification";
            public static string ConfirmVerificationCode { get; } = "/api/v1/reg/confirmation";
            public static string Authorization { get; } = "/api/v1/auth/authorize";
            public static string GetOutsideAdvertisements { get; } = "/api/v1/adverts/outsides";
            public static string GetUserAdvertisements { get; } = "/api/v1/adverts/my";
            public static string DeleteAdvertisement { get; } = "/api/v1/adverts/remove";
            public static string PublishAdvertisement { get; } = "/api/v1/adverts/add";
        }
    }
}
