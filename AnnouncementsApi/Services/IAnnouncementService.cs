using System;
using AnnouncementsApi.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AnnouncementsApi.Services
{
	public interface IAnnouncementService
	{
        Announcement? GetSingle(int id);
        EntityEntry<Announcement> Create(Announcement model);
        bool Delete(int id);
        Announcement? Update(Announcement model);
        IQueryable<Announcement> GetAll(QueryParameters queryParameters);
        Task<int> Save();
    }
}

