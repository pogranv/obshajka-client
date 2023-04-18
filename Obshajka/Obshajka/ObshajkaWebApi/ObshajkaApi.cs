using System;
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
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Obshajka.ObshajkaWebApi
{
    public static class ObshajkaApi
    {
        // TODO: исправить типы экспешинов
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
                using var response = await httpClient.PostAsJsonAsync(/*"http://localhost:80/api/reg/verification"*/ConnectionSettings.SendVerificationCode, newUser);
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

        public static async void RemoveAdvertisement(long advertisementId)
        {
            if (UserSettings.UserSettings.UseMocks)
            {
                MocksClass.RemoveAdvert(advertisementId);
                return;
            }
            // MocksClass.RemoveAdvert(advertisementId);
            var connectionString = $"{ConnectionSettings.DeleteAdvertisement}/{advertisementId}";
            using var response = await httpClient.DeleteAsync(connectionString);
            // using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.SendVerificationCode, newUser);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return;
                default:
                    throw new Exception("Не удалось удалить объявление :(");
            }
        }

        public static Advertisement PublishAndGetNewAdvert(Advertisement advertisement)
        {
            return MocksClass.BuplishAndGetNewAdvertisement(advertisement);
        }

        //public static async void PubslishNewAdvert(AdvertisementWithImage advertisement, Stream imageStream)
        //{
        //    //var connectionString = ConnectionSettings.PublishAdvertisement;
        //    //using var response = await httpClient.PostAsJsonAsync(connectionString, advertisement);
        //    //// using var response = await httpClient.PostAsJsonAsync(ConnectionSettings.SendVerificationCode, newUser);
        //    //Console.WriteLine(response.RequestMessage);
        //    //switch (response.StatusCode)
        //    //{
        //    //    case System.Net.HttpStatusCode.OK:
        //    //        return;
        //    //    default:
        //    //        throw new Exception("Не удалось удалить объявление :(");
        //    //}

        //    await using var stream = System.IO.File.OpenRead(@"D:\kek.jpg");

        //    var Details = new
        //    {
        //        CreatorId = 1,
        //        Title = "titile",
        //        Description = "kek",
        //        DormitoryId = 1,
        //        Price = 100,
        //    };




        //    using var request = new HttpRequestMessage(HttpMethod.Post, "AdvertisementWithImage");
        //    using var content = new MultipartFormDataContent
        //    {
        //        // file
        //        {  new StreamContent(stream) , "FileToUpload1", "Test.jpg" },

        //        // payload
        //        { 
        //            new StringContent(
        //            JsonSerializer.Serialize(Details),
        //            Encoding.UTF8,
        //            "application/json"),
        //            "Data" 
        //        },
        //    };


        //    HttpClient client = new HttpClient
        //    {
        //        BaseAddress = new Uri(UserSettings.ConnectionSettings.PublishAdvertisement)
        //    };
        //    // client.BaseAddress = new Uri(UserSettings.ConnectionSettings.PublishAdvertisement);

        //    request.Content = content;
        //    var response = await client.SendAsync(request);
        //    Console.WriteLine(response);
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return;
        //    }
        //}

        public static async void PubslishNewAdvert(PublishingAdvertisement advertisement, string imagePath)
        {
            var client = new HttpClient
            {
                BaseAddress = new("http://localhost:80")
            };


            // await using Stream stream = System.IO.File.OpenRead(imagePath);

            using Stream stream = !string.IsNullOrEmpty(imagePath) ? System.IO.File.OpenRead(imagePath) : await FileSystem.Current.OpenAppPackageFileAsync("default_advert_image.png");

            // await using var stream = imageStream;

            var payload = advertisement;

            
             
            // TODO: baseaddres можно сделать только хостинг и потом просто дописывать сюда путь до api
            using var request = new HttpRequestMessage(HttpMethod.Post, "/api/adverts/add"); // "api/GetAdvertisements/MakeAdvertisement

            using var content = new MultipartFormDataContent
    {
                // file
               // { new StreamContent(), "File", "kek" },
         { new StreamContent(stream), "FileToUpload1", "Test.txt" },

        // payload
        { new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"),
            "Data" },
        };

            request.Content = content;

            var response = await client.SendAsync(request);
        }
    }
}
