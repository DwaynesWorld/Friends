using FriendOrganizer.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels
{
    class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendDataService _friendService;
        private IEventAggregator _eventAggregator;
        private Friend _friend;

        public ICommand SaveCommand { get; }
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

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
            await _friendService.SaveAsync(Friend);
            _eventAggregator
                .GetEvent<AfterFriendSavedEvent>()
                .Publish(
                    new AfterFriendSaveEventArgs {
                        Id = Friend.Id,
                        DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                    }
                );
        }

        private bool OnSaveCanExecute()
        {
            //TODO: Check if Friend is valid.
            return true;
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
