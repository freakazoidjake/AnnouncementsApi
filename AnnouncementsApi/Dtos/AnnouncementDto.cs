namespace AnnouncementsApi.Dtos
{
    public class AnnouncementDto
    {
        public int? Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Author { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }
    }
}