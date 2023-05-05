using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObshajkaWebApi.Models;

namespace ObshajkaWebApi.Mocks
{
    internal static class MocksClass
    {
        static List<Advertisement> advertisements;
        static string defaultImage = "https://downloader.disk.yandex.ru/disk/8f0eb2f44b0d3d19177854d69c3f02d707c569bc926731ed038685546491bd5f/64503c58/fKqInKw3d7bLFOeFnMGnhMAEDDC3m8pKIC-PkDc8GWJJ9TsYpDzFNFbjqhrv2yFDm4Zd2ToZqtMsw1f__ZzOTi2gTlgtf5sNtH272ZsPXCar8npumZHI4midPdWhecNq?uid=1130000059567809&filename=776b1677-cacc-49a2-b07d-df65df5fe69a..jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=186836&hid=29aea2491c48f161b2b2ca263ccf30a1&media_type=image&tknv=v2&etag=7796abcd95a83c24b311c91ce72814b7";
        static string defaultImage2 = "https://avatars.mds.yandex.net/get-eda/3439028/79b8aa7956544dc76792670eb9d1b88a/400x400";

        static MocksClass()
        {
            advertisements = new List<Advertisement>
            {
                new Advertisement
                {
                    Id = 1,
                    CreatorId = 1,
                    CreatorName = "Name1",
                    Title = "Title1adfjsdafjsjdfjdsfffffffffffffffffffffffffffffffffffffffffff",
                    Description = "Description1",
                    DormitoryId = 1,
                    Price = 1000,
                    Image = defaultImage,
                },
                new Advertisement
                {
                    Id = 2,
                    CreatorId = 2,
                    CreatorName = "Хочу продать пирожки",
                    Title = "Хочу продать пирожки",
                    Description = "Продаю в подержанном состоянии",
                    DormitoryId = 1,
                    Price = 100,
                    Image = defaultImage2,
                },
                new Advertisement
                {
                    Id = 3,
                    CreatorId = 3,
                    CreatorName = "Name3",
                    Title = "Title3",
                    Description = "",
                    DormitoryId = 1,
                    Price = 10,
                },
                new Advertisement
                {
                    Id = 4,
                    CreatorId = 4,
                    CreatorName = "Name4",
                    Title = "Title4",
                    DormitoryId = 1,
                },
                new Advertisement
                {
                    Id = 5,
                    CreatorId = 5,
                    CreatorName = "Name5",
                    Title = "Title5",
                    Description = "Description5",
                    DormitoryId = 2,
                    Price = 100,
                    Image = defaultImage2,
                },
                new Advertisement
                {
                    Id = 6,
                    CreatorId = 6,
                    CreatorName = "Name6",
                    Title = "Title6",
                    Description = "Description6",
                    DormitoryId = 2,
                    Price = 100,
                    Image = defaultImage,
                },
                new Advertisement
                {
                    Id = 7,
                    CreatorId = 7,
                    CreatorName = "Name7",
                    Title = "Title7",
                    Description = "Description7",
                    DormitoryId = 2,
                    Price = 100,
                    Image = defaultImage2,
                },
                new Advertisement
                {
                    Id = 8,
                    CreatorId = 8,
                    CreatorName = "Name8",
                    Title = "Title8",
                    Description = "Description8",
                    DormitoryId = 2,
                    Price = 100,
                    Image = defaultImage2,
                },
                new Advertisement
                {
                    Id = 9,
                    CreatorId = 9,
                    CreatorName = "Name9",
                    Title = "Title9",
                    Description = "Description9",
                    DormitoryId = 1,
                    Price = 100,
                    Image = defaultImage,
                },
                new Advertisement
                {
                    Id = 10,
                    CreatorId = 10,
                    CreatorName = "Name10",
                    Title = "Title10",
                    Description = "Description10",
                    DormitoryId = 2,
                    Price = 100,
                    Image = defaultImage2,
                }
            };
            for (int i = 0; i < advertisements.Count; ++i)
            {
                advertisements[i].DateOfAddition = DateOnly.FromDateTime(DateTime.Now).ToString();
            }
        }
        public static List<Advertisement> GetAdvertisementsFromOtherUsers_Mock(long dormitoryId, long currentUserId)
        {
            return advertisements.Where(i => i.DormitoryId == dormitoryId && i.CreatorId != currentUserId).ToList();
        }

        public static List<Advertisement> GetAdvertisementsFromCurrentUser_Mock(long currentUserId)
        {
            return advertisements.Where(i => i.CreatorId == currentUserId).ToList();
        }

        public static void RemoveAdvert(long advertId)
        {
            advertisements.RemoveAll(i => i.Id == advertId);
        }

        public static Advertisement BuplishAndGetNewAdvertisement(Advertisement advert)
        {
            advert.Image = defaultImage;
            advert.Id = advertisements.Count;
            advertisements.Add(advert);
            return advert;
        }
    }
}
