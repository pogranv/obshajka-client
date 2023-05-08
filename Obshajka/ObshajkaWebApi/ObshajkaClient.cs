using System.Net.Http.Json;
using System.Net.Http.Headers;

using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Models;
using ObshajkaWebApi.Interfaces;
using ObshajkaWebApi.Utils;

namespace ObshajkaWebApi
{
    public class ObshajkaClient
    {
        private static HttpClient _httpClient;

        static ObshajkaClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new(ConnectionStrings.BaseAddress)
            };
            _httpClient.DefaultRequestHeaders.ConnectionClose = true;
        }

        /// <summary>
        /// По заданной почте и паролю возвращает идентификатор пользователя.
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        /// <exception cref="FailAuthorizeException"></exception>
        public async Task<long> AuthorizeUser(string email, string password)
        {
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

        /// <summary>
        /// По заданному имени, почте и паролю осуществляет отправку кода подтверждения на указанную почту.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="FailRegisterUserException"></exception>
        public async Task RegisterUser(string name, string email, string password)
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

        /// <summary>
        /// По заданной почте и коду подтверждения возвращает идентификатор пользователя в случае,
        /// если код совпал с тем, что был отправлен на указанную почту.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="FailConfirmVerificationCodeException"></exception>
        public async Task<long> ConfirmVerificationCode(string email, string code)
        {
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

        /// <summary>
        /// Осуществляет получение объявлений в рамках заданного общежития, владельцем которых
        /// не является заданный пользователь.
        /// </summary>
        /// <param name="dormitoryId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="FailGetAdvertisementsException"></exception>
        public async Task<IEnumerable<IAdvertisement>> GetAdvertisementsFromOtherUsers(long dormitoryId, long currentUserId)
        {
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

        /// <summary>
        /// Осуществляет получение объявлений владельцем которых
        /// является заданный пользователь.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="FailGetAdvertisementsException"></exception>
        public async Task<IEnumerable<IAdvertisement>> GetUserAdvertisements(long userId)
        {
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

        /// <summary>
        /// Осущеслвяет удаление объявления с сервера по его идентификатору.
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <returns></returns>
        /// <exception cref="FailRemoveAdvertisementException"></exception>
        public async Task RemoveAdvertisement(long advertisementId)
        {
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

        /// <summary>
        /// Осущесвляет добавление объявления на сервер.
        /// </summary>
        /// <param name="advertisement"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        /// <exception cref="FailPublishAdvertisementException"></exception>
        public async Task PubslishNewAdvertisement(IPublishingAdvertisement advertisement, string imagePath)
        {

            using var multipartFormContent = new MultipartFormDataContent();
            
            if (!string.IsNullOrEmpty(imagePath))
            {
                var fileStreamContent = new StreamContent(File.OpenRead(imagePath));
                multipartFormContent.Add(fileStreamContent, name: "Image", fileName: "image.jpg");
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            var description = advertisement.Description == null ? string.Empty : advertisement.Description;
            multipartFormContent.Add(new StringContent(advertisement.CreatorId.ToString()), name: "Details.CreatorId");
            multipartFormContent.Add(new StringContent(advertisement.Title), name: "Details.Title");
            multipartFormContent.Add(new StringContent(description), name: "Details.Description");
            multipartFormContent.Add(new StringContent(advertisement.DormitoryId.ToString()), name: "Details.DormitoryId");
            multipartFormContent.Add(new StringContent(advertisement.Price.ToString()), name: "Details.Price");

            CheckNetworkUtils.CheckNetworkAccess();

            using var response = await _httpClient.PostAsync(ConnectionStrings.PublishAdvertisement, multipartFormContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }
            throw new FailPublishAdvertisementException("Неизвестная ошибка, повторите попытку.");
        }
    }
}
