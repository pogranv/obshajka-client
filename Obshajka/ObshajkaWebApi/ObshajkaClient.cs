using System.Net.Http.Json;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Models;
using ObshajkaWebApi.Mocks;
using ObshajkaWebApi.Interfaces;
using ObshajkaWebApi.Utils;
using System.Net;
using System.IO;
using System;
using System.Reflection.Metadata;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using ObshajkaWebApi.Utils;

namespace ObshajkaWebApi
{
    public class ObshajkaClient
    {
        // todo: удалить
        private bool useMocks = false;

        private static class ConnectionStrings
        {
            public static string BaseAddress { get; } = "http://84.252.138.220";//"https://localhost:7082"; //"http://localhost:80";// "https://localhost:7082";// "http://localhost:80";
            public static string SendVerificationCode { get; } = "/api/v1/reg/verification";
            public static string ConfirmVerificationCode { get; } = "/api/v1/reg/confirmation";
            public static string Authorization { get; } = "/api/v1/auth/authorize";
            public static string GetOutsideAdvertisements { get; } = "/api/v1/adverts/outsides";
            public static string GetUserAdvertisements { get; } = "/api/v1/adverts/my";
            public static string DeleteAdvertisement { get; } = "/api/v1/adverts/remove";
            public static string PublishAdvertisement { get; } = "/api/v1/adverts/add";
        }

        private static HttpClient _httpClient;
        static ObshajkaClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new(ConnectionStrings.BaseAddress)
            };
            _httpClient.DefaultRequestHeaders.ConnectionClose = true;
        }
        public async Task<long> AuthorizeUser(string email, string password)
        {
            if (useMocks)
            {
                return 1;
            }
            var hashedPassword = HashUtils.GetHashString(password);
            var emailWithPassword = new EmailWithPassword(email, hashedPassword);

            CheckNetworkUtils.CheckNetworkAccess();

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

                CheckNetworkUtils.CheckNetworkAccess();

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

            CheckNetworkUtils.CheckNetworkAccess();

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

            CheckNetworkUtils.CheckNetworkAccess();

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

            CheckNetworkUtils.CheckNetworkAccess();

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

            CheckNetworkUtils.CheckNetworkAccess();

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




        void CreateDto(HttpResponseMessage? resp)
        {
            var kek = 1;
        }

        public class AdvertDetails
        {
            public long CreatorId { get; set; }

            public string Title { get; set; } = null!;

            public string? Description { get; set; }

            public int DormitoryId { get; set; }

            public int? Price { get; set; }
        }

        public class AdvertisementFromFront
        {
            public IFormFile? Image { get; set; }

            //[ModelBinder(BinderType = typeof(FormDataJsonBinder))]
            public AdvertDetails Details { get; set; }
        }

        public async void PubslishNewAdvertisement(IPublishingAdvertisement advertisement, /*Stream imageStream,*/ string imagePath)
        {

            using var multipartFormContent = new MultipartFormDataContent();
            
            if (!string.IsNullOrEmpty(imagePath))
            {
                var fileStreamContent = new StreamContent(File.OpenRead(imagePath)); // File.OpenRead(imagePath)
                multipartFormContent.Add(fileStreamContent, name: "Image", fileName: "logo.jpg");
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            // var fileStreamContent = new StreamContent(imageStream); // File.OpenRead(imagePath)

            

            // добавляем обычные данные
            multipartFormContent.Add(new StringContent(advertisement.CreatorId.ToString()), name: "Details.CreatorId");
            multipartFormContent.Add(new StringContent(advertisement.Title), name: "Details.Title");
            multipartFormContent.Add(new StringContent(advertisement.Description), name: "Details.Description");
            multipartFormContent.Add(new StringContent(advertisement.DormitoryId.ToString()), name: "Details.DormitoryId");
            multipartFormContent.Add(new StringContent(advertisement.Price.ToString()), name: "Details.Price");

            // добавляем файл

            CheckNetworkUtils.CheckNetworkAccess();

            // Отправляем данные
            using var response = await _httpClient.PostAsync(ConnectionStrings.PublishAdvertisement, multipartFormContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }
            throw new FailPublishAdvertisementException("Неизвестная ошибка, повторите попытку.");
        }

        // рабочая

        //public async void PubslishNewAdvertisement(IPublishingAdvertisement advertisement, Stream imageStream, string imagePath)
        //{

        //    using var multipartFormContent = new MultipartFormDataContent();
        //    // var imagePath = await FileSystem.Current.OpenAppPackageFileAsync("default_advert_image.png");
        //    var fileStreamContent = new StreamContent(File.OpenRead(imagePath)); // попробовать через путь

        //    multipartFormContent.Add(fileStreamContent, name: "Image", fileName: "logo.jpg");

        //    // добавляем обычные данные
        //    multipartFormContent.Add(new StringContent(advertisement.CreatorId.ToString()), name: "Details.CreatorId");
        //    multipartFormContent.Add(new StringContent(advertisement.Title), name: "Details.Title");
        //    multipartFormContent.Add(new StringContent(advertisement.Description), name: "Details.Description");
        //    multipartFormContent.Add(new StringContent(advertisement.DormitoryId.ToString()), name: "Details.DormitoryId");
        //    multipartFormContent.Add(new StringContent(advertisement.Price.ToString()), name: "Details.Price");

        //    // добавляем файл
        //    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");


        //    // Отправляем данные
        //    using var response = await _httpClient.PostAsync(ConnectionStrings.PublishAdvertisement, multipartFormContent);
        //    var lolkek = 1;
        //}


















        //public async void PubslishNewAdvertisement(IPublishingAdvertisement advertisement, Stream imageStream/*string imagePath, Stream mystream*/)
        //{
        //    //using var request = new HttpRequestMessage(HttpMethod.Post, ConnectionStrings.PublishAdvertisement)
        //    //{
        //    //    Version = HttpVersion.Version10,
        //    //};

        //    using var content = new MultipartFormDataContent
        //    {
        //        // image
        //        //{ new StreamContent(imageStream), "Image", "image.jpg" },

        //        // details
        //        { new StringContent(advertisement.CreatorId.ToString()), "Details.CreatorId" },
        //        { new StringContent(advertisement.Title), "Details.Title" },
        //        { new StringContent(advertisement.Description), "Details.Description" },
        //        { new StringContent(advertisement.DormitoryId.ToString()), "Details.DormitoryId" },
        //        { new StringContent(advertisement.Price.ToString()), "Details.Price" },
        //    };

        //    //request.Content = content;

        //    using var request = new HttpRequestMessage(HttpMethod.Post, ConnectionStrings.PublishAdvertisement)
        //    {
        //        Version = HttpVersion.Version10,
        //        Content = content,
        //    };

        //    try 
        //    {
        //        using var response = await _httpClient.SendAsync(request);
        //    } catch (Exception e)
        //    {
        //        using var response = await _httpClient.SendAsync(request);
        //    }


        //    //if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    //{
        //    //    return;
        //    //}
        //    //throw new FailPublishAdvertisementException("Неизвестная ошибка, повторите попытку.");
        //}


    }
}
