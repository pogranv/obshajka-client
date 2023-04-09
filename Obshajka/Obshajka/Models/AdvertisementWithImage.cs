using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Obshajka.Models
{
    public class AdvertisementWithImage
    {
        public AdvertisementDetails Details { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class AdvertisementDetails
    {
        public long CreatorId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int DormitoryId { get; set; }

        public int? Price { get; set; }

        public string? Image { get; set; }
    }
}
