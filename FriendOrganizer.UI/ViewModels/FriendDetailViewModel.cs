using FriendOrganizer.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendDataService _friendService;
        private IEventAggregator _eventAggregator;
        private Friend _friend;

        public Friend Friend { get { return _friend; } private set { _friend = value; OnPropertyChanged(); } }

        public FriendDetailViewModel(
            IFriendDataService friendService,
            IEventAggregator eventAggregator)
        {
            _friendService = friendService;
            _eventAggregator = eventAggregator;
            _eventAggregator
                .GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);
        }

        private async void OnOpenFriendDetailView(int id)
        {
            await LoadAsync(id);
        }

        public async Task LoadAsync(int id)
        {
            Friend = await _friendService.GetByIdAsync(id);
        }
    }
}
