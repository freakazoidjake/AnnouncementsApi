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
            return _dbContext.Announcements.Add(model);
        }

        public bool Delete(int id)
        {
            var model = _dbContext.Announcements.FirstOrDefault(x => x.Id == id);

            if(model == null)
            {
                return false;
            }

            _dbContext.Announcements.Remove(model);

            return true;
        }

        public IQueryable<Announcement> GetAll(QueryParameters queryParameters)
        {
            bool? orderByDesc = null;

            if(!string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                orderByDesc = queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            }

            IQueryable<Announcement> items = _dbContext.Announcements.Where(x => x.EndDate == null || x.EndDate > DateTime.UtcNow);

            if(orderByDesc == true)
            {
                items = items.OrderByDescending(x => x.StartDate);
            }

            if(!string.IsNullOrEmpty(queryParameters.Query))
            {
                items = items.Where(x => x.Author.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Body.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Subject.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return items.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public Announcement? GetSingle(int id)
        {
            return _dbContext.Announcements.FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public Announcement? Update(Announcement model)
        {
            var first = _dbContext.Announcements.FirstOrDefault(x => x.Id == model.Id);
            if(first == null)
            {
                return null;
            }

            // TODO: Find a better way to do this for all models/entities if doing more than just Announcements.

            first.Author = model.Author;
            first.Body = model.Body;
            first.EndDate = model.EndDate;
            first.StartDate = model.StartDate;
            first.Subject = model.Subject;

            _dbContext.Announcements.Update(first);

            return first;
        }
    }
}

