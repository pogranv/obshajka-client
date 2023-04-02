using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.UserSettings;
using Obshajka.Models;
using System.Linq;
using Microsoft.UI.Composition.Interactions;

namespace Obshajka.Mocks
{
    public static class MocksClass
    {
        static List<Advertisement> advertisements;

        static MocksClass()
        {
            advertisements = new List<Advertisement>
            {
                new Advertisement
                {
                    Id = 1,
                    CreatorId = 1,
                    CreatorName = "Name1",
                    Title = "Title1",
                    Description = "Description1",
                    DormitoryId = 1,
                    Price = 1000,
                    Image = "https://downloader.disk.yandex.ru/disk/f5c99cf86cf6edcb2cc459c92c0bc46bc367184aa67b02957b9e3fa09c9e0289/6429a456/fKqInKw3d7bLFOeFnMGnhBgW7RfbIJ_xhaDmR7XLEzmZ_jBuAuTFn1LEDavyEavi3mRzQSqbnf_HLfnAvkSQjJfvLZPdJyz4_HyRUQc0y1qr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=d9703f1c-d95f-445d-ac65-7ef70713aa3f.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=99226&hid=96418ab85f0075ba036106ca400f9a29&media_type=image&tknv=v2&etag=dc39a462f7b80a23dd47e9f84bb7fe40",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 2,
                    CreatorId = 2,
                    CreatorName = "Name2",
                    Title = "Title2",
                    Description = "Description2",
                    DormitoryId = 1,
                    Price = 100,
                    Image = "https://downloader.disk.yandex.ru/disk/f5c99cf86cf6edcb2cc459c92c0bc46bc367184aa67b02957b9e3fa09c9e0289/6429a456/fKqInKw3d7bLFOeFnMGnhBgW7RfbIJ_xhaDmR7XLEzmZ_jBuAuTFn1LEDavyEavi3mRzQSqbnf_HLfnAvkSQjJfvLZPdJyz4_HyRUQc0y1qr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=d9703f1c-d95f-445d-ac65-7ef70713aa3f.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=99226&hid=96418ab85f0075ba036106ca400f9a29&media_type=image&tknv=v2&etag=dc39a462f7b80a23dd47e9f84bb7fe40",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 3,
                    CreatorId = 3,
                    CreatorName = "Name3",
                    Title = "Title3",
                    Description = "Description3",
                    DormitoryId = 1,
                    Price = 10,
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 4,
                    CreatorId = 4,
                    CreatorName = "Name4",
                    Title = "Title4",
                    DormitoryId = 1,
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
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
                    Image = "https://downloader.disk.yandex.ru/disk/59753dbbfef3ff04fa54521c8916545fe954f25a9f3d40f247c33aa75901026e/6429b14b/fKqInKw3d7bLFOeFnMGnhKhVC793ABLwApza1RP2nNULVpaWHfhW1-U545AcNhJ6eNAt55vquPhFMWfLxz2k0m-ffARy2-i_lgfiktif1kWr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=aa6da913-dbd5-4394-9d05-3ed775699af7.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=265022&hid=a478cabaaa67459986fb15b4e43a887a&media_type=image&tknv=v2&etag=dee86f7f28865f8f9cf636812f3d0a49",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                }
            };
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

        public static void PublishAdvert(Advertisement advertisement)
        {
            advertisements.Add(advertisement);
        }

        public static Advertisement BuplishAndGetNewAdvertisement(Advertisement advert)
        {
            advert.Image = "https://downloader.disk.yandex.ru/disk/f5c99cf86cf6edcb2cc459c92c0bc46bc367184aa67b02957b9e3fa09c9e0289/6429a456/fKqInKw3d7bLFOeFnMGnhBgW7RfbIJ_xhaDmR7XLEzmZ_jBuAuTFn1LEDavyEavi3mRzQSqbnf_HLfnAvkSQjJfvLZPdJyz4_HyRUQc0y1qr8npumZHI4midPdWhecNq?uid=1130000059567809&filename=d9703f1c-d95f-445d-ac65-7ef70713aa3f.jpg&disposition=attachment&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1130000059567809&fsize=99226&hid=96418ab85f0075ba036106ca400f9a29&media_type=image&tknv=v2&etag=dc39a462f7b80a23dd47e9f84bb7fe40";
            advertisements.Add(advert);
            return advert;
        }
    }
}
