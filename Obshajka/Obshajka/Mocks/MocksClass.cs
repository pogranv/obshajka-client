using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.UserSettings;
using Obshajka.Models;

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
                    Image = "image1",
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
                    Image = "image2",
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
                    Title = "Title3",
                    DormitoryId = 1,
                    DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
                }
            };
        }

        public static List<Advertisement> GetAdvertisements_Mock()
        {
            return advertisements;
        }
    }
}
