using AnnouncementsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementsApi.Services
{
    public class AnnouncementDbContext : DbContext
    {
        public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
    }
}



