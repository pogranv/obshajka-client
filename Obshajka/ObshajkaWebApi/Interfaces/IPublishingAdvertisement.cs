namespace ObshajkaWebApi.Interfaces
{
    public interface IPublishingAdvertisement
    {
        public long CreatorId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int DormitoryId { get; set; }

        public int? Price { get; set; }
    }
}
