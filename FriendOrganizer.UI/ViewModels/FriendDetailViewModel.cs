using FriendOrganizer.Model;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Services;
using FriendOrganizer.UI.Wrapper;
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
        private FriendWrapper _friend;

        public ICommand SaveCommand { get; }
        public FriendWrapper Friend { get { return _friend; } private set { _friend = value; OnPropertyChanged(); } }

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

        public async Task LoadAsync(int id)
        {
            Friend = new FriendWrapper(await _friendService.GetByIdAsync(id));
            Friend.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private async void OnSaveExecute()
        {
            await _friendService.SaveAsync(Friend.Model);
            _eventAggregator
                .GetEvent<AfterFriendSavedEvent>()
                .Publish(new AfterFriendSaveEventArgs { Id = Friend.Id, DisplayMember = $"{Friend.FirstName} {Friend.LastName}" });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors;
        }

        private async void OnOpenFriendDetailView(int id) => await LoadAsync(id);
    }
}
