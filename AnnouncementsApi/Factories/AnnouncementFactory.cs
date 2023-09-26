using AnnouncementsApi.Dtos;
using AnnouncementsApi.Models;

namespace AnnouncementsApi.Factories
{
    public static class AnnouncementFactory
    {
        public static AnnouncementDto ToDto(Announcement model)
        {
            return new AnnouncementDto
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Author = model.Author,
                Subject = model.Subject,
                Body = model.Body
            };
        }

        public static Announcement? FromDto(AnnouncementDto dto)
        {
            var startDate = dto.StartDate;
            var author = dto.Author;
            var subject = dto.Subject;
            var body = dto.Body;

            if (startDate == null || author == null || subject == null || body == null)
            {
                return null;
            }

            return new Announcement
            {
                Author = author,
                Subject = subject,
                Body = body,
                StartDate = (DateTime)startDate,
                EndDate = dto.EndDate
            };
        }
    }
}

