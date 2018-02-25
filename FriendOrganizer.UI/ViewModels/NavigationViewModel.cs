using FriendOrganizer.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;
        private NavigationItemViewModel _selectedFriend;

        public NavigationItemViewModel SelectedFriend
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
        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public NavigationViewModel(
            IFriendLookupDataService friendLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(OnAfterFriendSaved);

            Friends = new ObservableCollection<NavigationItemViewModel>();
        }

        private void OnAfterFriendSaved(AfterFriendSaveEventArgs e)
        {
            var lookupItem = Friends.Single(f => f.Id == e.Id);
            lookupItem.DisplayMember = e.DisplayMember;
        }

        public async Task LoadAsync()
        {
            Friends.Clear();
            foreach (var item in await _friendLookupDataService.GetFriendLookupAsync())
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }
    }
}
