using System;
using AnnouncementsApi.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AnnouncementsApi.Services
{
	public class AnnouncementService: IAnnouncementService
	{
        private readonly AnnouncementDbContext _dbContext;

		public AnnouncementService(AnnouncementDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public EntityEntry<Announcement> Create(Announcement model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Announcement> GetAll(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Announcement? GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Save()
        {
            throw new NotImplementedException();
        }

        public Announcement? Update(Announcement model)
        {
            throw new NotImplementedException();
        }
    }
}

