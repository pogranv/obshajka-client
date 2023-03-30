using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.UserSettings;
using Obshajka.Models;
using System.Linq;

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
                    Image = "dotnet_bot.png",
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
                    Image = "dotnet_bot.png",
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
                    DormitoryId = 1,
                    Price = 100,
                    Image = "dotnet_bot.png",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 6,
                    CreatorId = 6,
                    CreatorName = "Name6",
                    Title = "Title6",
                    Description = "Description6",
                    DormitoryId = 1,
                    Price = 100,
                    Image = "dotnet_bot.png",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 7,
                    CreatorId = 7,
                    CreatorName = "Name7",
                    Title = "Title7",
                    Description = "Description7",
                    DormitoryId = 1,
                    Price = 100,
                    Image = "dotnet_bot.png",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 8,
                    CreatorId = 8,
                    CreatorName = "Name8",
                    Title = "Title8",
                    Description = "Description8",
                    DormitoryId = 1,
                    Price = 100,
                    Image = "dotnet_bot.png",
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
                    Image = "dotnet_bot.png",
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                },
                new Advertisement
                {
                    Id = 10,
                    CreatorId = 10,
                    CreatorName = "Name10",
                    Title = "Title10",
                    Description = "Description10",
                    DormitoryId = 1,
                    Price = 100,
                    Image = "dotnet_bot.png",
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
    }
}
