using ObshajkaWebApi.Interfaces;

namespace ObshajkaWebApi.Models
{
    internal sealed class PublishingAdvertisement : IPublishingAdvertisement
    {
        public long CreatorId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int DormitoryId { get; set; }

        public int? Price { get; set; }
    }
}
