using System;
namespace AnnouncementsApi.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public required string Author { get; set; }

        public required string Subject { get; set; }

        public required string Body { get; set; }
    }
}
