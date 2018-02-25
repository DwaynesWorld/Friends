using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Services
{
    public class LookupDataService : IFriendLookupDataService
    {
        private Func<FriendOrganizerDbContext> _conextCreator;

        public LookupDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _conextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = _conextCreator())
            {
                return await ctx.Friends.AsNoTracking().Select(f => new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.FirstName + " " + f.LastName
                }).ToListAsync();
            }
        }
    }
}
