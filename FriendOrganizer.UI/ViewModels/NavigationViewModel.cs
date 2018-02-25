using FriendOrganizer.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;
        private LookupItem _selectedFriend;

        public LookupItem SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
                if (_selectedFriend != null)
                {
                    _eventAggregator
                        .GetEvent<OpenFriendDetailViewEvent>()
                        .Publish(_selectedFriend.Id);
                }
            }
        }
        public ObservableCollection<LookupItem> Friends { get; }

        public NavigationViewModel(
            IFriendLookupDataService friendLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;

            Friends = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            Friends.Clear();
            foreach (var item in await _friendLookupDataService.GetFriendLookupAsync())
            {
                Friends.Add(item);
            }
        }
    }
}
