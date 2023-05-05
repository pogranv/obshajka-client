using ObshajkaWebApi.Interfaces;

namespace ObshajkaWebApi.Models
{
    internal class Advertisement : IAdvertisement
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
    }
}
