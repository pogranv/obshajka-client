using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Obshajka.UserSettings
{
    // хранение кэширование фото по сети https://metanit.com/sharp/maui/4.9.php
    // чекнуть сколько номеров общаг и добавить в создание объявы
    // ограничение на воодимые данные при рестистрации и входе
    // сделать запрос списка только фотку и описание и цену, а потом запрашивать еще и описание при открытии
    // TODO чекнуть срок действия ссылки на скачивание, а то чет была проблема, что ссылка недействительна
    public static class UserSettings
    {
        public static long UserId { get; set; }
        public static long SelectedDormitoryIdFilter { get; set; } = 1;
        public static string UserName { get; set; }
        public static bool UseMocks { get; set; } = false;
    }

    public static class ConnectionSettings
    {
        public static string SendVerificationCode { get; set; } = "https://localhost:7060/api/Registration/SendVerificationCode";
        public static string ConfirmVerificationCode { get; set; } = "https://localhost:7060/api/Registration/ConfirmVerificationCode";
        public static string Authorization { get; set; } = "https://localhost:7060/api/Authorization/Authorization";
        public static string GetOutsideAdvertisements { get; set; } = "https://localhost:7060/api/GetAdvertisements/GetOutsideAdvertisements";
        public static string GetUserAdvertisements { get; set; } = "https://localhost:7060/api/GetAdvertisements/GetUserAdvertisements";
        public static string DeleteAdvertisement { get;} = "https://localhost:7060/api/GetAdvertisements/DeleteAdvertisement";
        public static string PublishAdvertisement { get; } = "https://localhost:7060/api";
    }
}
