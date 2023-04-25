using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObshajkaWebApi.Interfaces;

namespace Obshajka.Models
{
    public class Advertisement : IAdvertisement
    {
        public long Id { get; set; }

        public long CreatorId { get; set; }

        public string CreatorName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int DormitoryId { get; set; }

        public int? Price { get; set; }

        public string? Image { get; set; }
        public string DateOfAddition { get; set; }

        public static Advertisement Build(IAdvertisement advert)
        {
            return new Advertisement
            {
                Id = advert.Id,
                CreatorId = advert.CreatorId,
                CreatorName = advert.CreatorName,
                Title = advert.Title,
                Description = advert.Description,
                DormitoryId = advert.DormitoryId,
                Price = advert.Price,
                Image = advert.Image,
                DateOfAddition = advert.DateOfAddition,
            };
        }
    }
}
