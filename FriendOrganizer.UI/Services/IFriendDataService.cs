using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Services
{
    public interface IFriendDataService
    {
        Task<Friend> GetByIdAsync(int id);
    }
}