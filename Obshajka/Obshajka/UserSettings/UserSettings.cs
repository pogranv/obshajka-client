using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.UserSettings
{
    // хранение кэширование фото по сети https://metanit.com/sharp/maui/4.9.php
    // чекнуть сколько номеров общаг и добавить в создание объявы
    // ограничение на воодимые данные при рестистрации и входе
    public static class UserSettings
    {
        public static long UserId { get; set; }
        public static long SelectedDormitoryIdFilter { get; set; } = 1;
        public static string UserName { get; set; }
    }
}
