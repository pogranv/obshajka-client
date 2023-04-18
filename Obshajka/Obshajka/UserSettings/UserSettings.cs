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
        public static string SendVerificationCode { get; set; } = "http://localhost:80/api/reg/verification";
        public static string ConfirmVerificationCode { get; set; } = "http://localhost:80/api/reg/confirmation";
        public static string Authorization { get; set; } = "http://localhost:80/api/auth/authorize";
        public static string GetOutsideAdvertisements { get; set; } = "http://localhost:80/api/adverts/outsides";
        public static string GetUserAdvertisements { get; set; } = "http://localhost:80/api/adverts/my";
        public static string DeleteAdvertisement { get;} = "http://localhost:80/api/adverts/remove";
        public static string PublishAdvertisement { get; } = "http://localhost:80/api/adverts/add";
    }
}
